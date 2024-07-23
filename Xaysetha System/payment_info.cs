using Npgsql;
using System.Data;
using System.Drawing;
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
    }
}
