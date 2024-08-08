using Npgsql;
using System;
using System.Data;
using System.Windows.Forms;

namespace Xaysetha_System
{
    public partial class city_management_add : Form
    {
        NpgsqlCommand cmd;
        NpgsqlDataAdapter adapter;
        db_connect cn = new db_connect();
        public int id;

        public city_management_add()
        {
            InitializeComponent();
            cn.getConnect();
            cbProvinceLoad();
        }

        void cbProvinceLoad()
        {
            adapter = new NpgsqlDataAdapter("SELECT * FROM tb_province", cn.conn);
            DataSet dsProvince = new DataSet();
            adapter.Fill(dsProvince);

            cbProvince.DataSource = dsProvince.Tables[0];

            cbProvince.DisplayMember = "province_name";
            cbProvince.ValueMember = "province_id";
        }

        public void dataChange(string sql, string messageBox)
        {
            switch (messageBox)
            {
                case "ເພີ່ມ":

                    cmd = new NpgsqlCommand("select COUNT(*) from tb_district;", cn.conn);

                    id = int.Parse(cmd.ExecuteScalar().ToString());

                    break;

            }
            //cmd = new NpgsqlCommand(sql, cn.conn);

            if (int.TryParse(cbProvince.SelectedValue?.ToString(), out int provinceID))
            {
                try
                {
                    using (var cmd = new NpgsqlCommand(sql, cn.conn))
                    {
                        cmd.Parameters.AddWithValue("@districtID", id);
                        cmd.Parameters.AddWithValue("@districtName", txtVillage.Text);
                        cmd.Parameters.AddWithValue("@provinceID", provinceID);

                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show(messageBox + "ເມືອງສຳເລັດ!", "ແຈ້ງເຕືອນ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    txtVillage.Clear();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("ຂໍອະໄພ, ລະບົບຂັດຂ້ອງ\n" + ex.Message, "ແຈ້ງເຕືອນ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                // Handle the case where parsing fails, e.g., by clearing the combobox or showing an error
                //comboboxDistrict.DataSource = null;
            }
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnClose_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        public void fetchDataFromMainPage(int districtID, string districtName, string task)
        {
            this.id = districtID;
            txtVillage.Text = districtName;

            string provinceName = string.Empty;

            // Use a parameterized query to avoid SQL injection
            cmd = new NpgsqlCommand("SELECT province_name FROM tb_province JOIN tb_district ON tb_province.province_id = tb_district.province_id WHERE tb_district.district_name = @districtName", cn.conn);
            cmd.Parameters.AddWithValue("@districtName", districtName);

            object result = cmd.ExecuteScalar();
            if (result != null)
            {
                provinceName = result.ToString();
            }

            cbProvince.Text = provinceName;

            labelHeader.Text = $"{task}ຊື່ເມືອງ";
            btnSave.Text = task;

        }

        private void btnSave_Click(object sender, System.EventArgs e)
        {
            switch (btnSave.Text)
            {
                case "ເພີ່ມ":

                    dataChange("INSERT INTO tb_district (district_id, district_name, province_id) VALUES (@districtID, @districtName, @provinceID)", btnSave.Text);

                break;

                case "ແກ້ໄຂ":

                    DialogResult result = MessageBox.Show("ທ່ານຕ້ອງການແກ້ໄຂຂໍ້ມູນນີ້ບໍ?", "ແຈ້ງເຕືອນ", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (result == DialogResult.Yes)
                    {
                        dataChange("UPDATE tb_district SET district_name=@districtName, province_id=@provinceID WHERE district_id=@districtID", btnSave.Text);
                        //dataChange("UPDATE tb_village SET \"villageName\"=@villageName WHERE \"villageID\"=" + label_id.Text + ";", "ແກ້ໄຂ");
                    }

                break;
            }
        }
    }
}
