using System;
using System.Drawing;

namespace Hunter
{
    /// <summary>
    /// Хищник
    /// </summary>
    class Hunter : Creature
    {
        /// <summary>
        /// Изменение массы
        /// </summary>
        private const float _areaChange = 0.9995f;
        public Hunter() : base(1)
        {
            // Инициализируем площадь еды
            Area = 300;
        }
        /// <summary>
        /// Рисовать хищника
        /// </summary>
        /// <param name="g">Объект графики</param>
        public override void Draw(Graphics g)
        {
            g.FillEllipse(_brush, X - _radius, Y - _radius, _radius * 2, _radius * 2);
        }
        /// <summary>
        /// Обновить данные хищника
        /// </summary>
        protected override void Update()
        {
            // Уменьшаем площадь
            Area *= _areaChange;
            // Проверяем не вышел ли хищник за пределы поля
            if (X + _radius > Scene.Field.Width && _dx > 0 || X - _radius < 0 && _dx < 0)
                _dx = -_dx;
            if (Y + _radius > Scene.Field.Height && _dy > 0 || Y - _radius < 0 && _dy < 0)
                _dy = -_dy;
            // Двигаем хищника по направлению скорости движения
            X += _dx;
            Y += _dy;
            // Съедаем что можно
            foreach (var item in Scene.GameObjects)
            {
                // Отбираем только существ
                Creature go = item.Value as Creature;
                // Если существо достаточно близко то съедаем
                if (go != null && go != this && Math.Sqrt(Math.Pow(go.X - X, 2) + Math.Pow(go.Y - Y, 2)) < _radius)
                {
                    if (Destroy(item.Key))
                    {
                        Area += go.Area;
                    }
                }
            }
        }
    }
}
