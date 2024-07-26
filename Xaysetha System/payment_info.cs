using Npgsql;
//using Org.BouncyCastle.Math;
using System;
using System.Data;
using System.Drawing;
using System.Numerics;
using System.Windows.Forms;

namespace Xaysetha_System
{
    public partial class payment_info : Form
    {
        db_connect cn = new db_connect();
        NpgsqlCommand cmd;
        NpgsqlDataAdapter adapter;
        public DataTable dataTable = new DataTable();
        PaymentDialog paymentDialog = new PaymentDialog();

        public payment_info()
        {
            InitializeComponent();
            cn.getConnect();
            CustomizedGridView();
            AdjustColumnWidths(data, 10);
        }

        public void loadData(string sql)
        {
            if (data.Rows.Count > 0)
            {
                data.AutoGenerateColumns = false;

                adapter = new NpgsqlDataAdapter(sql, cn.conn);
                adapter.Fill(dataTable);
                data.DataSource = dataTable;

                data.Columns[0].DataPropertyName = "payment_id";
                data.Columns[1].DataPropertyName = "tenant_id";
                data.Columns[2].DataPropertyName = "firstname";
                data.Columns[3].DataPropertyName = "duration";
                data.Columns[4].DataPropertyName = "price";
                data.Columns[5].DataPropertyName = "user_id";
            }
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

        string name, surname, gender;

        private void data_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string columnName = data.Columns[e.ColumnIndex].Name;
            DataGridViewRow row = data.Rows[e.RowIndex];
            BigInteger payment_id = BigInteger.Parse(row.Cells[0].Value?.ToString());
            string tenantID = row.Cells[1].Value?.ToString(),
                userID = row.Cells[5].Value?.ToString();
            int duration = int.Parse(row.Cells[3].Value?.ToString());
            float price = float.Parse(row.Cells[4].Value?.ToString());


            switch (columnName)
            {
                case "Print":

                    cmd = new NpgsqlCommand("SELECT firstname, lastname, gender FROM tb_tenant WHERE \"tenantID\"=@tenantID", cn.conn);

                    cmd.Parameters.AddWithValue("@tenantID", BigInteger.Parse(tenantID));

                    NpgsqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        name = reader["firstname"].ToString();
                        surname = reader["lastname"].ToString();
                        gender = reader["gender"].ToString();
                    }

                    reader.Close();

                    printing loadBill = new printing();

                    loadBill.loadDataToReport(payment_id, name, surname, price, duration, userID, gender);

                    loadBill.Show();

                    break;


                case "Edit":

                    paymentDialog.fetchDataFromTable(payment_id, tenantID, userID, duration, price, "ແກ້ໄຂ");
                    paymentDialog.ShowDialog();

                    break;

                case "Delete":

                    DialogResult result = MessageBox.Show("ທ່ານຕ້ອງການລຶບຂໍ້ມູນນີ້ບໍ?", "ແຈ້ງເຕືອນ", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (result == DialogResult.Yes)
                    {
                        cmd = new NpgsqlCommand("DELETE FROM tb_payment WHERE payment_id=@paymentID", cn.conn);

                        try
                        {
                            cmd.Parameters.AddWithValue("@paymentID", payment_id);

                            cmd.ExecuteNonQuery();

                            MessageBox.Show("ລຶບຜູ້ໃຊ້ງານສຳເລັດ!", "ແຈ້ງເຕືອນ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            dataTable.Clear();

                            loadData("SELECT * FROM tb_payment;");

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("ຂໍອະໄພ, ລະບົບຂັດຂ້ອງ\n" + ex.Message, "ແຈ້ງເຕືອນ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                    break;
            }
        }
    }
}
