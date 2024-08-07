using System;
using System.Windows.Forms;

namespace Xaysetha_System
{
    public partial class fee_management : Form
    {
        public fee_management()
        {
            InitializeComponent();
        }

        private void btnAddVillage_Click(object sender, EventArgs e)
        {
            fee_add feeAdd = new fee_add();
            feeAdd.Show();
        }
    }
}
