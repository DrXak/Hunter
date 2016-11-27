using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Hunter
{
    /// <summary>
    /// Индикатор скорости смены кадров
    /// </summary>
    class FPS : GameObject
    {
        class FramePerSecond
        {
            private int lastTick;
            public int LastFrameRate;
            private int frameRate;
            public int CalculateFrameRate()
            {
                if (Environment.TickCount - lastTick >= 1000)
                {
                    LastFrameRate = frameRate;
                    frameRate = 0;
                    lastTick = Environment.TickCount;
                }
                frameRate++;
                return LastFrameRate;
            }
        }
        FramePerSecond ActionPerSecond;
        FramePerSecond DrawPerSecond;
        private readonly Font _fpsFont = new Font("Cuprum", 16, FontStyle.Bold);
        public FPS()
        {
            Layer = 10000;
            ActionPerSecond = new FramePerSecond();
            DrawPerSecond = new FramePerSecond();
        }
        protected override void Update()
        {
            ActionPerSecond.CalculateFrameRate();
        }
        public override void Draw(Graphics g)
        {
            DrawPerSecond.CalculateFrameRate();
            string str = string.Format("APS: {0}\nDPS: {1}",
                ActionPerSecond.LastFrameRate,
                DrawPerSecond.LastFrameRate);
            g.DrawString(str, _fpsFont, new SolidBrush(Color.DarkOrange), Scene.Field.Width - 100, 10);
        }
    }
}