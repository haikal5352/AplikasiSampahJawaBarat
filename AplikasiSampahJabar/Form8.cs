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
    public partial class Form8 : Form
    {
        private DatabaseHelper dbHelper;
        private MongoClient client;
        private IMongoDatabase database;
        private DataTable userDataSet;

        public Form8()
        {
            InitializeComponent();

            this.StartPosition = FormStartPosition.CenterScreen;
            this.AutoScroll = true;
            this.WindowState = FormWindowState.Normal;
            this.Text = "Kelola User"; // Rename title

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
                dbHelper = new DatabaseHelper(database);
            }
            catch (Exception ex)
            {
                ErrorHandler.LogError(ex, "InitializeDatabase");
                ErrorHandler.ShowError("Gagal terhubung ke database.");
            }
        }

        private void InitializeUI()
        {
            LoadAllUsers();

            // Wire up events
            Button btnKeluar = this.Controls.Find("button4", true).FirstOrDefault() as Button;
            if (btnKeluar != null) btnKeluar.Click += (s, e) => this.Close();

            if (btnRefresh != null) btnRefresh.Click += (s, e) => LoadAllUsers();
            
            // Hapus Logic
            if (btnHapus != null) btnHapus.Click += BtnHapus_Click;
            
            // Export Logic
            if (btnExportPDF != null) btnExportPDF.Click += BtnExportUserPDF_Click;
            
            // Simpan (not needed for now or can show info)
            if (btnSimpan != null) btnSimpan.Click += (s, e) => ErrorHandler.ShowInfo("Fitur Edit User belum tersedia.", "Info");
        }

        private void LoadAllUsers()
        {
            try
            {
                // We need to implement GetAllUsers in DatabaseHelper first. 
                // Checks if it exists, otherwise we'll define it or implement inline if needed.
                // Assuming it exists based on previous file View.
                var users = dbHelper.GetAllUsers();

                userDataSet = new DataTable();
                userDataSet.Columns.Add("Id");
                userDataSet.Columns.Add("Username");
                userDataSet.Columns.Add("Nama");
                userDataSet.Columns.Add("Role");
                userDataSet.Columns.Add("Status");
                userDataSet.Columns.Add("Email");
                userDataSet.Columns.Add("Telepon");

                foreach (var u in users)
                {
                    userDataSet.Rows.Add(
                        u.Id,
                        u.Username,
                        u.Nama,
                        u.Role,
                        u.Status,
                        u.Email,
                        u.Telepon
                    );
                }

                if (dataGridView1 != null)
                {
                    dataGridView1.DataSource = userDataSet;
                    if (dataGridView1.Columns["Id"] != null) dataGridView1.Columns["Id"].Visible = false;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.LogError(ex, "LoadAllUsers");
                ErrorHandler.ShowError("Gagal memuat data user.");
            }
        }

        private void BtnHapus_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count == 0)
                {
                    ErrorHandler.ShowWarning("Pilih user yang akan dihapus!", "Peringatan");
                    return;
                }

                string id = dataGridView1.SelectedRows[0].Cells["Id"].Value.ToString();
                string username = dataGridView1.SelectedRows[0].Cells["Username"].Value.ToString();

                // Prevent deleting self or admin if needed
                if (username.ToLower() == "admin")
                {
                     ErrorHandler.ShowWarning("User Admin utama tidak boleh dihapus!", "Ditolak");
                     return;
                }

                if (ErrorHandler.ShowConfirmation($"Apakah Anda yakin ingin menghapus user '{username}'?", "Konfirmasi Hapus"))
                {
                    if (dbHelper.DeleteUser(ObjectId.Parse(id)))
                    {
                        ErrorHandler.ShowInfo("User berhasil dihapus.", Constants.TITLE_SUCCESS);
                        LoadAllUsers();
                    }
                    else
                    {
                        ErrorHandler.ShowError("Gagal menghapus user.");
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.LogError(ex, "BtnHapus_Click");
                ErrorHandler.ShowError("Terjadi kesalahan saat menghapus user.");
            }
        }

        private void BtnExportUserPDF_Click(object sender, EventArgs e)
        {
            try
            {
                if (userDataSet == null || userDataSet.Rows.Count == 0)
                {
                    ErrorHandler.ShowWarning("Tidak ada data user untuk diexport!", "Peringatan");
                    return;
                }

                SaveFileDialog sfd = new SaveFileDialog
                {
                    Filter = "PDF Files (*.pdf)|*.pdf",
                    FileName = $"Laporan_User_{DateTime.Now:yyyyMMdd_HHmm}.pdf"
                };

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    GenerateUserPDF(sfd.FileName);
                    ErrorHandler.ShowInfo("Laporan User PDF berhasil dibuat!", Constants.TITLE_SUCCESS);
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.LogError(ex, "BtnExportUserPDF_Click");
                ErrorHandler.ShowError("Gagal export PDF: " + ex.Message);
            }
        }

        private void GenerateUserPDF(string fileName)
        {
             using (PdfWriter writer = new PdfWriter(fileName))
             {
                 using (PdfDocument pdf = new PdfDocument(writer))
                 {
                     Document doc = new Document(pdf, PageSize.A4);
                     
                     doc.Add(new Paragraph("Laporan Data User Aplikasi Sampah")
                         .SetTextAlignment(TextAlignment.CENTER)
                         .SetFontSize(18));
                     
                     doc.Add(new Paragraph($"Tanggal Cetak: {DateTime.Now:dd/MM/yyyy HH:mm}")
                         .SetTextAlignment(TextAlignment.CENTER).SetFontSize(10));
                     
                     doc.Add(new Paragraph("\n"));

                     Table table = new Table(UnitValue.CreatePercentArray(new float[] { 20, 25, 15, 15, 25 }));
                     table.SetWidth(UnitValue.CreatePercentValue(100));

                     string[] headers = { "Username", "Nama", "Role", "Status", "Email" };
                     foreach (var header in headers)
                     {
                         table.AddHeaderCell(new Cell().Add(new Paragraph(header)).SetBackgroundColor(ColorConstants.LIGHT_GRAY));
                     }

                     foreach (DataRow row in userDataSet.Rows)
                     {
                         table.AddCell(row["Username"].ToString());
                         table.AddCell(row["Nama"].ToString());
                         table.AddCell(row["Role"].ToString());
                         table.AddCell(row["Status"].ToString());
                         table.AddCell(row["Email"].ToString());
                     }

                     doc.Add(table);
                     
                     doc.Add(new Paragraph("\n"));
                     doc.Add(new Paragraph($"Total User: {userDataSet.Rows.Count}"));
                 }
             }
        }
    }
}
