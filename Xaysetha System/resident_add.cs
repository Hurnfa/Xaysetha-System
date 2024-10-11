using Npgsql;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Numerics;
using System.Windows.Forms;

namespace Xaysetha_System
{
    public partial class resident_add : Form
    {
        NpgsqlCommand cmd;
        db_connect cn = new db_connect();
        DataTable datatable = new DataTable();
        public int total;

        public resident_add()
        {
            InitializeComponent();
            datePickerBirthday.MaxDate = DateTime.Now.AddYears(-18);
            datePickerFamBookIssuedDate.MaxDate = DateTime.Now.AddDays(-7);
            cn.getConnect();
        }

        public void fetchDataFromMainPage(
            string citizen_id,
            string name,
            string surname,
            string gender,
            string religion,
            string occupation,
            string addr,
            string header
            )
        {
            txtCitizenID.Text = citizen_id;
            txtName.Text = name;
            txtSurname.Text = surname;

            rdoMale.Checked = gender == "ຊາຍ";
            rdoFemale.Checked = gender == "ຍິງ";
            rdoOthers.Checked = !(rdoMale.Checked || rdoFemale.Checked);

            txtJobs.Text = occupation;
            txtAddr.Text = addr;

            cmd = new NpgsqlCommand("SELECT * FROM tb_citizen WHERE citizen_id=@citizenID", cn.conn);

            cmd.Parameters.AddWithValue("@citizenID", BigInteger.Parse(citizen_id));

            NpgsqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                datePickerBirthday.Value = (DateTime)reader["citizen_dob"];
                txtRace.Text = reader["citizen_race"].ToString();
                txtNationality.Text = reader["citizen_nationality"].ToString();
                txtEthnic.Text = reader["citizen_ethnic"].ToString();
                txtReligious.Text = reader["citizen_religion"].ToString();
                txtDadName.Text = reader["dad_name"].ToString();
                txtMomName.Text = reader["mom_name"].ToString();
                txtFamBookNums.Text = reader["fambook_nums"].ToString();
                txtWorkplace.Text = reader["workplace"].ToString();
                txtPhoneNums.Text = reader["tels"].ToString();

/*                if (reader["citizenPics"] == DBNull.Value)
                {
                    profilePictureBox.Image = null;
                }
                else
                {
                    byte[] img = (byte[])reader["citizenPics"];
                    MemoryStream memory = new MemoryStream(img);
                    profilePictureBox.Image = Image.FromStream(memory);
                }*/
            }

            reader.Close();

            /*            datePickerBirthday.Value = birth_day;
                        txtRace.Text = race;
                        txtNationality.Text = nationality;
                        txtEthnic.Text = ethnic;
                        txtReligious.Text = religion;
                        txtDadName.Text = dad_name;
                        txtMomName.Text = mom_name;
                        txtFamBookNums.Text = familyBook;
                        txtWorkplace.Text = workPlace;*/
            txtJobs.Text = occupation;
            txtAddr.Text = addr;
            //txtPhoneNums.Text = phoneNums;

            labelHeader.Text = header + "ຂໍ້ມູນ";
            btnSave.Text = header;
        }

        byte[] img;

