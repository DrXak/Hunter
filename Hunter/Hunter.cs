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
        // Минимальный размер
        protected const float _minArea = 300;
        // Минимальный радиус
        protected static readonly float _minRadius = (float)Math.Sqrt(_minArea / Math.PI);
        // Радиус обзора
        protected float _visibleRadius;
        // Передаём в базовый конструктор скорость и площадь
        public Hunter() : base(1, _minArea)
        {
            _visibleRadius = (float)Utility.Random.NextDouble() * 6;
        }
        // Рисовать хищника
        public override void Draw(Graphics g)
        {
            g.FillEllipse(Brush, Position.X - Radius, Position.Y - Radius, Radius * 2, Radius * 2);
        }
        // Обновить данные хищника
        protected override void Update()
        {
            // Обновить дополнительные данные хищника
            UpdateHunter();
            // Корректируем скорость
            _speed = (float)Math.Exp(-Math.Abs(Radius - _minRadius) / 50) * 1.4f;
            // Уменьшаем площадь
            if (Area > _minArea)
                Area *= _areaChange;
            // Проверяем не вышел ли хищник за пределы поля
            if (Position.X + Radius > Scene.Field.Width && _direction.X > 0 || Position.X - Radius < 0 && _direction.X < 0)
                _direction.X = -_direction.X;
            if (Position.Y + Radius > Scene.Field.Height && _direction.Y > 0 || Position.Y - Radius < 0 && _direction.Y < 0)
                _direction.Y = -_direction.Y;
            // Двигаем хищника по направлению скорости движения
            Position += _direction * _speed;
            // Блокируем список игровых объектов
            Scene.Lock.AcquireReaderLock(-1);
            // Создаём список объектов которые можно съесть
            var destroyList = Scene.GameObjects
                .OfType<Creature>()
                .Where(x => x != this && Area > x.Area && Vector2.Distance(Position, x.Position) < Radius)
                .ToList();
            Scene.Lock.ReleaseReaderLock();
            // Удаляем их с игры и увеличиваем собственную площадь
            foreach (var item in destroyList)
            {
                Area += item.Area;
                Destroy(item);
            }
        }
        // Обновить направление движения
        protected virtual void UpdateHunter()
        {
        }
    }
}
