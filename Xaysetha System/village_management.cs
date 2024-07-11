using Npgsql;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Xaysetha_System
{
    public partial class village_management : Form
    {
        NpgsqlCommand cmd;
        NpgsqlDataAdapter adapter;
        //NpgsqlConnection conn;
        db_connect cn = new db_connect();
        DataTable datatable = new DataTable();
        village_management_add village = new village_management_add();

        void loadData(string sql)
        {
            //data.Rows.Clear();
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
            displayTotalOfVillage();
        }

        public void displayTotalOfVillage()
        {
            cmd = new NpgsqlCommand("SELECT COUNT(*) FROM tb_village;", cn.conn);
            NpgsqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                labelTotalVillage.Text = "ທັງໝົດ " + reader["count"] + " ລາຍການ";
            }

            reader.Close();
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
            village.changeInsertToUpdate("ເພີ່ມຂໍ້ມູນບ້ານ", "ເພີ່ມ", "");
            village.Show();
        }

        private void txtFindVillage_TextChanged(object sender, EventArgs e)
        {
            datatable.Clear();

            loadData("SELECT * FROM tb_village WHERE CONCAT(\"villageID\", \"villageName\", \"villageEngName\") LIKE '%" + txtFindVillage.Text + "%';");
        }

        private void data_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            String columnName = data.Columns[e.ColumnIndex].Name;
            if (columnName == "editButton")
            {
                string cellData = data.Rows[e.RowIndex].Cells["VillageName"].Value.ToString();
                //int villageID = Convert.ToInt32(data.Rows[e.RowIndex].Cells["VillageName"].Value.ToString());

                village.changeInsertToUpdate("ແກ້ໄຂຂໍ້ມູນບ້ານ", "ແກ້ໄຂ", cellData);

                village.Show();

/*                village_management_add village_add = new village_management_add(cellData);
                village_add.Show();*/

            }
            else if (columnName == "delButton")
            {
                MessageBox.Show("Deleted");
            }
        }
    }
}
