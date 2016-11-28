using System;
using System.Drawing;
using System.Numerics;

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
        private const float _areaChange = 0.9996f;
        public Hunter() : base(1, 300)
        {
        }
        /// <summary>
        /// Рисовать хищника
        /// </summary>
        /// <param name="g">Объект графики</param>
        public override void Draw(Graphics g)
        {
            g.FillEllipse(Brush, Position.X - _radius, Position.Y - _radius, _radius * 2, _radius * 2);
        }
        /// <summary>
        /// Обновить данные хищника
        /// </summary>
        protected override void Update()
        {
            // Уменьшаем площадь
            Area *= _areaChange;
            // Проверяем не вышел ли хищник за пределы поля
            if (Position.X + _radius > Scene.Field.Width && _direction.X > 0 || Position.X - _radius < 0 && _direction.X < 0)
                _direction.X = -_direction.X;
            if (Position.Y + _radius > Scene.Field.Height && _direction.Y > 0 || Position.Y - _radius < 0 && _direction.Y < 0)
                _direction.Y = -_direction.Y;
            // Двигаем хищника по направлению скорости движения
            Position += _direction * _speed;
            // Съедаем что можно
            foreach (var item in Scene.GameObjects)
            {
                // Отбираем только существ
                Creature go = item.Value as Creature;
                // Если существо достаточно близко то съедаем
                if (go != null && go != this && Area > go.Area && Vector2.Distance(Position, go.Position) < _radius)
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
