using Npgsql;
using System;
using System.Data;
using System.Windows.Forms;

namespace Xaysetha_System
{

    public partial class Form2 : Form
    {
        db_connect cn = new db_connect();
        NpgsqlCommand cmd;
        NpgsqlDataAdapter adapter;
        DataSet dataSetVillage = new DataSet();

        public Form2()
        {
            InitializeComponent();
            cn.getConnect();
            loadTotalTenant();
            comboVillage1Load();
            comboVillage2Load();
            comboVillage3Load();
            comboVillage4Load();
        }

        void loadTotalTenant()
        {
            cmd = new NpgsqlCommand("SELECT COUNT(*) FROM tb_tenant;", cn.conn);

            label_total_tenant.Text = cmd.ExecuteScalar().ToString();
        }

        void comboVillageLoad()
        {
            adapter = new NpgsqlDataAdapter("SELECT * FROM tb_village;", cn.conn);
            adapter.Fill(dataSetVillage);

            cbVillage1.DataSource = cbVillage2.DataSource = cbVillage3.DataSource = cbVillage4.DataSource = dataSetVillage.Tables[0];

            cbVillage1.DisplayMember = cbVillage2.DisplayMember = cbVillage3.DisplayMember = cbVillage4.DisplayMember = "villageName";
            cbVillage1.ValueMember = cbVillage2.ValueMember = cbVillage3.ValueMember = cbVillage4.ValueMember = "villageID";
        }

        void comboVillage1Load()
        {
            DataSet ds1 = new DataSet();

            adapter = new NpgsqlDataAdapter("SELECT * FROM tb_village;", cn.conn);
            adapter.Fill(ds1);

            cbVillage1.DataSource = ds1.Tables[0];

            cbVillage1.DisplayMember = "villageName";
            cbVillage1.ValueMember = "villageID";
        }

        void comboVillage2Load()
        {
            DataSet ds2 = new DataSet();

            adapter = new NpgsqlDataAdapter("SELECT * FROM tb_village;", cn.conn);
            adapter.Fill(ds2);

            cbVillage2.DataSource = ds2.Tables[0];

            cbVillage2.DisplayMember = "villageName";
            cbVillage2.ValueMember = "villageID";
        }

        void comboVillage3Load()
        {
            DataSet ds3 = new DataSet();

            adapter = new NpgsqlDataAdapter("SELECT * FROM tb_village;", cn.conn);
            adapter.Fill(ds3);

            cbVillage3.DataSource = ds3.Tables[0];

            cbVillage3.DisplayMember = "villageName";
            cbVillage3.ValueMember = "villageID";
        }

        void comboVillage4Load()
        {
            DataSet ds4 = new DataSet();

            adapter = new NpgsqlDataAdapter("SELECT * FROM tb_village;", cn.conn);
            adapter.Fill(ds4);

            cbVillage4.DataSource = ds4.Tables[0];

            cbVillage4.DisplayMember = "villageName";
            cbVillage4.ValueMember = "villageID";
        }

        private void cbVillage2_SelectedIndexChanged(object sender, System.EventArgs e)
        {

            //cmd = new NpgsqlCommand("SELECT v.\"villageID\", v.\"villageName\", COUNT(rb.\"tenantID\") AS tenant_count FROM \"tb_residentialBook\" rb " +
            //    "JOIN tb_place p ON rb.\"placeID\" = p.\"placeID\" JOIN tb_village v ON p.\"villageID\" = v.\"villageID\"" +
            //    "WHERE v.\"villageID\" = @villageID GROUP BY v.\"villageID\", v.\"villageName\";", cn.conn);

            ///*cmd = new NpgsqlCommand("select count(*) from tb_tenant join \"tb_residentialBook\" on tb_tenant.\"tenantID\" = \"tb_residentialBook\".\"tenantID\"" +
            //    "join tb_place on \"tb_residentialBook\".\"placeID\" = tb_place.\"placeID\" " +
            //    "join tb_village on tb_place.\"villageID\" = tb_village.\"villageID\" where tb_village.\"villageID\"=@villageID", cn.conn);*/

            //cmd.Parameters.AddWithValue("@villageID", Convert.ToInt64(cbVillage2.ValueMember.ToString()));

            //NpgsqlDataReader reader2 = cmd.ExecuteReader();

            //while (reader2.Read())
            //{
            //    labelTotal2.Text = reader2["tenant_count"].ToString();
            //}

            //reader2.Close();

            string selectedItem = cbVillage2.Text;

            // Query information based on the selected item
            int tenantCount = QueryInformation(selectedItem);

            // Display the tenant count in the label
            labelTotal2.Text = tenantCount.ToString();
        }

        private int QueryInformation(string villageName)
        {
            int tenantCount = 0;

            // Example query (update with your actual query)
            string query = "SELECT v.\"villageID\", v.\"villageName\", COUNT(rb.\"tenantID\") AS tenant_count " +
                           "FROM \"tb_residentialBook\" rb " +
                           "JOIN tb_place p ON rb.\"placeID\" = p.\"placeID\" " +
                           "JOIN tb_village v ON p.\"villageID\" = v.\"villageID\" " +
                           "WHERE v.\"villageName\" = @villageName " +
                           "GROUP BY v.\"villageID\", v.\"villageName\";";

            // Using NpgsqlConnection and NpgsqlCommand to query the database

            NpgsqlCommand command = new NpgsqlCommand(query, cn.conn);
            command.Parameters.AddWithValue("@villageName", villageName);

            try
            {
                NpgsqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    tenantCount = reader.GetInt32(reader.GetOrdinal("tenant_count"));
                    //labelTotal2.Text = reader["tenant_count"].ToString();
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }


            return tenantCount;
        }
    }
}
