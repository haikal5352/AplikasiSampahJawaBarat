using System;
using System.Text.RegularExpressions;

namespace AplikasiSampahJabar
{
    public static class InputValidator
    {
        public static ValidationResult ValidateKabupaten(string kabupaten)
        {
            if (string.IsNullOrWhiteSpace(kabupaten))
                return new ValidationResult(false, "Nama Kabupaten/Kota tidak boleh kosong");
            
            if (kabupaten.Length < 3)
                return new ValidationResult(false, "Nama Kabupaten/Kota minimal 3 karakter");
            
            if (kabupaten.Length > 50)
                return new ValidationResult(false, "Nama Kabupaten/Kota maksimal 50 karakter");
            
            if (!Regex.IsMatch(kabupaten, @"^[a-zA-Z\s]+$"))
                return new ValidationResult(false, "Nama Kabupaten/Kota hanya boleh berisi huruf dan spasi");
            
            return new ValidationResult(true, "");
        }
        
        public static ValidationResult ValidateJenisSampah(string jenis)
        {
            if (string.IsNullOrWhiteSpace(jenis))
                return new ValidationResult(false, "Jenis Sampah tidak boleh kosong");
            
            if (jenis.Length < 3)
                return new ValidationResult(false, "Jenis Sampah minimal 3 karakter");
            
            if (jenis.Length > 30)
                return new ValidationResult(false, "Jenis Sampah maksimal 30 karakter");
            
            return new ValidationResult(true, "");
        }
        
        public static ValidationResult ValidateVolume(string volume)
        {
            if (string.IsNullOrWhiteSpace(volume))
                return new ValidationResult(false, "Volume tidak boleh kosong");
            
            if (!decimal.TryParse(volume, out decimal volumeValue))
                return new ValidationResult(false, "Volume harus berupa angka");
            
            if (volumeValue <= 0)
                return new ValidationResult(false, "Volume harus lebih besar dari 0");
            
            if (volumeValue > 999999)
                return new ValidationResult(false, "Volume terlalu besar (maksimal 999,999 ton)");
            
            return new ValidationResult(true, "");
        }
        
        public static ValidationResult ValidateAllInputs(string kabupaten, string jenis, string volume)
        {
            var kabupatenResult = ValidateKabupaten(kabupaten);
            if (!kabupatenResult.IsValid) return kabupatenResult;
            
            var jenisResult = ValidateJenisSampah(jenis);
            if (!jenisResult.IsValid) return jenisResult;
            
            var volumeResult = ValidateVolume(volume);
            if (!volumeResult.IsValid) return volumeResult;
            
            return new ValidationResult(true, "");
        }
    }
    
    public class ValidationResult
    {
        public bool IsValid { get; }
        public string ErrorMessage { get; }
        
        public ValidationResult(bool isValid, string errorMessage)
        {
            IsValid = isValid;
            ErrorMessage = errorMessage;
        }
    }
}