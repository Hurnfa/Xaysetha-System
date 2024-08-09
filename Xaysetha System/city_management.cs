using Npgsql;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Xaysetha_System
{
    public partial class city_management : Form
    {
        NpgsqlCommand cmd;
        NpgsqlDataAdapter adapter;
        //NpgsqlConnection conn;
        db_connect cn = new db_connect();
        DataTable datatable = new DataTable();
        city_management_add cityAdd = new city_management_add();

        public city_management()
        {
            InitializeComponent();
            CustomizedGridView();
            cn.getConnect();
            loadData("SELECT * FROM tb_district");
        }

        void displayTotal()
        {
            cmd = new NpgsqlCommand("SELECT COUNT(*) FROM tb_district", cn.conn);
            labelTotalVillage.Text = "ທັງໝົດ " + cmd.ExecuteScalar() + " ລາຍການ";
        }

        void loadData(string sql)
        {
            displayTotal();

            data.AutoGenerateColumns = false;
            adapter = new NpgsqlDataAdapter(sql, cn.conn);
            adapter.Fill(datatable);

            data.DataSource = datatable;
            data.Columns[0].DataPropertyName = "district_id";
            data.Columns[1].DataPropertyName = "district_name";
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

        private void btnAddVillage_Click(object sender, System.EventArgs e)
        { 
            new city_management_add().ShowDialog();
        }

        private void data_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string columnName = data.Columns[e.ColumnIndex].Name;
            DataGridViewRow row = data.Rows[e.RowIndex];

            int districtID = int.Parse(row.Cells[0].Value?.ToString());
            string districtName = row.Cells[1].Value?.ToString();

            switch(columnName)
            {
                case "btnEdit":

                    cityAdd.fetchDataFromMainPage(districtID, districtName, "ແກ້ໄຂ");
                    cityAdd.ShowDialog();

                break;

                case "btnDel":

                    DialogResult result = MessageBox.Show("ທ່ານຕ້ອງການແກ້ໄຂຂໍ້ມູນນີ້ບໍ?", "ແຈ້ງເຕືອນ", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (result == DialogResult.Yes)
                    {
                        cmd = new NpgsqlCommand("DELETE FROM tb_district WHERE district_id=@districtID", cn.conn);

                        try
                        {
                            cmd.Parameters.AddWithValue("@districtID", districtID);
                            cmd.ExecuteNonQuery();

                            MessageBox.Show("ລຶບຂໍ້ມູນສຳເລັດ", "ແຈ້ງເຕືອນ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            datatable.Clear();

                            displayTotal();

                            loadData("SELECT * FROM tb_district");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("ຂໍອະໄພ, ລະບົບຂັດຂ້ອງ\n" + ex.Message, "ແຈ້ງເຕືອນ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                    }

                break;
            }

        }

        private void txtSearch_TextChanged(object sender, System.EventArgs e)
        {
            datatable.Clear();

            loadData("SELECT * FROM tb_district WHERE CONCAT (district_name, district_name_en) LIKE '%"+txtSearch.Text+"%'");
        }
    }
}
