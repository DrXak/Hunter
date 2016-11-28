using System;
using System.Drawing;
using System.Numerics;
using System.Linq;

namespace Hunter
{
    // Хищник
    class Hunter : Creature
    {
        // Изменение массы
        private const float _areaChange = 0.9996f;
        // Передаём в базовый конструктор скорость и площадь
        public Hunter() : base(1, 300)
        {
        }
        // Рисовать хищника
        public override void Draw(Graphics g)
        {
            g.FillEllipse(Brush, Position.X - _radius, Position.Y - _radius, _radius * 2, _radius * 2);
        }
        // Обновить данные хищника
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
            // Блокируем список игровых объектов
            lock (Scene.GameObjects)
            {
                // Создаём список объектов которые можно съесть
                var destroyList = Scene.GameObjects
                    .OfType<Creature>()
                    .Where(x => x != this && Area > x.Area && Vector2.Distance(Position, x.Position) < _radius)
                    .ToList();
                // Удаляем их с игры и увеличиваем собственную площадь
                foreach (var item in destroyList)
                {
                    Area += item.Area;
                    Destroy(item);
                }
            }
        }
    }
}
