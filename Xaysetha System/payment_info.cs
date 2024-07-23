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
    public partial class payment_info : Form
    {
        db_connect cn = new db_connect();
        NpgsqlCommand cmd;
        NpgsqlDataAdapter adapter;
        DataTable dataTable = new DataTable();

        public payment_info()
        {
            InitializeComponent();
            cn.getConnect();
            CustomizedGridView();
            AdjustColumnWidths(data, 10);
        }

        public void loadData(string sql)
        {
            data.AutoGenerateColumns = false;

            adapter = new NpgsqlDataAdapter(sql, cn.conn);
            adapter.Fill(dataTable);
            data.DataSource = dataTable;

            data.Columns[0].DataPropertyName = "payment_id";
            data.Columns[1].DataPropertyName = "tenant_id";
            data.Columns[3].DataPropertyName = "duration";
            data.Columns[4].DataPropertyName = "price";
            data.Columns[5].DataPropertyName = "user_id";

            //cmd = new NpgsqlCommand("SELECT firstname, lastname FROM tb_tenant WHERE ");

        }

        public void CustomizedGridView()
        {
            data.ColumnHeadersDefaultCellStyle.Font = new Font("Noto Sans Lao", 10, FontStyle.Regular);
            data.ColumnHeadersHeight = 30;

        }

        private void AdjustColumnWidths(DataGridView dataGridView, int padding)
        {
            foreach (DataGridViewColumn column in dataGridView.Columns)
            {
                // Auto resize column based on the content
                dataGridView.AutoResizeColumn(column.Index, DataGridViewAutoSizeColumnMode.AllCells);

                // Add padding to the width
                column.Width += padding;
            }
        }
    }
}
