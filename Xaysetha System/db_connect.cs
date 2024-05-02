using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Xaysetha_System
{
    internal class db_connect
    {
        public MySqlConnection conn = new MySqlConnection();
        //public string path = "server=localhost; username=root; database=db_picture; port=3306; password=";
        //string pwd = "Risd8446+-";
        public string path = "server=35.220.141.128; username=bozzadyy; database=simple_db; port=3306; password=Risd8446+-";

        public void getConnect()
        {
            if (conn.State == System.Data.ConnectionState.Open)
            {
                conn.Close();
            }
            else
            {
                try
                {
                    conn.ConnectionString = path;
                    conn.Open();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                }
            }
        }
    }
}
