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
    public partial class jobs_management : Form
    {
        NpgsqlCommand cmd;
        NpgsqlDataAdapter adapter;
        db_connect cn = new db_connect();
        DataTable dt = new DataTable();
        jobs_add jonAdd = new jobs_add();

        public jobs_management()
        {
            InitializeComponent();
            cn.getConnect();
            loadData("SELECT * FROM tb_jobs;");
        }

        void totalCount()
        {
            cmd = new NpgsqlCommand("SELECT COUNT(*) FROM tb_jobs;", cn.conn);
            labelTotal.Text = $"ທັງໝົດ {cmd.ExecuteScalar()} ລາຍການ";
        }

        void loadData(string sql)
        {
            totalCount();
            data.AutoGenerateColumns = false;

            adapter = new NpgsqlDataAdapter(sql, cn.conn);
            adapter.Fill(dt);

            data.DataSource = dt;
            data.Columns[0].DataPropertyName = "jobs_id";
            data.Columns[1].DataPropertyName = "jobs_name";
        }

        private void data_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            String columnName = data.Columns[e.ColumnIndex].Name;

            int village_id = int.Parse(data.Rows[e.RowIndex].Cells[0].Value.ToString());

            switch (columnName)
            {
                case "editButton":

                    string cellData = data.Rows[e.RowIndex].Cells[1].Value.ToString();
                    int villageID;

                    if (int.TryParse(data.Rows[e.RowIndex].Cells[0].Value.ToString(), out villageID))
                    {
                        jonAdd.getDataFromMainPage(village_id, cellData, "ແກ້ໄຂ");

                        jonAdd.ShowDialog();
                    }

                    break;

                case "delButton":

                    DialogResult result = MessageBox.Show("ທ່ານຕ້ອງການແກ້ໄຂຂໍ້ມູນນີ້ບໍ?", "ແຈ້ງເຕືອນ", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (result == DialogResult.Yes)
                    {
                        cmd = new NpgsqlCommand("DELETE FROM tb_jobs WHERE jobs_id=@jobsID", cn.conn);

                        try
                        {
                            cmd.Parameters.AddWithValue("@jobsID", village_id);

                            cmd.ExecuteNonQuery();

                            MessageBox.Show("ລົບຂໍ້ມູນສຳເລັດ", "ແຈ້ງເຕືອນ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            dt.Clear();

                            loadData("SELECT * FROM tb_jobs");

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"ຂໍອະໄພ, ລະບົບຂັດຂ້ອງ \n{ex}", "ແຈ້ງເຕືອນ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                    }

                    break;
            }
        }

        private void btnAddJobs_Click(object sender, EventArgs e)
        {
            new jobs_add().ShowDialog();
        }
    }
}
