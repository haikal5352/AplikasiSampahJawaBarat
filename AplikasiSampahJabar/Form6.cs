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
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
            
            this.StartPosition = FormStartPosition.CenterScreen;
            this.AutoScroll = true;
            this.WindowState = FormWindowState.Normal;

            LoadData();
        }

        private void WireUpButtons()
        {
            btninputsampah.Click += BtnInputSampah_Click;
            btnjemputsampah.Click += BtnJemputSampah_Click;
            btnchatai.Click += BtnChatTrashy_Click;
            btnmaplokasisampah.Click += BtnLokasiSampah_Click;
        }

        private void BtnInputSampah_Click(object sender, EventArgs e)
        {
            this.Hide();
            using (Form1 formInput = new Form1())
            {
                formInput.ShowDialog();
            }
            this.Show();
            LoadData();
        }

        private void BtnJemputSampah_Click(object sender, EventArgs e)
        {
            this.Hide();
            using (Form9 formJemput = new Form9())
            {
                formJemput.ShowDialog();
            }
            this.Show();
            LoadData();
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

        private void Btninputsampah_Click(object sender, EventArgs e)
        {
            BtnInputSampah_Click(sender, e);
        }

        private void Btnjemputsampah_Click(object sender, EventArgs e)
        {
            BtnJemputSampah_Click(sender, e);
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
        
        private void Form6_Load(object sender, EventArgs e)
        {
             // Wire Refresh
             if (this.Controls.Find("btnRefresh", true).FirstOrDefault() is Button btnRefresh)
             {
                 btnRefresh.Click += (s, ev) => LoadData();
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

                // Create a simplified DataTable for dashboard
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

                // Show all items, sorted by time
                var sortedData = data.OrderByDescending(x => x.WaktuInput);

                foreach (var s in sortedData)
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

                // Assuming dataGridView1 exists (standard name)
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
    }
}
