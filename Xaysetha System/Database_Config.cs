using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Xaysetha_System
{
    public partial class Database_Config : Form
    {

        public db_connect database = new db_connect();
        public Database_Config()
        {
            InitializeComponent();
        }

        public void storeDatabaseInfo(string username, string password)
        {

        }

        private void txtUserName_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            string name = txtUserName.Text;
            string password = txtPassword.Text;
            database.userName = name;
            database.password = password;
            database.getConnect();
            this.Hide();
            Login loginPage = new Login();
        }
    }
}
  
