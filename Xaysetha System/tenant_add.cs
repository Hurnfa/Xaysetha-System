using Npgsql;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Numerics;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Xaysetha_System
{
    public partial class tenant_add : Form
    {

        public tenant_add(string username)
        {
            InitializeComponent();
            cn.getConnect();
            cbDistrictLoad();
            datePickerBirthday.MaxDate = DateTime.Now.AddYears(-18);
            datePickerFamBookIssueDate.MaxDate = DateTime.Now.AddDays(-7);
            label_username.Text = username;
        }

        NpgsqlCommand cmd;
        NpgsqlDataAdapter adapter;
        db_connect cn = new db_connect();
        DataTable datatable = new DataTable();

        void cbDistrictLoad()
        {
            adapter = new NpgsqlDataAdapter("SELECT * FROM tb_province;", cn.conn);
            DataSet dsProvince = new DataSet();
            adapter.Fill(dsProvince);

            comboboxProvince.DataSource = dsProvince.Tables[0];

            comboboxProvince.DisplayMember = "province_name";
            comboboxProvince.ValueMember = "province_id";
        }

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


            cmd = new NpgsqlCommand("SELECT * FROM tb_tenant WHERE tenant_id=@tenantID", cn.conn);

            cmd.Parameters.AddWithValue("@tenantID", BigInteger.Parse(tenant_id));

            NpgsqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                datePickerBirthday.Value = (DateTime)reader["tenant_dob"];
                txtNationality.Text = reader["tenant_nationality"].ToString();
                txtEthnic.Text = reader["ethnics"].ToString();
                txtReligion.Text = reader["religion"].ToString();
                txtFamBookID.Text = reader["fambook_nums"].ToString();
                datePickerFamBookIssueDate.Value = (DateTime)reader["fambook_date"];
                comboboxDistrict.Text = reader["district"].ToString();
                txtPhoneNums.Text = reader["tels"].ToString();
                txtJobs.Text = reader["jobs"].ToString();
                txtVillage.Text = reader["village"].ToString();
                comboboxProvince.Text = reader["province"].ToString();

/*                if (reader["tenantpics"] == DBNull.Value)
                {
                    profilePictureBox.Image = null;
                }
                else
                {
                    byte[] img = (byte[])reader["tenantpics"];
                    MemoryStream memory = new MemoryStream(img);
                    profilePictureBox.Image = Image.FromStream(memory);
                }*/
            }

            reader.Close();



            label_header.Text = "ຟອມ" + header + "ຂໍ້ມູນຜູ້ພັກເຊົ່າ";
            btnSave.Text = header;
        }

        public BigInteger tenantID;

        void dataChange(string sql, string messegeBox)
        {
            //BigInteger tenantID;

            if (txtTenantID.Text != "")
            {
                tenantID = BigInteger.Parse(txtTenantID.Text);
            }            

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

/*            MemoryStream memoryStream = new MemoryStream();

            profilePictureBox.Image.Save(memoryStream, profilePictureBox.Image.RawFormat);
*/
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
                cmd.Parameters.AddWithValue("@district", comboboxDistrict.Text);
                //cmd.Parameters.AddWithValue("@tenantpics", memoryStream.ToArray());
                cmd.Parameters.AddWithValue("@province", comboboxProvince.Text);
                cmd.Parameters.AddWithValue("@tenantStatus", "ຍັງບໍ່ໄດ້ອະນຸມັດ");

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

        string title;

        void sentDataToPayment(string sql, string messegeBox)
        {
            cmd = new NpgsqlCommand("SELECT tenant_id FROM tb_tenant ORDER BY tenant_id DESC LIMIT 1", cn.conn);
            tenantID = BigInteger.Parse(cmd.ExecuteScalar().ToString());
;
            BigInteger paymentID = new Random().Next();


            try
            {

                NpgsqlCommand cmd = new NpgsqlCommand(sql, cn.conn);



                cmd.Parameters.AddWithValue("@paymentID", paymentID);
                cmd.Parameters.AddWithValue("@tenantID", tenantID);
                cmd.Parameters.AddWithValue("@paymentStatus", "ລໍຖ້າຊຳລະ");
                cmd.Parameters.AddWithValue("@userID", label_username.Text);
                cmd.Parameters.AddWithValue("@pkgID", 1);
                cmd.Parameters.AddWithValue("price", 30000);

                cmd.ExecuteNonQuery();

                cmd = new NpgsqlCommand("SELECT tenant_gender FROM tb_tenant WHERE tenant_id=" + tenantID, cn.conn);
                cmd.Parameters.AddWithValue("@tenantID", tenantID);
                NpgsqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    title = reader["tenant_gender"].ToString();
                }

                reader.Close();


                printing loadBill = new printing();

                loadBill.loadDataToReport(paymentID, txtName.Text, txtSurname.Text, 30000, 1, label_username.Text, title);

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
                    dataChange(@"INSERT INTO public.tb_tenant(
	                    tenant_name, tenant_lastname, tenant_gender, tenant_dob, tenant_nationality, religion, ethnics, jobs, village, district, province, fambook_nums, fambook_date, tels, tenant_status)
	                    VALUES (@firstname, @lastname, @gender, @dob, @nationality, @religion, @ethnics, @occupation, @village, @district, @province, @fambookID, @famBookIssueDate, @phoneNums, @tenantStatus);", "ເພີ່ມ");

                    //dataChange("INSERT INTO tb_citizen VALUES (@citizenID, @name, @surname, @gender, @dob, @race, @nationality, @ethnic, @religion, @dad_name, @mom_name, @family_book, @workplace, @citizenPics, @occupation, @addr, @phoneNums);", "ເພີ່ມ");
                    //dataChange("INSERT INTO tb_tenant VALUES (@firstname, @lastname, @gender, @dob, @nationality, @religion, @ethnics, @occupation, @village, @district, @province, @fambookID, @famBookIssueDate, @phoneNums, @tenantStatus);", "ເພີ່ມ");

                    sentDataToPayment("INSERT INTO tb_payment (payment_id, tenant_id, pkg_id, price, payment_status, user_id) VALUES (@paymentID, @tenantID, @pkgID, @price, @paymentStatus, @userID)", "ເພີ່ມ");
                    break;

                case "ແກ້ໄຂ":

                    DialogResult result = MessageBox.Show("ທ່ານຕ້ອງການແກ້ໄຂຂໍ້ມູນນີ້ບໍ?", "ແຈ້ງເຕືອນ", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (result == DialogResult.Yes)
                    {
                        dataChange("UPDATE tb_tenant set firstname=@firstname, lastname=@lastname, gender=@gender, dob=@dob, nationality=@nationality, occupation=@occupation, village=@village, district=@district, \"fambookID\"=@fambookID, description=@description, \"famBookIssueDate\"=@famBookIssueDate, tenantpics=@tenantpics, religion=@religion, \"phoneNums\"=@phoneNums, ethnics=@ethnics, province=@province, tenant_status=@tenantStatus WHERE \"tenantID\"=@tenantID", "ແກ້ໄຂ");
                    }

                    break;
            }
        }

        private void txtTenantID_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtName_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtSurname_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtNationality_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtJobs_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtPhoneNums_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
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

        }

        private void txtFamBookID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
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

        }

        private void txtDistrict_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void comboboxProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboboxProvince.SelectedIndexChanged -= comboboxProvince_SelectedIndexChanged;

            DataSet dsDistrict = new DataSet();

            if (int.TryParse(comboboxProvince.SelectedValue?.ToString(), out int provinceID))
            {
                using (var cmd = new NpgsqlCommand("SELECT * FROM tb_district WHERE province_id=@provinceID", cn.conn))
                {
                    cmd.Parameters.AddWithValue("@provinceID", provinceID);
                    using (var adapter = new NpgsqlDataAdapter(cmd))
                    {
                        adapter.Fill(dsDistrict);
                    }
                }

                comboboxDistrict.DataSource = dsDistrict.Tables[0];
                comboboxDistrict.DisplayMember = "district_name";
                comboboxDistrict.ValueMember = "district_id";
            }
            else
            {
                // Handle the case where parsing fails, e.g., by clearing the combobox or showing an error
                comboboxDistrict.DataSource = null;
            }

            //Attach event again
            comboboxProvince.SelectedIndexChanged += comboboxProvince_SelectedIndexChanged;
        }

        private void comboboxProvince_SelectedValueChanged(object sender, EventArgs e)
        {

        }

        private void comboboxDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void linkLabelProvinceChoose_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }
    }
}

