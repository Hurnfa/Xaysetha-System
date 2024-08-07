using System;
using System.Windows.Forms;

namespace Xaysetha_System
{
    public partial class provice_management : Form
    {
        province_management_add provinceAdd = new province_management_add();
        public provice_management()
        {
            InitializeComponent();
        }

        private void btnAddProvince_Click(object sender, EventArgs e)
        {
            provinceAdd.Show();
        }
    }
}
