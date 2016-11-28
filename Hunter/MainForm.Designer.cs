namespace Hunter
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.играToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.зановоToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.пользовательToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.новыйToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ChooseMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RatingMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.BakgroundColorMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.HunterColorMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.паузаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.fpsLabel = new System.Windows.Forms.ToolStripTextBox();
            this.fpsTimer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 31);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1194, 717);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.играToolStripMenuItem,
            this.пользовательToolStripMenuItem,
            this.BakgroundColorMenuItem,
            this.HunterColorMenuItem,
            this.паузаToolStripMenuItem,
            this.fpsLabel});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(9, 3, 0, 3);
            this.menuStrip1.Size = new System.Drawing.Size(1194, 31);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // играToolStripMenuItem
            // 
            this.играToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.зановоToolStripMenuItem,
            this.ExitMenuItem});
            this.играToolStripMenuItem.Name = "играToolStripMenuItem";
            this.играToolStripMenuItem.Size = new System.Drawing.Size(57, 25);
            this.играToolStripMenuItem.Text = "Игра";
            // 
            // зановоToolStripMenuItem
            // 
            this.зановоToolStripMenuItem.Name = "зановоToolStripMenuItem";
            this.зановоToolStripMenuItem.Size = new System.Drawing.Size(152, 26);
            this.зановоToolStripMenuItem.Text = "Заново";
            this.зановоToolStripMenuItem.Click += new System.EventHandler(this.зановоToolStripMenuItem_Click);
            // 
            // ExitMenuItem
            // 
            this.ExitMenuItem.Name = "ExitMenuItem";
            this.ExitMenuItem.Size = new System.Drawing.Size(152, 26);
            this.ExitMenuItem.Text = "Выход";
            this.ExitMenuItem.Click += new System.EventHandler(this.ExitMenuItem_Click);
            // 
            // пользовательToolStripMenuItem
            // 
            this.пользовательToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.новыйToolStripMenuItem,
            this.ChooseMenuItem,
            this.RatingMenuItem});
            this.пользовательToolStripMenuItem.Name = "пользовательToolStripMenuItem";
            this.пользовательToolStripMenuItem.Size = new System.Drawing.Size(121, 25);
            this.пользовательToolStripMenuItem.Text = "Пользователь";
            // 
            // новыйToolStripMenuItem
            // 
            this.новыйToolStripMenuItem.Name = "новыйToolStripMenuItem";
            this.новыйToolStripMenuItem.Size = new System.Drawing.Size(213, 26);
            this.новыйToolStripMenuItem.Text = "Создать";
            this.новыйToolStripMenuItem.Click += new System.EventHandler(this.новыйToolStripMenuItem_Click);
            // 
            // ChooseMenuItem
            // 
            this.ChooseMenuItem.Name = "ChooseMenuItem";
            this.ChooseMenuItem.Size = new System.Drawing.Size(213, 26);
            this.ChooseMenuItem.Text = "Выбрать";
            // 
            // RatingMenuItem
            // 
            this.RatingMenuItem.Name = "RatingMenuItem";
            this.RatingMenuItem.Size = new System.Drawing.Size(213, 26);
            this.RatingMenuItem.Text = "Таблица рекордов";
            this.RatingMenuItem.Click += new System.EventHandler(this.RatingMenuItem_Click);
            // 
            // BakgroundColorMenuItem
            // 
            this.BakgroundColorMenuItem.Name = "BakgroundColorMenuItem";
            this.BakgroundColorMenuItem.Size = new System.Drawing.Size(98, 25);
            this.BakgroundColorMenuItem.Text = "Цвет фона";
            this.BakgroundColorMenuItem.Click += new System.EventHandler(this.BakgroundColorMenuItem_Click);
            // 
            // HunterColorMenuItem
            // 
            this.HunterColorMenuItem.Name = "HunterColorMenuItem";
            this.HunterColorMenuItem.Size = new System.Drawing.Size(124, 25);
            this.HunterColorMenuItem.Text = "Цвет хищника";
            this.HunterColorMenuItem.Click += new System.EventHandler(this.HunterColorMenuItem_Click);
            // 
            // паузаToolStripMenuItem
            // 
            this.паузаToolStripMenuItem.Name = "паузаToolStripMenuItem";
            this.паузаToolStripMenuItem.Size = new System.Drawing.Size(64, 25);
            this.паузаToolStripMenuItem.Text = "Пауза";
            this.паузаToolStripMenuItem.Click += new System.EventHandler(this.паузаToolStripMenuItem_Click);
            // 
            // fpsLabel
            // 
            this.fpsLabel.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.fpsLabel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.fpsLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fpsLabel.Name = "fpsLabel";
            this.fpsLabel.Size = new System.Drawing.Size(350, 25);
            this.fpsLabel.Text = "Action Per Frame: 60 Draw Per Frame: 60";
            // 
            // fpsTimer
            // 
            this.fpsTimer.Tick += new System.EventHandler(this.fpsTimer_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1194, 748);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Хищник";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem пользовательToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem новыйToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ChooseMenuItem;
        private System.Windows.Forms.ToolStripMenuItem RatingMenuItem;
        private System.Windows.Forms.ToolStripMenuItem BakgroundColorMenuItem;
        private System.Windows.Forms.ToolStripMenuItem HunterColorMenuItem;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.ToolStripMenuItem паузаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem играToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem зановоToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ExitMenuItem;
        private System.Windows.Forms.ToolStripTextBox fpsLabel;
        private System.Windows.Forms.Timer fpsTimer;
    }
}

