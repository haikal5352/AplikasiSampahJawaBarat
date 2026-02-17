using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace AplikasiSampahJabar
{
    public class SampahModel
    {
        [BsonId]
        public ObjectId Id { get; set; }
        
        [BsonElement("nama_sampah")]
        public string NamaSampah { get; set; }
        
        [BsonElement("jenis_sampah")]
        public string JenisSampah { get; set; }
        
        [BsonElement("lokasi_tps")]
        public string LokasiTPS { get; set; }
        
        [BsonElement("latitude")]
        public double Latitude { get; set; }
        
        [BsonElement("longitude")]
        public double Longitude { get; set; }
        
        [BsonElement("waktu_input")]
        public DateTime WaktuInput { get; set; }
        
        [BsonElement("catatan")]
        public string Catatan { get; set; }
        
        [BsonElement("nama_petugas_input")]
        public string NamaPetugasInput { get; set; }
        
        [BsonElement("status")]
        public string Status { get; set; }
        
        [BsonElement("tanggal_jemput")]
        public DateTime? TanggalJemput { get; set; }
        
        [BsonElement("nama_petugas_jemput")]
        public string NamaPetugasJemput { get; set; }

        [BsonElement("waktu_jemput")]
        public DateTime? WaktuJemput { get; set; }
    }
    
    public class LokasiTPS
    {
        public string Nama { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        
        public override string ToString()
        {
            return Nama;
        }
    }
}
