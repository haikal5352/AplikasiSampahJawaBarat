using System;

namespace AplikasiSampahJabar
{
    public static class Constants
    {
        // Database Constants
        public const string DATABASE_NAME = "db_sampah_jabar";
        public const string COLLECTION_NAME = "data_sampah";
        public const string CONFIG_FILE = "config.txt";
        public const string DEFAULT_CONNECTION_STRING = "mongodb+srv://averouse:averouse@trashedcluster.izsn7ac.mongodb.net/?appName=TrashedCluster";
        
        // Document Fields
        public const string FIELD_KABUPATEN = "Kabupaten";
        public const string FIELD_JENIS_SAMPAH = "JenisSampah";
        public const string FIELD_VOLUME = "Volume";
        public const string FIELD_WAKTU_INPUT = "WaktuInput";
        public const string FIELD_ID = "_id";
        
        // Messages
        public const string MSG_CONNECTION_SUCCESS = "Berhasil terhubung ke MongoDB Atlas!";
        public const string MSG_CONNECTION_FAILED = "Koneksi MongoDB Atlas gagal: {0}\nAplikasi akan berjalan dengan data dummy.";
        public const string MSG_NO_CONNECTION = "Tidak ada koneksi database. Data tidak dapat disimpan.";
        public const string MSG_SAVE_SUCCESS = "Data berhasil disimpan!";
        public const string MSG_DELETE_SUCCESS = "Data berhasil dihapus!";
        public const string MSG_SELECT_ROW = "Pilih baris data yang mau dihapus dulu!";
        public const string MSG_PDF_SUCCESS = "Laporan PDF berhasil dibuat!";
        public const string MSG_DELETE_CONFIRM = "Apakah Anda yakin ingin menghapus data ini?";
        public const string MSG_INVALID_DATA_FORMAT = "Format data tidak valid. Tidak dapat menghapus.";
        public const string MSG_DATABASE_ERROR = "Terjadi kesalahan database: {0}";
        public const string MSG_PDF_ERROR = "Gagal membuat PDF: {0}";
        public const string MSG_CHAT_ERROR = "Terjadi kesalahan pada chatbot: {0}";
        
        // PDF Constants
        public const string PDF_FILTER = "PDF Files|*.pdf";
        public const string PDF_TITLE = "Simpan Laporan Sampah";
        public const string PDF_REPORT_TITLE = "Laporan Data Sampah Jawa Barat";
        
        // Error Titles
        public const string TITLE_ERROR = "Error";
        public const string TITLE_WARNING = "Warning";
        public const string TITLE_CONFIRMATION = "Konfirmasi";
        public const string TITLE_SUCCESS = "Sukses";
        
        // Chat Constants
        public const string CHAT_GREETING = "Halo! Saya asisten data sampah Jabar. Ada yang bisa dibantu?";
        public const string CHAT_DATA_COUNT = "Saat ini tercatat ada {0} entri data sampah di sistem.";
        public const string CHAT_CREATOR = "Aplikasi ini dibuat oleh Mahasiswa ULBI yang rajin.";
        public const string CHAT_DEFAULT = "Maaf, saya belum mengerti. Coba tanya tentang 'jumlah data'.";
        
        // Performance Constants
        public const int DEFAULT_PAGE_SIZE = 50;
        public const int MAX_PAGE_SIZE = 200;
        public const int CACHE_DURATION_MINUTES = 5;
        
        // Date Format
        public const string DATE_FORMAT = "yyyy-MM-dd HH:mm:ss";
        
        // Dummy Data
        public const string DUMMY_KABUPATEN = "Bandung";
        public const string DUMMY_JENIS = "Organik";
        public const string DUMMY_VOLUME = "10";
    }
}