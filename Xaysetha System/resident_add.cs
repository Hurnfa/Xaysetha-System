using Npgsql;
using System;
using System.Data;
using System.Drawing;
using System.IO;
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
            cn.getConnect();
        }

        public void displayTotalOfUser()
        {
            cmd = new NpgsqlCommand("SELECT COUNT(*) FROM tb_citizen;", cn.conn);
            NpgsqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                total = int.Parse(reader["count"].ToString());
            }

            reader.Close();
        }

        public void dataChange(string sql, string messageBox)
        {
            displayTotalOfUser();

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
                cmd.Parameters.AddWithValue("@citizenID", txtCitizenID.Text);
                cmd.Parameters.AddWithValue("@name", txtName.Text);
                cmd.Parameters.AddWithValue("@surname", txtSurname.Text);
                cmd.Parameters.AddWithValue("@gender", gender);
                cmd.Parameters.AddWithValue("@dob", birthDay);
                /*2nd missing field*/
                cmd.Parameters.AddWithValue("@race", txtRace.Text);
                cmd.Parameters.AddWithValue("@nationality", txtNationality.Text);
                /*3rd missing field*/cmd.Parameters.AddWithValue("@ethnic", txtEthnic.Text);
                cmd.Parameters.AddWithValue("@religion", txtReligious.Text);
                cmd.Parameters.AddWithValue("@dad_name", txtDadName.Text);
                cmd.Parameters.AddWithValue("@mom_name", txtAddr.Text);
                cmd.Parameters.AddWithValue("@family_book", int.Parse(txtFamBookNums.Text));
                /*4th missing field*/cmd.Parameters.AddWithValue("@workplace", txtWorkplace.Text);
                cmd.Parameters.AddWithValue("@citizenPics", memoryStream.ToArray());
                cmd.Parameters.AddWithValue("@occupation", txtJobs.Text);
                cmd.Parameters.AddWithValue("@addr", txtWorkplace.Text);
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
            dataChange("INSERT INTO tb_citizen (\"citizenID\", name, surname, gender, dob, race, nationality, religion, dad_name, mom_name, family_book, \"citizenPics\", occupation, addr, \"phoneNums\") VALUES (@citizenID, @name, @surname, @gender, @dob, @race, @nationality, @religion, @dad_name, @mom_name, @family_book, @citizenPics, @occupation, @addr, @phoneNums);", "ເພີ່ມ");
        }
    }
}
