# Aplikasi Pengelolaan Sampah Jawa Barat (AppsSampah) 🌍♻️

Aplikasi ini merupakan sistem manajemen pengelolaan sampah berbasis desktop yang dirancang untuk mempermudah monitoring, pelaporan, dan edukasi pengelolaan sampah di wilayah Jawa Barat. Proyek ini dikembangkan sebagai **Ujian Akhir Semester (UAS) Semester 3 - D4 Teknik Informatika**.

## 🚀 Fitur Utama

### 1. Sistem Multi-Role
Aplikasi mendukung tiga level akses dengan otoritas yang berbeda:
- **Admin**: Mengelola seluruh data sampah, data user, dan memantau statistik dashboard.
- **Petugas**: Fokus pada manajemen penjemputan sampah dan pembaruan status laporan.
- **Pengguna (Masyarakat)**: Melaporkan titik sampah, memantau riwayat laporan, dan berinteraksi dengan AI.

### 2. Trashy AI (Intelligent Assistant) 🤖
Integrasi **Mistral AI** dengan metode **RAG (Retrieval-Augmented Generation)**:
- AI dapat menjawab pertanyaan seputar edukasi lingkungan (daur ulang, kompos, dll).
- AI memiliki akses ke konteks database terbaru (jumlah laporan pending, TPS terpadat, dll) untuk memberikan jawaban yang akurat terkait data real-time.
- Riwayat percakapan dapat diekspor ke format PDF.

### 3. Visualisasi Peta Sampah 📍
- Pemetaan lokasi penumpukan sampah menggunakan **Leaflet.js** dalam kontrol WebBrowser.
- Penanda (Markers) yang menunjukkan titik sampah berdasarkan koordinat GPS dari database.

### 4. Manajemen Data & Pelaporan
- **Direct PDF Export**: Menghasilkan laporan data sampah secara instan dari dashboard.
- **Status Tracking**: Pemantauan status sampah mulai dari *Menunggu*, *Dijemput*, hingga *Selesai*.
- **Pagination**: Sistem navigasi data yang rapi untuk menangani volume data yang besar.

### 5. Navigasi Modern (Page-Switching)
- Antarmuka yang mulus dengan transisi antar form yang otomatis menyembunyikan jendela sebelumnya (Hide/Show logic).

## 🛠️ Tech Stack

- **Bahasa Pemrograman**: C# (.NET Framework)
- **Framework UI**: Windows Forms (WinForms)
- **Database**: MongoDB (NoSQL)
- **AI Engine**: Mistral AI API
- **Library Utama**:
  - `MongoDB.Driver`: Koneksi ke database NoSQL.
  - `iText7`: Pembuatan dokumen PDF profesional.
  - `Newtonsoft.Json`: Parsing data API dan konfigurasi.
  - `Leaflet.js`: Visualisasi peta interaktif.

## 📂 Struktur Proyek

```text
AplikasiSampahJawaBarat/
├── AplikasiSampahJabar/
│   ├── assets/             # Aset gambar dan file HTML Map
│   ├── Constants.cs        # Konfigurasi sistem dan teks konstanta
│   ├── DatabaseHelper.cs   # Logika CRUD MongoDB
│   ├── MistralAIHelper.cs # Integrasi AI dan RAG context
│   ├── ExportHelper.cs     # Logika pembuatan PDF
│   └── Form1 - Form12.cs   # Antarmuka aplikasi
├── config.txt              # Konfigurasi database string
└── README.md
```

## ⚙️ Persiapan & Instalasi

1. **Prasyarat**:
   - Visual Studio 2019/2022.
   - .NET Framework 4.7.2 atau versi terbaru.
   - MongoDB (Local atau MongoDB Atlas).

2. **Koneksi Database**:
   - Buka file `config.txt` di root folder.
   - Tempelkan *Connection String* MongoDB Anda ke dalam file tersebut.

3. **API Key Mistral AI**:
   - Buka `Constants.cs`.
   - Ganti `YOUR_MISTRAL_API_KEY_HERE` dengan API Key dari [Mistral Console](https://console.mistral.ai/).

4. **Build & Run**:
   - Buka file `.sln` menggunakan Visual Studio.
   - Lakukan *Restore NuGet Packages*.
   - Tekan `F5` untuk menjalankan aplikasi.

## 📝 Catatan Pengembang

Aplikasi ini dikembangkan dengan menekankan pada efisiensi akses data dan kemudahan penggunaan bagi masyarakat awam maupun petugas lapangan. Fitur AI diharapkan dapat meningkatkan kesadaran masyarakat tentang pentingnya pemilahan sampah.

---
**© 2026 - UAS Pemrograman Berorientasi Objek**
