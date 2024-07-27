using Microsoft.Reporting.WinForms;
using Npgsql;
using System;
using System.Data;
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
            ///*            cmd = new NpgsqlCommand("SELECT rb.\"resBookID\", t.\"firstname\", t.\"lastname\", t.\"gender\", t.\"dob\", " +
            //                "t.\"nationality\" rb.\"issueDate\", rb.\"expDate\", p.\"placeName\", v.\"villageName\" " +
            //                "FROM \"tb_residentialBook\" rb JOIN \"tb_place\" p ON rb.\"placeID\" = p.\"placeID\" " +
            //                "JOIN \"tb_village\" v ON p.\"villageID\" = v.\"villageID\" " +
            //                "JOIN \"tb_tenant\" t ON rb.\"tenantID\" = t.\"tenantID\" WHERE v.\"villageID\" = 10352 and rb.\"issueDate\" BETWEEN '2024-05-27' AND '2024-07-27'; ", cn.conn);*/

            //            cmd = new NpgsqlCommand("SELECT rb.\"resBookID\",\r\n       t.\"firstname\",\r\n       t.\"lastname\",\r\n       t.\"gender\",\r\n       t.\"dob\",\r\n       t.\"nationality\",\r\n       rb.\"issueDate\",\r\n       rb.\"expDate\",\r\n       p.\"placeName\",\r\n       v.\"villageName\"\r\nFROM \"tb_residentialBook\" rb\r\nJOIN \"tb_place\" p ON rb.\"placeID\" = p.\"placeID\"\r\nJOIN \"tb_village\" v ON p.\"villageID\" = v.\"villageID\"\r\nJOIN \"tb_tenant\" t ON rb.\"tenantID\" = t.\"tenantID\"\r\nWHERE v.\"villageID\" = 10352 and rb.\"issueDate\" BETWEEN '2024-05-27' AND '2024-07-27';", cn.conn);

            //            NpgsqlDataReader reader = cmd.ExecuteReader();

            //            while (reader.Read())
            //            {
            //                rp.Add(new ReportParameter("tenantBookID", reader["resBookID"].ToString()));
            //                rp.Add(new ReportParameter("tenantName", reader["firstname"].ToString()));
            //                rp.Add(new ReportParameter("tenantLName", reader["lastname"].ToString()));
            //                rp.Add(new ReportParameter("tenantGender", reader["gender"].ToString()));

            //                DateTime birthDay = DateTime.Parse(reader["dob"].ToString());

            //                rp.Add(new ReportParameter("tenantBirthday", birthDay.ToString("dd/MM/yyyy")));

            //                rp.Add(new ReportParameter("placeName", reader["placeName"].ToString()));
            //                rp.Add(new ReportParameter("village", reader["villageName"].ToString()));
            //            }

            //            reader.Close();

            //            reportViewer1.LocalReport.SetParameters(rp);
            //            reportViewer1.RefreshReport();
            // Create and configure the DataTable
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("resBookID", typeof(string));
            dataTable.Columns.Add("firstname", typeof(string));
            dataTable.Columns.Add("lastname", typeof(string));
            dataTable.Columns.Add("gender", typeof(string));
            dataTable.Columns.Add("dob", typeof(DateTime));
            dataTable.Columns.Add("nationality", typeof(string));
            dataTable.Columns.Add("issueDate", typeof(DateTime));
            dataTable.Columns.Add("expDate", typeof(DateTime));
            dataTable.Columns.Add("placeName", typeof(string));
            dataTable.Columns.Add("villageName", typeof(string));

            // Execute the query and fill the DataTable
            using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT rb.\"resBookID\",\r\n       t.\"firstname\",\r\n       t.\"lastname\",\r\n       t.\"gender\",\r\n       t.\"dob\",\r\n       t.\"nationality\",\r\n       rb.\"issueDate\",\r\n       rb.\"expDate\",\r\n       p.\"placeName\",\r\n       v.\"villageName\"\r\nFROM \"tb_residentialBook\" rb\r\nJOIN \"tb_place\" p ON rb.\"placeID\" = p.\"placeID\"\r\nJOIN \"tb_village\" v ON p.\"villageID\" = v.\"villageID\"\r\nJOIN \"tb_tenant\" t ON rb.\"tenantID\" = t.\"tenantID\"\r\nWHERE v.\"villageID\" = 10352 and rb.\"issueDate\" BETWEEN '2024-05-27' AND '2024-07-27';", cn.conn))
            {
                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        DataRow row = dataTable.NewRow();
                        row["resBookID"] = reader["resBookID"].ToString();
                        row["firstname"] = reader["firstname"].ToString();
                        row["lastname"] = reader["lastname"].ToString();
                        row["gender"] = reader["gender"].ToString();
                        row["dob"] = DateTime.Parse(reader["dob"].ToString());
                        row["nationality"] = reader["nationality"].ToString();
                        row["issueDate"] = DateTime.Parse(reader["issueDate"].ToString());
                        row["expDate"] = DateTime.Parse(reader["expDate"].ToString());
                        row["placeName"] = reader["placeName"].ToString();
                        row["villageName"] = reader["villageName"].ToString();
                        dataTable.Rows.Add(row);
                    }
                }
            }

            // Set the DataTable as the data source for the ReportViewer
            reportViewer1.LocalReport.DataSources.Clear();
            ReportDataSource reportDataSource = new ReportDataSource("DataSet1", dataTable); // Ensure "DataSetName" matches the name in your RDLC report
            reportViewer1.LocalReport.DataSources.Add(reportDataSource);
            reportViewer1.RefreshReport();
        }


        private void Report_Load(object sender, EventArgs e)
        {

            this.reportViewer1.RefreshReport();
        }
    }
}
