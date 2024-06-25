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
    public partial class payment_info : Form
    {
        public payment_info()
        {
            InitializeComponent();
            CustomizedGridView();
            AdjustColumnWidths(data, 10);
        }
        public void CustomizedGridView()
        {
            data.ColumnHeadersDefaultCellStyle.Font = new Font("Noto Sans Lao", 10, FontStyle.Regular);
            data.ColumnHeadersHeight = 30;

        }

        private void AdjustColumnWidths(DataGridView dataGridView, int padding)
        {
            foreach (DataGridViewColumn column in dataGridView.Columns)
            {
                // Auto resize column based on the content
                dataGridView.AutoResizeColumn(column.Index, DataGridViewAutoSizeColumnMode.AllCells);

                // Add padding to the width
                column.Width += padding;
            }
        }
    }
}
