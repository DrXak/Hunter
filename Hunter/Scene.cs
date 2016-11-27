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
        public static readonly ConcurrentDictionary<Guid, GameObject> GameObjects;
        private static readonly Color _backgroundColor;
        private static Bitmap _buffer;
        private static Graphics _graphics;
        public static Control Field { get; private set; }
        public static bool IsAlive { get; private set; } = false;
        public static Vector2 Cursor
        {
            get
            {
                Point p = new Point();
                Field.Invoke(new Action(()=>p = Field.PointToClient(System.Windows.Forms.Cursor.Position)));
                return new Vector2(p.X, p.Y);
            }
        }

        static Scene()
        {
            GameObjects = new ConcurrentDictionary<Guid, GameObject>();
            _backgroundColor = Color.Black;
        }
        public Scene(Control control)
        {
            Field = control;
            _buffer = new Bitmap(Field.Width, Field.Height);
            _graphics = Graphics.FromImage(_buffer);

            Field.Paint += OnPaint;

            GameObject.Instantiate<Hunter>(10);
            GameObject.Instantiate(new Generator<Hunter>(100));
            GameObject.Instantiate<FPS>();
            GameObject.Instantiate<Food>(100);
            GameObject.Instantiate(new Generator<Food>(20));
            GameObject.Instantiate<Player>();

            Thread workerThread = new Thread(Worker);
            workerThread.IsBackground = true;
            workerThread.Start();
            Start();
        }
        private void OnPaint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImageUnscaled(_buffer, Point.Empty);
        }
        private void Worker()
        {
            while (true)
            {
                lock (_buffer)
                {
                    _graphics.CompositingQuality = CompositingQuality.HighQuality;
                    _graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    _graphics.Clear(_backgroundColor);
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
        public void Start()
        {
            IsAlive = true;
            lock (GameObjects)
            {
                foreach (var item in GameObjects)
                {
                    item.Value.Start();
                }
            }
        }
        public void Stop()
        {
            IsAlive = false;
        }
    }
}
