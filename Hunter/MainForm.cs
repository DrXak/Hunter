using System;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using System.Threading;
using Microsoft.VisualBasic;

namespace Hunter
{
    // Основная форма
    public partial class MainForm : Form
    {
        // Сцена
        private Scene _scene;
        // База данных
        private DataEntities db;
        // Идентификатор игрока
        private int _userId = 1;
        // Цвет хищника игрока
        private Color _userHunterColor
        {
            get { return Color.FromArgb(db.User.First(x => x.Id == _userId).HunterColor); }
            set { db.User.First(x => x.Id == _userId).HunterColor = value.ToArgb(); }
        }
        // Цвет фона игрока
        private Color _userBackgroundColor
        {
            get { return Color.FromArgb(db.User.First(x => x.Id == _userId).BackgroundColor); }
            set { db.User.First(x => x.Id == _userId).BackgroundColor = value.ToArgb(); }
        }
        // Цвет хищника текущий
        private Color _currentHunterColor
        {
            get {  return _scene.Player.Brush.Color; }
            set {  _scene.Player.Brush.Color = value; }
        }
        // Цвет фона текущий
        private Color _currentBackgroundColor
        {
            get { return _scene.BackgroundColor; }
            set { _scene.BackgroundColor = value; }
        }
        // Инициализируем данные в конструкторе
        public MainForm()
        {
            InitializeComponent();
        }
        // Инициализируем дополнительные данные при загрузке окна
        private void MainForm_Load(object sender, EventArgs e)
        {
            // Загружаем базу данных
            LoadDataBase();
            // Перезапускаем сцену
            RestartScene();
        }
        // Загрузить базу данных
        private void LoadDataBase()
        {
            // Создаём объект базы данных
            db = new DataEntities();
            // Загружаем всех пользователей
            foreach (var item in db.User)
            {
                ChooseMenuItem.DropDownItems.Add(item.Name, null, StripItem_Click);
            }
        }
        // Перезапустить сцену
        private void RestartScene()
        {
            // Если сцена уже создана то останавливаем её
            if (_scene != null)
            {
                _scene.StopDrawing();
                _scene.StopActing();
            }
            // Создаём новую сцену
            _scene = new Scene(pictureBox1);
            // Подписываемся на событие смерти игркоа
            _scene.Player.Destroing += Player_Destroing;
            // Загружаем данные игрока
            LoadUser();
        }
        // Сохраняем рекорд при смерти игрока
        private void Player_Destroing(object sender, EventArgs e)
        {
            SaveRecord();
        }
        // Загрузить данные игрока
        private void LoadUser()
        {
            Text = "Хищник - " + db.User.First(x => x.Id == _userId).Name;
            _currentBackgroundColor = _userBackgroundColor;
            _currentHunterColor = _userHunterColor;
        }
        // Сохраняем рекорд
        private void SaveRecord()
        {
            int record = (int)_scene.Player.Record;
            // Если в таблице рекордов нет рекорда лучше, то сохраняем
            if (db.Rating.All(x=>x.Score < record))
            {
                db.Rating.Add(new Rating { UserId = _userId, Date = DateTime.Now, Score = record });
                db.SaveChanges();
            }
        }
        // Загружаем пользователя при нажатии на имя
        private void StripItem_Click(object sender, EventArgs e)
        {
            var item = (ToolStripItem)sender;
            _userId = db.User.First(x => x.Name == item.Text).Id;
            LoadUser();
        }
        // Устанавливаем новый цвет хищника
        private void HunterColorMenuItem_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                Color color = colorDialog1.Color;
                _currentHunterColor = color;
                _userHunterColor = color;
            }
        }
        // Устанавливаем новый цвет фона
        private void BakgroundColorMenuItem_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                Color color = colorDialog1.Color;
                _currentBackgroundColor = color;
                _userBackgroundColor = color;
            }
        }
        // Ставим на паузу и запускаем игру
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
        // Создать нового пользователя
        private void новыйToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string name = Interaction.InputBox("Введите новое имя пользователя", "Создать");
            if (name != "")
            {
                var user = new User
                {
                    Name = name,
                    BackgroundColor = Color.Black.ToArgb(),
                    HunterColor = Color.White.ToArgb()
                };
                db.User.Add(user);
                db.SaveChanges();
                _userId = user.Id;
                LoadDataBase();
                LoadUser();
            }
        }
        // Начать игру заново
        private void зановоToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RestartScene();
        }
        // Выйти из игры
        private void ExitMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
        // Показать таблицу рейтингов
        private void RatingMenuItem_Click(object sender, EventArgs e)
        {
            SaveRecord();
            var form = new RatingForm();
            form.Show();
        }
        // Сохраняем данные при выходе
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _scene.StopActing();
            _scene.StopDrawing();
            // Ждём пока не остановятся все потоки
            bool IsSceneRunning;
            do
            {
                lock (Scene.GameObjects)
                {
                    IsSceneRunning = Scene.GameObjects.Any(x => x.Thread.IsAlive);
                }
                if (IsSceneRunning)
                    Thread.Sleep(10);
            } while (IsSceneRunning);

            SaveRecord();
            db.SaveChanges();
        }
        // Обновление положения курсора в сцене
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (_scene != null)
            {
                Scene.Cursor = new System.Numerics.Vector2(e.Location.X, e.Location.Y);
            }
        }
    }
}
