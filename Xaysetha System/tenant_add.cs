using Npgsql;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Numerics;
using System.Windows.Forms;

namespace Xaysetha_System
{
    public partial class tenant_add : Form
    {

        public tenant_add(string username)
        {
            InitializeComponent();
            cn.getConnect();
            datePickerBirthday.MaxDate = DateTime.Now.AddYears(-18);
            datePickerFamBookIssueDate.MaxDate = DateTime.Now.AddDays(-7);
            label_username.Text = username;
        }

        NpgsqlCommand cmd;
        db_connect cn = new db_connect();
        DataTable datatable = new DataTable();

        public void fetchDataFromMainPage(
            string tenant_id,
            string name,
            string surname,
            string gender,
            string village,
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


            cmd = new NpgsqlCommand("SELECT * FROM tb_tenant WHERE \"tenantID\"=@tenantID", cn.conn);

            cmd.Parameters.AddWithValue("@tenantID", BigInteger.Parse(tenant_id));

            NpgsqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                datePickerBirthday.Value = (DateTime)reader["dob"];
                txtNationality.Text = reader["nationality"].ToString();
                txtEthnic.Text = reader["ethnics"].ToString();
                txtReligion.Text = reader["religion"].ToString();
                txtFamBookID.Text = reader["fambookID"].ToString();
                datePickerFamBookIssueDate.Value = (DateTime)reader["famBookIssueDate"];
                txtDistrict.Text = reader["district"].ToString();
                txtPhoneNums.Text = reader["phoneNums"].ToString();
                txtJobs.Text = reader["occupation"].ToString();
                txtVillage.Text = reader["village"].ToString();

                if (reader["tenantpics"] == DBNull.Value)
                {
                    profilePictureBox.Image = null;
                }
                else
                {
                    byte[] img = (byte[])reader["tenantpics"];
                    MemoryStream memory = new MemoryStream(img);
                    profilePictureBox.Image = Image.FromStream(memory);
                }
            }

            reader.Close();



            label_header.Text = "ຟອມ" + header + "ຂໍ້ມູນຜູ້ພັກເຊົ່າ";
            btnSave.Text = header;
        }

        void dataChange(string sql, string messegeBox)
        {
            BigInteger tenantID = BigInteger.Parse(txtTenantID.Text);

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
                cmd.Parameters.AddWithValue("@tenantID", tenantID);
                cmd.Parameters.AddWithValue("@firstname", txtName.Text);
                cmd.Parameters.AddWithValue("@lastname", txtSurname.Text);
                cmd.Parameters.AddWithValue("@gender", gender);
                cmd.Parameters.AddWithValue("@nationality", txtNationality.Text);
                cmd.Parameters.AddWithValue("@religion", txtReligion.Text);
                cmd.Parameters.AddWithValue("@dob", birthDay);
                cmd.Parameters.AddWithValue("@occupation", txtJobs.Text);
                cmd.Parameters.AddWithValue("@phoneNums", txtPhoneNums.Text);
                cmd.Parameters.AddWithValue("@ethnics", txtEthnic.Text);
                cmd.Parameters.AddWithValue("@fambookID", BigInteger.Parse(txtFamBookID.Text));
                cmd.Parameters.AddWithValue("@description", "");
                cmd.Parameters.AddWithValue("@famBookIssueDate", datePickerFamBookIssueDate.Value);
                cmd.Parameters.AddWithValue("@village", txtVillage.Text);
                cmd.Parameters.AddWithValue("@district", txtDistrict.Text);
                cmd.Parameters.AddWithValue("@tenantpics", memoryStream.ToArray());

                cmd.ExecuteNonQuery();

                MessageBox.Show(messegeBox + "ຜູ້ໃຊ້ງານສຳເລັດ!", "ແຈ້ງເຕືອນ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //new entrance_management().Show();
                new open_child_form().OpenChildForm(new entrance_management(label_username.Text));
                //Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ຂໍອະໄພ, ລະບົບຂັດຂ້ອງ\n" + ex.Message, "ແຈ້ງເຕືອນ", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
        void sentDataToPayment(string sql, string messegeBox)
        {
            BigInteger tenantID = BigInteger.Parse(txtTenantID.Text),
                paymentID = new Random().Next();
            try
            {

                NpgsqlCommand cmd = new NpgsqlCommand(sql, cn.conn);



                cmd.Parameters.AddWithValue("@paymentID", paymentID);
                cmd.Parameters.AddWithValue("@tenantID", tenantID);
                cmd.Parameters.AddWithValue("@duration", 0);
                cmd.Parameters.AddWithValue("price", 0);
                cmd.Parameters.AddWithValue("@paymentStatus", "ລໍຖ້າຊຳລະ");
                cmd.Parameters.AddWithValue("@userID", label_username.Text);

                cmd.ExecuteNonQuery();

                printing loadBill = new printing();

                loadBill.loadDataToReport(paymentID, txtName.Text, txtSurname.Text, 0, 0, label_username.Text);

                loadBill.Show();
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
                    dataChange("INSERT INTO tb_tenant VALUES (@tenantID, @firstname, @lastname, @gender, @dob, @nationality, @occupation, @village, @district, @fambookID, @description, @famBookIssueDate, @tenantpics, @religion, @phoneNums, @ethnics);", "ເພີ່ມ");
                    sentDataToPayment("INSERT INTO tb_payment (payment_id, tenant_id, duration, price, payment_status, user_id) VALUES (@paymentID, @tenantID, @duration, @price, @paymentStatus, @userID)", "ເພີ່ມ");
                    break;

                case "ແກ້ໄຂ":

                    DialogResult result = MessageBox.Show("ທ່ານຕ້ອງການແກ້ໄຂຂໍ້ມູນນີ້ບໍ?", "ແຈ້ງເຕືອນ", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (result == DialogResult.Yes)
                    {
                        dataChange("UPDATE tb_tenant set firstname=@firstname, lastname=@lastname, gender=@gender, dob=@dob, nationality=@nationality, occupation=@occupation, village=@village, district=@district, \"fambookID\"=@fambookID, description=@description, \"famBookIssueDate\"=@famBookIssueDate, tenantpics=@tenantpics, religion=@religion, \"phoneNums\"=@phoneNums, ethnics=@ethnics WHERE \"tenantID\"=@tenantID", "ແກ້ໄຂ");
                    }

                    break;
            }
        }

        private void txtTenantID_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
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

        private void txtNationality_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtReligion_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtEthnic_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtFamBookID_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtVillage_KeyPress(object sender, KeyPressEventArgs e)
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
    }
}

