﻿using Npgsql;
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
        int duration;
        book_printing printing = new book_printing();

        public PrintDialog(int duration)
        {
            InitializeComponent();
            cn.getConnect();
            comboboxPlaceLoad();
            this.duration = duration;
            
        }


        public void fetchDataFromMainPage(BigInteger paymentID, string name, string surname, int duration)
        {
            DateTime issueDate = datePickerIssueDate.Value,
                expDate = issueDate.AddMonths(duration);

            datePickerExpDate.Value = expDate;

            txtTenantName.Text = name + " " + surname;

            cmd = new NpgsqlCommand("SELECT tenant_id FROM tb_tenant WHERE tenant_name=@firstname", cn.conn);
            cmd.Parameters.AddWithValue("@firstname", name);
            NpgsqlDataReader reader = cmd.ExecuteReader();


            while (reader.Read())
            {
                txtTenantID.Text = reader["tenant_id"].ToString();
            }

            reader.Close();

            txtBookID.Text = $"{txtTenantID.Text}/ປກສ.ກ3";

            lblID.Text = $"ເລກທີ່: {txtTenantID.Text}/ປກສ.ກ3";
            lblName.Text = $"ຂອງທ່ານ: {name} {surname}";

        }

        void comboboxPlaceLoad()
        {
            adapter = new NpgsqlDataAdapter("SELECT * FROM tb_place", cn.conn);
            DataSet dataSetPlace = new DataSet();
            adapter.Fill(dataSetPlace);
            comboboxPlace.DataSource = dataSetPlace.Tables[0];

            comboboxPlace.DisplayMember = "place_name";
            comboboxPlace.ValueMember = "place_id";

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

            DateTime expiryDate = selectedDate.AddMonths(duration);

            datePickerExpDate.Value = expiryDate;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            cmd = new NpgsqlCommand("INSERT INTO tb_book VALUES (@resBookID, @tenantID, @placeID, @issueDate, @expDate, @purpose)", cn.conn);

            try
            {
                cmd.Parameters.AddWithValue("@resBookID", txtBookID.Text);
                cmd.Parameters.AddWithValue("@tenantID", BigInteger.Parse(txtTenantID.Text));
                cmd.Parameters.AddWithValue("@placeID", comboboxPlace.SelectedValue);
                cmd.Parameters.AddWithValue("@issueDate", datePickerIssueDate.Value);
                cmd.Parameters.AddWithValue("@expDate", datePickerExpDate.Value);
                cmd.Parameters.AddWithValue("@purpose", txtPurpose.Text);

                cmd.ExecuteNonQuery();

                MessageBox.Show("ບັນທຶກສຳເລັດ!", "ແຈ້ງເຕືອນ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //need to be fix
                //printing.loadDataToReport(txtTenantID.Text, null, comboboxPlace.Text);

                printing.Show();

                Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ຂໍອະໄພ, ລະບົບຂັດຂ້ອງ\n" + ex.Message, "ແຈ້ງເຕືອນ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        place_add placeAdd = new place_add();

        private void linkLabelShowPlaceAdd_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            placeAdd.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            placeAdd.ShowDialog();
        }
    }
}
