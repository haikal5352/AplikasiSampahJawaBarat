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
    public partial class Form4 : Form
    {
        private DatabaseHelper dbHelper;
        
        public Form4()
        {
            InitializeComponent();
            InitializeDatabase();
            txtloginadminpass.PasswordChar = '●';
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
        
        private void btnlogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtloginadminname.Text))
            {
                ErrorHandler.ShowWarning(Constants.MSG_USERNAME_EMPTY, Constants.TITLE_WARNING);
                return;
            }
            
            if (string.IsNullOrWhiteSpace(txtloginadminpass.Text))
            {
                ErrorHandler.ShowWarning(Constants.MSG_PASSWORD_EMPTY, Constants.TITLE_WARNING);
                return;
            }
            
            var admin = dbHelper.Login(txtloginadminname.Text.Trim(), txtloginadminpass.Text, Constants.ROLE_ADMIN);
            
            if (admin != null)
            {
                AuthManager.CurrentUser = admin;
                ErrorHandler.ShowInfo(Constants.MSG_LOGIN_SUCCESS, Constants.TITLE_SUCCESS);
                
                Form5 dashboardAdmin = new Form5();
                dashboardAdmin.Show();
                this.Hide();
            }
            else
            {
                // Error handled in DatabaseHelper.Login with specific message
                // ErrorHandler.ShowWarning(Constants.MSG_LOGIN_FAILED, Constants.TITLE_WARNING);
            }
        }
        
        private void btnkembalikehalamanloginuserpetugas_Click(object sender, EventArgs e)
        {
            Form2 loginForm = new Form2();
            loginForm.Show();
            this.Close();
        }
        
        private void txtloginadminname_TextChanged(object sender, EventArgs e) { }
        private void txtloginadminpass_TextChanged(object sender, EventArgs e) { }
    }
}
