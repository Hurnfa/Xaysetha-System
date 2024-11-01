﻿/*using Org.BouncyCastle.Math;*/
using Npgsql;
using System;
using System.Data;
using System.Numerics;
using System.Threading;
using System.Windows.Forms;

namespace Xaysetha_System
{
    public partial class PaymentDialog : Form
    {
        db_connect cn = new db_connect();
        NpgsqlCommand cmd;
        NpgsqlDataAdapter adapter;
        int duration;
        public string userID;

        public PaymentDialog()
        {
            InitializeComponent();
            cn.getConnect();
            cbPkgLoad();
        }

        void cbPkgLoad()
        {
            adapter = new NpgsqlDataAdapter("SELECT * FROM tb_package", cn.conn);
            
            DataSet dsPkg = new DataSet();

            adapter.Fill(dsPkg);

            cbPackage.DataSource = dsPkg.Tables[0];
            cbPackage.DisplayMember = "pkg_name";
            cbPackage.ValueMember = "pkg_id";
        }

        public void fetchDataFromAddTenent(string tenantID, string tenantName, string userID)
        {
            txtTenantID.Text = tenantID;
            txtTenantName.Text = tenantName;
            txtUser.Text = userID;
        }

        public void fetchDataFromMainPage(string userID)
        {
            txtUser.Text = userID;
        }

        public void fetchDataFromTable(
            BigInteger paymentID,
            string tenantID,
            string userID,
            string header
        )
        {
            label_payment_id.Text = paymentID.ToString();
            txtTenantID.Text = tenantID;

            cmd = new NpgsqlCommand("SELECT tenant_name, tenant_lastname FROM tb_tenant WHERE tenant_id=@tenantID", cn.conn);

            cmd.Parameters.AddWithValue("@tenantID", BigInteger.Parse(tenantID));

            NpgsqlDataReader reader1 = cmd.ExecuteReader();

            while (reader1.Read())
            {
                txtTenantName.Text = reader1["tenant_name"].ToString() + " " + reader1["tenant_lastname"].ToString();
            }

            reader1.Close();

            //txtPrice.Text = price.ToString();

            txtUser.Text = userID;

            cmd = new NpgsqlCommand("SELECT * FROM tb_user WHERE user_id=@userID", cn.conn);

            this.userID = userID;

            cmd.Parameters.AddWithValue("@userID", userID);

            NpgsqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                txtUser.Text = $"{reader["user_name"]} {reader["user_lastname"]}";
            }
            reader.Close();

            cmd = new NpgsqlCommand("SELECT payment_status FROM tb_payment WHERE payment_id=@paymentID", cn.conn);

            cmd.Parameters.AddWithValue("@paymentID", paymentID);

            comboboxPaymentStatus.Text = cmd.ExecuteScalar().ToString();

            btnSave.Text = header + "ການຊຳລະ";

            txtTenantID.ReadOnly = true;
            txtTenantName.ReadOnly = true;
            txtUser.ReadOnly = true;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Hide();
        }

        string name, surname, gender;

