using Npgsql;
using System;
using System.Data;
using System.Windows.Forms;

namespace Xaysetha_System
{
    public partial class user_management_add : Form
    {
        NpgsqlCommand cmd;
        db_connect cn = new db_connect();
        DataTable datatable = new DataTable();

        private Form activeForm = null;
        private readonly string userID;
        public user_management_add(string userID)
        {
            InitializeComponent();
            panelUser.AutoScroll = true;
            cn.getConnect();
            this.userID = userID;
            // Define your SQL query with parameters
            string query = "SELECT userID, userName, userLName, gender, role, phoneNums, userPassword FROM tb_user WHERE userID = @userID";

            // Create a SqlCommand with parameters
            NpgsqlCommand command = new NpgsqlCommand(query, cn.conn);

            // Add parameters to the command
            command.Parameters.AddWithValue("@userID", userID);

            // Execute the query and retrieve data
            NpgsqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                // Assuming you have textboxes named txtUserID, txtUserName, txtUserLName, txtGender, txtRole, txtPhoneNums, txtUserPassword
                txtUsername.Text = reader.GetValue(0).ToString();
                txtName.Text = reader.GetValue(1).ToString();
                txtSurname.Text = reader.GetValue(2).ToString();
                string gender = reader.GetValue(3).ToString();
                rdoMale.Checked = gender == "ຊາຍ";
                rdoFemale.Checked = gender == "ຍິງ";
                rdoOthers.Checked = !(rdoMale.Checked || rdoFemale.Checked);
                txtRole.Text = reader.GetValue(5).ToString();
                txtPassword.Text = reader.GetValue(6).ToString();
            }


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

        void dataChange(string sql)
        {
            string gender = "ບໍ່ລະບຸ";

            if (rdoMale.Checked)
            {
                gender = "ຊາຍ";
            }
            else if (rdoFemale.Checked)
            {
                gender = "ຍິງ";
            }

            cmd = new NpgsqlCommand(sql, cn.conn);

            try
            {
                cmd.Parameters.AddWithValue("@userID", txtUsername.Text);
                cmd.Parameters.AddWithValue("@userName", txtName.Text);
                cmd.Parameters.AddWithValue("@userLName", txtSurname.Text);
                cmd.Parameters.AddWithValue("@gender", gender);
                cmd.Parameters.AddWithValue("@role", txtRole.Text);
                cmd.Parameters.AddWithValue("@phoneNums", int.Parse(txtPhoneNums.Text));
                cmd.Parameters.AddWithValue("@userPassword", txtPassword.Text);

                cmd.ExecuteNonQuery();

                MessageBox.Show("ເພີ່ມຜູ້ໃຊ້ງານສຳເລັດ!", "ແຈ້ງເຕືອນ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                OpenChildForm(new user_management());
            }
            catch (Exception ex)
            {
                MessageBox.Show("ຂໍອະໄພ, ລະບົບຂັດຂ້ອງ\n" + ex.Message, "ແຈ້ງເຕືອນ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            OpenChildForm(new user_management());
        }

        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            /*            if (chkShowPassword.Checked)
                        {
                            txtPassword.UseSystemPasswordChar = false;
                        }
                        else
                        {
                            txtPassword.UseSystemPasswordChar = true;
                        }*/
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            dataChange("INSERT INTO tb_user VALUES(@userID, @userName, @userLName, @phoneNums, @userPassword, @gender, @role);");
        }

        private void labelLinkBackToAddUser_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenChildForm(new user_management());
        }
    }
}
