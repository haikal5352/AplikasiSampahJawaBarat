using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;

namespace AplikasiSampahJabar
{
    public partial class Form1 : Form
    {
        private SampahHelper sampahHelper;
        private MongoClient client;
        private IMongoDatabase database;

        public Form1()
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
            // Setup Jenis Sampah
            cmbjenissampah.Items.Clear();
            cmbjenissampah.Items.Add(Constants.JENIS_ORGANIK);
            cmbjenissampah.Items.Add(Constants.JENIS_ANORGANIK);
            cmbjenissampah.Items.Add(Constants.JENIS_B3);
            cmbjenissampah.SelectedIndex = 0;

            // Setup Lokasi TPS
            cmblokasitps.Items.Clear();
            var tpsList = SampahHelper.GetLokasiTPSList();
            foreach (var tps in tpsList)
            {
                cmblokasitps.Items.Add(tps.Nama);
            }
            if (cmblokasitps.Items.Count > 0) cmblokasitps.SelectedIndex = 0;

            // Setup Date
            dateinputsampah.Value = DateTime.Now;
            
            // Setup Petugas
            if (AuthManager.IsLoggedIn)
            {
                // Optional: Show petugas name in a label if needed
            }
            
            // Wire up Keluar button
            btnkeluarmenuinput.Click += (s, e) => this.Close();
        }

        private void btnSimpan_Click(object sender, EventArgs e)
        {
            try
            {
                if (!AuthManager.IsLoggedIn)
                {
                    ErrorHandler.ShowWarning("Anda harus login terlebih dahulu!", "Akses Ditolak");
                    this.Close();
                    return;
                }

                // Validate Inputs
                string namaSampah = txtnamasampah.Text.Trim();
                string jenisSampah = cmbjenissampah.SelectedItem?.ToString();
                string lokasiTPS = cmblokasitps.SelectedItem?.ToString();
                string catatan = richtxtcatatansampah.Text.Trim();
                
                // Validate Nama Sampah
                if (string.IsNullOrEmpty(namaSampah))
                {
                    ErrorHandler.ShowWarning("Nama Sampah tidak boleh kosong!", "Validasi");
                    return;
                }

                // Validate Lokasi (Get Lat/Long)
                var tpsList = SampahHelper.GetLokasiTPSList();
                var selectedTPS = tpsList.FirstOrDefault(t => t.Nama == lokasiTPS);
                if (selectedTPS == null)
                {
                    ErrorHandler.ShowWarning("Pilih Lokasi TPS yang valid!", "Validasi");
                    return;
                }

                // Create Model
                var sampah = new SampahModel
                {
                    NamaSampah = namaSampah,
                    JenisSampah = jenisSampah,
                    LokasiTPS = lokasiTPS,
                    Latitude = selectedTPS.Latitude,
                    Longitude = selectedTPS.Longitude,
                    WaktuInput = dateinputsampah.Value,
                    Catatan = catatan,
                    NamaPetugasInput = AuthManager.CurrentUser?.Nama ?? "Unknown",
                    Status = Constants.STATUS_MENUNGGU,
                    TanggalJemput = null,
                    NamaPetugasJemput = null
                    // Note: Volume is not in the current model but exists in Form1 UI (beratsampah). 
                    // If needed we can add it to the model later. For now we skip it in the model or add it to notes.
                };
                
                // Add volume to notes if provided
                if (beratsampah.Value > 0)
                {
                    sampah.Catatan += $" [Berat: {beratsampah.Value} Kg]";
                }

                // Save
                if (sampahHelper.InsertSampah(sampah))
                {
                    ErrorHandler.ShowInfo(Constants.MSG_SAMPAH_SAVE_SUCCESS, Constants.TITLE_SUCCESS);
                    ClearInputs();
                    LoadData(); // Refresh grid
                }
                else
                {
                    ErrorHandler.ShowError("Gagal menyimpan data sampah.");
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.LogError(ex, "btnSimpan_Click");
                ErrorHandler.ShowError("Terjadi kesalahan sistem: " + ex.Message);
            }
        }

        private void ClearInputs()
        {
            txtnamasampah.Text = "";
            richtxtcatatansampah.Text = "";
            beratsampah.Value = 0;
            dateinputsampah.Value = DateTime.Now;
            if (cmbjenissampah.Items.Count > 0) cmbjenissampah.SelectedIndex = 0;
            if (cmblokasitps.Items.Count > 0) cmblokasitps.SelectedIndex = 0;
        }

        private void btnHapus_Click(object sender, EventArgs e)
        {
            // Not used in Input Form
        }

        private void btnExportPDF_Click(object sender, EventArgs e)
        {
             // Not used in Input Form
        }

        private void btnPrevious_Click(object sender, EventArgs e) { }
        private void btnNext_Click(object sender, EventArgs e) { }
        private void btnRefresh_Click(object sender, EventArgs e) { }
        private void Form1_Load(object sender, EventArgs e) 
        { 

            
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                if (sampahHelper == null) return;
                
                var data = sampahHelper.GetAllSampah();
                
                DataTable dt = new DataTable();
                dt.Columns.Add("Nama", typeof(string));
                dt.Columns.Add("Jenis", typeof(string));
                dt.Columns.Add("Lokasi", typeof(string));
                dt.Columns.Add("Status", typeof(string));
                
                // Show recent 20
                foreach (var s in data.OrderByDescending(x => x.WaktuInput).Take(20))
                {
                    dt.Rows.Add(s.NamaSampah, s.JenisSampah, s.LokasiTPS, s.Status);
                }
                
                if (this.Controls.Find("dataGridView1", true).FirstOrDefault() is DataGridView dgv)
                {
                    dgv.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                // Silent catch for load to not annoy user in input form
                ErrorHandler.LogError(ex, "Form1_LoadData");
            }
        }
        private void txtnamasampah_TextChanged(object sender, EventArgs e) { }
        private void cmbjenissampah_SelectedIndexChanged(object sender, EventArgs e) { }
        private void cmblokasitps_SelectedIndexChanged(object sender, EventArgs e) { }
        private void beratsampah_ValueChanged(object sender, EventArgs e) { }
        private void richtxtcatatansampah_TextChanged(object sender, EventArgs e) { }
        private void dateinputsampah_ValueChanged(object sender, EventArgs e) { }
    }
}