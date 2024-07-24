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

            cmd = new NpgsqlCommand("SELECT * FROM tb_citizen WHERE \"citizenID\"=@citizenID", cn.conn);

            cmd.Parameters.AddWithValue("@citizenID", BigInteger.Parse(citizen_id));

            NpgsqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                datePickerBirthday.Value = (DateTime)reader["dob"];
                txtRace.Text = reader["race"].ToString();
                txtNationality.Text = reader["nationality"].ToString();
                txtEthnic.Text = reader["ethnic"].ToString();
                txtReligious.Text = reader["religion"].ToString();
                txtDadName.Text = reader["dad_name"].ToString();
                txtMomName.Text = reader["mom_name"].ToString();
                txtFamBookNums.Text = reader["family_book"].ToString();
                txtWorkplace.Text = reader["workplace"].ToString();
                txtPhoneNums.Text = reader["phoneNums"].ToString();

                if (reader["citizenPics"] == DBNull.Value)
                {
                    profilePictureBox.Image = null;
                }
                else
                {
                    byte[] img = (byte[])reader["citizenPics"];
                    MemoryStream memory = new MemoryStream(img);
                    profilePictureBox.Image = Image.FromStream(memory);
                }
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

        public void dataChange(string sql, string messageBox)
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

            MemoryStream memoryStream = new MemoryStream();

            profilePictureBox.Image.Save(memoryStream, profilePictureBox.Image.RawFormat);

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
                cmd.Parameters.AddWithValue("@citizenPics", memoryStream.ToArray());
                cmd.Parameters.AddWithValue("@occupation", txtJobs.Text);
                cmd.Parameters.AddWithValue("@addr", txtAddr.Text);
                cmd.Parameters.AddWithValue("@phoneNums", int.Parse(txtPhoneNums.Text));

                cmd.ExecuteNonQuery();

                MessageBox.Show(messageBox + "ຜູ້ໃຊ້ງານສຳເລັດ!", "ແຈ້ງເຕືອນ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                new open_child_form().OpenChildForm(new resident_management());

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

                    dataChange("INSERT INTO tb_citizen VALUES (@citizenID, @name, @surname, @gender, @dob, @race, @nationality, @ethnic, @religion, @dad_name, @mom_name, @family_book, @workplace, @citizenPics, @occupation, @addr, @phoneNums);", "ເພີ່ມ");

                    break;

                case "ແກ້ໄຂ":

                    DialogResult result = MessageBox.Show("ທ່ານຕ້ອງການແກ້ໄຂຂໍ້ມູນນີ້ບໍ?", "ແຈ້ງເຕືອນ", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (result == DialogResult.Yes)
                    {
                        dataChange("UPDATE tb_citizen set name=@name, surname=@surname, gender=@gender, dob=@dob, race=@race, nationality=@nationality, ethnic=@ethnic, religion=@religion, dad_name=@dad_name, mom_name=@mom_name, family_book=@family_book, workplace=@workplace, \"citizenPics\"=@citizenPics, occupation=@occupation, addr=@addr, \"phoneNums\"=@phoneNums WHERE \"citizenID\"=@citizenID;", "ແກ້ໄຂ");
                    }

                    break;
            }
        }

        private void txtName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtCitizenID_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtSurname_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtReligious_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtNationality_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
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
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtEthnic_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtJobs_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtFamBookNums_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtDadName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtMomName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtAddr_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtDistrict_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void guna2TextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtWorkplace_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
