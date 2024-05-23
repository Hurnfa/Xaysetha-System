using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Xaysetha_System
{
    public class db_connect
    {
        public MySqlConnection conn = new MySqlConnection();
        //public string path = "server=localhost; username=root; database=db_picture; port=3306; password=";
        //string pwd = "Risd8446+-";

        public string userName { get; set; }
        public string password { get; set; }
        private string path;


        public string GetConnectionString()
        {
            string pathString = "server=35.220.141.128; username=" + userName + "; database=simple_db; port=3306; password=" + password +"";

            return path = pathString;
        }

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
