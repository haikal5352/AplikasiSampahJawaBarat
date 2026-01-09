using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using MongoDB.Bson;
using MongoDB.Driver;

// Tambahkan using lain yang diperlukan

namespace AplikasiSampahJabar
{
    public partial class Form1 : Form
    {
        // 1. Variabel Global untuk MongoDB [cite: 54]
        private MongoClient client;

        private IMongoDatabase database;
        private IMongoCollection<BsonDocument> collection;

        public Form1()
        {
            InitializeComponent();
            InitializeMongoDB(); // Panggil koneksi saat aplikasi mulai [cite: 231]
            TampilDataKeGrid();  // Load data awal
        }

        // 2. Method Koneksi Database
        private void InitializeMongoDB()
        {
            try
            {
                // Baca connection string dari file config
                string connectionString;
                if (System.IO.File.Exists("config.txt"))
                {
                    connectionString = System.IO.File.ReadAllText("config.txt").Trim();
                }
                else
                {
                    connectionString = "mongodb+srv://averouse:averouse@trashedcluster.izsn7ac.mongodb.net/?appName=TrashedCluster";
                }
                
                client = new MongoClient(connectionString);
                database = client.GetDatabase("db_sampah_jabar");
                collection = database.GetCollection<BsonDocument>("data_sampah");
                
                // Test koneksi
                database.RunCommand<BsonDocument>(new BsonDocument("ping", 1));
                MessageBox.Show("Berhasil terhubung ke MongoDB Atlas!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Koneksi MongoDB Atlas gagal: {ex.Message}\nAplikasi akan berjalan dengan data dummy.");
                collection = null;
            }
        }

        private void TampilDataKeGrid()
        {
            if (collection == null)
            {
                // Jika tidak ada koneksi database, tampilkan data dummy
                DataTable dt = new DataTable();
                dt.Columns.Add("Kabupaten");
                dt.Columns.Add("JenisSampah");
                dt.Columns.Add("Volume");
                dt.Columns.Add("WaktuInput");
                
                DataRow row = dt.NewRow();
                row["Kabupaten"] = "Bandung";
                row["JenisSampah"] = "Organik";
                row["Volume"] = "10";
                row["WaktuInput"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                dt.Rows.Add(row);
                
                dataGridView1.DataSource = dt;
                return;
            }

            // Ambil semua data dari MongoDB
            var documents = collection.Find(new BsonDocument()).ToList();
            DataTable dt2 = new DataTable();

            if (documents.Count > 0)
            {
                // A. Buat Kolom Header Otomatis dari Data Pertama [cite: 73]
                foreach (var element in documents[0].Elements)
                {
                    dt2.Columns.Add(element.Name); // Mengambil nama key sebagai judul kolom [cite: 82]
                }

                // B. Isi Baris Data [cite: 89]
                foreach (var doc in documents)
                {
                    DataRow row = dt2.NewRow();
                    foreach (var element in doc.Elements)
                    {
                        row[element.Name] = element.Value.ToString(); // Isi nilai per sel [cite: 99]
                    }
                    dt2.Rows.Add(row); // Masukkan baris ke tabel [cite: 105]
                }
            }

            dataGridView1.DataSource = dt2; // Tampilkan ke layar [cite: 112]
        }

        private void btnSimpan_Click(object sender, EventArgs e)
        {
            if (collection == null)
            {
                MessageBox.Show("Tidak ada koneksi database. Data tidak dapat disimpan.");
                return;
            }

            // Membuat dokumen BSON baru dari inputan user
            var dokumenBaru = new BsonDocument
    {
        { "Kabupaten", txtKabupaten.Text },
        { "JenisSampah", txtJenis.Text },
        { "Volume", txtVolume.Text },
        { "WaktuInput", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") }
    };

            collection.InsertOne(dokumenBaru); // Kirim ke MongoDB Atlas
            MessageBox.Show("Data berhasil disimpan!");
            TampilDataKeGrid(); // Refresh tabel
        }
        private void btnHapus_Click(object sender, EventArgs e)
        {
            // Cek apakah ada baris yang dipilih
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Ambil ID dari baris yang dipilih (pastikan kolom _id ada di grid)
                // Kolom 0 biasanya adalah "_id" jika kita load semua dari MongoDB
                var idSampah = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                var objectId = ObjectId.Parse(idSampah);

                // Hapus dari Database
                var filter = Builders<BsonDocument>.Filter.Eq("_id", objectId);
                collection.DeleteOne(filter);

                MessageBox.Show("Data berhasil dihapus!");
                TampilDataKeGrid(); // Refresh tabel
            }
            else
            {
                MessageBox.Show("Pilih baris data yang mau dihapus dulu!");
            }
        }

        private void btnExportPDF_Click(object sender, EventArgs e)
        {
            // 1. Siapkan dialog penyimpanan file [cite: 147]
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "PDF Files|*.pdf";
            sfd.Title = "Simpan Laporan Sampah";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                // 2. Ambil data terbaru [cite: 145]
                var documents = collection.Find(new BsonDocument()).ToList();

                // 3. Proses penulisan PDF menggunakan iText7 [cite: 156]
                using (PdfWriter writer = new PdfWriter(sfd.FileName))
                {
                    using (PdfDocument pdf = new PdfDocument(writer))
                    {
                        Document doc = new Document(pdf); // Membuat halaman dokumen [cite: 168]

                        // Judul Laporan
                        doc.Add(new Paragraph("Laporan Data Sampah Jawa Barat").SetFontSize(18));

                        // 4. Loop data dan masukkan ke PDF [cite: 171]
                        foreach (var d in documents)
                        {
                            // Mengambil isi dokumen dan menjadikannya paragraf [cite: 175]
                            string barisData = $"Wilayah: {d["Kabupaten"]}, Jenis: {d["JenisSampah"]}, Volume: {d["Volume"]} Ton";
                            doc.Add(new Paragraph(barisData));
                        }
                    }
                }
                MessageBox.Show("Laporan PDF berhasil dibuat!");
            }
        }


        private void btnKirimChat_Click(object sender, EventArgs e)
        {
            string pertanyaan = txtChatInput.Text.ToLower();
            string jawaban = "";

            // Logika chatbot sederhana
            if (pertanyaan.Contains("halo") || pertanyaan.Contains("selamat"))
            {
                jawaban = "Halo! Saya asisten data sampah Jabar. Ada yang bisa dibantu?";
            }
            else if (pertanyaan.Contains("total") || pertanyaan.Contains("jumlah"))
            {
                // Fitur pintar: Menghitung jumlah data di database
                long jumlahData = collection.CountDocuments(new BsonDocument());
                jawaban = $"Saat ini tercatat ada {jumlahData} entri data sampah di sistem.";
            }
            else if (pertanyaan.Contains("siapa yang buat"))
            {
                jawaban = "Aplikasi ini dibuat oleh Mahasiswa ULBI yang rajin.";
            }
            else
            {
                jawaban = "Maaf, saya belum mengerti. Coba tanya tentang 'jumlah data'.";
            }

            // Tampilkan chat
            rtbChatArea.AppendText("User: " + txtChatInput.Text + "\n");
            rtbChatArea.AppendText("Bot: " + jawaban + "\n\n");
            txtChatInput.Clear();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtJenis_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtKabupaten_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtVolume_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}