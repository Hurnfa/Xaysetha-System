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

        public void fetchDataFromMainPage(
            string tenant_id,
            string name,
            string surname,
            string gender,
            string occupation,
            string addr,
            string header
            )
        {
            txtTenantID.Text = tenant_id;
            txtName.Text = name;
            txtSurname.Text = surname;

            rdoMale.Checked = gender == "ຊາຍ";
            rdoFemale.Checked = gender == "ຍິງ";
            rdoOthers.Checked = !(rdoMale.Checked || rdoFemale.Checked);

            //txtJobs.Text = occupation;
            txtProvince.Text = addr;

            cmd = new NpgsqlCommand("SELECT * FROM tb_tenant WHERE \"tenantID\"=@tenantID", cn.conn);

            cmd.Parameters.AddWithValue("@citizenID", BigInteger.Parse(tenant_id));

            NpgsqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                datePickerBirthday.Value = (DateTime)reader["dob"];
                //txtRace.Text = reader["race"].ToString();
                txtNationality.Text = reader["nationality"].ToString();
                //txtEthnic.Text = reader["ethnic"].ToString();
                //txtReligious.Text = reader["religion"].ToString();
                //txtDadName.Text = reader["dad_name"].ToString();
                //txtMomName.Text = reader["mom_name"].ToString();
                txtFamBookID.Text = reader["family_book"].ToString();
                //txtWorkplace.Text = reader["workplace"].ToString();
                //txtPhoneNums.Text = reader["phoneNums"].ToString();

                if (reader["tenantpics"] == DBNull.Value)
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
            //txtAddr.Text = addr;
            //txtPhoneNums.Text = phoneNums;

            label_header.Text = "ຟອມ"+header + "ຂໍ້ມູນຜູ້ພັກເຊົ່າ";
            btnSave.Text = header;
        }

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
