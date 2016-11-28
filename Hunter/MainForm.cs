using System;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using Microsoft.VisualBasic;

namespace Hunter
{
    /// <summary>
    /// Основная форма
    /// </summary>
    public partial class MainForm : Form
    {
        private Scene _scene;
        private DataEntities db;
        private int _userId = 1;

        private Color _userHunterColor
        {
            get { return Color.FromArgb(db.User.First(x => x.Id == _userId).HunterColor); }
            set { db.User.First(x => x.Id == _userId).HunterColor = value.ToArgb(); }
        }
        private Color _userBackgroundColor
        {
            get { return Color.FromArgb(db.User.First(x => x.Id == _userId).BackgroundColor); }
            set { db.User.First(x => x.Id == _userId).BackgroundColor = value.ToArgb(); }
        }
        private Color _currentHunterColor
        {
            get {  return _scene.Player.Brush.Color; }
            set {  _scene.Player.Brush.Color = value; }
        }
        private Color _currentBackgroundColor
        {
            get { return _scene.BackgroundColor; }
            set { _scene.BackgroundColor = value; }
        }

        public MainForm()
        {
            InitializeComponent();
        }
        private void LoadUser()
        {
            _currentBackgroundColor = _userBackgroundColor;
            _currentHunterColor = _userHunterColor;
        }

        private void RestartScene()
        {
            if (_scene != null)
            {
                _scene.StopDrawing();
                _scene.StopActing();
            }
            _scene = new Scene(pictureBox1);
            _scene.Player.Destroing += Player_Destroing;
            LoadUser();
        }
        private void Player_Destroing(object sender, EventArgs e)
        {
            SaveRecord();
        }
        private void SaveRecord()
        {
            int record = (int)_scene.Player.Record;
            if (db.Rating.All(x=>x.Score < record))
            {
                db.Rating.Add(new Rating { UserId = _userId, Date = DateTime.Now, Score = record });
                db.SaveChanges();
            }
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            db = new DataEntities();
            foreach (var item in db.User)
            {
                ChooseMenuItem.DropDownItems.Add(item.Name, null, StripItem_Click);
            }
            RestartScene();
        }
        private void StripItem_Click(object sender, EventArgs e)
        {
            var item = (ToolStripItem)sender;
            _userId = db.User.First(x => x.Name == item.Text).Id;
            LoadUser();
        }
        private void HunterColorMenuItem_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                Color color = colorDialog1.Color;
                _currentHunterColor = color;
                _userHunterColor = color;
            }
        }
        private void BakgroundColorMenuItem_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                Color color = colorDialog1.Color;
                _currentBackgroundColor = color;
                _userBackgroundColor = color;
            }
        }

        private void паузаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Scene.IsActing)
            {
                _scene.StopActing();
            }
            else
            {
                _scene.StartActing();
            }
        }
        private void новыйToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string name = Interaction.InputBox("Введите новое имя пользователя", "Создать");
            if (name != "")
            {
                _userId = db.User.Add(new User { Name = name }).Id;
                db.SaveChanges();
                ChooseMenuItem.DropDownItems.Add(name, null, StripItem_Click);
                LoadUser();
            }
        }
        private void зановоToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RestartScene();
        }
        private void ExitMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void RatingMenuItem_Click(object sender, EventArgs e)
        {
            SaveRecord();
            var form = new RatingForm();
            form.Show();
        }
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveRecord();
            db.SaveChanges();
        }
    }
}
