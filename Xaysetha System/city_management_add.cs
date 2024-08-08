using Npgsql;
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

        public void dataChange()
        {

        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnClose_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, System.EventArgs e)
        {
            switch (btnSave.Text)
            {
                case "ເພີ່ມ":



                break;

                case "ແກ້ໄຂ":

                    DialogResult result = MessageBox.Show("ທ່ານຕ້ອງການແກ້ໄຂຂໍ້ມູນນີ້ບໍ?", "ແຈ້ງເຕືອນ", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (result == DialogResult.Yes)
                    {
                        //dataChange("UPDATE tb_village SET \"villageName\"=@villageName WHERE \"villageID\"=" + label_id.Text + ";", "ແກ້ໄຂ");
                    }

                break;
            }
        }
    }
}
