using Npgsql;
using System.Data;
using System.Reflection;
using System.Windows.Forms;

namespace Xaysetha_System
{
    public partial class exportBook : Form
    {
        private Form activeForm = null;
        NpgsqlCommand cmd;
        NpgsqlDataAdapter adapter;
        db_connect cn = new db_connect();
        DataTable datatable = new DataTable();
        export_book_info exportBookInfo = new export_book_info();
        tenant_confirmation cf = new tenant_confirmation();

        public exportBook()
        {
            InitializeComponent();
            cn.getConnect();

            cf.loadData("SELECT tb_tenant.\"tenantID\", tb_tenant.firstname,tb_tenant.lastname, tb_tenant.occupation, tb_payment.duration, tb_tenant.\"phoneNums\", tb_payment.payment_status from tb_tenant " +
                    "join tb_payment on tb_tenant.\"tenantID\" = tb_payment.tenant_id where tb_payment.payment_status = 'ຊຳລະແລ້ວ';");

            OpenChildForm(cf, statusControl.TabPages[0]);
            displayTotalOfData(0);

        }

        string sql;

        public void displayTotalOfData(int index)
        {
            switch (index)
            {
                case 0:

                    sql = "SELECT COUNT(*) FROM tb_payment WHERE payment_status='ຊຳລະແລ້ວ';";

                    break;

                case 1:

                    sql = "SELECT COUNT(*) FROM \"tb_residentialBook\";";

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

        private void statusControl_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            int selectedIndex = statusControl.SelectedIndex;
            string status;
            

            switch (selectedIndex)
            {
                case 0:

                    //tenant_confirmation cf = new tenant_confirmation();
                    cf.loadData("SELECT tb_tenant.\"tenantID\", tb_tenant.firstname,tb_tenant.lastname, tb_tenant.occupation, tb_payment.duration, tb_tenant.\"phoneNums\", tb_payment.payment_status from tb_tenant " +
                "join tb_payment on tb_tenant.\"tenantID\" = tb_payment.tenant_id where tb_payment.payment_status = 'ຊຳລະແລ້ວ';");

                    OpenChildForm(cf, statusControl.TabPages[selectedIndex]);

                    displayTotalOfData(selectedIndex);

                    break;

                case 1:

                    export_book_info tb_exportbook = new export_book_info();
                    tb_exportbook.loadData("SELECT \"tb_residentialBook\".\"resBookID\", tb_tenant.firstname, tb_tenant.lastname, \"tb_place\".\"placeName\", \"tb_residentialBook\".\"issueDate\", \"tb_residentialBook\".\"expDate\" FROM \"tb_residentialBook\" JOIN tb_tenant ON \"tb_residentialBook\".\"tenantID\" = tb_tenant.\"tenantID\" JOIN \"tb_place\" ON \"tb_residentialBook\".\"placeID\" = \"tb_place\".\"placeID\";");
                    OpenChildForm(tb_exportbook, statusControl.TabPages[selectedIndex]);

                    displayTotalOfData(selectedIndex);

                    break;

                case 2:
                    OpenChildForm(new payment_info(), statusControl.TabPages[selectedIndex]);
                    break;
                // Add more cases if you have more tabs
                default:
                    break;
            }
        }
    }
}
