using Npgsql;
using System;
using System.Data;
using System.Web.UI;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Xaysetha_System
{
    public partial class fee_management : Form
    {
        NpgsqlCommand cmd;
        NpgsqlDataAdapter adapter;
        db_connect cn = new db_connect();
        DataTable datatable = new DataTable();
        fee_add feeAdd = new fee_add();

        public fee_management()
        {
            InitializeComponent();
            cn.getConnect();
            loadData("SELECT * FROM tb_package");
        }

        void displayTotal()
        {
            cmd = new NpgsqlCommand("SELECT COUNT(*) FROM tb_package", cn.conn);
            labelTotalVillage.Text = "ທັງໝົດ " + cmd.ExecuteScalar() + " ລາຍການ";
        }

        void loadData(string sql)
        {
            displayTotal();
            data.AutoGenerateColumns = false;

            adapter = new NpgsqlDataAdapter(sql, cn.conn);
            adapter.Fill(datatable);

            data.DataSource = datatable;
            data.Columns[0].DataPropertyName = "pkg_id";
            data.Columns[1].DataPropertyName = "pkg_name";
            data.Columns[2].DataPropertyName = "pkg_duration";
            data.Columns[3].DataPropertyName = "pkg_price";
        }

        private void btnAddVillage_Click(object sender, EventArgs e)
        {
            new fee_add().ShowDialog();        
        }

        private void txtFindVillage_TextChanged(object sender, EventArgs e)
        {
            datatable.Clear();

            loadData("SELECT * FROM tb_package WHERE CONCAT (pkg_name) LIKE '%"+txtFindVillage.Text+"%'");
        }

        private void data_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            String columnName = data.Columns[e.ColumnIndex].Name;
            DataGridViewRow row = data.Rows[e.RowIndex];
            int id = int.Parse(row.Cells[0].Value?.ToString()),
                duration = int.Parse(row.Cells[2].Value?.ToString());

            string pkgName = row.Cells[1].Value?.ToString(),
                price = row.Cells[3].Value?.ToString();


            switch (columnName)
            {
                case "btnEdit":

                    feeAdd.fetchDataFromMainPage(id, pkgName, duration, price, "ແກ້ໄຂ");
                    feeAdd.ShowDialog();

                break;

                case "btnDel":

                    DialogResult result = MessageBox.Show("ທ່ານຕ້ອງການແກ້ໄຂຂໍ້ມູນນີ້ບໍ?", "ແຈ້ງເຕືອນ", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (result == DialogResult.Yes)
                    {
                        cmd = new NpgsqlCommand("DELETE FROM tb_package WHERE pkg_id=@pkgID", cn.conn);

                        try
                        {
                            cmd.Parameters.AddWithValue("@pkgID", id);

                            cmd.ExecuteNonQuery();
                            MessageBox.Show("ລຶບຄ່າທຳນຽມສຳເລັດ!", "ແຈ້ງເຕືອນ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            datatable.Clear();

                            loadData("SELECT * FROM tb_package");

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
