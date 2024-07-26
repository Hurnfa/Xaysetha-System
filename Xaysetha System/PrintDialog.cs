using Npgsql;
using System;
using System.Data;
using System.Numerics;
using System.Windows.Forms;

namespace Xaysetha_System
{
    public partial class PrintDialog : Form
    {
        db_connect cn = new db_connect();
        NpgsqlCommand cmd;
        NpgsqlDataAdapter adapter;

        public PrintDialog()
        {
            InitializeComponent();
            cn.getConnect();
            comboboxPlaceLoad();
        }


        public void fetchDataFromMainPage(BigInteger paymentID, string name, string surname, int duration)
        {
            /*            cmd = new NpgsqlCommand("SELECT duration FROM tb_payment WHERE payment_id=@paymentID", cn.conn);
                        cmd.Parameters.AddWithValue("@paymentID", paymentID);
                        NpgsqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            duration = int.Parse(reader["duration"].ToString());
                        }

                        reader.Close();*/

            DateTime issueDate = datePickerIssueDate.Value,
                expDate = issueDate.AddMonths(duration);

            datePickerExpDate.Value = expDate;

            txtTenantName.Text = name + " " + surname;

            cmd = new NpgsqlCommand("SELECT \"tenantID\" FROM tb_tenant WHERE firstname=@firstname", cn.conn);
            cmd.Parameters.AddWithValue("@firstname", name);
            NpgsqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                txtTenantID.Text = reader["tenantID"].ToString();
            }

            reader.Close();

            txtBookID.Text = new Random().Next().ToString();

        }

        void comboboxPlaceLoad()
        {
            adapter = new NpgsqlDataAdapter("SELECT * FROM tb_place", cn.conn);
            DataSet dataSetPlace = new DataSet();
            adapter.Fill(dataSetPlace);
            comboboxPlace.DataSource = dataSetPlace.Tables[0];

            comboboxPlace.DisplayMember = "placeName";
            comboboxPlace.ValueMember = "placeID";

        }

        private void guna2HtmlLabel2_Click(object sender, EventArgs e)
        {

        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void datePickerIssueDate_ValueChanged(object sender, EventArgs e)
        {
            DateTime selectedDate = datePickerIssueDate.Value;

            DateTime expiryDate = selectedDate.AddMonths(1);

            datePickerExpDate.Value = expiryDate;
        }
    }
}
