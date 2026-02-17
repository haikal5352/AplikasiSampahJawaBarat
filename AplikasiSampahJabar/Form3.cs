using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MongoDB.Driver;

namespace AplikasiSampahJabar
{
    public partial class Form3 : Form
    {
        private DatabaseHelper dbHelper;
        
        public Form3()
        {
            InitializeComponent();
            InitializeDatabase();
            txtregisterpass.PasswordChar = '●';
        }
        
        private void InitializeDatabase()
        {
            try
            {
                string connectionString = System.IO.File.Exists(Constants.CONFIG_FILE) 
                    ? System.IO.File.ReadAllText(Constants.CONFIG_FILE).Trim() 
                    : Constants.DEFAULT_CONNECTION_STRING;
                
                var client = new MongoClient(connectionString);
                var database = client.GetDatabase(Constants.DATABASE_NAME);
                dbHelper = new DatabaseHelper(database);
            }
            catch (Exception ex)
            {
                ErrorHandler.LogError(ex, "InitializeDatabase");
                ErrorHandler.ShowError("Gagal terhubung ke database!");
            }
        }
        
        private void btnregistrasiuser_Click(object sender, EventArgs e)
        {
            RegisterUser(Constants.ROLE_PENGGUNA);
        }
        
        private void btnregistrasipetugas_Click(object sender, EventArgs e)
        {
            RegisterUser(Constants.ROLE_PETUGAS);
        }
        
        private void RegisterUser(string role)
        {
            if (string.IsNullOrWhiteSpace(txtregistername.Text))
            {
                ErrorHandler.ShowWarning(Constants.MSG_USERNAME_EMPTY, Constants.TITLE_WARNING);
                return;
            }
            
            if (string.IsNullOrWhiteSpace(txtregisterpass.Text))
            {
                ErrorHandler.ShowWarning(Constants.MSG_PASSWORD_EMPTY, Constants.TITLE_WARNING);
                return;
            }
            
            if (txtregisterpass.Text.Length < 6)
            {
                ErrorHandler.ShowWarning(Constants.MSG_PASSWORD_MIN, Constants.TITLE_WARNING);
                return;
            }
            
            var newUser = new UserModel
            {
                Username = txtregistername.Text.Trim(),
                Password = txtregisterpass.Text,
                Role = role,
                Nama = txtregistername.Text.Trim(),
                Email = "",
                Telepon = "",
                Status = Constants.STATUS_AKTIF
            };
            
            if (dbHelper.Register(newUser))
            {
                ErrorHandler.ShowInfo(Constants.MSG_REGISTER_SUCCESS, Constants.TITLE_SUCCESS);
                Form2 loginForm = new Form2();
                loginForm.Show();
                this.Close();
            }
            else
            {
                ErrorHandler.ShowWarning(Constants.MSG_REGISTER_FAILED, Constants.TITLE_WARNING);
            }
        }
        
        private void btnkembalihalamanlogin_Click(object sender, EventArgs e)
        {
            Form2 loginForm = new Form2();
            loginForm.Show();
            this.Close();
        }
        
        private void btnkehalamanloginadmin_Click(object sender, EventArgs e)
        {
            Form4 loginAdmin = new Form4();
            loginAdmin.Show();
            this.Close();
        }
        
        private void txtregistername_TextChanged(object sender, EventArgs e) { }
        private void txtregisterpass_TextChanged(object sender, EventArgs e) { }
    }
}
