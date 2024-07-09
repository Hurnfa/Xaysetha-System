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
    public partial class village_management_add : Form
    {
        NpgsqlCommand cmd;
        db_connect cn = new db_connect();
        DataTable datatable = new DataTable();
        public int id;

        void dataChange(string sql)
        {
            cmd = new NpgsqlCommand("select \"villageID\" from tb_village order by \"villageID\" desc limit 1;", cn.conn);

            NpgsqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                id = Convert.ToInt32(reader["villageID"]) + 1;
            }

            reader.Close();

            cmd = new NpgsqlCommand(sql, cn.conn);

            try
            {
                cmd.Parameters.AddWithValue("@villageID", id);
                cmd.Parameters.AddWithValue("@villageName", txtVillage.Text);

                cmd.ExecuteNonQuery();

                MessageBox.Show("ເພີ່ມບ້ານສຳເລັດ!", "ແຈ້ງເຕືອນ", MessageBoxButtons.OK, MessageBoxIcon.Information);

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

        public village_management_add()
        {
            InitializeComponent();
            cn.getConnect();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            dataChange("INSERT INTO tb_village (\"villageID\", \"villageName\") VALUES (@villageID, @villageName);");
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Hide();
        }
    }
}
