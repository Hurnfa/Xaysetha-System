using Npgsql;
using System.Data;
using System.Windows.Forms;

namespace Xaysetha_System
{
    public partial class exportBook : Form
    {
        public exportBook()
        {
            InitializeComponent();
            cn.getConnect();
            loadData("SELECT tb_tenant.\"tenantID\", tb_tenant.firstname,tb_tenant.lastname, tb_tenant.occupation, tb_tenant.\"phoneNums\", tb_payment.payment_status from tb_tenant " +
                "join tb_payment on tb_tenant.\"tenantID\" = tb_payment.tenant_id where tb_payment.payment_status = 'ຊຳລະແລ້ວ';");
        }

        NpgsqlCommand cmd;
        NpgsqlDataAdapter adapter;
        db_connect cn = new db_connect();
        DataTable datatable = new DataTable();
        export_book_info exportBookInfo = new export_book_info();


        void loadData(string sql)
        {
            data.AutoGenerateColumns = false;

            adapter = new NpgsqlDataAdapter(sql, cn.conn);
            adapter.Fill(datatable);
            data.DataSource = datatable;

            data.Columns[0].DataPropertyName = "tenantID";
            data.Columns[1].DataPropertyName = "firstname";
            data.Columns[2].DataPropertyName = "lastname";
            //data.Columns[3].DataPropertyName = "firstname";
            data.Columns[4].DataPropertyName = "phoneNums";
            data.Columns[5].DataPropertyName = "occupation";
        }

        private void statusControl_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            int selectedIndex = statusControl.SelectedIndex;
            string status;
            object value = exportBookInfo.datatable.Clear();

            switch (selectedIndex)
            {
                case 0:
                    table_.loadData("select * from tb_payment inner join tb_tenant on tb_payment.tenant_id = tb_tenant.\"tenantID\" where payment_status = 'ຊຳລະແລ້ວ';");
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
    }
}
