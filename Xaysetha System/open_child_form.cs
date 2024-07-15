using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Xaysetha_System
{
    internal class open_child_form
    {
        public Form activeForm = null;

        public void OpenChildForm(Form childForm)
        {
            // Close the current active form if there is one
            if (activeForm != null)
            {
                activeForm.Close();
            }

            // Set the new form as the active form
            activeForm = childForm;

            // Configure the form to be displayed within the panel
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;

            // Clear the panel and add the new form
            Dashboard.instance.panelContainerInstance.Controls.Clear();
            Dashboard.instance.panelContainerInstance.Controls.Add(childForm);
            childForm.BringToFront();
            childForm.Show();
        }
    }
}
