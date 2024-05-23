using MySqlConnector;
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
    public partial class Login : Form
    {
        MySqlCommand cmd;
        MySqlDataAdapter adapter;
        DataTable dt = new DataTable();
        db_connect cn = new db_connect();

        public Login()
        {
            InitializeComponent();
            cn.getConnect();
        }

        private void btnLogIn_Click(object sender, EventArgs e)
        {
            Dashboard dashboard = new Dashboard();
            dashboard.Show();
            this.Hide();
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
