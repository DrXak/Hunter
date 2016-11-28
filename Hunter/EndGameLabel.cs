using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hunter
{
    class EndGameLabel : GameObject
    {
        private static readonly Font _font = new Font("Cuprum", 40, FontStyle.Bold);
        private readonly PointF point;
        public EndGameLabel()
        {
            point = new PointF(Scene.Field.Width / 2 - 150, Scene.Field.Height / 2 - 60);
            Layer = 10000;
        }
        public override void Draw(Graphics g)
        {
            g.DrawString("Поражение", _font, Brushes.DarkRed, point);
        }
    }
}
