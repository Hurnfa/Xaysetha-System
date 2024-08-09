using Npgsql;
using System;
using System.Data;
using System.Windows.Forms;

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

        void loadData(string sql)
        {
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
            
            
        }

        private void txtFindVillage_TextChanged(object sender, EventArgs e)
        {
            datatable.Clear();

            loadData("SELECT * FROM tb_package WHERE CONCAT (pkg_name) LIKE '%"+txtFindVillage.Text+"%'");
        }

        private void data_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            String columnName = data.Columns[e.ColumnIndex].Name;

            switch (columnName)
            {
                case "btnEdit":

                    feeAdd.ShowDialog();

                break;

                case "btnDel":

                    DialogResult result = MessageBox.Show("ທ່ານຕ້ອງການແກ້ໄຂຂໍ້ມູນນີ້ບໍ?", "ແຈ້ງເຕືອນ", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (result == DialogResult.Yes)
                    {
                        //village.dataChange("DELETE FROM tb_village WHERE \"villageID\"=" + village_id + "", "ລຶບ");
                    }

                 break;
            }
        }
    }
}