        public void dataChange(string sql, string messageBox)
        {
            MemoryStream memoryStream = new MemoryStream();
            
            try
            {
                BigInteger citizenID = BigInteger.Parse(txtCitizenID.Text);

                string gender = "ບໍ່ລະບຸ";

                if (rdoMale.Checked)
                {
                    gender = "ຊາຍ";
                }
                else if (rdoFemale.Checked)
                {
                    gender = "ຍິງ";
                }

                DateTime birthDay = datePickerBirthday.Value,
                    famBookIssuedDate = datePickerFamBookIssuedDate.Value;

/*                if (profilePictureBox.Image == null)
                {
                    MessageBox.Show("ຂໍອະໄພ, ກະລຸນາໃສ່ຮູບກ່ອນ", "ແຈ້ງເຕືອນ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {             
                    profilePictureBox.Image.Save(memoryStream, profilePictureBox.Image.RawFormat);

                    img = memoryStream.ToArray();
                }*/


 
                cmd = new NpgsqlCommand(sql, cn.conn);

                try
                {
                    /*1st missing field*/
                    cmd.Parameters.AddWithValue("@citizenID", citizenID);
                    cmd.Parameters.AddWithValue("@name", txtName.Text);
                    cmd.Parameters.AddWithValue("@surname", txtSurname.Text);
                    cmd.Parameters.AddWithValue("@gender", gender);
                    cmd.Parameters.AddWithValue("@dob", birthDay);
                    /*2nd missing field*/
                    cmd.Parameters.AddWithValue("@race", txtRace.Text);
                    cmd.Parameters.AddWithValue("@nationality", txtNationality.Text);
                    /*3rd missing field*/
                    cmd.Parameters.AddWithValue("@ethnic", txtEthnic.Text);
                    cmd.Parameters.AddWithValue("@religion", txtReligious.Text);
                    cmd.Parameters.AddWithValue("@dad_name", txtDadName.Text);
                    cmd.Parameters.AddWithValue("@mom_name", txtAddr.Text);
                    cmd.Parameters.AddWithValue("@family_book", BigInteger.Parse(txtFamBookNums.Text));
                    /*4th missing field*/
                    cmd.Parameters.AddWithValue("@workplace", txtWorkplace.Text);
                    //cmd.Parameters.AddWithValue("@citizenPics", img);
                    cmd.Parameters.AddWithValue("@jobs", txtJobs.Text);
                    cmd.Parameters.AddWithValue("@addr", txtAddr.Text);
                    cmd.Parameters.AddWithValue("@phoneNums", txtPhoneNums.Text);

                    cmd.ExecuteNonQuery();

                    MessageBox.Show(messageBox + "ຜູ້ໃຊ້ງານສຳເລັດ!", "ແຈ້ງເຕືອນ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    new open_child_form().OpenChildForm(new resident_management());

                }
                catch (Exception ex)
                {
                    MessageBox.Show("ຂໍອະໄພ, ລະບົບຂັດຂ້ອງ\n" + ex.Message, "ແຈ້ງເຕືອນ", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ຂໍອະໄພ, ລະບົບຂັດຂ້ອງ\n" + ex.Message, "ແຈ້ງເຕືອນ", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void profilePictureBox_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "Choose img(*.jpg; *.png; *.gif; *.bmp)| *.jpg; *.png; *.gif; *.bmp";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                profilePictureBox.Image = Image.FromFile(openFileDialog.FileName);
            }
        }

        private void linkLableBack_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new open_child_form().OpenChildForm(new resident_management());
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            new open_child_form().OpenChildForm(new resident_management());
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            switch (btnSave.Text)
            {
                case "ບັນທຶກ":

                    dataChange("INSERT INTO tb_citizen VALUES (@citizenID, @name, @surname, @gender, @dob, @nationality, @race,  @ethnic, @religion, @dad_name, @mom_name, @family_book, @workplace, @addr, @phoneNums, @jobs);", "ເພີ່ມ");

                    break;

                case "ແກ້ໄຂ":

                    DialogResult result = MessageBox.Show("ທ່ານຕ້ອງການແກ້ໄຂຂໍ້ມູນນີ້ບໍ?", "ແຈ້ງເຕືອນ", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (result == DialogResult.Yes)
                    {
                        dataChange("UPDATE tb_citizen set " +
                            "citizen_name=@name, " +
                            "citizen_lastname=@surname, " +
                            "citizen_gender=@gender, " +
                            "citizen_dob=@dob, " +
                            "citizen_nationality=@nationality, " +
                            "citizen_race=@race, " +
                            "citizen_ethnic=@ethnic, " +
                            "citizen_religion=@religion, " +
                            "citizen_dad=@dad_name, " +
                            "citizen_mom=@mom_name, " +
                            "fambook_nums=@family_book, " +
                            "workplace=@workplace, " +
                            "addrs=@addr, " +
                            "tel=@phoneNums," +
                            "jobs=@jobs WHERE citizen_id=@citizenID;", "ແກ້ໄຂ");
                    }

                    break;
            }
        }

        private void txtName_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtCitizenID_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtSurname_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtReligious_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtNationality_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtPhoneNums_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtRace_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtEthnic_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtJobs_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtFamBookNums_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtDadName_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtMomName_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtAddr_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtDistrict_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void guna2TextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtWorkplace_KeyPress(object sender, KeyPressEventArgs e)
        {

        }
    }
}
