using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Xaysetha_System
{
    public partial class resident_add : Form
    {
        public resident_add()
        {
            InitializeComponent();
        }

        private void profilePictureBox_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "Choose img(*.jpg; *.png; *.gif; *.bmp)| *.jpg; *.png; *.gif; *.bmp";

            if(openFileDialog.ShowDialog() == DialogResult.OK)
            {
                profilePictureBox.Image = Image.FromFile(openFileDialog.FileName);
            }
        }
    }
}
