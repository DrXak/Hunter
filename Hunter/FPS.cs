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
        public class FramePerSecond
        {
            private int lastTick;
            public int LastFrameRate;
            private int frameRate;
            public int RecommendedWaitTime = 12;
            public int CalculateFrameRate()
            {
                if (Environment.TickCount - lastTick >= 100)
                {
                    LastFrameRate = frameRate;
                    frameRate = 0;
                    lastTick = Environment.TickCount;

                    if (LastFrameRate < 60 && RecommendedWaitTime > 0)
                        RecommendedWaitTime -= 1;
                    if (LastFrameRate > 70)
                        RecommendedWaitTime += 1;
                }
                frameRate+=10;
                return LastFrameRate;
            }
        }
        public FramePerSecond APS { get; private set; }
        public FramePerSecond DPS { get; private set; }
        private readonly Font _fpsFont = new Font("Cuprum", 13);
        public FPS()
        {
            Layer = 10000;
            APS = new FramePerSecond();
            DPS = new FramePerSecond();
        }
        protected override void Update()
        {
            APS.CalculateFrameRate();
            Scene.DrawWaitTime = DPS.RecommendedWaitTime;
            Scene.ActionWaitTime = APS.RecommendedWaitTime;
        }
        public override void Draw(Graphics g)
        {
            DPS.CalculateFrameRate();
            string str = string.Format("Action Per Second: {0}\nDraw Per Second: {1}",
                APS.LastFrameRate,
                DPS.LastFrameRate);
            //g.DrawString(str, _fpsFont, new SolidBrush(Color.Black), Scene.Field.Width - 200, 10);
        }
    }
}