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

        public PaymentDialog()
        {
            InitializeComponent();
            cn.getConnect();
        }

        public void fetchDataFromTable(
            BigInteger paymentID, 
            string tenantID, 
            string userID, 
            int duration, 
            float price
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

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }
    }
}
