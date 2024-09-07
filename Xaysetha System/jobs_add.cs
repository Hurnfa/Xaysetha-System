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

        public jobs_add()
        {
            InitializeComponent();

        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }
    }
}
