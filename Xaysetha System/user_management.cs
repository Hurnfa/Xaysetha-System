using Npgsql;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Xaysetha_System
{
    public partial class user_management : Form
    {
        NpgsqlCommand cmd;
        NpgsqlDataAdapter adapter;
        db_connect cn = new db_connect();
        DataTable datatable = new DataTable();
        user_management_add addUser = new user_management_add();

        private Form activeForm = null;

        void loadData(string sql)
        {
            //data.Rows.Clear();
            data.AutoGenerateColumns = false;
            adapter = new NpgsqlDataAdapter(sql, cn.conn);
            adapter.Fill(datatable);
            data.DataSource = datatable;
            data.Columns["userID"].DataPropertyName = "userID";
            data.Columns["userName"].DataPropertyName = "userName";
            data.Columns["Surname"].DataPropertyName = "userLName";
            data.Columns["Gender"].DataPropertyName = "gender";
            data.Columns["role"].DataPropertyName = "role";
            data.Columns["Tel"].DataPropertyName = "phoneNums";
            data.Columns["password"].DataPropertyName = "userPassword";
        }

        public void displayTotalOfUser()
        {
            cmd = new NpgsqlCommand("SELECT COUNT(*) FROM tb_user;", cn.conn);
            NpgsqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                labelTotalUser.Text = "ທັງໝົດ " + reader["count"] + " ລາຍການ";
            }

            reader.Close();
        }

        public user_management()
        {
            InitializeComponent();
            CustomizedGridView();
            data.Dock = DockStyle.Fill;
            data.AllowUserToAddRows = false;
            data.RowTemplate.Height = 30;
            cn.getConnect();
            displayTotalOfUser();
            loadData("SELECT * FROM tb_user;");
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

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            OpenChildForm(new user_management_add());
        }

        private void data_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            String columnName = data.Columns[e.ColumnIndex].Name;
            DataGridViewRow row = data.Rows[e.RowIndex];

            string userID = row.Cells[0].Value?.ToString(),
                    name = row.Cells[1].Value?.ToString(),
                    surname = row.Cells[2].Value?.ToString(),
                    gender = row.Cells[3].Value?.ToString(),
                    role = row.Cells[4].Value?.ToString(),
                    phoneNums = row.Cells[5].Value?.ToString(),
                    userPassword = row.Cells[6].Value?.ToString();

            switch (columnName)
            {
                case "editButton":

                    addUser.fetchDataFromMainPage(userID, name, surname, gender, role, phoneNums, userPassword);

                    addUser.changeInsertToUpdate("ແກ້ໄຂຂໍ້ມູນ", "ແກ້ໄຂ");
                    OpenChildForm(addUser);

                    break;

                case "delButton":

                    DialogResult result = MessageBox.Show("ທ່ານຕ້ອງການລຶບຂໍ້ມູນ " + userID + " ນີ້ບໍ?", "ແຈ້ງເຕືອນ", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (result == DialogResult.Yes)
                    {
                        /*addUser.dataChange("DELETE FROM tb_user WHERE \"userID\"='" + userID + "'", "ລຶບ");*/

                        cmd = new NpgsqlCommand("DELETE FROM tb_user WHERE \"userID\"=@userID;", cn.conn);

                        try
                        {
                            cmd.Parameters.AddWithValue("@userID", userID);

                            cmd.ExecuteNonQuery();

                            MessageBox.Show("ລຶບຜູ້ໃຊ້ງານສຳເລັດ!", "ແຈ້ງເຕືອນ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            datatable.Clear();

                            loadData("SELECT * FROM tb_user;");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("ຂໍອະໄພ, ລະບົບຂັດຂ້ອງ\n" + ex.Message, "ແຈ້ງເຕືອນ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                    }

                    break;
            }
        }

        private void txtUserSearch_TextChanged(object sender, EventArgs e)
        {
            datatable.Clear();

            loadData("SELECT * FROM tb_user WHERE CONCAT(\"userID\", \"userName\", \"userLName\", gender, role, \"phoneNums\", \"userPassword\") LIKE '%" + txtUserSearch.Text + "%';");
        }
    }
}
