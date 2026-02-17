using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using MongoDB.Driver;
using System.Collections.Generic;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Kernel.Geom;
using iText.Kernel.Colors;

namespace AplikasiSampahJabar
{
    public partial class Form11 : Form
    {
        private SampahHelper sampahHelper;
        private MongoClient client;
        private IMongoDatabase database;
        private DataTable fullDataSet;

        public Form11()
        {
            InitializeComponent();
            
            this.StartPosition = FormStartPosition.CenterScreen;
            this.AutoScroll = true;
            this.WindowState = FormWindowState.Normal;

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
            LoadDataForPreview();
            
            // Wire up events
            if (btnExportPDF != null) btnExportPDF.Click += btnExport_Click;
            if (btnRefresh != null) btnRefresh.Click += (s, e) => LoadDataForPreview();

             // Wire up Keluar
            Button btnKeluar = this.Controls.Find("button4", true).FirstOrDefault() as Button;
            if (btnKeluar != null) btnKeluar.Click += (s, e) => this.Close();
        }

        private void LoadDataForPreview()
        {
            try
            {
                var sampahList = sampahHelper.GetAllSampah();
                
                fullDataSet = new DataTable();
                fullDataSet.Columns.Add("Nama Sampah");
                fullDataSet.Columns.Add("Jenis");
                fullDataSet.Columns.Add("Lokasi TPS");
                fullDataSet.Columns.Add("Status");
                fullDataSet.Columns.Add("Waktu Input");

                foreach (var s in sampahList)
                {
                    fullDataSet.Rows.Add(
                        s.NamaSampah,
                        s.JenisSampah,
                        s.LokasiTPS,
                        s.Status,
                        s.WaktuInput.ToString("dd/MM/yyyy")
                    );
                }

                if (dataGridView1 != null)
                {
                    dataGridView1.DataSource = fullDataSet;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.LogError(ex, "LoadDataForPreview");
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
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
                    FileName = $"Laporan_Sampah_{DateTime.Now:yyyyMMdd_HHmm}.pdf"
                };

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    GeneratePDF(sfd.FileName);
                    ErrorHandler.ShowInfo("PDF berhasil dibuat!", Constants.TITLE_SUCCESS);
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.LogError(ex, "btnExport_Click");
                ErrorHandler.ShowError("Gagal export PDF: " + ex.Message);
            }
        }

        private void GeneratePDF(string fileName)
        {
            try
            {
                using (PdfWriter writer = new PdfWriter(fileName))
                {
                    using (PdfDocument pdf = new PdfDocument(writer))
                    {
                        Document doc = new Document(pdf, PageSize.A4.Rotate());
                        
                        // Title
                        Paragraph title = new Paragraph("Laporan Data Sampah Jawa Barat")
                            .SetTextAlignment(TextAlignment.CENTER)
                            .SetFontSize(20);
                            // .SetBold() removed as it might be an extension method issue; using default font for now or add explicit font later if needed
                        doc.Add(title);
                        
                        doc.Add(new Paragraph($"Tanggal Cetak: {DateTime.Now:dd/MM/yyyy HH:mm}")
                            .SetTextAlignment(TextAlignment.CENTER).SetFontSize(10));
                        
                        doc.Add(new Paragraph("\n"));

                        // Table
                        Table table = new Table(UnitValue.CreatePercentArray(new float[] { 20, 15, 20, 15, 15 }));
                        table.SetWidth(UnitValue.CreatePercentValue(100));

                        // Headers
                        string[] headers = { "Nama Sampah", "Jenis", "Lokasi TPS", "Status", "Waktu Input" };
                        foreach (var header in headers)
                        {
                            // Using standard font helper effectively makes it bold if we used the right font, 
                            // but simply removing SetBold to fix compile error first. 
                            // Can use .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD)) if critical.
                            table.AddHeaderCell(new Cell().Add(new Paragraph(header)).SetBackgroundColor(ColorConstants.LIGHT_GRAY));
                        }

                        // Data
                        foreach (DataRow row in fullDataSet.Rows)
                        {
                            table.AddCell(row["Nama Sampah"].ToString());
                            table.AddCell(row["Jenis"].ToString());
                            table.AddCell(row["Lokasi TPS"].ToString());
                            table.AddCell(row["Status"].ToString());
                            table.AddCell(row["Waktu Input"].ToString());
                        }

                        doc.Add(table);
                        
                        // Footer / Summary
                        doc.Add(new Paragraph("\n"));
                        doc.Add(new Paragraph($"Total Data: {fullDataSet.Rows.Count} items."));
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Form11_Load(object sender, EventArgs e) 
        { 

            
            LoadDataForPreview();
        }
        private void button4_Click(object sender, EventArgs e) { }
    }
}
