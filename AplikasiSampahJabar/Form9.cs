using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Data;

namespace AplikasiSampahJabar
{
    public partial class Form9 : Form
    {
        private SampahHelper sampahHelper;
        private MongoClient client;
        private IMongoDatabase database;
        private List<SampahModel> pendingSampahList;

        public Form9()
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
            // Setup Status
            cmbstatussampah.Items.Clear();
            cmbstatussampah.Items.Add(Constants.STATUS_MENUNGGU);
            cmbstatussampah.Items.Add(Constants.STATUS_DIJEMPUT);
            cmbstatussampah.Items.Add(Constants.STATUS_SELESAI);
            cmbstatussampah.Items.Add(Constants.STATUS_BATAL);
            cmbstatussampah.SelectedIndex = 0;

            // Setup Date
            datejemputsampah.Value = DateTime.Now;

            // Load Data
            // Load Data
            // Auto-migrate old data if any
            int migrated = sampahHelper.MigratePendingToMenunggu();
            if (migrated > 0)
            {
                // Optional: Notify user or just log
                // ErrorHandler.ShowInfo($"Berhasil memigrasi {migrated} data lama ke status Menunggu.", "Info Sistem");
            }

            LoadSampahPending();

            // Wire up Keluar Button
            // Assuming button4 is the 'Keluar' button based on previous context/design
            // Wire up Keluar Button
            Button btnKeluar = this.Controls.Find("button4", true).FirstOrDefault() as Button;
            if (btnKeluar != null)
            {
                btnKeluar.Click += (s, e) => this.Close();
            }

            // Wire up Simpan Button
            Button btnSimpan = this.Controls.Find("btnSimpan", true).FirstOrDefault() as Button;
            if (btnSimpan != null)
            {
                btnSimpan.Click += btnSimpan_Click;
            }

            // Wire up Refresh Button
            Button btnRefresh = this.Controls.Find("btnRefresh", true).FirstOrDefault() as Button;
            if (btnRefresh != null)
            {
                btnRefresh.Click += (s, e) => LoadSampahPending();
            }
        }

        private void LoadSampahPending()
        {
            try
            {
                // Ensure helper is initialized
                if (sampahHelper == null) return;

                // User wants to see ALL items, not just Menunggu
                pendingSampahList = sampahHelper.GetAllSampah();
                
                cmbnamasampah.Items.Clear();
                cmbnamasampah.DisplayMember = "Text";
                cmbnamasampah.ValueMember = "Value";

                foreach (var sampah in pendingSampahList)
                {
                    // Add status to display text so user knows what they are picking
                    string display = $"{sampah.NamaSampah} ({sampah.Status})";
                    cmbnamasampah.Items.Add(new ComboBoxItem { Text = display, Value = sampah });
                }

                if (cmbnamasampah.Items.Count > 0)
                    cmbnamasampah.SelectedIndex = 0;

                // Populate DataGridView
                DataTable dt = new DataTable();
                dt.Columns.Add("Id"); // Hidden
                dt.Columns.Add("Nama Sampah");
                dt.Columns.Add("Lokasi");
                dt.Columns.Add("Status"); 
                dt.Columns.Add("Waktu Input");
                dt.Columns.Add("Catatan");

                // Show all items, sorted by time
                foreach (var s in pendingSampahList.OrderByDescending(x => x.WaktuInput))
                {
                    dt.Rows.Add(s.Id, s.NamaSampah, s.LokasiTPS, s.Status, s.WaktuInput.ToString("dd/MM HH:mm"), s.Catatan);
                }

                if (this.Controls.Find("dataGridView1", true).FirstOrDefault() is DataGridView dgv)
                {
                    dgv.DataSource = null; // Clear first to ensure fresh binding
                    dgv.DataSource = dt;
                    if (dgv.Columns["Id"] != null) dgv.Columns["Id"].Visible = false;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.LogError(ex, "LoadSampahPending");
            }
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

                if (cmbnamasampah.SelectedItem == null)
                {
                    ErrorHandler.ShowWarning("Pilih sampah yang akan dijemput!", "Validasi");
                    return;
                }

                var selectedItem = (ComboBoxItem)cmbnamasampah.SelectedItem;
                var selectedSampah = (SampahModel)selectedItem.Value;

                string status = cmbstatussampah.SelectedItem?.ToString() ?? Constants.STATUS_DIJEMPUT;
                string catatan = richtxtcatatansampah.Text.Trim();
                // We might want to update the note as well if the user changed it
                // But UpdateSampahStatus only updates status/jemput info. 
                // Let's assume note update is not required or add it if needed.
                
                string namaPetugas = AuthManager.CurrentUser.Nama;
                DateTime tanggalJemput = datejemputsampah.Value;

                if (sampahHelper.UpdateSampahStatus(selectedSampah.Id.ToString(), status, namaPetugas, tanggalJemput))
                {
                    ErrorHandler.ShowInfo("Status sampah berhasil diperbarui!", Constants.TITLE_SUCCESS);
                    
                    // Clear UI
                    richtxtcatatansampah.Text = "";
                    LoadSampahPending();
                    
                    // Update cache
                    PerformanceManager.ClearCache();
                }
                else
                {
                    ErrorHandler.ShowError("Gagal memperbarui status sampah.");
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.LogError(ex, "btnSimpan_Click");
                ErrorHandler.ShowError("Terjadi kesalahan sistem: " + ex.Message);
            }
        }

        private class ComboBoxItem
        {
            public string Text { get; set; }
            public object Value { get; set; }
            public override string ToString() => Text;
        }

        private void Form9_Load(object sender, EventArgs e) 
        {

            
            this.StartPosition = FormStartPosition.CenterScreen;
            this.AutoScroll = true;
            this.WindowState = FormWindowState.Normal;
            
            // Wire up grid selection
            var dgv = this.Controls.Find("dataGridView1", true).FirstOrDefault() as DataGridView;
            if (dgv != null)
            {
                dgv.CellClick += DataGridView1_CellClick;
                dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            }
        }
        private void panel10_Paint(object sender, PaintEventArgs e) { }
        private void cmbstatussampah_SelectedIndexChanged(object sender, EventArgs e) { }
        private void cmbnamasampah_SelectedIndexChanged(object sender, EventArgs e) 
        {
             if (cmbnamasampah.SelectedItem is ComboBoxItem item && item.Value is SampahModel sampah)
             {
                 richtxtcatatansampah.Text = sampah.Catatan;
             }
        }
        private void richtxtcatatansampah_TextChanged(object sender, EventArgs e) { }
        private void datejemputsampah_ValueChanged(object sender, EventArgs e) { }
        
        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var dgv = sender as DataGridView;
                var row = dgv.Rows[e.RowIndex];
                var id = row.Cells["Id"].Value.ToString();

                // Find in ComboBox
                foreach (ComboBoxItem item in cmbnamasampah.Items)
                {
                    if (item.Value is SampahModel s && s.Id.ToString() == id)
                    {
                        cmbnamasampah.SelectedItem = item;
                        break;
                    }
                }
            }
        }
    }
}
