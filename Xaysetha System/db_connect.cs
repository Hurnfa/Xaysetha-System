using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace Xaysetha_System
{
    public class db_connect
    {
        //public MySqlConnection conn = new MySqlConnection();
        //public string path = "server=localhost; username=root; database=db_picture; port=3306; password=";
        //public string path = "User Id=postgres.vikiezgjlynnublheair;Password=Huawei@123+2023;Server=aws-0-ap-southeast-1.pooler.supabase.com;Port=6543;Database=postgres;";
        //string pwd = "Risd8446+-";

        public string userName { get; set; }
        public string password { get; set; }
        //private string path;


        /*        public string GetConnectionString()
                {
                    string pathString = "server=35.220.141.128; username=" + userName + "; database=simple_db; port=3306; password=" + password +"";

                    return path = pathString;
                }*/

        public NpgsqlConnection conn = new NpgsqlConnection();
        public string path = "Server=127.0.0.1;Port=5432;Database=xaysetha_system;User Id=postgres;Password=11052002;";

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
                    //MessageBox.Show("Connected!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                }
            }
        }
    }
}
