using System;
using System.IO;
using System.Windows.Forms;

namespace AplikasiSampahJabar
{
    public static class ErrorHandler
    {
        private const string LOG_FILE = "error_log.txt";
        
        public static void LogError(Exception ex, string context = "")
        {
            try
            {
                string logEntry = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] ERROR in {context}: {ex.Message}\n" +
                                 $"Stack Trace: {ex.StackTrace}\n" +
                                 new string('-', 80) + "\n";
                
                File.AppendAllText(LOG_FILE, logEntry);
            }
            catch
            {
                // Jika logging gagal, abaikan untuk mencegah infinite loop
            }
        }
        
        public static void ShowError(string message, string title = "Error")
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        
        public static void ShowWarning(string message, string title = "Warning")
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        
        public static void ShowInfo(string message, string title = "Information")
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        
        public static bool ShowConfirmation(string message, string title = "Confirmation")
        {
            return MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
        }
    }
}