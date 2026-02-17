using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace AplikasiSampahJabar
{
    public class SampahHelper
    {
        private IMongoCollection<SampahModel> _sampahCollection;
        
        public SampahHelper(IMongoDatabase database)
        {
            _sampahCollection = database.GetCollection<SampahModel>(Constants.COLLECTION_SAMPAH);
        }
        
        public static List<LokasiTPS> GetLokasiTPSList()
        {
            return new List<LokasiTPS>
            {
                new LokasiTPS { Nama = "TPS Sarimadu", Latitude = -6.8830149, Longitude = 107.5734911 },
                new LokasiTPS { Nama = "TPS Ciwaruga", Latitude = -6.8619824, Longitude = 107.5768654 },
                new LokasiTPS { Nama = "TPS Siliwangi", Latitude = -6.884960, Longitude = 107.608821 },
                new LokasiTPS { Nama = "TPS Summer Hill", Latitude = -6.870325, Longitude = 107.586476 },
                new LokasiTPS { Nama = "TPS Logistik", Latitude = -6.873724, Longitude = 107.575666 }
            };
        }
        
        public bool InsertSampah(SampahModel sampah)
        {
            try
            {
                _sampahCollection.InsertOne(sampah);
                return true;
            }
            catch (Exception ex)
            {
                ErrorHandler.LogError(ex, "InsertSampah");
                return false;
            }
        }
        
        public List<SampahModel> GetAllSampah()
        {
            try
            {
                return _sampahCollection.Find(_ => true).ToList();
            }
            catch (Exception ex)
            {
                ErrorHandler.LogError(ex, "GetAllSampah");
                ErrorHandler.ShowError($"Gagal mengambil data sampah: {ex.Message}");
                return new List<SampahModel>();
            }
        }
        
        public SampahModel GetSampahById(ObjectId id)
        {
            try
            {
                return _sampahCollection.Find(s => s.Id == id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                ErrorHandler.LogError(ex, "GetSampahById");
                return null;
            }
        }
        
        public bool UpdateSampah(SampahModel sampah)
        {
            try
            {
                var filter = Builders<SampahModel>.Filter.Eq(s => s.Id, sampah.Id);
                var result = _sampahCollection.ReplaceOne(filter, sampah);
                return result.ModifiedCount > 0;
            }
            catch (Exception ex)
            {
                ErrorHandler.LogError(ex, "UpdateSampah");
                return false;
            }
        }
        
        public bool JemputSampah(ObjectId id, DateTime tanggalJemput, string namaPetugas)
        {
            try
            {
                var filter = Builders<SampahModel>.Filter.Eq(s => s.Id, id);
                var update = Builders<SampahModel>.Update
                    .Set(s => s.Status, Constants.STATUS_DIJEMPUT)
                    .Set(s => s.TanggalJemput, tanggalJemput)
                    .Set(s => s.NamaPetugasJemput, namaPetugas);
                
                var result = _sampahCollection.UpdateOne(filter, update);
                return result.ModifiedCount > 0;
            }
            catch (Exception ex)
            {
                ErrorHandler.LogError(ex, "JemputSampah");
                return false;
            }
        }
        
        public bool DeleteSampah(string id)
        {
            try
            {
                if (!ObjectId.TryParse(id, out ObjectId objectId))
                {
                    ErrorHandler.LogError(new Exception("Invalid ObjectId format"), "DeleteSampah");
                    return false;
                }

                var filter = Builders<SampahModel>.Filter.Eq(s => s.Id, objectId);
                var result = _sampahCollection.DeleteOne(filter);
                return result.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                ErrorHandler.LogError(ex, "DeleteSampah");
                return false;
            }
        }
        
        public List<SampahModel> GetSampahByStatus(string status)
        {
            try
            {
                return _sampahCollection.Find(s => s.Status == status).ToList();
            }
            catch (Exception ex)
            {
                ErrorHandler.LogError(ex, "GetSampahByStatus");
                ErrorHandler.ShowError($"Gagal mengambil data status '{status}': {ex.Message}");
                return new List<SampahModel>();
            }
        }
        
        public Dictionary<string, int> GetSampahByLokasi()
        {
            try
            {
                var result = new Dictionary<string, int>();
                var allSampah = _sampahCollection.Find(_ => true).ToList();
                
                foreach (var sampah in allSampah)
                {
                    if (result.ContainsKey(sampah.LokasiTPS))
                        result[sampah.LokasiTPS]++;
                    else
                        result[sampah.LokasiTPS] = 1;
                }
                
                return result;
            }
            catch (Exception ex)
            {
                ErrorHandler.LogError(ex, "GetSampahByLokasi");
                return new Dictionary<string, int>();
            }
        }
        public Dictionary<string, (int Count, double Volume)> GetSampahByLokasiAggregated()
        {
            try
            {
                var result = new Dictionary<string, (int Count, double Volume)>();
                var allSampah = _sampahCollection.Find(s => s.Status == Constants.STATUS_PENDING).ToList();
                
                // Initialize with all TPS locations to ensure they appear on map even if empty
                var tpsList = GetLokasiTPSList();
                foreach (var tps in tpsList)
                {
                    result[tps.Nama] = (0, 0);
                }

                foreach (var sampah in allSampah)
                {
                    if (result.ContainsKey(sampah.LokasiTPS))
                    {
                        var current = result[sampah.LokasiTPS];
                        // Assuming volume logic needs to be implemented or we just count for now. 
                        // If Volume is needed in SampahModel, we should add it. 
                        // For now let's assume we just count.
                         result[sampah.LokasiTPS] = (current.Count + 1, current.Volume);
                    }
                }
                
                return result;
            }
            catch (Exception ex)
            {
                ErrorHandler.LogError(ex, "GetSampahByLokasiAggregated");
                return new Dictionary<string, (int Count, double Volume)>();
            }
        }
        public bool UpdateSampahStatus(string id, string status, string namaPetugas, DateTime? tanggalJemput)
        {
            try
            {
                if (!ObjectId.TryParse(id, out ObjectId objectId))
                {
                    ErrorHandler.LogError(new Exception("Invalid ObjectId format"), "UpdateSampahStatus");
                    return false;
                }

                var filter = Builders<SampahModel>.Filter.Eq(s => s.Id, objectId);
                var update = Builders<SampahModel>.Update
                    .Set(s => s.Status, status)
                    .Set(s => s.NamaPetugasJemput, namaPetugas)
                    .Set(s => s.TanggalJemput, tanggalJemput)
                    .Set(s => s.WaktuJemput, tanggalJemput); 

                var result = _sampahCollection.UpdateOne(filter, update);
                return result.ModifiedCount > 0;
            }
            catch (Exception ex)
            {
                ErrorHandler.LogError(ex, "UpdateSampahStatus");
                return false;
            }
        }
        public int MigratePendingToMenunggu()
        {
            try
            {
                // Find all documents with Status "Pending"
                var filter = Builders<SampahModel>.Filter.Eq(s => s.Status, "Pending");
                var update = Builders<SampahModel>.Update.Set(s => s.Status, Constants.STATUS_MENUNGGU);
                
                var result = _sampahCollection.UpdateMany(filter, update);
                return (int)result.ModifiedCount;
            }
            catch (Exception ex)
            {
                ErrorHandler.LogError(ex, "MigratePendingToMenunggu");
                return 0;
            }
        }
    }
}
