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
    public partial class jobs_management : Form
    {
        NpgsqlCommand cmd;
        NpgsqlDataAdapter adapter;
        db_connect cn = new db_connect();
        DataTable dt = new DataTable();

        public jobs_management()
        {
            InitializeComponent();
            cn.getConnect();
            loadData("SELECT * FROM tb_jobs;");
        }

        void totalCount()
        {
            cmd = new NpgsqlCommand("SELECT COUNT(*) FROM tb_jobs;", cn.conn);
            labelTotal.Text = $"ທັງໝົດ {cmd.ExecuteScalar()} ລາຍການ";
        }

        void loadData(string sql)
        {
            totalCount();
            data.AutoGenerateColumns = false;

            adapter = new NpgsqlDataAdapter(sql, cn.conn);
            adapter.Fill(dt);

            data.DataSource = dt;
            data.Columns[0].DataPropertyName = "jobs_id";
            data.Columns[1].DataPropertyName = "jobs_name";
        }
    }
}
