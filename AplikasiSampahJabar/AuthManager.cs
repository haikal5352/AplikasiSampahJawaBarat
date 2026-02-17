using System;
using System.Security.Cryptography;
using System.Text;

namespace AplikasiSampahJabar
{
    public static class AuthManager
    {
        private static UserModel _currentUser;
        
        public static UserModel CurrentUser
        {
            get { return _currentUser; }
            set { _currentUser = value; }
        }
        
        public static bool IsLoggedIn => _currentUser != null;
        
        public static string CurrentRole => _currentUser?.Role ?? "";
        
        public static bool IsAdmin => CurrentRole == "Admin";
        public static bool IsPetugas => CurrentRole == "Petugas";
        public static bool IsPengguna => CurrentRole == "Pengguna";
        
        public static void Logout()
        {
            _currentUser = null;
        }
        
        public static string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }
        
        public static bool VerifyPassword(string password, string hash)
        {
            string hashOfInput = HashPassword(password);
            return StringComparer.OrdinalIgnoreCase.Compare(hashOfInput, hash) == 0;
        }
    }
}
