using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace Xaysetha_System
{
    public partial class Dashboard : Form
    {
        private readonly Color defaultColor = Color.FromArgb(23, 112, 188); // Blue Background

        private readonly Color defaultFontColor = Color.FromArgb(255, 255, 255); // White Font

        private readonly Color activeFontColor = Color.FromArgb(36, 125, 201); // Blue Active Font

        private readonly Color activeColor = Color.FromArgb(236, 236, 236); // Grey Active Background

        private Guna.UI2.WinForms.Guna2Button activeButton;

        public Dashboard()
        {
            InitializeComponent();
            customizedDesign();
        }

        private Form activeForm = null;
        private void OpenChildForm(Form childForm)
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
            panelContainer.Controls.Clear();
            panelContainer.Controls.Add(childForm);
            childForm.BringToFront();
            childForm.Show();
        }

        private void customizedDesign() { 
            infoSubMenu.Visible = false;
        }

        private void hideSubMenu() {
            if (infoSubMenu.Visible == true) {
                infoSubMenu.Visible = false;
            }
        }

        private void showSubMenu(Panel subMenu) {
            if (subMenu.Visible == false) {
                hideSubMenu();
                subMenu.Visible = true;
            }
            else
                subMenu.Visible = false;
        }

        private void ChangeButtonAppearance(Guna.UI2.WinForms.Guna2Button clickedButton)
        {
            // Reset the previously active button, if there was one
            if (activeButton != null && activeButton != clickedButton)
            {
                activeButton.FillColor = defaultColor;
                activeButton.ForeColor = defaultFontColor;
            }

            // Set the clicked button to active
            clickedButton.FillColor = activeColor;
            clickedButton.ForeColor = activeFontColor;

            // Update the activeButton to the clicked button
            activeButton = clickedButton;
        }



        private void btnInfo_Click(object sender, EventArgs e)
        {
            showSubMenu(infoSubMenu);
        }

        private void btnUserManagement_Click(object sender, EventArgs e)
        {
            ChangeButtonAppearance(btnUserManagement);
        }

        private void btnVilManagement_Click(object sender, EventArgs e)
        {
            ChangeButtonAppearance(btnVilManagement);
        }

        private void btnTenantManagement_Click(object sender, EventArgs e)
        {
            ChangeButtonAppearance(btnTenantManagement);
        }

        private void btnHomeManagement_Click(object sender, EventArgs e)
        {
            ChangeButtonAppearance(btnHomeManagement);
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Form2());
        }


    }
}
