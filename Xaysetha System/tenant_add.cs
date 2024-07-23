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
                new open_child_form().OpenChildForm(new entrance_management());
                Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ຂໍອະໄພ, ລະບົບຂັດຂ້ອງ\n" + ex.Message, "ແຈ້ງເຕືອນ", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        //public BigInteger GenerateUniquePaymentID()
        //{
        //    BigInteger paymentID = 1;
        //    bool isUnique = false;

        //    while (!isUnique)
        //    {
        //        paymentID = random.Next(100000, 999999); // Adjust the range as needed

        //        using (NpgsqlCommand checkCmd = new NpgsqlCommand("SELECT COUNT(*) FROM tb_payment WHERE payment_id = @paymentID", cn.conn))
        //        {
        //            checkCmd.Parameters.AddWithValue("@paymentID", paymentID);
        //            BigInteger count = BigInteger.Parse(checkCmd.ExecuteScalar().ToString());

        //            if (count == 0)
        //            {
        //                isUnique = true;
        //            }
        //        }
        //    }

        //    return paymentID;
        //}

        void sentDataToPayment(string sql, string messegeBox)
        {
            //BigInteger paymentID = GenerateUniquePaymentID();
            BigInteger tenantID = BigInteger.Parse(txtTenantID.Text);
            Random random = new Random();

            try
            {

                NpgsqlCommand cmd = new NpgsqlCommand(sql, cn.conn);

                cmd.Parameters.AddWithValue("@paymentID", random.Next());
                cmd.Parameters.AddWithValue("@tenantID", tenantID);
                cmd.Parameters.AddWithValue("@duration", 0);
                cmd.Parameters.AddWithValue("price", 0);
                cmd.Parameters.AddWithValue("@paymentStatus", "ລໍຖ້າຊຳລະ");
                cmd.Parameters.AddWithValue("@userID", "admin");

                cmd.ExecuteNonQuery();

                //MessageBox.Show(messegeBox + "ຜູ້ໃຊ້ງານສຳເລັດ!", "ແຈ້ງເຕືອນ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //new open_child_form().OpenChildForm(new entrance_management());
                //Hide();
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
    }
}
