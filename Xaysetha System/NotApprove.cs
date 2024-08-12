using Microsoft.Reporting.WinForms;
using Npgsql;
using System;
using System.Data;
using System.Windows.Forms;

namespace Xaysetha_System
{
    public partial class NotApprove : Form
    {
        db_connect cn = new db_connect();
        NpgsqlCommand cmd;
        public NotApprove()
        {
            InitializeComponent();
        }

        private void NotApprove_Load(object sender, EventArgs e)
        {
            cn.getConnect();
            loadData();
        }
        public void loadData()
        {
            ReportParameterCollection rp = new ReportParameterCollection();

            cmd = new NpgsqlCommand(@"SELECT ""tenantID"", firstname, tenant_status FROM ""public"".tb_tenant where tenant_status IS NULL OR tenant_status = 'ຍັງບໍ່ໄດ້ອະນຸມັດ'", cn.conn);
            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd);
            DataTable dt3 = new DataTable();
            adapter.Fill(dt3);
            reportViewer2.LocalReport.DataSources.Clear();
            ReportDataSource reportDataSource3 = new ReportDataSource("DataSet3", dt3);
            reportViewer2.LocalReport.DataSources.Add(reportDataSource3);
            reportViewer2.RefreshReport();
        }
    }
}
