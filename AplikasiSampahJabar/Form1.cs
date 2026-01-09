using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using MongoDB.Bson;
using MongoDB.Driver;

namespace AplikasiSampahJabar
{
    public partial class Form1 : Form
    {
        private MongoClient client;
        private IMongoDatabase database;
        private IMongoCollection<BsonDocument> collection;
        private PaginationInfo paginationInfo = new PaginationInfo();
        private DataTable fullDataSet;

        public Form1()
        {
            InitializeComponent();
            InitializeMongoDB();
            TampilDataKeGrid();
            ShowWelcomeMessage();
        }
        private void InitializeMongoDB()
        {
            try
            {
                string connectionString = GetConnectionString();
                
                client = new MongoClient(connectionString);
                database = client.GetDatabase(Constants.DATABASE_NAME);
                collection = database.GetCollection<BsonDocument>(Constants.COLLECTION_NAME);
                
                database.RunCommand<BsonDocument>(new BsonDocument("ping", 1));
                ErrorHandler.ShowInfo(Constants.MSG_CONNECTION_SUCCESS, Constants.TITLE_SUCCESS);
            }
            catch (Exception ex)
            {
                ErrorHandler.LogError(ex, "InitializeMongoDB");
                ErrorHandler.ShowWarning(string.Format(Constants.MSG_CONNECTION_FAILED, ex.Message), Constants.TITLE_WARNING);
                collection = null;
            }
        }

        private string GetConnectionString()
        {
            try
            {
                if (System.IO.File.Exists(Constants.CONFIG_FILE))
                {
                    return System.IO.File.ReadAllText(Constants.CONFIG_FILE).Trim();
                }
                return Constants.DEFAULT_CONNECTION_STRING;
            }
            catch (Exception ex)
            {
                ErrorHandler.LogError(ex, "GetConnectionString");
                return Constants.DEFAULT_CONNECTION_STRING;
            }
        }

        private void TampilDataKeGrid()
        {
            if (collection == null)
            {
                LoadDummyData();
                return;
            }

            // Cek cache terlebih dahulu
            if (PerformanceManager.IsCacheValid())
            {
                LoadDataFromCache();
            }
            else
            {
                LoadDataFromDatabase();
            }
        }

