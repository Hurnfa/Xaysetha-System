﻿using Npgsql;
using System.Data;
using System.Drawing;
using System.Numerics;
using System.Windows.Forms;

namespace Xaysetha_System
{
    public partial class export_book_info : Form
    {
        NpgsqlCommand cmd;
        NpgsqlDataAdapter adapter;
        db_connect cn = new db_connect();
        DataTable datatable = new DataTable();

        public export_book_info()
        {
            InitializeComponent();
            cn.getConnect();
            CustomizedGridView();
        }

        public void loadData(string sql)
        {
            data.AutoGenerateColumns = false;

            adapter = new NpgsqlDataAdapter(sql, cn.conn);
            adapter.Fill(datatable);
            data.DataSource = datatable;

            data.Columns[0].DataPropertyName = "resBookID";
            data.Columns[1].DataPropertyName = "firstname";
            data.Columns[2].DataPropertyName = "lastname";
            data.Columns[3].DataPropertyName = "placeName";
            data.Columns[4].DataPropertyName = "issueDate";
            data.Columns[5].DataPropertyName = "expDate";
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

        private void data_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string columnName = data.Columns[e.ColumnIndex].Name;
            DataGridViewRow row = data.Rows[e.RowIndex];
            BigInteger bookID = BigInteger.Parse(row.Cells[0].Value?.ToString());
            string name = row.Cells[1].Value?.ToString(),
                placeName = row.Cells[3].Value?.ToString();

            book_printing printing = new book_printing();

            switch (columnName)
            {
                case "btnPrint":

                    printing.loadDataToReport(0, name, placeName);
                    printing.Show();

                break;
            }
        }
    }
}
