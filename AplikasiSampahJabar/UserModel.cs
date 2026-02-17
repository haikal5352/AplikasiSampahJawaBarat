using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace AplikasiSampahJabar
{
    public class UserModel
    {
        [BsonId]
        public ObjectId Id { get; set; }
        
        [BsonElement("username")]
        public string Username { get; set; }
        
        [BsonElement("password")]
        public string Password { get; set; }
        
        [BsonElement("role")]
        public string Role { get; set; }
        
        [BsonElement("nama")]
        public string Nama { get; set; }
        
        [BsonElement("email")]
        public string Email { get; set; }
        
        [BsonElement("telepon")]
        public string Telepon { get; set; }
        
        [BsonElement("status")]
        public string Status { get; set; }
        
        [BsonElement("created_at")]
        public DateTime CreatedAt { get; set; }
    }
}