        private void LoadDataFromCache()
        {
            try
            {
                fullDataSet = PerformanceManager.GetCachedData();
                if (fullDataSet != null)
                {
                    paginationInfo.TotalRecords = fullDataSet.Rows.Count;
                    DisplayPagedData();
                }
                else
                {
                    LoadDataFromDatabase();
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.LogError(ex, "LoadDataFromCache");
                LoadDataFromDatabase();
            }
        }

        private void LoadDummyData()
        {
            fullDataSet = new DataTable();
            fullDataSet.Columns.Add(Constants.FIELD_KABUPATEN);
            fullDataSet.Columns.Add(Constants.FIELD_JENIS_SAMPAH);
            fullDataSet.Columns.Add(Constants.FIELD_VOLUME);
            fullDataSet.Columns.Add(Constants.FIELD_WAKTU_INPUT);
            
            DataRow row = fullDataSet.NewRow();
            row[Constants.FIELD_KABUPATEN] = Constants.DUMMY_KABUPATEN;
            row[Constants.FIELD_JENIS_SAMPAH] = Constants.DUMMY_JENIS;
            row[Constants.FIELD_VOLUME] = Constants.DUMMY_VOLUME;
            row[Constants.FIELD_WAKTU_INPUT] = DateTime.Now.ToString(Constants.DATE_FORMAT);
            fullDataSet.Rows.Add(row);
            
            paginationInfo.TotalRecords = fullDataSet.Rows.Count;
            paginationInfo.CurrentPage = 1;
            DisplayPagedData();
        }

        private void DisplayPagedData()
        {
            try
            {
                if (fullDataSet == null || fullDataSet.Rows.Count == 0)
                {
                    dataGridView1.DataSource = null;
                    UpdatePaginationUI();
                    return;
                }

                var pagedData = PerformanceManager.GetPagedData(
                    fullDataSet, 
                    paginationInfo.CurrentPage, 
                    paginationInfo.PageSize);
                
                dataGridView1.DataSource = pagedData;
                UpdatePaginationUI();
            }
            catch (Exception ex)
            {
                ErrorHandler.LogError(ex, "DisplayPagedData");
                ErrorHandler.ShowError("Terjadi kesalahan saat menampilkan data.");
            }
        }

        private void UpdatePaginationUI()
        {
            lblPageInfo.Text = $"Page {paginationInfo.CurrentPage} of {paginationInfo.TotalPages} ({paginationInfo.TotalRecords} records)";
            btnPrevious.Enabled = paginationInfo.HasPreviousPage;
            btnNext.Enabled = paginationInfo.HasNextPage;
        }

        private void LoadDataFromDatabase()
        {
            try
            {
                // Gunakan projection untuk mengurangi data transfer
                var projection = Builders<BsonDocument>.Projection
                    .Include(Constants.FIELD_ID)
                    .Include(Constants.FIELD_KABUPATEN)
                    .Include(Constants.FIELD_JENIS_SAMPAH)
                    .Include(Constants.FIELD_VOLUME)
                    .Include(Constants.FIELD_WAKTU_INPUT);

                var documents = collection.Find(new BsonDocument())
                    .Project(projection)
                    .ToList();

                fullDataSet = new DataTable();

                if (documents.Count > 0)
                {
                    foreach (var element in documents[0].Elements)
                    {
                        fullDataSet.Columns.Add(element.Name);
                    }

                    foreach (var doc in documents)
                    {
                        DataRow row = fullDataSet.NewRow();
                        foreach (var element in doc.Elements)
                        {
                            row[element.Name] = element.Value.ToString();
                        }
                        fullDataSet.Rows.Add(row);
                    }
                }

                // Update cache
                PerformanceManager.UpdateCache(fullDataSet);
                
                paginationInfo.TotalRecords = fullDataSet.Rows.Count;
                paginationInfo.CurrentPage = 1; // Reset ke halaman pertama
                DisplayPagedData();
            }
            catch (Exception ex)
            {
                ErrorHandler.LogError(ex, "LoadDataFromDatabase");
                ErrorHandler.ShowError(string.Format(Constants.MSG_DATABASE_ERROR, ex.Message));
                LoadDummyData();
            }
        }

        private void btnSimpan_Click(object sender, EventArgs e)
        {
            try
            {
                if (collection == null)
                {
                    ErrorHandler.ShowWarning(Constants.MSG_NO_CONNECTION, Constants.TITLE_WARNING);
                    return;
                }

                // Validasi input
                var validation = InputValidator.ValidateAllInputs(txtKabupaten.Text, txtJenis.Text, txtVolume.Text);
                if (!validation.IsValid)
                {
                    ErrorHandler.ShowWarning(validation.ErrorMessage, Constants.TITLE_WARNING);
                    return;
                }

                var dokumenBaru = new BsonDocument
                {
                    { Constants.FIELD_KABUPATEN, txtKabupaten.Text.Trim() },
                    { Constants.FIELD_JENIS_SAMPAH, txtJenis.Text.Trim() },
                    { Constants.FIELD_VOLUME, txtVolume.Text.Trim() },
                    { Constants.FIELD_WAKTU_INPUT, DateTime.Now.ToString(Constants.DATE_FORMAT) }
                };

                collection.InsertOne(dokumenBaru);
                ErrorHandler.ShowInfo(Constants.MSG_SAVE_SUCCESS, Constants.TITLE_SUCCESS);
                
                // Clear cache untuk memaksa refresh data
                PerformanceManager.ClearCache();
                TampilDataKeGrid();
                ClearInputFields();
            }
            catch (Exception ex)
            {
                ErrorHandler.LogError(ex, "btnSimpan_Click");
                ErrorHandler.ShowError(string.Format(Constants.MSG_DATABASE_ERROR, ex.Message));
            }
        }

        private void ClearInputFields()
        {
            txtKabupaten.Clear();
            txtJenis.Clear();
            txtVolume.Clear();
        }
        private void btnHapus_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count == 0)
                {
                    ErrorHandler.ShowWarning(Constants.MSG_SELECT_ROW, Constants.TITLE_WARNING);
                    return;
                }

                if (collection == null)
                {
                    ErrorHandler.ShowWarning(Constants.MSG_NO_CONNECTION, Constants.TITLE_WARNING);
                    return;
                }

                // Konfirmasi penghapusan
                if (!ErrorHandler.ShowConfirmation(Constants.MSG_DELETE_CONFIRM, Constants.TITLE_CONFIRMATION))
                {
                    return;
                }

