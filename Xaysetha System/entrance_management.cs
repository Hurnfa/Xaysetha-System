using Npgsql;
using System;
using System.Data;
using System.Drawing;
using System.Numerics;
using System.Windows.Forms;

namespace Xaysetha_System
{
    public partial class entrance_management : Form
    {
        private Form activeForm = null;

        public entrance_management(string username)
        {
            InitializeComponent();
            cn.getConnect();
            displayTotalOfUser();
            loadData("SELECT * FROM tb_tenant;");
            CustomizedGridView();
            label_user.Text = username;
        }

        NpgsqlCommand cmd;
        NpgsqlDataAdapter adapter;
        //NpgsqlConnection conn;
        db_connect cn = new db_connect();
        DataTable datatable = new DataTable();
        //tenant_add tenantAdd = new tenant_add();

        public void displayTotalOfUser()
        {
            cmd = new NpgsqlCommand("SELECT COUNT(*) FROM tb_tenant;", cn.conn);
            NpgsqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                labelTotal.Text = "ທັງໝົດ " + reader["count"] + " ລາຍການ";
            }

            reader.Close();
        }

        public void loadData(string sql)
        {
            data.AutoGenerateColumns = false;

            adapter = new NpgsqlDataAdapter(sql, cn.conn);
            adapter.Fill(datatable);
            data.DataSource = datatable;

            data.Columns[0].DataPropertyName = "tenant_id";
            data.Columns[1].DataPropertyName = "tenant_name";
            data.Columns[2].DataPropertyName = "tenant_lastname";
            data.Columns[3].DataPropertyName = "tenant_gender";
            data.Columns[4].DataPropertyName = "village";
        }

        public void CustomizedGridView()
        {
            data.ColumnHeadersDefaultCellStyle.Font = new Font("Noto Sans Lao", 10, FontStyle.Regular);
            data.ColumnHeadersHeight = 30;

        }


        private void btnAddUser_Click(object sender, EventArgs e)
        {
            tenant_add tenantAdd = new tenant_add(label_user.Text);
            tenantAdd.Show();
        }

        private void data_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            String columnName = data.Columns[e.ColumnIndex].Name;
            DataGridViewRow row = data.Rows[e.RowIndex];
            BigInteger tenant_id = BigInteger.Parse(row.Cells[0].Value.ToString());
            string name = row.Cells[1].Value?.ToString(),
                surname = row.Cells[2].Value?.ToString(),
                 gender = row.Cells[3].Value?.ToString(),
                village = row.Cells[4].Value?.ToString();

            switch (columnName)
            {
                case "editButton":

                    //tenantAdd.fetchDataFromMainPage();
                    tenant_add tenantAdd = new tenant_add(label_user.Text);
                    tenantAdd.fetchDataFromMainPage(Convert.ToString(tenant_id), name, surname, gender, village, "ແກ້ໄຂ");
                    tenantAdd.Show();


                    break;

                case "delButton":

                    DialogResult result = MessageBox.Show("ທ່ານຕ້ອງການລຶບຂໍ້ມູນນີ້ບໍ?", "ແຈ້ງເຕືອນ", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (result == DialogResult.Yes)
                    {
                        cmd = new NpgsqlCommand("DELETE FROM tb_tenant WHERE tenant_id=@tenantID", cn.conn);

                        try
                        {
                            cmd.Parameters.AddWithValue("@tenantID", tenant_id);

                            cmd.ExecuteNonQuery();

                            MessageBox.Show("ລຶບຜູ້ໃຊ້ງານສຳເລັດ!", "ແຈ້ງເຕືອນ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            datatable.Clear();

                            loadData("SELECT * FROM tb_tenant;");

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("ຂໍອະໄພ, ລະບົບຂັດຂ້ອງ\n" + ex.Message, "ແຈ້ງເຕືອນ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    break;
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

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            datatable.Clear();

            //loadData("SELECT * FROM tb_tenant WHERE CONCAT(\"tenantID\", firstname, lastname) LIKE '%"+txtSearch.Text+"%'");
            loadData($"SELECT * FROM tb_tenant WHERE CONCAT(tenant_id, tenant_name, tenant_lastname) LIKE '{txtSearch.Text}%'");
        }
    }
}
