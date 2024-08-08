using Npgsql;
using System;
using System.ComponentModel.Design;
using System.Windows.Forms;

namespace Xaysetha_System
{
    public partial class province_management_add : Form
    {
        NpgsqlCommand cmd;
        db_connect cn = new db_connect();
        public int id;

        public province_management_add()
        {
            InitializeComponent();
            cn.getConnect();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Hide();
        }

        void dataChange(string sql, string messageBox)
        {
            cmd = new NpgsqlCommand("SELECT province_name FROM tb_province WHERE province_name=@provinceName;", cn.conn);

            cmd.Parameters.AddWithValue("@provinceName", txtVillage.Text);

            string output = (string)cmd.ExecuteScalar();

            if (output == txtVillage.Text)
            {
                MessageBox.Show("ຂໍ້ມູນດັ່ງກ່າວມີຢູ່ໃນລະບົບແລ້ວ!", "ແຈ້ງເຕືອນ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                switch (messageBox)
                {
                    case "ເພີ່ມ":

                        cmd = new NpgsqlCommand("select COUNT(*) from tb_province", cn.conn);

                        id = int.Parse(cmd.ExecuteScalar().ToString())+1;

/*                        reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            id = Convert.ToInt32(reader["villageID"]) + 1;
                        }

                        reader.Close();*/

                        break;

                }
                cmd = new NpgsqlCommand(sql, cn.conn);

                try
                {
                    cmd.Parameters.AddWithValue("@provinceID", id);
                    cmd.Parameters.AddWithValue("@provinceName", txtVillage.Text);

                    cmd.ExecuteNonQuery();

                    MessageBox.Show(messageBox + "ແຂວງສຳເລັດ!", "ແຈ້ງເຕືອນ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    txtVillage.Clear();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("ຂໍອະໄພ, ລະບົບຂັດຂ້ອງ\n" + ex.Message, "ແຈ້ງເຕືອນ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public void fetchDataFromMainPage(string id, string name, string task)
        {
            labelHeader.Text = task + "ຊື່ແຂວງ";
            btnSave.Text = task;
            txtVillage.Text = name;
            this.id = int.Parse(id);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtVillage.Text != "")
            {
                switch (btnSave.Text)
                {
                    case "ເພີ່ມ":

                        dataChange("INSERT INTO tb_province (province_id, province_name) VALUES (@provinceID, @provinceName)", btnSave.Text);

                    break;

                    case "ແກ້ໄຂ":

                        DialogResult result = MessageBox.Show("ທ່ານຕ້ອງການແກ້ໄຂຂໍ້ມູນນີ້ບໍ?", "ແຈ້ງເຕືອນ", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                        if (result == DialogResult.Yes)
                        {
                            dataChange("UPDATE tb_province SET province_name = @provinceName WHERE province_id=@provinceID", btnSave.Text);
                        }

                     break;
                }
            }
            else
            {
                MessageBox.Show("ກະລຸນາເພີ່ມຂໍ້ມູນໃສ່ກ່ອນ", "ແຈ້ງເຕືອນ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
