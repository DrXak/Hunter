using System;
using System.Drawing;
using System.Numerics;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Hunter
{
    class Player : Hunter
    {
        public Player()
        {
            _brush = new SolidBrush(Color.White);
        }
        protected override void Update()
        {
            _direction = Vector2.Normalize(Scene.Cursor - Position);
            base.Update();
        }
    }
}
