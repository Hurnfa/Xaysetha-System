using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Xaysetha_System
{
    public partial class user_management_add : Form
    {
        NpgsqlCommand cmd;
        db_connect cn = new db_connect();
        DataTable datatable = new DataTable();

        private Form activeForm = null;

        public user_management_add()
        {
            InitializeComponent();
            panelUser.AutoScroll = true;
            cn.getConnect();
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
                MessageBox.Show("ຂໍອະໄພ, ລະບົບຂັດຂ້ອງ\n"+ex.Message, "ແຈ້ງເຕືອນ", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
    }
}
