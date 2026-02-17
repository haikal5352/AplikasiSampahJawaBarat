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
    public partial class Form2 : Form
    {
        private DatabaseHelper dbHelper;
        
        public Form2()
        {
            InitializeComponent();
            InitializeDatabase();
            txtloginpass.PasswordChar = '●';
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
        
        private void btnloginuser_Click(object sender, EventArgs e)
        {
            LoginUser(Constants.ROLE_PENGGUNA);
        }
        
        private void btnloginpetugas_Click(object sender, EventArgs e)
        {
            LoginUser(Constants.ROLE_PETUGAS);
        }
        
        private void LoginUser(string role)
        {
            if (string.IsNullOrWhiteSpace(txtloginname.Text))
            {
                ErrorHandler.ShowWarning(Constants.MSG_USERNAME_EMPTY, Constants.TITLE_WARNING);
                return;
            }
            
            if (string.IsNullOrWhiteSpace(txtloginpass.Text))
            {
                ErrorHandler.ShowWarning(Constants.MSG_PASSWORD_EMPTY, Constants.TITLE_WARNING);
                return;
            }
            
            var user = dbHelper.Login(txtloginname.Text.Trim(), txtloginpass.Text, role);
            
            if (user != null)
            {
                AuthManager.CurrentUser = user;
                ErrorHandler.ShowInfo(Constants.MSG_LOGIN_SUCCESS, Constants.TITLE_SUCCESS);
                
                if (role == Constants.ROLE_PENGGUNA)
                {
                    Form7 dashboardUser = new Form7();
                    dashboardUser.Show();
                }
                else if (role == Constants.ROLE_PETUGAS)
                {
                    Form6 dashboardPetugas = new Form6();
                    dashboardPetugas.Show();
                }
                
                this.Hide();
            }
            else
            {
                ErrorHandler.ShowWarning(Constants.MSG_LOGIN_FAILED, Constants.TITLE_WARNING);
            }
        }
        
        private void btnregis_Click_1(object sender, EventArgs e)
        {
            Form3 registerForm = new Form3();
            registerForm.Show();
            this.Hide();
        }
        
        private void btnkehalamanloginadmin_Click(object sender, EventArgs e)
        {
            Form4 loginAdmin = new Form4();
            loginAdmin.Show();
            this.Hide();
        }
        
        private void Form2_Load(object sender, EventArgs e) { }
        private void txtloginname_TextChanged(object sender, EventArgs e) { }
        private void txtloginpass_TextChanged(object sender, EventArgs e) { }
    }
}
