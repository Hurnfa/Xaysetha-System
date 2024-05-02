using MySql.Data.MySqlClient;
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
        MySqlDataAdapter adt;
        DataTable dt = new DataTable();
        db_connect cn = new db_connect();

        public Login()
        {
            InitializeComponent();
            cn.getConnect();
        }

        private void btnLogIn_Click(object sender, EventArgs e)
        {
            if (txtUserID.Text != "" && txtPwd.Text != "")
            {
                cmd = new MySqlCommand("SELECT * FROM tb_simple WHERE name=@name AND surname=@surname", cn.conn);

                try
                {
                    cmd.Parameters.AddWithValue("@name", txtUserID.Text);
                    cmd.Parameters.AddWithValue("@surname", txtPwd.Text);

                    //cmd.ExecuteNonQuery();

                    adt = new MySqlDataAdapter(cmd);

                    adt.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        MessageBox.Show("Welcome, " + txtUserID.Text, "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        /*SqlDataReader rd = cmd.ExecuteReader();

                        Form1 f1 = new Form1();

                        while (rd.Read())
                        {
                            //f1.displayName(rd["acc_name"].ToString() + " " + rd["surname"].ToString(), rd["pos"].ToString());
                        }*/

                        //new Form1().Show();
                        //Hide();
                    }
                    else
                    {
                        //clearText();
                        MessageBox.Show("No user found!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Please insert your username and password first!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
