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
    // Игрок
    class Player : Hunter
    {
        // Рекордный радиус
        public float Record { get; private set; } = 0;
        // Обновляем данные игрока
        protected override void Update()
        {
            // Устанавливаем движение по направлению курсора
            _direction = Vector2.Normalize(Scene.Cursor - Position);
            // Вызываем функцию обновления базового класса
            base.Update();
            // Обновляем рекорд
            Record = Math.Max(_radius, Record);
        }
    }
}
