using Npgsql;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Xaysetha_System
{
    public partial class place_management : Form
    {
        private Form activeForm = null;

        NpgsqlCommand cmd;
        NpgsqlDataAdapter adapter;
        db_connect cn = new db_connect();
        DataTable datatable = new DataTable();
        place_add placeAdd = new place_add();

        void loadData(string sql)
        {
            //data.Rows.Clear();
            data.AutoGenerateColumns = false;
            adapter = new NpgsqlDataAdapter(sql, cn.conn);
            adapter.Fill(datatable);
            data.DataSource = datatable;

            data.Columns["placeID"].DataPropertyName = "place_id";
            data.Columns["placeName"].DataPropertyName = "place_name";
            data.Columns["placeType"].DataPropertyName = "place_type";
            data.Columns["villageName"].DataPropertyName = "village_name";
            data.Columns["houseUnit"].DataPropertyName = "house_unit";
            data.Columns["houseNumber"].DataPropertyName = "house_number";
            data.Columns["ownerName"].DataPropertyName = "citizen_name";
        }

        public place_management()
        {
            InitializeComponent();
            CustomizedGridView();
            cn.getConnect();
            loadData("SELECT * from tb_place JOIN tb_citizen on tb_place.citizen_id = tb_citizen.citizen_id JOIN tb_village on tb_place.village_id = tb_village.village_id;");

        }
        public void CustomizedGridView()
        {
            data.ColumnHeadersDefaultCellStyle.Font = new Font("Noto Sans Lao", 10, FontStyle.Regular);
            data.ColumnHeadersHeight = 30;

        }

        private void OpenChildForm(Form childForm)
        {
            // Close the current active form if there is one
            if (activeForm != null)
            {
                activeForm.Close();
            }

            // Set the new form as the active form
            activeForm = childForm;

            // Configure the form to be displayed within the panel
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;

            // Clear the panel and add the new form
            Dashboard.instance.panelContainerInstance.Controls.Clear();
            Dashboard.instance.panelContainerInstance.Controls.Add(childForm);
            childForm.BringToFront();
            childForm.Show();
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            OpenChildForm(new place_add());
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
            String columnName = data.Columns[e.ColumnIndex].Name;
            DataGridViewRow row = data.Rows[e.RowIndex];

            string placeID = row.Cells[0].Value?.ToString(),
                placeName = row.Cells[1].Value?.ToString(),
                placeType = row.Cells[2].Value?.ToString(),
                villageID = row.Cells[3].Value?.ToString(),
                unit = row.Cells[4].Value?.ToString(),
                houseNum = row.Cells[5].Value?.ToString(),
                citizenID = row.Cells[6].Value?.ToString();

            switch (columnName)
            {
                case "editButton":

                    placeAdd.fetchDataFromMainPage(placeID, placeName, placeType, villageID, citizenID, unit, houseNum, "ແກ້ໄຂ");
                    OpenChildForm(placeAdd);

                    break;

                case "delButton":

                    DialogResult result = MessageBox.Show("ທ່ານຕ້ອງການລຶບຂໍ້ມູນນີ້ບໍ?", "ແຈ້ງເຕືອນ", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (result == DialogResult.Yes)
                    {
                        cmd = new NpgsqlCommand("DELETE FROM tb_place WHERE place_id=@placeID;", cn.conn);

                        try
                        {
                            cmd.Parameters.AddWithValue("@placeID", placeID);

                            cmd.ExecuteNonQuery();

                            MessageBox.Show("ລຶບຜູ້ໃຊ້ງານສຳເລັດ!", "ແຈ້ງເຕືອນ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            datatable.Clear();

                            loadData("SELECT * FROM tb_place;");
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

            //loadData("SELECT * FROM tb_place WHERE CONCAT (\"placeID\", \"citizentID\", \"villageID\", \"placeName\",\"placeHouseUnit\", \"placeHouseNumber\", \"placeType\") LIKE '%" + txtSearch.Text + "%'");
            loadData($"SELECT * FROM tb_place WHERE CONCAT (place_id, citizen_id, village_id, place_name, house_unit, house_number, place_type) LIKE '%{txtSearch.Text}%'");
        }
    }
}
