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
            txtTenantID.Text = tenantID;

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

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Hide();
        }
    }
}
