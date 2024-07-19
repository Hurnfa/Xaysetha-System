using Microsoft.Reporting.Map.WebForms.BingMaps;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;
using Npgsql;
using System;
using System.Data;
using System.Drawing;
using System.Numerics;
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
        resident_add resident = new resident_add();

        void loadData(string sql)
        {
            //data.Rows.Clear();
            data.AutoGenerateColumns = false;
            adapter = new NpgsqlDataAdapter(sql, cn.conn);
            adapter.Fill(datatable);
            data.DataSource = datatable;
            data.Columns["citizenID"].DataPropertyName = "citizenID";
            data.Columns["citizenName"].DataPropertyName = "name";
            data.Columns["Surname"].DataPropertyName = "surname";
            data.Columns["villageName"].DataPropertyName = "addr";
            data.Columns["Tel"].DataPropertyName = "phoneNums";
            data.Columns["Gender"].DataPropertyName = "gender";
            data.Columns["Religious"].DataPropertyName = "religion";
            data.Columns["Job"].DataPropertyName = "occupation";
        }

        void cbVillageLoad()
        {
            System.Data.DataSet dataSetVillage = new System.Data.DataSet();
            adapter = new NpgsqlDataAdapter("SELECT * FROM tb_village;", cn.conn);
            adapter.Fill(dataSetVillage);
            cbVillage.DataSource = dataSetVillage.Tables[0];

            cbVillage.DisplayMember = "villageName";
            cbVillage.ValueMember = "villageName";
        }

        public void dataChange(string sql, string messageBox)
        {

        }

        public resident_management()
        {
            InitializeComponent();
            CustomizedGridView();
            cn.getConnect();
            getTotalCitizen();
            cbVillageLoad();
            loadData("SELECT * FROM tb_citizen;");
        }

        void getTotalCitizen()
        {
            cmd = new NpgsqlCommand("SELECT COUNT(*) FROM tb_citizen;", cn.conn);

            string id = cmd.ExecuteScalar().ToString();

            labelTotal.Text = "ທັງໝົດ "+id+" ລາຍການ";
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

        private void btnResident_Click(object sender, EventArgs e)
        {
            OpenChildForm(new resident_add());
        }

        private void data_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            String columnName = data.Columns[e.ColumnIndex].Name;
            DataGridViewRow row = data.Rows[e.RowIndex];
            BigInteger citizen_id = BigInteger.Parse(row.Cells[0].Value.ToString());
            string name = row.Cells[1].Value?.ToString(),
                surname = row.Cells[2].Value?.ToString(),
                village = row.Cells[3].Value?.ToString(),
                phoneNums = row.Cells[4].Value?.ToString(),
                gender = row.Cells[5].Value?.ToString(),
                religion = row.Cells[6].Value?.ToString(),
                jobs = row.Cells[7].Value?.ToString();


            switch (columnName)
            {
                case "editButton":
                    //resident.fetchDataFromMainPage();
                    resident.fetchDataFromMainPage(Convert.ToString(citizen_id), name, surname, gender, religion, jobs, village, "ແກ້ໄຂ");
                    OpenChildForm(resident);

                    break;

                case "delButton":

                    DialogResult result = MessageBox.Show("ທ່ານຕ້ອງການລຶບຂໍ້ມູນນີ້ບໍ?", "ແຈ້ງເຕືອນ", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (result == DialogResult.Yes)
                    {
                        cmd = new NpgsqlCommand("DELETE FROM tb_citizen WHERE \"citizenID\"=@citizenID", cn.conn);

                        try
                        {
                            cmd.Parameters.AddWithValue("@citizenID", citizen_id);

                            cmd.ExecuteNonQuery();

                            MessageBox.Show("ລຶບຜູ້ໃຊ້ງານສຳເລັດ!", "ແຈ້ງເຕືອນ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            datatable.Clear();

                            loadData("SELECT * FROM tb_citizen;");

                        }
                        catch(Exception ex)
                        {
                            MessageBox.Show("ຂໍອະໄພ, ລະບົບຂັດຂ້ອງ\n" + ex.Message, "ແຈ້ງເຕືອນ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                    }

                    break;
            }
        }

        private void txtNameSearch_TextChanged(object sender, EventArgs e)
        {
            datatable.Clear();

            loadData("SELECT * FROM tb_citizen WHERE CONCAT (name) LIKE '%"+txtNameSearch.Text+"%';");
        }

        private void cbVillage_SelectedIndexChanged(object sender, EventArgs e)
        {
            datatable.Clear();

            loadData("SELECT * FROM tb_citizen WHERE CONCAT (name) LIKE '%" + txtNameSearch.Text + "%';");
        }

        private void cbGender_SelectedIndexChanged(object sender, EventArgs e)
        {
            datatable.Clear();

            switch (cbGender.Text)
            {
                case " ":
                    loadData("SELECT * FROM tb_citizen;");
                    break;

                default:

                    loadData("SELECT * FROM tb_citizen WHERE CONCAT (gender) LIKE '%" + cbGender.Text + "%';");

                    break;
            }
           
        }

        private void numberPickerAge_ValueChanged(object sender, EventArgs e)
        {

        }

        private void cbJobs_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }   
}
