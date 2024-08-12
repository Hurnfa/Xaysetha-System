using Mysqlx.Crud;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using TheArtOfDevHtmlRenderer.Adapters;

namespace Xaysetha_System
{

    public partial class Form2 : Form
    {
        db_connect cn = new db_connect();
        NpgsqlCommand cmd;
        NpgsqlDataAdapter adapter;
        DataSet dataSetVillage = new DataSet();
        public string sql;

        public Form2()
        {
            InitializeComponent();
            cn.getConnect();
            loadTotalTenant();
            comboVillage1Load();
            DisplayChart();
            
            //comboVillage2Load();
            //comboVillage3Load();
            //comboVillage4Load();
            //comboVillage5Load();
        }

        void loadTotalTenant()
        {
            cmd = new NpgsqlCommand("SELECT COUNT(*) FROM tb_tenant;", cn.conn);

            label_total_tenant.Text = cmd.ExecuteScalar().ToString();
        }

        private void DisplayChart()
        {
            // SQL query
            string query = @"SELECT v.""villageName"", COUNT(rb.""tenantID"") AS Population
                            FROM ""tb_residentialBook"" rb
                            JOIN tb_place p ON rb.""placeID"" = p.""placeID""
                            JOIN tb_village v ON p.""villageID"" = v.""villageID""
                            GROUP BY v.""villageID"", v.""villageName""
                            ORDER BY Population DESC
                            LIMIT 5;";

            // List to store the data
            List<VillagePopulation> data = new List<VillagePopulation>();

            cmd = new NpgsqlCommand(query, cn.conn);

            try
            {
                NpgsqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    data.Add(new VillagePopulation
                    {
                        VillageId = reader["villageName"].ToString(),
                        Population = Convert.ToInt32(reader["Population"])
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            ChartArea chartArea = new ChartArea();
            chart2.ChartAreas.Add(chartArea);

            Series series = new Series
            {
                Name = "Population",
                IsValueShownAsLabel = true,
                ChartType = SeriesChartType.Column
            };

            chart2.Series.Add(series);

            foreach (var item in data)
            {
                series.Points.AddXY(item.VillageId, item.Population);
            }

            chart2.Invalidate();
        }

        void comboVillage1Load()
        {
            DataSet ds1 = new DataSet();

            adapter = new NpgsqlDataAdapter("SELECT * FROM tb_village;", cn.conn);
            adapter.Fill(ds1);

            cbVillage5.DataSource = ds1.Tables[0];

            cbVillage5.DisplayMember = "villageName";
            cbVillage5.ValueMember = "villageID";
        }

        public class VillagePopulation
        {
            public string VillageId { get; set; }
            public int Population { get; set; }
        }

        private void btnMakeReport_Click(object sender, EventArgs e)
        {
            if (rdoCitizen.Checked)
            {
                new Form3().Show();
            }
            else if (rdoTenant.Checked)
            {
                Report loadTenant = new Report();
                loadTenant.loadDataToReport(cbVillage5.SelectedValue.ToString(), datePicker1.Value, datePicker2.Value);
                loadTenant.Show();
            }

        }

        private void rdoTenant_CheckedChanged(object sender, EventArgs e)
        {
            sql = "";
        }

        private void rdoCitizen_CheckedChanged(object sender, EventArgs e)
        {
            sql = "";
        }

        private void rdoExpire_CheckedChanged(object sender, EventArgs e)
        {
            sql = "";
        }

        private void rdoNotApprove_CheckedChanged(object sender, EventArgs e)
        {
            sql = "";
        }
    }



    /*    void comboVillageLoad()
        {
            adapter = new NpgsqlDataAdapter("SELECT * FROM tb_village;", cn.conn);
            adapter.Fill(dataSetVillage);

            cbVillage1.DataSource = dataSetVillage.Tables[0];

            cbVillage1.DisplayMember = cbVillage2.DisplayMember = cbVillage3.DisplayMember = cbVillage4.DisplayMember = "villageName";
            cbVillage1.ValueMember = cbVillage2.ValueMember = cbVillage3.ValueMember = cbVillage4.ValueMember = "villageID";
        }*/

    //void comboVillage2Load()
    //{
    //    DataSet ds2 = new DataSet();

    //    adapter = new NpgsqlDataAdapter("SELECT * FROM tb_village;", cn.conn);
    //    adapter.Fill(ds2);

    //    cbVillage2.DataSource = ds2.Tables[0];

    //    cbVillage2.DisplayMember = "villageName";
    //    cbVillage2.ValueMember = "villageID";
    //}

    //void comboVillage3Load()
    //{
    //    DataSet ds3 = new DataSet();

    //    adapter = new NpgsqlDataAdapter("SELECT * FROM tb_village;", cn.conn);
    //    adapter.Fill(ds3);

    //    cbVillage3.DataSource = ds3.Tables[0];

    //    cbVillage3.DisplayMember = "villageName";
    //    cbVillage3.ValueMember = "villageID";
    //}

    //void comboVillage4Load()
    //{
    //    DataSet ds4 = new DataSet();

    //    adapter = new NpgsqlDataAdapter("SELECT * FROM tb_village;", cn.conn);
    //    adapter.Fill(ds4);

    //    cbVillage4.DataSource = ds4.Tables[0];

    //    cbVillage4.DisplayMember = "villageName";
    //    cbVillage4.ValueMember = "villageID";
    //}

    /*    private int QueryInformation(string villageName)
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
        }*/

    //private void cbVillage3_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    string selectedItem = cbVillage3.Text;

    //    // Query information based on the selected item
    //    int tenantCount = QueryInformation(selectedItem);

    //    // Display the tenant count in the label
    //    labelTotal3.Text = tenantCount.ToString();

    //    labelDisplayVillage3.Text = "ຈຳນວນຜູ້ພັກເຊົາ\r\n(ບ້ານ " + cbVillage3.Text + ")\r\n";
    //}

    //private void cbVillage4_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    string selectedItem = cbVillage4.Text;

    //    // Query information based on the selected item
    //    int tenantCount = QueryInformation(selectedItem);

    //    // Display the tenant count in the label
    //    labelTotal4.Text = tenantCount.ToString();

    //    labelDisplayVillage4.Text = "ຈຳນວນຜູ້ພັກເຊົາ\r\n(ບ້ານ " + cbVillage4.Text + ")\r\n";
    //}

    //private void cbVillage1_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    string selectedItem = cbVillage1.Text;

    //    // Query information based on the selected item
    //    int tenantCount = QueryInformation(selectedItem);

    //    // Display the tenant count in the label
    //    labelTotal1.Text = tenantCount.ToString();

    //    labelDisplayVillage1.Text = "ຈຳນວນຜູ້ພັກເຊົາ\r\n(ບ້ານ " + cbVillage1.Text + ")\r\n";
    //}


}