        void dataChange(string sql, string des, BigInteger paymentID)
        {
            cmd = new NpgsqlCommand(sql, cn.conn);

            if (int.TryParse(cbPackage.SelectedValue?.ToString(), out int pkgID))
            {
                try
                {
                    cmd.Parameters.AddWithValue("@paymentID", paymentID);
                    cmd.Parameters.AddWithValue("@tenantID", BigInteger.Parse(txtTenantID.Text));
                    //cmd.Parameters.AddWithValue("@description", des);
                    cmd.Parameters.AddWithValue("@price", float.Parse(txtPrice.Text));
                    cmd.Parameters.AddWithValue("@paymentDate", DateTime.Now);
                    cmd.Parameters.AddWithValue("@paymentStatus", comboboxPaymentStatus.Text);
                    cmd.Parameters.AddWithValue("@pkgID", pkgID);
                    cmd.Parameters.AddWithValue("@duration", pkg_duration);

                    cmd.Parameters.AddWithValue("@userID", txtUser.Text);

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("ຊຳລະເງິນສຳເລັດ!", "ແຈ້ງເຕືອນ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ຂໍອະໄພ, ລະບົບຂັດຂ້ອງ\n" + ex.Message, "ແຈ້ງເຕືອນ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            //printing section

            try
            {
                //cmd = new NpgsqlCommand("SELECT tenant_name, tenant_lastname, gender FROM tb_tenant WHERE tenant_id=@tenantID", cn.conn);
                cmd = new NpgsqlCommand(@"select * from tb_payment 
                    join tb_tenant on tb_payment.tenant_id = tb_tenant.tenant_id 
                    join tb_package on tb_payment.pkg_id = tb_package.pkg_id 
                    where tb_payment.tenant_id=@tenantID", cn.conn);

                cmd.Parameters.AddWithValue("@tenantID", BigInteger.Parse(txtTenantID.Text));

                NpgsqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    name = reader["tenant_name"].ToString();
                    surname = reader["tenant_lastname"].ToString();
                    gender = reader["tenant_gender"].ToString();
                    duration = int.Parse(reader["pkg_duration"].ToString());
                }

                reader.Close();

                printing loadBill = new printing();

                switch (btnSave.Text)
                {
                    case "ຊຳລະ":

                        loadBill.loadDataToReport(paymentID, name, surname, float.Parse(txtPrice.Text), duration, txtUser.Text, gender);

                    break;

                    case "ແກ້ໄຂການຊຳລະ":

                        loadBill.loadDataToReport(paymentID, name, surname, float.Parse(txtPrice.Text), duration, this.userID, gender);

                    break;
                }


                //loadBill.loadDataToReport(paymentID, name, surname, float.Parse(txtPrice.Text), duration, txtUser.Text, gender);

                loadBill.Show();

                //new payment_info().dataTable.Clear();

                Hide();

            }
            catch (Exception ex)
            {
                MessageBox.Show("ຂໍອະໄພ, ລະບົບຂັດຂ້ອງ\n" + ex.Message, "ແຈ້ງເຕືອນ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void updateTenantStatus()
        {
            cmd = new NpgsqlCommand("UPDATE tb_tenant SET tenant_status='ອະນຸມັດແລ້ວ' WHERE tenant_id=@tenantID", cn.conn);

            try
            {
                cmd.Parameters.AddWithValue("@tenantID", BigInteger.Parse(txtTenantID.Text));

                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show("ຂໍອະໄພ, ລະບົບຂັດຂ້ອງ\n" + ex.Message, "ແຈ້ງເຕືອນ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            switch (btnSave.Text)
            {
                case "ຊຳລະ":

                    dataChange(@"INSERT INTO public.tb_payment(
	                    payment_id, tenant_id, pkg_id, price, payment_status, user_id, duration)
	                    VALUES (@paymentID, @tenantID, @pkgID, @price, @paymentStatus, @userID, @duration);",
                        "ຈ່າຍຄ່າລົງທະບຽນພັກເຊົ່າ",
                        new Random().Next()
                    );

                    break;

                case "ແກ້ໄຂການຊຳລະ":

                    DialogResult result = MessageBox.Show("ທ່ານຕ້ອງການແກ້ໄຂຂໍ້ມູນນີ້ບໍ?", "ແຈ້ງເຕືອນ", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (result == DialogResult.Yes)
                    {
                        dataChange("UPDATE tb_payment SET pkg_id=@pkgID, price=@price, payment_status=@paymentStatus, payment_date=@paymentDate, duration=@duration WHERE payment_id=@paymentID",
                            "ຈ່າຍຄ່າລົງທະບຽນພັກເຊົ່າ",
                            BigInteger.Parse(label_payment_id.Text)
                        );
                        updateTenantStatus();
                    }

                    break;
            }

        }

        private void rdoOneMonth_CheckedChanged(object sender, EventArgs e)
        {
            txtPrice.Clear();
            duration = 1;
            txtPrice.Text = "30000";
            comboboxPaymentStatus.Text = "ຊຳລະແລ້ວ";
        }

        private void rdoThreeMonths_CheckedChanged(object sender, EventArgs e)
        {
            txtPrice.Clear();
            duration = 3;
            txtPrice.Text = "30000";
            comboboxPaymentStatus.Text = "ຊຳລະແລ້ວ";
        }

        private void rdoSixMonths_CheckedChanged(object sender, EventArgs e)
        {
            txtPrice.Clear();
            duration = 6;
            txtPrice.Text = "60000";
            comboboxPaymentStatus.Text = "ຊຳລະແລ້ວ";
        }

        private void txtTenantID_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case (char)Keys.Enter:

                    cmd = new NpgsqlCommand("SELECT tenant_name, tenant_lastname FROM tb_tenant WHERE tenant_id="+txtTenantID.Text, cn.conn);

                    NpgsqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        txtTenantName.Text = reader["tenant_name"].ToString()+" "+reader["tenant_lastname"].ToString();
                    }

                    reader.Close();

                    break;
            }
        }

        string price;
        int pkg_duration;

        private void cbPackage_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbPackage.SelectedIndexChanged -= cbPackage_SelectedIndexChanged;

            if (int.TryParse(cbPackage.SelectedValue?.ToString(), out int provinceID))
            {
                using (var cmd = new NpgsqlCommand("SELECT * FROM tb_package WHERE pkg_id=@pkgID", cn.conn))
                {
                    cmd.Parameters.AddWithValue("@pkgID", provinceID);

                    NpgsqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        price = reader["pkg_price"].ToString();
                        pkg_duration = int.Parse(reader["pkg_duration"].ToString());
                    }

                    reader.Close();

                    //price = cmd.ExecuteScalar().ToString();
                }
            }
            else
            {
                // Handle the case where parsing fails, e.g., by clearing the combobox or showing an error
                
            }

            txtPrice.Text = price;
            //Attach event again
            cbPackage.SelectedIndexChanged += cbPackage_SelectedIndexChanged;
        }

        private void comboboxPaymentStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboboxPaymentStatus.Text)
            {
                case " ":


                    txtPrice.Clear();

                    break;
            }
        }

        private void txtTenantName_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case (char)Keys.Enter:

                    cmd = new NpgsqlCommand("SELECT tenant_id FROM tb_tenant WHERE tenant_name='" + txtTenantName.Text + "'", cn.conn);

                    NpgsqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        txtTenantID.Text = reader["tenant_id"].ToString();
                    }

                    reader.Close();

                    break;
            }
        }
    }
}

