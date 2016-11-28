using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hunter
{
    public partial class RatingForm : Form
    {
        public RatingForm()
        {
            InitializeComponent();
        }

        private void RatingForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dataDataSet.RatingTable' table. You can move, or remove it, as needed.
            this.ratingTableTableAdapter.Fill(this.dataDataSet.RatingTable);
            dataGridView1.DefaultCellStyle.SelectionBackColor = dataGridView1.DefaultCellStyle.BackColor;
            dataGridView1.DefaultCellStyle.SelectionForeColor = dataGridView1.DefaultCellStyle.ForeColor;
        }
    }
}
