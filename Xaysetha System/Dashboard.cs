using Guna.UI2.WinForms;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Xaysetha_System
{
    public partial class Dashboard : Form
    {
        private readonly Color defaultColor = Color.FromArgb(23, 112, 188); // Blue Background

        private readonly Color defaultFontColor = Color.FromArgb(255, 255, 255); // White Font

        private readonly Color activeFontColor = Color.FromArgb(36, 125, 201); // Blue Active Font

        private readonly Color activeColor = Color.FromArgb(236, 236, 236); // Grey Active Background

        private Guna.UI2.WinForms.Guna2Button activeButton;

        public static Dashboard instance;

        public Guna2Panel panelContainerInstance;

        public Dashboard()
        {
            InitializeComponent();
            customizedDesign();
            instance = this;
            panelContainerInstance = panelContainer;
        }

        public void showUserName(string name, string surname, string role, string userid)
        {
            lbName.Text = name + " " + surname;
            labelRole.Text = role;
            label_username.Text = userid;

            if (role != "admin")
            {
                btnUserManagement.Enabled = false;
            }
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

        private void customizedDesign()
        {
            infoSubMenu.Visible = false;
        }

        private void hideSubMenu()
        {
            if (infoSubMenu.Visible == true)
            {
                infoSubMenu.Visible = false;
            }
        }

        private void showSubMenu(Panel subMenu)
        {

            switch (subMenu.Visible)
            {
                case false:

                    hideSubMenu();
                    subMenu.Visible = true;

                    break;

                default:

                    subMenu.Visible = false;

                    break;
            }

            /*            if (subMenu.Visible == false)
                        {
                            hideSubMenu();
                            subMenu.Visible = true;
                        }
                        else
                            subMenu.Visible = false;*/
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
            labelHeader.Text = btnUserManagement.Text;
            OpenChildForm(new user_management());
        }

        private void btnVilManagement_Click(object sender, EventArgs e)
        {
            ChangeButtonAppearance(btnVilManagement);
            labelHeader.Text = btnVilManagement.Text;
            OpenChildForm(new village_management());
        }

        private void btnTenantManagement_Click(object sender, EventArgs e)
        {
            ChangeButtonAppearance(btnTenantManagement);
            labelHeader.Text = btnTenantManagement.Text;
            OpenChildForm(new resident_management());
        }

        private void btnHomeManagement_Click(object sender, EventArgs e)
        {
            ChangeButtonAppearance(btnHomeManagement);
            labelHeader.Text = btnHomeManagement.Text;
            OpenChildForm(new place_management());
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("ທ່ານຕ້ອງການອອກຈາກລະບົບບໍ?", "ແຈ້ງເຕືອນ", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);

            if (result == DialogResult.Yes)
            {
                new Login().Show();
                Hide();
            }
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            labelHeader.Text = btnDashboard.Text;
            OpenChildForm(new Form2());
        }

        private void btnEntry_Click(object sender, EventArgs e)
        {
            labelHeader.Text = btnEntry.Text;
            OpenChildForm(new entrance_management(label_username.Text));
        }

        private void btnLeave_Click(object sender, EventArgs e)
        {
            //payment button

            labelHeader.Text = btnLeave.Text;
            payment payment = new payment();
            payment.fetchDataFromMainPage(label_username.Text);
            OpenChildForm(payment);
        }

        private void logo_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("ທ່ານຕ້ອງການອອກຈາກໂປຣແກຣມບໍ?", "ແຈ້ງເຕືອນ", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnPrinting_Click(object sender, EventArgs e)
        {
            labelHeader.Text = btnPrinting.Text;
            OpenChildForm(new exportBook());
        }

        private void labelRole_Click(object sender, EventArgs e)
        {

        }

        private void btnCity_Click(object sender, EventArgs e)
        {
            //ChangeButtonAppearance(btnVilManagement);
            //labelHeader.Text = btnVilManagement.Text;
            OpenChildForm(new city_management());
        }

        private void btnProvince_Click(object sender, EventArgs e)
        {
            OpenChildForm(new provice_management());
        }

        private void feeManagement_Click(object sender, EventArgs e)
        {
            OpenChildForm(new fee_management());
        }

        private void btnJobs_Click(object sender, EventArgs e)
        {
            OpenChildForm(new jobs_management());
        }
    }
}
