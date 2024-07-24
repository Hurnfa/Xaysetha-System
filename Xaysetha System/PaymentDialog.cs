/*using Org.BouncyCastle.Math;*/
using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Xaysetha_System
{
    public partial class PaymentDialog : Form
    {
        db_connect cn = new db_connect();
        NpgsqlCommand cmd;
        int duration;

        public PaymentDialog()
        {
            InitializeComponent();
            cn.getConnect();
        }

        public void fetchDataFromMainPage(string userID)
        {
            txtUser.Text = userID;
        }

        public void fetchDataFromTable(
            BigInteger paymentID,
            string tenantID,
            string userID,
            int duration,
            float price,
            string header
        )
        {
            label_payment_id.Text = paymentID.ToString();
            txtTenantID.Text = tenantID;

            cmd = new NpgsqlCommand("SELECT firstname, lastname FROM tb_tenant WHERE \"tenantID\"=@tenantID", cn.conn);

            cmd.Parameters.AddWithValue("@tenantID", BigInteger.Parse(tenantID));

            NpgsqlDataReader reader1 = cmd.ExecuteReader();

            while (reader1.Read())
            {
                txtTenantName.Text = reader1["firstname"].ToString() + " " + reader1["lastname"].ToString();
            }

            reader1.Close();

            switch (duration)
            {
                case 1:

                    rdoOneMonth.Checked = true;

                    break;

                case 3:

                    rdoThreeMonths.Checked = true;

                    break;

                case 6:

                    rdoSixMonths.Checked = true;

                    break;
            }

            txtPrice.Text = price.ToString();

            cmd = new NpgsqlCommand("SELECT * FROM tb_user WHERE \"userID\"=@userID", cn.conn);

            cmd.Parameters.AddWithValue("@userID", userID);

            NpgsqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                txtUser.Text = reader["userName"].ToString() + " " + reader["userLName"].ToString();
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

        void dataChange(string sql, string des, BigInteger paymentID)
        {
            cmd = new NpgsqlCommand(sql, cn.conn);

            try
            {
                cmd.Parameters.AddWithValue("@paymentID", paymentID);
                cmd.Parameters.AddWithValue("@tenant_id", BigInteger.Parse(txtTenantID.Text));
                cmd.Parameters.AddWithValue("@description", des);
                cmd.Parameters.AddWithValue("@duration", duration);
                cmd.Parameters.AddWithValue("@price", float.Parse(txtPrice.Text));
                cmd.Parameters.AddWithValue("@paymentDate", DateTime.Now);
                cmd.Parameters.AddWithValue("@paymentStatus", comboboxPaymentStatus.Text);
                cmd.Parameters.AddWithValue("@user_id", txtUser.Text);

                cmd.ExecuteNonQuery();

                MessageBox.Show("ຊຳລະເງິນສຳເລັດ!", "ແຈ້ງເຕືອນ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //new payment_info().dataTable.Clear();

                Hide();

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

                    dataChange("INSERT INTO tb_payment VALUES(@paymentID, @tenant_id, @description, @duration, @price, @paymentDate, @paymentStatus, @user_id);",
                        "ຕໍ່ອາຍຸພັກເຊົ່າ",
                        new Random().Next()
                    );

                break;

                case "ແກ້ໄຂການຊຳລະ":

                    DialogResult result = MessageBox.Show("ທ່ານຕ້ອງການແກ້ໄຂຂໍ້ມູນນີ້ບໍ?", "ແຈ້ງເຕືອນ", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (result == DialogResult.Yes)
                    {
                        dataChange("UPDATE tb_payment SET duration=@duration, price=@price, payment_date=@paymentDate, payment_status=@paymentStatus WHERE payment_id=@paymentID",
                            "ຈ່າຍຄ່າລົງທະບຽນພັກເຊົ່າ",
                            BigInteger.Parse(label_payment_id.Text)
                        );
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

        private void comboboxPaymentStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboboxPaymentStatus.Text)
            {
                case " ":

                    rdoOneMonth.Checked = false;
                    rdoThreeMonths.Checked = false;
                    rdoSixMonths.Checked = false;
                    txtPrice.Clear();

                break;
            }
        }

        private void txtTenantName_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case (char)Keys.Enter:

                    cmd = new NpgsqlCommand("SELECT \"tenantID\" FROM tb_tenant WHERE firstname='" + txtTenantName.Text + "'", cn.conn);

                    NpgsqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        txtTenantID.Text = reader["tenantID"].ToString();
                    }

                    reader.Close();

                break;
            }
        }
    }
}

