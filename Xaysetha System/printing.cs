using Microsoft.Reporting.WinForms;
using Npgsql;
//using Org.BouncyCastle.Math;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Windows.Forms;

namespace Xaysetha_System
{
    public partial class printing : Form
    {
        db_connect cn = new db_connect();
        NpgsqlCommand cmd;

        public printing()
        {
            InitializeComponent();
            cn.getConnect();
        }

        public void loadDataToReport(
            BigInteger paymentID,
            string tenantName,
            string tenantLName,
            float price,
            int duration,
            string username)
        {
            cmd = new NpgsqlCommand("SELECT \"userName\", \"userLName\" FROM tb_user WHERE \"userID\"=@userID", cn.conn); // Assuming 'connection' is your NpgsqlConnection
            cmd.Parameters.AddWithValue("@userID", username);

            NpgsqlDataReader reader = cmd.ExecuteReader();
            // Create a list to hold ReportParameter objects
           ReportParameterCollection rp = new ReportParameterCollection();

            while (reader.Read())
            {
                rp.Add(new ReportParameter("userName", reader["userName"].ToString()));
                rp.Add(new ReportParameter("userLName", reader["userLName"].ToString()));
            }

            reader.Close();

            rp.Add(new ReportParameter("paymentID", paymentID.ToString()));
            rp.Add(new ReportParameter("tenantName", tenantName));
            rp.Add(new ReportParameter("tenantLName", tenantLName));
            rp.Add(new ReportParameter("price", price.ToString()));
            rp.Add(new ReportParameter("paymentDate", DateTime.Now.ToString()));
            rp.Add(new ReportParameter("duration", duration.ToString()));

            reportViewer1.LocalReport.SetParameters(rp);
            reportViewer1.RefreshReport();
        }

        private void printing_Load(object sender, EventArgs e)
        {
            this.reportViewer1.RefreshReport();
        }
    }
}
