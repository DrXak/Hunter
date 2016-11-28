using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using System.Drawing.Drawing2D;
using System.Numerics;

namespace Hunter
{
    class Scene
    {
        public static ConcurrentDictionary<Guid, GameObject> GameObjects;
        public Color BackgroundColor;
        private Bitmap _buffer;
        private Graphics _graphics;
        private Thread workerThread;
        public static Control Field { get; private set; }
        public static bool IsActing { get; private set; } = false;
        public static bool IsDrawing { get; private set; } = true;
        public Player Player { get; private set; }
        public static Vector2 Cursor
        {
            get
            {
                Point p = new Point();
                Field.Invoke(new Action(()=>p = Field.PointToClient(System.Windows.Forms.Cursor.Position)));
                return new Vector2(p.X, p.Y);
            }
        }
        public Scene(Control control)
        {
            GameObjects = new ConcurrentDictionary<Guid, GameObject>();
            BackgroundColor = Color.Black;
            Field = control;
            _buffer = new Bitmap(Field.Width, Field.Height);
            _graphics = Graphics.FromImage(_buffer);

            Field.Paint += OnPaint;

            GameObject.Instantiate<Hunter>(10);
            GameObject.Instantiate(new Generator<Hunter>(100));
            GameObject.Instantiate<Food>(100);
            GameObject.Instantiate(new Generator<Food>(20));
            Player = new Player();
            Player.Destroing += Player_Destroing;
            GameObject.Instantiate(Player);

            StartDrawing();
            StartActing();
        }
        private void Player_Destroing(object sender, EventArgs e)
        {
            StopActing();
            GameObject.Instantiate<EndGameLabel>();
        }
        private void OnPaint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImageUnscaled(_buffer, Point.Empty);
        }
        private void Worker()
        {
            while (IsDrawing)
            {
                lock (_buffer)
                {
                    _graphics.CompositingQuality = CompositingQuality.HighQuality;
                    _graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    _graphics.Clear(BackgroundColor);
                    lock (GameObjects)
                    {
                        foreach (var item in GameObjects.OrderBy(x => x.Value.Layer))
                        {
                            item.Value.Draw(_graphics);
                        }
                    }
                }
                Field.Invalidate();
                Thread.Sleep(12);
            }
        }
        private void StartDrawing()
        {
            IsDrawing = true;
            workerThread = new Thread(Worker);
            workerThread.IsBackground = true;
            workerThread.Start();
        }
        public void StartActing()
        {
            IsActing = true;
            lock (GameObjects)
            {
                foreach (var item in GameObjects)
                {
                    item.Value.Start();
                }
            }
        }
        public void StopActing()
        {
            IsActing = false;
        }
        public void StopDrawing()
        {
            IsDrawing = false;
        }
    }
}
