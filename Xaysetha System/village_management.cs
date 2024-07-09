﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace Xaysetha_System
{
    public partial class village_management : Form
    {
        NpgsqlCommand cmd;
        NpgsqlDataAdapter adapter;
        NpgsqlConnection conn;
        db_connect cn = new db_connect();
        DataTable datatable = new DataTable();

        void loadData(string sql)
        {
            data.Rows.Clear();
            data.AutoGenerateColumns = false;
            adapter = new NpgsqlDataAdapter(sql, cn.conn);
            adapter.Fill(datatable);
            data.DataSource = datatable;
            data.Columns["villageID"].DataPropertyName = "villageID";
            data.Columns["VillageName"].DataPropertyName = "villageName";
        }


        public village_management()
        {
            InitializeComponent();
            CustomizedGridView();
            cn.getConnect();
            loadData("SELECT * FROM tb_village;");
        }
        public void CustomizedGridView()
        {
            data.ColumnHeadersDefaultCellStyle.Font = new Font("Noto Sans Lao", 10, FontStyle.Regular);
            data.ColumnHeadersHeight = 30;

        }

        private void data_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex == -1 && e.ColumnIndex >= 0)
            {
                e.PaintBackground(e.CellBounds, true);

                using (Brush brush = new SolidBrush(Color.FromArgb(144, 189, 214)))
                {
                    e.Graphics.FillRectangle(brush, e.CellBounds);
                }

                using (Pen pen = new Pen(data.GridColor))
                {
                    Rectangle rect = e.CellBounds;
                    rect.Width -= 1;
                    rect.Height -= 1;
                    e.Graphics.DrawRectangle(pen, rect);
                }

                e.PaintContent(e.CellBounds);

                e.Handled = true;
            }
        }

        private void btnAddVillage_Click(object sender, EventArgs e)
        {
            village_management_add village = new village_management_add();
            village.Show();
        }
    }
}