                var cellValue = dataGridView1.SelectedRows[0].Cells[0].Value;
                if (cellValue == null || string.IsNullOrEmpty(cellValue.ToString()))
                {
                    ErrorHandler.ShowWarning(Constants.MSG_INVALID_DATA_FORMAT, Constants.TITLE_WARNING);
                    return;
                }

                if (!ObjectId.TryParse(cellValue.ToString(), out ObjectId objectId))
                {
                    ErrorHandler.ShowWarning(Constants.MSG_INVALID_DATA_FORMAT, Constants.TITLE_WARNING);
                    return;
                }

                var filter = Builders<BsonDocument>.Filter.Eq(Constants.FIELD_ID, objectId);
                var result = collection.DeleteOne(filter);

                if (result.DeletedCount > 0)
                {
                    ErrorHandler.ShowInfo(Constants.MSG_DELETE_SUCCESS, Constants.TITLE_SUCCESS);
                    
                    // Clear cache untuk memaksa refresh data
                    PerformanceManager.ClearCache();
                    TampilDataKeGrid();
                }
                else
                {
                    ErrorHandler.ShowWarning("Data tidak ditemukan atau sudah dihapus.", Constants.TITLE_WARNING);
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.LogError(ex, "btnHapus_Click");
                ErrorHandler.ShowError(string.Format(Constants.MSG_DATABASE_ERROR, ex.Message));
            }
        }

        private void btnExportPDF_Click(object sender, EventArgs e)
        {
            try
            {
                if (collection == null)
                {
                    ErrorHandler.ShowWarning(Constants.MSG_NO_CONNECTION, Constants.TITLE_WARNING);
                    return;
                }

                SaveFileDialog sfd = new SaveFileDialog
                {
                    Filter = Constants.PDF_FILTER,
                    Title = Constants.PDF_TITLE
                };

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    GeneratePDFReport(sfd.FileName);
                    ErrorHandler.ShowInfo(Constants.MSG_PDF_SUCCESS, Constants.TITLE_SUCCESS);
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.LogError(ex, "btnExportPDF_Click");
                ErrorHandler.ShowError(string.Format(Constants.MSG_PDF_ERROR, ex.Message));
            }
        }

