using System;
using System.Windows.Forms;

namespace Xaysetha_System
{
    public partial class entrance_management : Form
    {
        private Form activeForm = null;
        public entrance_management()
        {
            InitializeComponent();

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



        private void btnAddUser_Click(object sender, EventArgs e)
        {
            tenant_add tenantAdd = new tenant_add();
            tenantAdd.Show();
        }
    }
}
