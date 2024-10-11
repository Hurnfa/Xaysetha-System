using Npgsql;
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
    public partial class jobs_add : Form
    {
        NpgsqlCommand cmd;
        db_connect cn = new db_connect();
        int jobs_id;

        public jobs_add()
        {
            InitializeComponent();
            cn.getConnect();
        }

        public void getDataFromMainPage(int id, string name, string task)
        {
            labelHeader.Text = $"{task}ອາຊີບ";
            jobs_id = id;
            txtVillage.Text = name;
            btnSave.Text = task;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Hide();
        }
    }
}
