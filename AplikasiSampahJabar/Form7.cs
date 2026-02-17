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
    public partial class Form7 : Form
    {
        public Form7()
        {
            InitializeComponent();
            WireUpButtons();
            
            this.StartPosition = FormStartPosition.CenterScreen;
            this.AutoScroll = true;
            this.WindowState = FormWindowState.Normal;

            LoadData();
        }
        
        private void WireUpButtons()
        {
            btnchatai.Click += BtnChatTrashy_Click;
            btnmaplokasisampah.Click += BtnLokasiSampah_Click;
        }
        
        private void BtnChatTrashy_Click(object sender, EventArgs e)
        {
            this.Hide();
            using (Form11 chatForm = new Form11())
            {
                chatForm.ShowDialog();
            }
            this.Show();
        }
        
        private void BtnLokasiSampah_Click(object sender, EventArgs e)
        {
            this.Hide();
            using (Form10 formMap = new Form10())
            {
                formMap.ShowDialog();
            }
            this.Show();
        }
        
        private void BtnDetailSampah_Click(object sender, EventArgs e)
        {
            // Repurpose as Refresh
            LoadData();
        }
        
        private void BtnLogout_Click(object sender, EventArgs e)
        {
            if (ErrorHandler.ShowConfirmation("Apakah Anda yakin ingin logout?", "Konfirmasi Logout"))
            {
                AuthManager.Logout();
                Form2 loginForm = new Form2();
                loginForm.Show();
                this.Close();
            }
        }
        
        private void BtnExportPDF_Click(object sender, EventArgs e)
        {
            try
            {
                string connectionString = System.IO.File.Exists(Constants.CONFIG_FILE)
                    ? System.IO.File.ReadAllText(Constants.CONFIG_FILE).Trim()
                    : Constants.DEFAULT_CONNECTION_STRING;
                var client = new MongoClient(connectionString);
                var database = client.GetDatabase(Constants.DATABASE_NAME);
                var sampahHelper = new SampahHelper(database);
                
                var data = sampahHelper.GetAllSampah();
                ExportHelper.ExportToPDF(data);
            }
            catch (Exception ex)
            {
                ErrorHandler.ShowError("Gagal memulai export PDF: " + ex.Message);
            }
        }
        
        private void Btnmaplokasisampah_Click(object sender, EventArgs e)
        {
            BtnLokasiSampah_Click(sender, e);
        }
        
        private void Btndetailsampah_Click(object sender, EventArgs e)
        {
            BtnDetailSampah_Click(sender, e);
        }
        
        private void Btnchatai_Click(object sender, EventArgs e)
        {
            BtnChatTrashy_Click(sender, e);
        }

        private void Form7_Load(object sender, EventArgs e)
        {
             // Wire Refresh
             if (this.Controls.Find("btnRefresh", true).FirstOrDefault() is Button btnRefresh)
             {
                 btnRefresh.Click += (s, ev) => LoadData();
             }

    // Wire up Chart Button
             if (this.Controls.Find("buttonChartSampah", true).FirstOrDefault() is Button btnChart)
     {
   btnChart.Click += BtnChartSampah_Click;
             }
        }

        private void LoadData()
        {
            try
            {
                string connectionString = System.IO.File.Exists(Constants.CONFIG_FILE)
                    ? System.IO.File.ReadAllText(Constants.CONFIG_FILE).Trim()
                    : Constants.DEFAULT_CONNECTION_STRING;
                var client = new MongoClient(connectionString);
                var database = client.GetDatabase(Constants.DATABASE_NAME);
                var sampahHelper = new SampahHelper(database);
                
                var data = sampahHelper.GetAllSampah();
                
                DataTable dt = new DataTable();
                dt.Columns.Add("Nama Sampah", typeof(string));
                dt.Columns.Add("Jenis", typeof(string));
                dt.Columns.Add("Lokasi TPS", typeof(string));
                dt.Columns.Add("Status", typeof(string));
                dt.Columns.Add("Waktu Input", typeof(string));
                dt.Columns.Add("Catatan", typeof(string));
                dt.Columns.Add("Petugas Input", typeof(string));
                dt.Columns.Add("Petugas Jemput", typeof(string));
                dt.Columns.Add("Waktu Jemput", typeof(string));
                
                foreach (var s in data.OrderByDescending(x => x.WaktuInput))
                {
                    dt.Rows.Add(
                        s.NamaSampah, 
                        s.JenisSampah, 
                        s.LokasiTPS, 
                        s.Status, 
                        s.WaktuInput.ToString("dd/MM/yyyy HH:mm"),
                        s.Catatan,
                        s.NamaPetugasInput,
                        s.NamaPetugasJemput,
                        s.WaktuJemput.HasValue ? s.WaktuJemput.Value.ToString("dd/MM/yyyy HH:mm") : "-"
                    );
                }
                
                if (this.Controls.Find("dataGridView1", true).FirstOrDefault() is DataGridView dgv)
                {
                    dgv.DataSource = null;
                    dgv.DataSource = dt;
                    dgv.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.ShowError($"Gagal memuat data: {ex.Message}");
            }
        }

        private void BtnChartSampah_Click(object sender, EventArgs e)
        {
this.Hide();
      using (Form12 formChart = new Form12())
      {
              formChart.ShowDialog();
            }
            this.Show();
        }
    }
}
