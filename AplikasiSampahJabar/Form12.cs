using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Kernel.Colors;
using iText.Kernel.Geom;

namespace AplikasiSampahJabar
{
    public partial class Form12 : Form
    {
        private SampahHelper sampahHelper;
        private MongoClient client;
        private IMongoDatabase database;
        private DataTable fullDataSet;

        public Form12()
        {
            InitializeComponent();

            this.StartPosition = FormStartPosition.CenterScreen;
            this.AutoScroll = true;
            this.WindowState = FormWindowState.Normal;
            this.Text = "Detail Sampah"; 

            InitializeDatabase();
            InitializeUI();
        }

        private void InitializeDatabase()
        {
            try
            {
                string connectionString = System.IO.File.Exists(Constants.CONFIG_FILE)
                    ? System.IO.File.ReadAllText(Constants.CONFIG_FILE).Trim()
                    : Constants.DEFAULT_CONNECTION_STRING;

                client = new MongoClient(connectionString);
                database = client.GetDatabase(Constants.DATABASE_NAME);
                sampahHelper = new SampahHelper(database);
            }
            catch (Exception ex)
            {
                ErrorHandler.LogError(ex, "InitializeDatabase");
                ErrorHandler.ShowError("Gagal terhubung ke database.");
            }
        }

        private void InitializeUI()
        {
            LoadAllSampah();
            
            // Wire up events for specific buttons
            if (btnRefresh != null) btnRefresh.Click += (s, e) => LoadAllSampah();
            if (btnHapus != null) btnHapus.Click += BtnHapus_Click;
            if (btnExportPDF != null) btnExportPDF.Click += BtnExportPDF_Click;
            if (btnSimpan != null) btnSimpan.Click += BtnSimpan_Click;
            
            // Keluar button (button4 in designer)
            if (button4 != null) button4.Click += (s, e) => this.Close();
        }

        private void LoadAllSampah()
        {
            try
            {
                var sampahList = sampahHelper.GetAllSampah();
                
                fullDataSet = new DataTable();
                fullDataSet.Columns.Add("Id");
                fullDataSet.Columns.Add("Nama Sampah");
                fullDataSet.Columns.Add("Jenis");
                fullDataSet.Columns.Add("Lokasi TPS");
                fullDataSet.Columns.Add("Waktu Input");
                fullDataSet.Columns.Add("Status");
                fullDataSet.Columns.Add("Catatan");
                fullDataSet.Columns.Add("Petugas Input");
                fullDataSet.Columns.Add("Petugas Jemput");
                fullDataSet.Columns.Add("Waktu Jemput");

                foreach (var s in sampahList)
                {
                    fullDataSet.Rows.Add(
                        s.Id,
                        s.NamaSampah,
                        s.JenisSampah,
                        s.LokasiTPS,
                        s.WaktuInput.ToString("dd/MM/yyyy HH:mm"),
                        s.Status,
                        s.Catatan,
                        s.NamaPetugasInput,
                        s.NamaPetugasJemput,
                        s.WaktuJemput.HasValue ? s.WaktuJemput.Value.ToString("dd/MM/yyyy HH:mm") : "-"
                    );
                }

                if (dataGridView1 != null)
                {
                    dataGridView1.DataSource = fullDataSet;
                    if (dataGridView1.Columns["Id"] != null) dataGridView1.Columns["Id"].Visible = false;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.LogError(ex, "LoadAllSampah");
            }
        }

        private void BtnHapus_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count == 0)
                {
                    ErrorHandler.ShowWarning("Pilih data yang akan dihapus!", "Peringatan");
                    return;
                }

                string id = dataGridView1.SelectedRows[0].Cells["Id"].Value.ToString();
                
                if (ErrorHandler.ShowConfirmation("Apakah Anda yakin ingin menghapus data ini?", "Konfirmasi Hapus"))
                {
                    if (sampahHelper.DeleteSampah(id))
                    {
                        ErrorHandler.ShowInfo("Data berhasil dihapus.", Constants.TITLE_SUCCESS);
                        LoadAllSampah();
                        PerformanceManager.ClearCache();
                    }
                    else
                    {
                        ErrorHandler.ShowError("Gagal menghapus data.");
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.LogError(ex, "BtnHapus_Click");
            }
        }
        
        private void BtnExportPDF_Click(object sender, EventArgs e)
        {
             try
            {
                if (fullDataSet == null || fullDataSet.Rows.Count == 0)
                {
                    ErrorHandler.ShowWarning("Tidak ada data untuk diexport!", "Peringatan");
                    return;
                }

                SaveFileDialog sfd = new SaveFileDialog
                {
                    Filter = "PDF Files (*.pdf)|*.pdf",
                    FileName = $"Laporan_Sampah_Detail_{DateTime.Now:yyyyMMdd_HHmm}.pdf"
                };

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    GeneratePDF(sfd.FileName);
                    ErrorHandler.ShowInfo("Laporan Detail PDF berhasil dibuat!", Constants.TITLE_SUCCESS);
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.LogError(ex, "BtnExportPDF_Click");
                ErrorHandler.ShowError("Gagal export PDF: " + ex.Message);
            }
        }
        
        private void GeneratePDF(string fileName)
        {
             using (PdfWriter writer = new PdfWriter(fileName))
             {
                 using (PdfDocument pdf = new PdfDocument(writer))
                 {
                     Document doc = new Document(pdf, PageSize.A4.Rotate());
                     
                     doc.Add(new Paragraph("Laporan Detail Data Sampah")
                         .SetTextAlignment(TextAlignment.CENTER)
                         .SetFontSize(18));
                     
                     doc.Add(new Paragraph($"Tanggal Cetak: {DateTime.Now:dd/MM/yyyy HH:mm}")
                         .SetTextAlignment(TextAlignment.CENTER).SetFontSize(10));
                     
                     doc.Add(new Paragraph("\n"));

                     Table table = new Table(UnitValue.CreatePercentArray(new float[] { 15, 10, 15, 15, 10, 15, 10, 10 }));
                     table.SetWidth(UnitValue.CreatePercentValue(100));

                     string[] headers = { "Nama", "Jenis", "Lokasi", "Waktu Input", "Status", "Catatan", "Petugas", "Jemput" };
                     foreach (var header in headers)
                     {
                         table.AddHeaderCell(new Cell().Add(new Paragraph(header)).SetBackgroundColor(ColorConstants.LIGHT_GRAY));
                     }

                     foreach (DataRow row in fullDataSet.Rows)
                     {
                         table.AddCell(row["Nama Sampah"].ToString());
                         table.AddCell(row["Jenis"].ToString());
                         table.AddCell(row["Lokasi TPS"].ToString());
                         table.AddCell(row["Waktu Input"].ToString());
                         table.AddCell(row["Status"].ToString());
                         table.AddCell(row["Catatan"].ToString());
                         table.AddCell(row["Petugas Input"].ToString());
                         table.AddCell(row["Waktu Jemput"].ToString());
                     }

                     doc.Add(table);
                 }
             }
        }

        private void BtnSimpan_Click(object sender, EventArgs e)
        {
            ErrorHandler.ShowInfo("Fitur Edit/Simpan di Detail belum diaktifkan.", "Info");
        }
    }
}
