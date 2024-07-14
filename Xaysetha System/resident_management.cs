using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Xaysetha_System
{
    public partial class resident_management : Form
    {
        private Form activeForm = null;

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
            data.Columns["userID"].DataPropertyName = "villageID";
            data.Columns["profile"].DataPropertyName = "villageName";
            data.Columns["residentName"].DataPropertyName = "villageID";
            data.Columns["Surname"].DataPropertyName = "villageName";
            data.Columns["villageName"].DataPropertyName = "villageID";
            data.Columns["Tel"].DataPropertyName = "villageName";
            data.Columns["Gender"].DataPropertyName = "villageID";
            data.Columns["Religious"].DataPropertyName = "villageName";
            data.Columns["Job"].DataPropertyName = "villageID";
        }

        void cbVillageLoad()
        {
            DataSet dataSetVillage = new DataSet();
            adapter = new NpgsqlDataAdapter("SELECT * FROM tb_village;", cn.conn);
            adapter.Fill(dataSetVillage);
            cbVillage.DataSource = dataSetVillage.Tables[0];

            cbVillage.DisplayMember = "villageName";
            cbVillage.ValueMember = "villageName";
        }

        public resident_management()
        {
            InitializeComponent();
            CustomizedGridView();
            cn.getConnect();
            cbVillageLoad();
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

        private void data_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex == -1 && e.ColumnIndex >= 0)
            {
                e.PaintBackground(e.CellBounds, true);

                

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

        private void btnResident_Click(object sender, EventArgs e)
        {
            OpenChildForm(new resident_add());
        }

        private void data_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            String columnName = data.Columns[e.ColumnIndex].Name;
            if (columnName == "editButton")
            {
                OpenChildForm(new resident_add());
            }
            else if (columnName == "delButton")
            {
                MessageBox.Show("Deleted");
            }
        }
    }
}
