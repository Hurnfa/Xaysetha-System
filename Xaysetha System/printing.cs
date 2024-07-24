using Microsoft.Reporting.WebForms;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Xaysetha_System
{
    public partial class printing : Form
    {
        db_connect cn = new db_connect();
        NpgsqlCommand cmd;

        public printing(
            string paymentID,
            string tenantName,
            string tenantLName,
            float price,
            int duration,
            string username
        )
        {
            InitializeComponent();

            cmd = new NpgsqlCommand("SELECT \"userName\", \"userLName\" FROM tb_user WHERE \"userID\"=@userID", cn.conn); // Assuming 'connection' is your NpgsqlConnection
            cmd.Parameters.AddWithValue("@userID", username);

            NpgsqlDataReader reader = cmd.ExecuteReader();

            // Create a list to hold ReportParameter objects
            List<ReportParameter> reportParameters = new List<ReportParameter>();

            while (reader.Read())
            {
                reportParameters.Add(new ReportParameter("userName", reader["userName"].ToString()));
                reportParameters.Add(new ReportParameter("userLName", reader["userLName"].ToString()));
            }

            reader.Close();

            reportParameters.Add(new ReportParameter("paymentID", paymentID));
            reportParameters.Add(new ReportParameter("tenantName", tenantName));
            reportParameters.Add(new ReportParameter("tenantLName", tenantLName));
            reportParameters.Add(new ReportParameter("price", price.ToString()));
            reportParameters.Add(new ReportParameter("duration", duration.ToString()));

            reportViewer1.LocalReport.SetParameters((IEnumerable<Microsoft.Reporting.WinForms.ReportParameter>)reportParameters);
            reportViewer1.RefreshReport();
        }

        private void printing_Load(object sender, EventArgs e)
        {
            this.reportViewer1.RefreshReport();
        }
    }
}
