using Npgsql;
using System;
using System.Windows.Forms;

namespace Xaysetha_System
{
    public partial class fee_add : Form
    {
        public int id;
        db_connect cn = new db_connect();
        NpgsqlCommand cmd;

        public fee_add()
        {
            InitializeComponent();
            cn.getConnect();
        }

        void dataChange(string sql, string messageBox)
        {
            cmd = new NpgsqlCommand(sql, cn.conn);

            try
            {
                cmd.Parameters.AddWithValue("@pkgID", id);
                cmd.Parameters.AddWithValue("@pkgName", txtName.Text);
                cmd.Parameters.AddWithValue("@pkgDuration", numberPickerDuration.Value);
                cmd.Parameters.AddWithValue("@pkgPrice", float.Parse(txtPrice.Text));

                cmd.ExecuteNonQuery();

                MessageBox.Show(messageBox + "ຄ່າທຳນຽມສຳເລັດ!", "ແຈ້ງເຕືອນ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtName.Clear();
                txtPrice.Clear();
                numberPickerDuration.Value = 0;

            }
            catch (Exception ex)
            {
                MessageBox.Show("ຂໍອະໄພ, ລະບົບຂັດຂ້ອງ\n" + ex.Message, "ແຈ້ງເຕືອນ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Hide();
        }

        public void fetchDataFromMainPage(int pkgID, string name, int duration, string price, string task)
        {
            id = pkgID;
            txtName.Text = name;
            numberPickerDuration.Value = duration;
            txtPrice.Text = price;
            btnSave.Text = task;
            labelHeader.Text = $"{task}ຄ່າທຳນຽມ";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            switch (btnSave.Text)
            {
                case "ເພີ່ມ":

                    dataChange("INSERT INTO tb_package (pkg_name, pkg_duration, pkg_price) VALUES (@pkgName, @pkgDuration, @pkgPrice)", btnSave.Text);

                break;

                case "ແກ້ໄຂ":

                    DialogResult result = MessageBox.Show("ທ່ານຕ້ອງການແກ້ໄຂຂໍ້ມູນນີ້ບໍ?", "ແຈ້ງເຕືອນ", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (result == DialogResult.Yes)
                    {
                        dataChange("UPDATE tb_package SET pkg_name=@pkgName, pkg_duration=@pkgDuration, pkg_price=@pkgPrice WHERE pkg_id=@pkgID", btnSave.Text);

                    }

                break;
            }
        }
    }
}
