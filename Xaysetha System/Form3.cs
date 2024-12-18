﻿using Microsoft.Reporting.WinForms;
using Npgsql;
using System;
using System.Data;
using System.Windows.Forms;

namespace Xaysetha_System
{
    public partial class Form3 : Form
    {
        db_connect cn = new db_connect();
        NpgsqlCommand cmd;

        public Form3()
        {
            InitializeComponent();
            cn.getConnect();
            loadDataCitizen();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            this.reportViewer2.RefreshReport();
        }

        DateTime dateTime = DateTime.Now;

        public void loadDataCitizen()
        {
            ReportParameterCollection rp = new ReportParameterCollection();
            rp.Add(new ReportParameter("issueDate", dateTime.ToString("dd/MM/yyyy")));
            reportViewer2.LocalReport.SetParameters(rp);

            //cmd = new NpgsqlCommand("SELECT \"citizenID\", name, surname, gender, dob, religion, family_book, \"phoneNums\", addr FROM \"public\".tb_citizen", cn.conn);
            cmd = new NpgsqlCommand(@"SELECT citizen_id, citizen_name, citizen_lastname, citizen_gender, citizen_dob, citizen_religion, fambook_nums, tel, addrs FROM public.tb_citizen", cn.conn);

            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd);
            DataTable dt2 = new DataTable();
            adapter.Fill(dt2);
            reportViewer2.LocalReport.DataSources.Clear();
            ReportDataSource reportDataSource2 = new ReportDataSource("DataSet1", dt2);
            reportViewer2.LocalReport.DataSources.Add(reportDataSource2);
            reportViewer2.RefreshReport();
        }
    }
}
