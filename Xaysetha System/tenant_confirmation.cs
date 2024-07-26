using Npgsql;
using System;
/*using Org.BouncyCastle.Math;*/
using System.Data;
using System.Drawing;
using System.Numerics;
using System.Windows.Forms;

namespace Xaysetha_System
{
    public partial class tenant_confirmation : Form
    {
        NpgsqlCommand cmd;
        NpgsqlDataAdapter adapter;
        db_connect cn = new db_connect();
        DataTable datatable = new DataTable();
        export_book_info exportBookInfo = new export_book_info();

        public tenant_confirmation()
        {
            InitializeComponent();
            cn.getConnect();
            CustomizedGridView();
            /*            loadData("SELECT tb_tenant.\"tenantID\", tb_tenant.firstname,tb_tenant.lastname, tb_tenant.occupation, tb_tenant.\"phoneNums\", tb_payment.payment_status from tb_tenant " +
                            "join tb_payment on tb_tenant.\"tenantID\" = tb_payment.tenant_id where tb_payment.payment_status = 'ຊຳລະແລ້ວ';");*/
        }

        public void loadData(string sql)
        {
            data.AutoGenerateColumns = false;

            adapter = new NpgsqlDataAdapter(sql, cn.conn);
            adapter.Fill(datatable);
            data.DataSource = datatable;

            data.Columns[0].DataPropertyName = "tenantID";
            data.Columns[1].DataPropertyName = "firstname";
            data.Columns[2].DataPropertyName = "lastname";
            data.Columns[3].DataPropertyName = "duration";
            data.Columns[4].DataPropertyName = "payment_status";
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

            BigInteger paymentID = BigInteger.Parse(row.Cells[0].Value?.ToString());

            string name = row.Cells[1].Value?.ToString(),
                surname = row.Cells[2].Value?.ToString();

            int duration = int.Parse(row.Cells[3].Value?.ToString());

            switch (columnName)
            {
                case "printButton":

                    PrintDialog print = new PrintDialog();
                    print.fetchDataFromMainPage(paymentID, name, surname, duration);
                    print.ShowDialog();

                    break;
            }
        }
    }
}
