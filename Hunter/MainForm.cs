using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Threading;
using System.Linq;

namespace Hunter
{
    /// <summary>
    /// Основная форма
    /// </summary>
    public partial class MainForm : Form
    {
        private Scene _scene;
        public MainForm()
        {
            InitializeComponent();
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            _scene = new Scene(pictureBox1);
            _scene.Start();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
        }
    }
}
