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

        private Form activeForm = null;

        void loadData(string sql)
        {
            data.Rows.Clear();
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

        public user_management()
        {
            InitializeComponent();
            CustomizedGridView();
            data.Dock = DockStyle.Fill;
            data.AllowUserToAddRows = false;
            data.RowTemplate.Height = 30;
            cn.getConnect();
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
            OpenChildForm(new user_management_add(""));
        }

        private void data_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            String columnName = data.Columns[e.ColumnIndex].Name;
            if (columnName == "editButton")
            {
                string userID = data.Rows[e.RowIndex].Cells["userID"].Value.ToString();
                OpenChildForm(new user_management_add(userID));
            }
            else if (columnName == "delButton")
            {
                MessageBox.Show("Deleted");
            }
        }
    }
}
