using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using MongoDB.Driver;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace AplikasiSampahJabar
{
    [System.Runtime.InteropServices.ComVisible(true)]
    public partial class Form10 : Form
    {
        private SampahHelper sampahHelper;
        private MongoClient client;
        private IMongoDatabase database;
        private WebBrowser webBrowser;

        public Form10()
        {
            InitializeComponent();

            this.StartPosition = FormStartPosition.CenterScreen;
            this.AutoScroll = true;
            this.WindowState = FormWindowState.Normal;

            InitializeDatabase();
            InitializeMap();
            InitializeButton();
        }
        
        private void InitializeButton()
        {
             // Assuming button4 is the Keluar button
            Button btnKeluar = this.Controls.Find("button4", true).FirstOrDefault() as Button;
            if (btnKeluar != null)
            {
                btnKeluar.Click += (s, e) => this.Close();
            }
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

        private void InitializeMap()
        {
            // Create WebBrowser dynamically
            webBrowser = new WebBrowser();
            webBrowser.Dock = DockStyle.Fill;
            webBrowser.ScriptErrorsSuppressed = true;
            webBrowser.IsWebBrowserContextMenuEnabled = false;
            webBrowser.ObjectForScripting = this;
            
            // Add a container panel to control layout
            Panel mapPanel = new Panel();
            mapPanel.Dock = DockStyle.Fill;
            mapPanel.Controls.Add(webBrowser);
            
            this.Controls.Add(mapPanel);
            mapPanel.BringToFront(); 

            // Load HTML using StartupPath (more reliable in WinForms)
            string appDir = AppDomain.CurrentDomain.BaseDirectory;
            string htmlPath = Path.Combine(appDir, "assets", "map.html");
            
            if (File.Exists(htmlPath))
            {
                // Force absolute file URI
                webBrowser.Navigate("file://" + htmlPath.Replace("\\", "/"));
                webBrowser.DocumentCompleted += WebBrowser_DocumentCompleted;
            }
            else
            {
                ErrorHandler.ShowError($"File map.html tidak ditemukan di: {htmlPath}. Pastikan folder 'assets' ada di output directory.");
            }
        }

        private void WebBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            LoadMapData();
        }

        private void LoadMapData()
        {
            try
            {
                // Get Aggregated Data
                var aggregatedData = sampahHelper.GetSampahByLokasiAggregated();
                var tpsList = SampahHelper.GetLokasiTPSList();

                var mapData = new List<object>();

                foreach (var tps in tpsList)
                {
                    int count = 0;
                    if (aggregatedData.ContainsKey(tps.Nama))
                    {
                        count = aggregatedData[tps.Nama].Count;
                    }

                    mapData.Add(new
                    {
                        Nama = tps.Nama,
                        Latitude = tps.Latitude,
                        Longitude = tps.Longitude,
                        Count = count
                    });
                }

                string json = JsonConvert.SerializeObject(mapData);
                if (webBrowser.Document != null)
                {
                    webBrowser.Document.InvokeScript("loadData", new object[] { json });
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.LogError(ex, "LoadMapData");
            }
        }

        private void Form10_Load(object sender, EventArgs e)
        {
           // Handled in Constructor
        }

        private void panel5_Paint(object sender, PaintEventArgs e) { }
    }
}
