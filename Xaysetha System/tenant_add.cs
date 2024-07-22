using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Xaysetha_System
{
    public partial class tenant_add : Form
    {
        public tenant_add()
        {
            InitializeComponent();
            cn.getConnect();
        }

        NpgsqlCommand cmd;
        db_connect cn = new db_connect();
        DataTable datatable = new DataTable();

        void dataChange(string sql, string messegeBox)
        {
            BigInteger citizenID = BigInteger.Parse(txtTenantID.Text);

            string gender = "ບໍ່ລະບຸ";

            if (rdoMale.Checked)
            {
                gender = "ຊາຍ";
            }
            else if (rdoFemale.Checked)
            {
                gender = "ຍິງ";
            }

            DateTime birthDay = datePickerBirthday.Value;

            MemoryStream memoryStream = new MemoryStream();

            profilePictureBox.Image.Save(memoryStream, profilePictureBox.Image.RawFormat);

            cmd = new NpgsqlCommand(sql, cn.conn);

            try
            {
                cmd.Parameters.AddWithValue("@tenantID", citizenID);
                cmd.Parameters.AddWithValue("@firstname", txtName.Text);
                cmd.Parameters.AddWithValue("@lastname", txtSurname.Text);
                cmd.Parameters.AddWithValue("@gender", gender);
                cmd.Parameters.AddWithValue("@dob", birthDay);
                cmd.Parameters.AddWithValue("@nationality", txtNationality.Text);
                cmd.Parameters.AddWithValue("@occupation", txtJobs.Text);
                cmd.Parameters.AddWithValue("@village", txtVillage.Text);
                cmd.Parameters.AddWithValue("@district", txtDistrict.Text);
                cmd.Parameters.AddWithValue("@province", txtProvince.Text);
                cmd.Parameters.AddWithValue("@fambookID", BigInteger.Parse(txtFamBookID.Text));
                /*1st missing field*/ cmd.Parameters.AddWithValue("@description", "");
                cmd.Parameters.AddWithValue("@famBookIssue", datePickerFamBookIssueDate.Value);
                cmd.Parameters.AddWithValue("@tenantpics", memoryStream.ToArray());

                cmd.ExecuteNonQuery();

                MessageBox.Show(messegeBox + "ຜູ້ໃຊ້ງານສຳເລັດ!", "ແຈ້ງເຕືອນ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //new entrance_management().Show();
                new open_child_form().OpenChildForm(new entrance_management());
                Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ຂໍອະໄພ, ລະບົບຂັດຂ້ອງ\n" + ex.Message, "ແຈ້ງເຕືອນ", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Hide();
            //Dispose();
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            switch (btnSave.Text)
            {
                case "ບັນທຶກ":

                    //dataChange("INSERT INTO tb_citizen VALUES (@citizenID, @name, @surname, @gender, @dob, @race, @nationality, @ethnic, @religion, @dad_name, @mom_name, @family_book, @workplace, @citizenPics, @occupation, @addr, @phoneNums);", "ເພີ່ມ");
                    dataChange("INSERT INTO tb_tenant VALUES (@tenantID, @firstname, @lastname, @gender, @dob, @nationality, @occupation, @village, @district, @province, @fambookID, @description, @famBookIssue, @tenantpics);", "ເພີ່ມ");
                break;

                case "ແກ້ໄຂ":

                    DialogResult result = MessageBox.Show("ທ່ານຕ້ອງການແກ້ໄຂຂໍ້ມູນນີ້ບໍ?", "ແຈ້ງເຕືອນ", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (result == DialogResult.Yes)
                    {
                        //dataChange("UPDATE tb_citizen set name=@name, surname=@surname, gender=@gender, dob=@dob, race=@race, nationality=@nationality, ethnic=@ethnic, religion=@religion, dad_name=@dad_name, mom_name=@mom_name, family_book=@family_book, workplace=@workplace, \"citizenPics\"=@citizenPics, occupation=@occupation, addr=@addr, \"phoneNums\"=@phoneNums WHERE \"citizenID\"=@citizenID;", "ແກ້ໄຂ");
                    }

                break;
            }
        }
    }
}
