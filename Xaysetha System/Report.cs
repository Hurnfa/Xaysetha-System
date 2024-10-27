using Microsoft.Reporting.WinForms;
using Npgsql;
using System;
using System.Data;
using System.Numerics;
using System.Runtime.InteropServices.ComTypes;
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

        /*        public void loadDataToReport()
                {
                    cmd = new NpgsqlCommand("SELECT \"public\".\"tb_residentialBook\".\"resBookID\", \"public\".tb_tenant.firstname, \"public\".tb_tenant.lastname, \"public\".tb_tenant.gender, \"public\".\"tb_residentialBook\".\"issueDate\", \"public\".\"tb_residentialBook\".\"expDate\", \"public\".tb_place.\"placeName\", \"public\".tb_village.\"villageName\" FROM \"public\".\"tb_residentialBook\", \"public\".tb_tenant, \"public\".tb_place, \"public\".tb_village WHERE  \"public\".\"tb_residentialBook\".\"tenantID\" = \"public\".tb_tenant.\"tenantID\" AND \"public\".\"tb_residentialBook\".\"placeID\" = \"public\".tb_place.\"placeID\" AND \"public\".tb_place.\"villageID\" = \"public\".tb_village.\"villageID\"", cn.conn);
                    NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    reportViewer1.LocalReport.DataSources.Clear();
                    ReportDataSource reportDataSource = new ReportDataSource("DataSet1", dt);
                    reportViewer1.LocalReport.DataSources.Add(reportDataSource);
                    reportViewer1.RefreshReport();
                }*/
        public void loadDataToReport(string village, DateTime start, DateTime stop)
        {
            cmd = new NpgsqlCommand("SELECT village_name FROM tb_village WHERE village_id=@villageID", cn.conn);

            cmd.Parameters.AddWithValue("@villageID", BigInteger.Parse(village));

            string villageName = cmd.ExecuteScalar().ToString();

/*            cmd = new NpgsqlCommand(@"SELECT public.tb_book.book_id,
                                    ""public"".tb_tenant.tenant_name,
                                    ""public"".tb_tenant.tenant_lastname, 
                                    ""public"".tb_tenant.tenant_gender,
                                    ""public"".tb_book.issue_date,
                                    ""public"".tb_book.exp_date,
                                    ""public"".tb_place.place_name,
                                    ""public"".tb_village.village_name
                                    FROM
                                        ""public"".tb_book,
                                        ""public"".tb_tenant, 
                                        ""public"".tb_place,
                                        ""public"".tb_village 
                                    WHERE  
                                        ""public"".tb_book.tenant_id = ""public"".tb_tenant.tenant_id
                                    AND 
                                        ""public"".tb_book.place_id = ""public"".tb_place.place_id
                                    AND 
                                        ""public"".tb_place.village_id = @villageID
                                    and
                                        public.tb_book.issue_date
                                    BETWEEN 
                                        @startDate AND @stopDate", cn.conn);*/

            cmd = new NpgsqlCommand(@"SELECT ""public"".tb_book.book_id, ""public"".tb_tenant.tenant_name, ""public"".tb_tenant.tenant_lastname, ""public"".tb_tenant.tenant_gender, ""public"".tb_place.place_name, ""public"".tb_village.village_name, ""public"".tb_book.issue_date, ""public"".tb_book.exp_date
FROM   ""public"".tb_book, ""public"".tb_place, ""public"".tb_tenant, ""public"".tb_village
WHERE ""public"".tb_book.place_id = ""public"".tb_place.place_id AND ""public"".tb_book.tenant_id = ""public"".tb_tenant.tenant_id AND ""public"".tb_place.village_id = ""public"".tb_village.village_id", cn.conn);

            cmd.Parameters.AddWithValue("@villageID", BigInteger.Parse(village));
            cmd.Parameters.AddWithValue("@startDate", start);
            cmd.Parameters.AddWithValue("@stopDate", stop);

            ReportParameterCollection rp = new ReportParameterCollection();

            rp.Add(new ReportParameter("startDate", start.ToString("dd/MM/yyyy")));
            rp.Add(new ReportParameter("endDate", stop.ToString("dd/MM/yyyy")));
            rp.Add(new ReportParameter("village", villageName));

            rp.Add(new ReportParameter("issueDate", DateTime.Now.ToString("dd/MM/yyyy")));

            reportViewer1.LocalReport.SetParameters(rp);

            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            reportViewer1.LocalReport.DataSources.Clear();
            ReportDataSource reportDataSource = new ReportDataSource("DataSet1", dt);
            reportViewer1.LocalReport.DataSources.Add(reportDataSource);
            reportViewer1.RefreshReport();
        }

        private void Report_Load(object sender, EventArgs e)
        {

            this.reportViewer1.RefreshReport();
        }
    }
}
