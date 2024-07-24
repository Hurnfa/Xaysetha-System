using Microsoft.Reporting.WebForms;
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

            ReportParameterCollection rp = new ReportParameterCollection();

            cmd = new NpgsqlCommand("SELECT \"userName\", \"userLName\" FROM tb_user WHERE \"userID\"=@userID");
            cmd.Parameters.AddWithValue("@userID", username);

            NpgsqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                rp.Add(new ReportParameter("userName", reader["userName"].ToString()));
                rp.Add(new ReportParameter("userLName", reader["userLName"].ToString()));
            }

            reader.Close();

            ReportParameter payment_id = new ReportParameter("paymentID", paymentID),
                tenant_name = new ReportParameter("tenantName", tenantName),
                tenant_lname = new ReportParameter("tenantName", tenantName);

            //reportViewer1.LocalReport.SetParameters(payment_id);

            /*            rp.Add(new ReportParameter("paymentID", paymentID));
                        rp.Add(new ReportParameter("tenantName", tenantName));
                        rp.Add(new ReportParameter("tenantLName", tenantLName));
                        rp.Add(new ReportParameter("price", price.ToString()));
                        rp.Add(new ReportParameter("duration", duration.ToString()));
            */
            //reportViewer1.LocalReport.SetParameters(rp);

            reportViewer1.RefreshReport();
        }

        private void printing_Load(object sender, EventArgs e)
        {
            this.reportViewer1.RefreshReport();
        }
    }
}
