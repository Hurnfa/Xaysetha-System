using Npgsql;
using System;
using System.Windows.Forms;

namespace Xaysetha_System
{
    public partial class village_management_add : Form
    {
        NpgsqlCommand cmd;
        db_connect cn = new db_connect();
        //Before pushing
        //DataTable datatable = new DataTable();
        public int id;

        public void dataChange(string sql, string messageBox)
        {
            cmd = new NpgsqlCommand("SELECT \"villageName\" FROM tb_village WHERE \"villageName\"=@villageName;", cn.conn);

            cmd.Parameters.AddWithValue("@villageName", txtVillage.Text);

            string output = (string)cmd.ExecuteScalar();

            NpgsqlDataReader reader;

            if (output == txtVillage.Text)
            {
                MessageBox.Show("ຂໍ້ມູນດັ່ງກ່າວມີຢູ່ໃນລະບົບແລ້ວ!", "ແຈ້ງເຕືອນ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                switch (messageBox)
                {
                    case "ເພີ່ມ":

                        cmd = new NpgsqlCommand("select \"villageID\" from tb_village order by \"villageID\" desc limit 1;", cn.conn);

                        reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            id = Convert.ToInt32(reader["villageID"]) + 1;
                        }

                        reader.Close();

                        break;

                }
                cmd = new NpgsqlCommand(sql, cn.conn);

                try
                {
                    cmd.Parameters.AddWithValue("@villageID", id);
                    cmd.Parameters.AddWithValue("@villageName", txtVillage.Text);

                    cmd.ExecuteNonQuery();

                    MessageBox.Show(messageBox + "ບ້ານສຳເລັດ!", "ແຈ້ງເຕືອນ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    /*                new village_management().Show();

                                    Hide();*/

                    txtVillage.Clear();

                    //OpenChildForm(new user_management());
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ຂໍອະໄພ, ລະບົບຂັດຂ້ອງ\n" + ex.Message, "ແຈ້ງເຕືອນ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        /*        public village_management_add(string cellData)
                {
                    InitializeComponent();
                    cn.getConnect();
                    txtVillage.Text = cellData;
                }*/

        public void changeInsertToUpdate(string header, string button, string cellData, int vilID)
        {
            labelHeader.Text = header;
            btnSave.Text = button;
            txtVillage.Text = cellData;
            label_id.Text = vilID.ToString();
        }

        public village_management_add()
        {
            InitializeComponent();
            cn.getConnect();
            getID();
        }

        void getID()
        {
            if (txtVillage.TextLength != 0 && btnSave.Text == "ແກ້ໄຂ")
            {
                cmd = new NpgsqlCommand("SELECT \"villageID\" FROM tb_village WHERE \"villageName\"=@villageName;", cn.conn);

                cmd.Parameters.AddWithValue("@villageName", txtVillage.Text);

                NpgsqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    label_id.Text = reader["villageID"].ToString();
                }

                reader.Close();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtVillage.Text != "")
            {
                switch (btnSave.Text)
                {
                    case "ເພີ່ມ":

                        dataChange("INSERT INTO tb_village (\"villageID\", \"villageName\") VALUES (@villageID, @villageName);", "ເພີ່ມ");

                        break;

                    case "ແກ້ໄຂ":

                        DialogResult result = MessageBox.Show("ທ່ານຕ້ອງການແກ້ໄຂຂໍ້ມູນນີ້ບໍ?", "ແຈ້ງເຕືອນ", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                        if (result == DialogResult.Yes)
                        {
                            dataChange("UPDATE tb_village SET \"villageName\"=@villageName WHERE \"villageID\"=" + label_id.Text + ";", "ແກ້ໄຂ");
                        }

                        break;
                }
            }
            else
            {
                MessageBox.Show("ກະລຸນາເພີ່ມຂໍ້ມູນໃສ່ກ່ອນ", "ແຈ້ງເຕືອນ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Hide();
        }
    }
}
