using Npgsql;
using System;
using System.Data;
using System.Windows.Forms;

namespace Xaysetha_System
{
    public partial class provice_management : Form
    {
        NpgsqlCommand cmd;
        NpgsqlDataAdapter adapter;
        //NpgsqlConnection conn;
        db_connect cn = new db_connect();
        DataTable datatable = new DataTable();
        province_management_add provinceAdd = new province_management_add();

        public provice_management()
        {
            InitializeComponent();
            cn.getConnect();
            loadData("SELECT * FROM tb_province");
        }

        void countTotal()
        {
            cmd = new NpgsqlCommand("SELECT COUNT(*) FROM tb_province", cn.conn);
            labelTotalVillage.Text = "ທັງໝົດ "+cmd.ExecuteScalar()+" ລາຍການ";
        }

        void loadData(string sql)
        {
            countTotal();

            data.AutoGenerateColumns = false;
            adapter = new NpgsqlDataAdapter(sql, cn.conn);
            adapter.Fill(datatable);

            data.DataSource = datatable;
            data.Columns[0].DataPropertyName = "province_id";
            data.Columns[1].DataPropertyName = "province_name";
        }

        private void btnAddProvince_Click(object sender, EventArgs e)
        {
            new province_management_add().ShowDialog();
        }

        private void data_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            String columnName = data.Columns[e.ColumnIndex].Name;
            DataGridViewRow row = data.Rows[e.RowIndex];

            string provinceID = row.Cells[0].Value?.ToString(),
                provinceName = row.Cells[1].Value?.ToString();

            switch (columnName)
            {
                case "btnEdit":

                    provinceAdd.fetchDataFromMainPage(provinceID, provinceName, "ແກ້ໄຂ");
                    provinceAdd.ShowDialog();

                break;

                case "btnDel":

                    DialogResult result = MessageBox.Show("ທ່ານຕ້ອງການແກ້ໄຂຂໍ້ມູນນີ້ບໍ?", "ແຈ້ງເຕືອນ", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (result == DialogResult.Yes)
                    {
                        cmd = new NpgsqlCommand("DELETE FROM tb_province WHERE province_id=@provinceID", cn.conn);

                        try
                        {
                            cmd.Parameters.AddWithValue("@provinceID", int.Parse(provinceID));

                            cmd.ExecuteNonQuery();

                            MessageBox.Show("ລຶບຜູ້ໃຊ້ງານສຳເລັດ!", "ແຈ້ງເຕືອນ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            datatable.Clear();

                            countTotal();

                            loadData("SELECT * FROM tb_province");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("ຂໍອະໄພ, ລະບົບຂັດຂ້ອງ\n" + ex.Message, "ແຈ້ງເຕືອນ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                break;
            }


        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            datatable.Clear();

            loadData("SELECT * FROM tb_province WHERE CONCAT (province_name, province_name_en) LIKE '%"+txtSearch.Text+"%'");
        }
    }
}
