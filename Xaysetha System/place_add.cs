using Npgsql;
using System;
using System.Data;
using System.Windows.Forms;

namespace Xaysetha_System
{
    public partial class place_add : Form
    {
        NpgsqlCommand cmd;
        NpgsqlDataAdapter adapter;
        db_connect cn = new db_connect();
        DataTable datatable = new DataTable();

        public place_add()
        {
            InitializeComponent();
            cn.getConnect();
            comboBoxVillageLoad();
            comboBoxCitizenLoad();
        }

        void comboBoxVillageLoad()
        {
            //Detach event
            combobox_village.SelectedIndexChanged -= combobox_village_SelectedIndexChanged;

            DataSet dataSetVillage = new DataSet();
            adapter = new NpgsqlDataAdapter("SELECT * FROM tb_village;", cn.conn);
            adapter.Fill(dataSetVillage);
            combobox_village.DataSource = dataSetVillage.Tables[0];

            combobox_village.DisplayMember = "villageName";
            combobox_village.ValueMember = "villageID";

            //Attach event again
            combobox_village.SelectedIndexChanged += combobox_village_SelectedIndexChanged;
        }

        void comboBoxCitizenLoad()
        {
            //Detach event
            combobox_citizen.SelectedIndexChanged -= combobox_citizen_SelectedIndexChanged;

            DataSet dataSetCitizen = new DataSet();
            adapter = new NpgsqlDataAdapter("SELECT * FROM tb_citizen;", cn.conn);
            adapter.Fill(dataSetCitizen);
            combobox_citizen.DataSource = dataSetCitizen.Tables[0];

            combobox_citizen.DisplayMember = "name";
            combobox_citizen.ValueMember = "citizenID";

            //Attach event again
            combobox_citizen.SelectedIndexChanged += combobox_citizen_SelectedIndexChanged;
        }

        public void dataChange(string sql, string messageBox)
        {
            cmd = new NpgsqlCommand(sql, cn.conn);

            try
            {
                cmd.Parameters.AddWithValue("@placeID", txtHouseUnit.Text + "/" + txtHouseNums.Text);
                cmd.Parameters.AddWithValue("@placeType", combobox_place_type.Text);
                cmd.Parameters.AddWithValue("@citizenID", combobox_citizen.SelectedValue);
                cmd.Parameters.AddWithValue("@villageID", combobox_village.SelectedValue);
                cmd.Parameters.AddWithValue("@placeName", txtPlaceName.Text);
                cmd.Parameters.AddWithValue("@placeHouseUnit", txtHouseUnit.Text);
                cmd.Parameters.AddWithValue("@placeHouseNumber", txtHouseNums.Text);

                cmd.ExecuteNonQuery();

                MessageBox.Show(messageBox + "ຜູ້ໃຊ້ງານສຳເລັດ!", "ແຈ້ງເຕືອນ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                OpenChildForm(new place_management());
            }
            catch (Exception ex)
            {
                MessageBox.Show("ຂໍອະໄພ, ລະບົບຂັດຂ້ອງ\n" + ex.Message, "ແຈ້ງເຕືອນ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void fetchDataFromMainPage(string id, string placeName, string placeType, string village, string placeOwner, string unit, string houseNum, string header)
        {
            txtPlaceName.Text = placeName;
            label_place_id.Text = id;
            labelHeader.Text = header + "ຂໍ້ມູນ";
            btnSave.Text = header;
            txtHouseNums.Text = houseNum;
            txtHouseUnit.Text = unit;
            combobox_village.Text = village;
            combobox_place_type.Text = placeType;
        }

        private Form activeForm = null;
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

        private void btnBack_Click(object sender, EventArgs e)
        {
            OpenChildForm(new place_management());
        }

        private void linkLabelBack_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenChildForm(new place_management());
        }

        private void combobox_village_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void combobox_citizen_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            switch (btnSave.Text)
            {
                case "ບັນທຶກ":

                    dataChange("INSERT INTO tb_place VALUES (@placeID, @citizenID, @villageID, @placeName, @placeHouseUnit, @placeHouseNumber, @placeType)", "ເພີ່ມ");

                    break;

                case "ແກ້ໄຂ":

                    DialogResult result = MessageBox.Show("ທ່ານຕ້ອງການແກ້ໄຂຂໍ້ມູນນີ້ບໍ?", "ແຈ້ງເຕືອນ", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (result == DialogResult.Yes)
                    {
                        dataChange("UPDATE tb_place SET \"citizentID\"=@citizenID, \"villageID\"=@villageID, \"placeName\"=@placeName, \"placeHouseUnit\"=@placeHouseUnit, \"placeHouseNumber\"=@placeHouseNumber, \"placeType\"=@placeType WHERE \"placeID\"=@placeID", "ແກ້ໄຂ");
                    }

                    break;

            }
        }

        private void txtPlaceName_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void txtHouseNums_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
      (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void txtHouseUnit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
      (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }
    }
}