        private void GeneratePDFReport(string fileName)
        {
            try
            {
                // Gunakan data dari cache jika tersedia untuk performa lebih baik
                DataTable dataForPDF = PerformanceManager.IsCacheValid() ? 
                    PerformanceManager.GetCachedData() : fullDataSet;
                
                if (dataForPDF == null)
                {
                    // Jika tidak ada data cached, ambil langsung dari database
                    var documents = collection.Find(new BsonDocument()).ToList();
                    
                    using (PdfWriter writer = new PdfWriter(fileName))
                    {
                        using (PdfDocument pdf = new PdfDocument(writer))
                        {
                            Document doc = new Document(pdf);
                            doc.Add(new Paragraph(Constants.PDF_REPORT_TITLE).SetFontSize(18));

                            if (documents.Count == 0)
                            {
                                doc.Add(new Paragraph("Tidak ada data untuk ditampilkan."));
                            }
                            else
                            {
                                foreach (var d in documents)
                                {
                                    string barisData = $"Wilayah: {d[Constants.FIELD_KABUPATEN]}, Jenis: {d[Constants.FIELD_JENIS_SAMPAH]}, Volume: {d[Constants.FIELD_VOLUME]} Ton";
                                    doc.Add(new Paragraph(barisData));
                                }
                            }
                        }
                    }
                }
                else
                {
                    // Gunakan data dari cache/memory
                    using (PdfWriter writer = new PdfWriter(fileName))
                    {
                        using (PdfDocument pdf = new PdfDocument(writer))
                        {
                            Document doc = new Document(pdf);
                            doc.Add(new Paragraph(Constants.PDF_REPORT_TITLE).SetFontSize(18));

                            if (dataForPDF.Rows.Count == 0)
                            {
                                doc.Add(new Paragraph("Tidak ada data untuk ditampilkan."));
                            }
                            else
                            {
                                foreach (DataRow row in dataForPDF.Rows)
                                {
                                    string barisData = $"Wilayah: {row[Constants.FIELD_KABUPATEN]}, Jenis: {row[Constants.FIELD_JENIS_SAMPAH]}, Volume: {row[Constants.FIELD_VOLUME]} Ton";
                                    doc.Add(new Paragraph(barisData));
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.LogError(ex, "GeneratePDFReport");
                throw;
            }
        }


        private void btnKirimChat_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtChatInput.Text))
                {
                    ErrorHandler.ShowWarning("Silakan masukkan pertanyaan terlebih dahulu.", Constants.TITLE_WARNING);
                    return;
                }

                string pertanyaan = txtChatInput.Text.ToLower();
                string jawaban = GetChatResponse(pertanyaan);

                rtbChatArea.AppendText($"User: {txtChatInput.Text}\n");
                rtbChatArea.AppendText($"Bot: {jawaban}\n\n");
                txtChatInput.Clear();
            }
            catch (Exception ex)
            {
                ErrorHandler.LogError(ex, "btnKirimChat_Click");
                ErrorHandler.ShowError(string.Format(Constants.MSG_CHAT_ERROR, ex.Message));
            }
        }

        private void ShowWelcomeMessage()
        {
            rtbChatArea.AppendText("=== SELAMAT DATANG DI APLIKASI SAMPAH JABAR ===\n\n");
            rtbChatArea.AppendText("Bot: Halo! Saya asisten data sampah Jabar. Berikut perintah yang bisa saya bantu:\n\n");
            rtbChatArea.AppendText("📊 Ketik 'jumlah data' atau 'total' - untuk melihat total data sampah\n");
            rtbChatArea.AppendText("👋 Ketik 'halo' atau 'selamat' - untuk menyapa\n");
            rtbChatArea.AppendText("👨‍💻 Ketik 'siapa yang buat' - untuk info pembuat aplikasi\n");
            rtbChatArea.AppendText("❓ Ketik 'help' atau 'bantuan' - untuk melihat perintah ini lagi\n\n");
            rtbChatArea.AppendText("Silakan ketik pertanyaan Anda di bawah!\n\n");
        }

        private string GetChatResponse(string pertanyaan)
        {
            try
            {
                if (pertanyaan.Contains("halo") || pertanyaan.Contains("selamat"))
                {
                    return Constants.CHAT_GREETING;
                }
                else if (pertanyaan.Contains("total") || pertanyaan.Contains("jumlah"))
                {
                    if (collection != null)
                    {
                        // Gunakan cached data jika tersedia untuk performa lebih baik
                        long jumlahData;
                        if (PerformanceManager.IsCacheValid())
                        {
                            var cachedData = PerformanceManager.GetCachedData();
                            jumlahData = cachedData?.Rows.Count ?? 0;
                        }
                        else
                        {
                            jumlahData = collection.CountDocuments(new BsonDocument());
                        }
                        return string.Format(Constants.CHAT_DATA_COUNT, jumlahData);
                    }
                    return Constants.MSG_NO_CONNECTION;
                }
                else if (pertanyaan.Contains("siapa yang buat"))
                {
                    return Constants.CHAT_CREATOR;
                }
                else if (pertanyaan.Contains("help") || pertanyaan.Contains("bantuan"))
                {
                    return "Perintah yang tersedia:\n" +
                           "• 'jumlah data' - lihat total data\n" +
                           "• 'halo' - menyapa bot\n" +
                           "• 'siapa yang buat' - info pembuat\n" +
                           "• 'help' - tampilkan perintah ini";
                }
                
                return Constants.CHAT_DEFAULT;
            }
            catch (Exception ex)
            {
                ErrorHandler.LogError(ex, "GetChatResponse");
                return "Maaf, terjadi kesalahan saat memproses pertanyaan Anda.";
            }
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            if (paginationInfo.HasPreviousPage)
            {
                paginationInfo.CurrentPage--;
                DisplayPagedData();
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (paginationInfo.HasNextPage)
            {
                paginationInfo.CurrentPage++;
                DisplayPagedData();
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                PerformanceManager.ClearCache();
                paginationInfo.CurrentPage = 1;
                TampilDataKeGrid();
                ErrorHandler.ShowInfo("Data berhasil di-refresh!", Constants.TITLE_SUCCESS);
            }
            catch (Exception ex)
            {
                ErrorHandler.LogError(ex, "btnRefresh_Click");
                ErrorHandler.ShowError("Gagal me-refresh data: " + ex.Message);
            }
        }
    }
}