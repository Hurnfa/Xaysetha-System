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
        DataTable dataTable = new DataTable();

        public payment()
        {
            InitializeComponent();
            cn.getConnect();
            OpenChildForm(table_payment, statusControl.TabPages[0]);
            //table_payment.loadData("SELECT * FROM tb_payment;");
            //loadData("SELECT * from tb_place INNER JOIN tb_citizen on tb_place.\"citizentID\" = tb_citizen.\"citizenID\" INNER JOIN tb_village on tb_place.\"villageID\" = tb_village.\"villageID\";");
            table_payment.loadData("SELECT * from tb_payment INNER JOIN tb_tenant on tb_payment.tenant_id = tb_tenant.\"tenantID\";");

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
                activeForm.Close();
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

            switch (selectedIndex)
            {
                case 0:
                    OpenChildForm(new payment_info(), statusControl.TabPages[selectedIndex]);
                    break;
                case 1:
                    OpenChildForm(new payment_info(), statusControl.TabPages[selectedIndex]);
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
            payment.Show();
        }
    }
}
