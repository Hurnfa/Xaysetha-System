using Npgsql;
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
        NpgsqlCommand cmd;
        NpgsqlDataAdapter adapter;
        db_connect cn = new db_connect();
        DataTable datatable = new DataTable();

        public Database_Config()
        {
            InitializeComponent();
            cn.getConnect();
        }

        public void storeDatabaseInfo(string username, string password)
        {

        }

        private void txtUserName_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
/*            string name = txtUserName.Text;
            string password = txtPassword.Text;
            database.userName = name;
            database.password = password;
            database.getConnect();
            this.Hide();
            Login loginPage = new Login();*/
            getLogIn();
        }

        void getLogIn()
        {
            if (txtUserName.Text != "" && txtPassword.Text != "")
            {
                cmd = new NpgsqlCommand("SELECT * FROM tb_user WHERE \"userID\"=@userID AND \"userPassword\"=@userPassword", cn.conn);

                try
                {
                    cmd.Parameters.AddWithValue("@userID", txtUserName.Text);
                    cmd.Parameters.AddWithValue("@userPassword", txtPassword.Text);

                    //cmd.ExecuteNonQuery();

                    adapter = new NpgsqlDataAdapter(cmd);

                    adapter.Fill(datatable);

                    if (datatable.Rows.Count > 0)
                    {
                        //MessageBox.Show("ສະບາຍດີ , " + txtUserID.Text, "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        NpgsqlDataReader reader = cmd.ExecuteReader();

                        Dashboard dashboard = new Dashboard();

                        while (reader.Read())
                        {
                            //MessageBox.Show("ສະບາຍດີທ່ານ " + reader["userName"].ToString() +" "+ reader["userLName"].ToString(), "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            dashboard.showUserName(reader["userName"].ToString(), reader["userLName"].ToString(), reader["role"].ToString(), txtUserName.Text);
                        }

                        /*SqlDataReader rd = cmd.ExecuteReader();

                        Form1 f1 = new Form1();

                        while (rd.Read())
                        {
                            //f1.displayName(rd["acc_name"].ToString() + " " + rd["surname"].ToString(), rd["pos"].ToString());
                        }*/

                        dashboard.Show();
                        Hide();
                    }
                    else
                    {
                        //clearText();
                        MessageBox.Show("No user found!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ຂໍອະໄພ, ລະບົບຂັດຂ້ອງ\n" + ex.Message, "ແຈ້ງເຕືອນ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please insert your username and password first!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
  
