using Microsoft.Reporting.WinForms;
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
    public partial class Report : Form
    {
        db_connect cn = new db_connect();
        NpgsqlCommand cmd;
        NpgsqlDataAdapter adapter;
        DataSet dataSetVillage = new DataSet();
        ReportParameterCollection rp = new ReportParameterCollection();

        public Report()
        {
            InitializeComponent();
            cn.getConnect();
        }

        public void loadDataToReport()
        {
/*            cmd = new NpgsqlCommand("SELECT rb.\"resBookID\", t.\"firstname\", t.\"lastname\", t.\"gender\", t.\"dob\", " +
                "t.\"nationality\" rb.\"issueDate\", rb.\"expDate\", p.\"placeName\", v.\"villageName\" " +
                "FROM \"tb_residentialBook\" rb JOIN \"tb_place\" p ON rb.\"placeID\" = p.\"placeID\" " +
                "JOIN \"tb_village\" v ON p.\"villageID\" = v.\"villageID\" " +
                "JOIN \"tb_tenant\" t ON rb.\"tenantID\" = t.\"tenantID\" WHERE v.\"villageID\" = 10352 and rb.\"issueDate\" BETWEEN '2024-05-27' AND '2024-07-27'; ", cn.conn);*/

            cmd = new NpgsqlCommand("SELECT rb.\"resBookID\",\r\n       t.\"firstname\",\r\n       t.\"lastname\",\r\n       t.\"gender\",\r\n       t.\"dob\",\r\n       t.\"nationality\",\r\n       rb.\"issueDate\",\r\n       rb.\"expDate\",\r\n       p.\"placeName\",\r\n       v.\"villageName\"\r\nFROM \"tb_residentialBook\" rb\r\nJOIN \"tb_place\" p ON rb.\"placeID\" = p.\"placeID\"\r\nJOIN \"tb_village\" v ON p.\"villageID\" = v.\"villageID\"\r\nJOIN \"tb_tenant\" t ON rb.\"tenantID\" = t.\"tenantID\"\r\nWHERE v.\"villageID\" = 10352 and rb.\"issueDate\" BETWEEN '2024-05-27' AND '2024-07-27';", cn.conn);

            NpgsqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                rp.Add(new ReportParameter("tenantBookID", reader["resBookID"].ToString()));
                rp.Add(new ReportParameter("tenantName", reader["firstname"].ToString()));
                rp.Add(new ReportParameter("tenantLName", reader["lastname"].ToString()));
                rp.Add(new ReportParameter("tenantGender", reader["gender"].ToString()));

                DateTime birthDay = DateTime.Parse(reader["dob"].ToString());

                rp.Add(new ReportParameter("tenantBirthday", birthDay.ToString("dd/MM/yyyy")));

                rp.Add(new ReportParameter("placeName", reader["placeName"].ToString()));
                rp.Add(new ReportParameter("village", reader["villageName"].ToString()));
            }

            reader.Close();

            reportViewer1.LocalReport.SetParameters(rp);
            reportViewer1.RefreshReport();
        }


        private void Report_Load(object sender, EventArgs e)
        {

            this.reportViewer1.RefreshReport();
        }
    }
}
