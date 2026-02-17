using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace AplikasiSampahJabar
{
    public class DatabaseHelper
    {
        private IMongoCollection<UserModel> _usersCollection;
        
        public DatabaseHelper(IMongoDatabase database)
        {
            _usersCollection = database.GetCollection<UserModel>("users");
            InitializeAdminUser();
        }

        public bool CheckConnection()
        {
            try
            {
                // Ping the database to check connection
                return _usersCollection.Database.RunCommandAsync((Command<BsonDocument>)"{ping:1}").Wait(5000);
            }
            catch
            {
                return false;
            }
        }
        
        private void InitializeAdminUser()
        {
            try
            {
                // Ensure admin always exists and has the correct password (admin123)
                var filter = Builders<UserModel>.Filter.Eq(u => u.Username, "admin");
                var update = Builders<UserModel>.Update
                    .Set(u => u.Password, AuthManager.HashPassword("admin123"))
                    .Set(u => u.Role, "Admin")
                    .Set(u => u.Nama, "Administrator")
                    .Set(u => u.Status, "Aktif")
                    .SetOnInsert(u => u.Email, "admin@sampahjabar.com")
                    .SetOnInsert(u => u.Telepon, "08123456789")
                    .SetOnInsert(u => u.CreatedAt, DateTime.Now);

                _usersCollection.UpdateOne(filter, update, new UpdateOptions { IsUpsert = true });
            }
            catch (Exception ex)
            {
                ErrorHandler.LogError(ex, "InitializeAdminUser");
            }
        }
        
        public UserModel Login(string username, string password, string role)
        {
            try
            {
                // DEBUGGING LOGIC: Find user by username only first
                var user = _usersCollection.Find(u => u.Username == username).FirstOrDefault();
                
                if (user == null)
                {
                    ErrorHandler.ShowWarning($"User '{username}' tidak ditemukan di database.", "Login Gagal");
                    return null;
                }

                if (user.Role != role)
                {
                     ErrorHandler.ShowWarning($"Role salah. User ini adalah '{user.Role}', bukan '{role}'.", "Login Gagal");
                     return null;
                }

                if (user.Status != "Aktif")
                {
                     ErrorHandler.ShowWarning("Akun ini tidak aktif/nonaktif.", "Login Gagal");
                     return null;
                }

                if (!AuthManager.VerifyPassword(password, user.Password))
                {
                    // For security, usually we don't say this, but for debugging:
                    ErrorHandler.ShowWarning("Password salah.", "Login Gagal");
                    return null;
                }

                return user;
            }
            catch (Exception ex)
            {
                ErrorHandler.LogError(ex, "Login");
                ErrorHandler.ShowError($"Terjadi kesalahan sistem saat login: {ex.Message}");
                return null;
            }
        }
        
        public bool Register(UserModel user)
        {
            try
            {
                var exists = _usersCollection.Find(u => u.Username == user.Username).FirstOrDefault();
                if (exists != null)
                {
                    return false;
                }
                
                user.Password = AuthManager.HashPassword(user.Password);
                user.CreatedAt = DateTime.Now;
                user.Status = "Aktif";
                _usersCollection.InsertOne(user);
                return true;
            }
            catch (Exception ex)
            {
                ErrorHandler.LogError(ex, "Register");
                return false;
            }
        }
        
        public List<UserModel> GetAllUsers()
        {
            try
            {
                return _usersCollection.Find(_ => true).ToList();
            }
            catch (Exception ex)
            {
                ErrorHandler.LogError(ex, "GetAllUsers");
                return new List<UserModel>();
            }
        }
        
        public bool UpdateUser(UserModel user)
        {
            try
            {
                var filter = Builders<UserModel>.Filter.Eq(u => u.Id, user.Id);
                var result = _usersCollection.ReplaceOne(filter, user);
                return result.ModifiedCount > 0;
            }
            catch (Exception ex)
            {
                ErrorHandler.LogError(ex, "UpdateUser");
                return false;
            }
        }
        
        public bool DeleteUser(ObjectId userId)
        {
            try
            {
                var filter = Builders<UserModel>.Filter.Eq(u => u.Id, userId);
                var result = _usersCollection.DeleteOne(filter);
                return result.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                ErrorHandler.LogError(ex, "DeleteUser");
                return false;
            }
        }
    }
}
