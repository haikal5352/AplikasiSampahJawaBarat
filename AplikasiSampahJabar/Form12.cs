using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MongoDB.Driver;

namespace AplikasiSampahJabar
{
    public partial class Form12 : Form
    {
        private SampahHelper sampahHelper;
        private MongoClient client;
    private IMongoDatabase database;

    public Form12()
        {
            InitializeComponent();
        InitializeDatabase();
        LoadChartData();
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
        // Wire up Keluar Button
        Button btnKeluar = this.Controls.Find("button4", true).FirstOrDefault() as Button;
        if (btnKeluar != null)
      {
      btnKeluar.Click += (s, e) => this.Close();
        }

   // Wire up Export PDF Button
 Button btnExportPDF = this.Controls.Find("btnExportPDF", true).FirstOrDefault() as Button;
            if (btnExportPDF != null)
            {
           btnExportPDF.Click += BtnExportPDF_Click;
  }
        }

        private void LoadChartData()
        {
            try
            {
                if (sampahHelper == null) return;

             // Get data sampah by jenis from database
    Dictionary<string, int> jenisData = sampahHelper.GetSampahByJenis();

      if (jenisData.Count == 0)
       {
        ErrorHandler.ShowWarning("Tidak ada data sampah di database.", "Peringatan");
      return;
  }

       // Clear existing chart data
         chartsampah.Series.Clear();
 chartsampah.ChartAreas[0].AxisX.LabelStyle.Angle = -45;

       // Create new series for chart
    System.Windows.Forms.DataVisualization.Charting.Series series = new System.Windows.Forms.DataVisualization.Charting.Series("Jumlah Sampah");
  series.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
         series.Color = Color.ForestGreen;
 series.BorderColor = Color.DarkGreen;
            series.BorderWidth = 2;

 // Add data points from dictionary
         foreach (var item in jenisData)
         {
        series.Points.AddXY(item.Key, item.Value);
   }

      chartsampah.Series.Add(series);

      // Format chart
          chartsampah.Titles.Clear();
                System.Windows.Forms.DataVisualization.Charting.Title title = new System.Windows.Forms.DataVisualization.Charting.Title();
    title.Text = "Grafik Jumlah Sampah Per Jenis";
         title.Font = new Font("Arial", 14, FontStyle.Bold);
          chartsampah.Titles.Add(title);

       // Set axis labels
    chartsampah.ChartAreas[0].AxisX.Title = "Jenis Sampah";
     chartsampah.ChartAreas[0].AxisY.Title = "Jumlah (Unit)";

  chartsampah.Invalidate();
}
            catch (Exception ex)
      {
     ErrorHandler.LogError(ex, "LoadChartData");
    ErrorHandler.ShowError($"Gagal memuat data chart: {ex.Message}");
     }
    }

        private void BtnExportPDF_Click(object sender, EventArgs e)
        {
       try
      {
  SaveFileDialog sfd = new SaveFileDialog
            {
    Filter = "PDF Files (*.pdf)|*.pdf",
      FileName = $"Grafik_Sampah_{DateTime.Now:yyyyMMdd_HHmm}.pdf"
};

        if (sfd.ShowDialog() == DialogResult.OK)
         {
        // Save chart as image first
      string tempImagePath = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "chart_temp.png");
       chartsampah.SaveImage(tempImagePath, System.Windows.Forms.DataVisualization.Charting.ChartImageFormat.Png);

      // Generate PDF with chart image
             ExportHelper.ExportChartToPDF(sfd.FileName, tempImagePath);
       ErrorHandler.ShowInfo("Laporan grafik berhasil diekspor ke PDF!", Constants.TITLE_SUCCESS);

         // Clean up temp file
  if (System.IO.File.Exists(tempImagePath))
  System.IO.File.Delete(tempImagePath);
           }
         }
            catch (Exception ex)
            {
         ErrorHandler.LogError(ex, "BtnExportPDF_Click");
 ErrorHandler.ShowError("Gagal export chart ke PDF: " + ex.Message);
        }
        }
    }
}
