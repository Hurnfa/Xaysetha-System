using Npgsql;
using System;
using System.Data;
using System.Windows.Forms;

namespace Xaysetha_System
{
    public partial class payment : Form
    {
        private Form activeForm = null;
        payment_info table_payment = new payment_info();
        db_connect cn = new db_connect();
        NpgsqlDataAdapter adapter;
        NpgsqlCommand cmd;
        DataTable dataTable = new DataTable();

        public payment()
        {
            InitializeComponent();
            cn.getConnect();
            displayTotalOfData("");
            OpenChildForm(table_payment, statusControl.TabPages[0]);
            //table_payment.loadData("SELECT * FROM tb_payment;");
            //loadData("SELECT * from tb_place INNER JOIN tb_citizen on tb_place.\"citizentID\" = tb_citizen.\"citizenID\" INNER JOIN tb_village on tb_place.\"villageID\" = tb_village.\"villageID\";");
            table_payment.loadData("SELECT * from tb_payment INNER JOIN tb_tenant on tb_payment.tenant_id = tb_tenant.\"tenantID\";");

        }

        public void displayTotalOfData(string status)
        {
            string sql;

            switch (status)
            {
                case "":

                    sql = "SELECT COUNT(*) FROM tb_payment;";

                    break;

                default:

                    sql = "SELECT COUNT(*) FROM tb_payment WHERE payment_status='" + status + "'";

                    break;
            }

            cmd = new NpgsqlCommand(sql, cn.conn);
            NpgsqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                labelTotalRecords.Text = "ທັງໝົດ " + reader["count"] + " ລາຍການ";
            }

            reader.Close();
        }

        public void fetchDataFromMainPage(string userID)
        {
            label_userid.Text = userID;
        }

        void loadData(string sql)
        {
            adapter = new NpgsqlDataAdapter(sql, cn.conn);
            adapter.Fill(dataTable);
        }

        private void OpenChildForm(Form childForm, TabPage tabPage)
        {
            // Close the current active form if there is one
            if (activeForm != null)
            {
                activeForm.Hide();
            }

            // Set the new form as the active form
            activeForm = childForm;

            // Configure the form to be displayed within the tab page
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;

            // Clear the tab page controls and add the new form
            tabPage.Controls.Clear();
            tabPage.Controls.Add(childForm);

            // Bring the form to the front and show it
            childForm.Show();
            childForm.BringToFront();
        }

        private void statusControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = statusControl.SelectedIndex;
            string status;
            table_payment.dataTable.Clear();

            switch (selectedIndex)
            {
                case 0:
                    displayTotalOfData("ຊຳລະແລ້ວ");
                    table_payment.loadData("select * from tb_payment inner join tb_tenant on tb_payment.tenant_id = tb_tenant.\"tenantID\" where payment_status = 'ຊຳລະແລ້ວ';");
                    OpenChildForm(table_payment, statusControl.TabPages[selectedIndex]);
                    break;
                case 1:
                    displayTotalOfData("ລໍຖ້າຊຳລະ");
                    table_payment.loadData("select * from tb_payment inner join tb_tenant on tb_payment.tenant_id = tb_tenant.\"tenantID\" where payment_status = 'ລໍຖ້າຊຳລະ';");
                    OpenChildForm(table_payment, statusControl.TabPages[selectedIndex]);
                    break;
                case 2:
                    OpenChildForm(new payment_info(), statusControl.TabPages[selectedIndex]);
                    break;
                // Add more cases if you have more tabs
                default:
                    break;
            }
        }

        private void btnPayUser_Click(object sender, EventArgs e)
        {
            PaymentDialog payment = new PaymentDialog();
            payment.fetchDataFromMainPage(label_userid.Text);
            payment.Show();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            table_payment.dataTable.Clear();

            table_payment.loadData("select * from tb_payment inner join tb_tenant on tb_payment.tenant_id = tb_tenant.\"tenantID\" where CONCAT (payment_id, firstname) LIKE '%"+txtSearch.Text+"%'");
        }
    }
}
