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
    public partial class printing_docs : Form
    {
        public printing_docs()
        {
            //InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Xaysetha_System.Report1.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(12, 12);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(1238, 649);
            this.reportViewer1.TabIndex = 0;
            // 
            // printing_docs
            // 
            this.ClientSize = new System.Drawing.Size(1262, 673);
            this.Controls.Add(this.reportViewer1);
            this.Name = "printing_docs";
            this.Load += new System.EventHandler(this.printing_Load);
            this.ResumeLayout(false);

        }

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;

        private void printing_Load(object sender, EventArgs e)
        {

            this.reportViewer1.RefreshReport();
        }
    }
}
