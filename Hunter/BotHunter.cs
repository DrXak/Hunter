using System;
using System.Linq;
using System.Numerics;

namespace Hunter
{
    // Хищник управляемый компьютером
    class BotHunter : Hunter
    {
        // Обновляем направление движения
        protected override void UpdateHunter()
        {
            // Радиус обзора
            float R = Radius * _visibleRadius;
            Scene.Lock.AcquireReaderLock(-1);
            // Список целей
            var gs = Scene.GameObjects
                .OfType<Creature>()
                .Where(x => x != this && Vector2.Distance(Position, x.Position) < R)
                .ToList();
            Scene.Lock.ReleaseReaderLock();
            // Если есть цели, то меняем направление
            if (gs.Count != 0)
            {
                // Взвешенная сумма
                Vector2 V = Vector2.Zero;
                foreach (var item in gs)
                {
                    // Разность размеров
                    var da = (Area - item.Area) / Area;
                    // Предпочтительность цели
                    float O = (float)(Math.Sqrt(600) * da / (1 + 150 * da * da));
                    // Вектор к цели
                    var r = item.Position - Position;
                    // Устанавливаем приоритетность направления
                    float D = (float)Math.Exp(-r.Length());
                    // Корректируем результат
                    V += Vector2.Normalize(r) * O * D;
                }
                // Инициализируем новое направление
                if (V == Vector2.Zero)
                {
                    //_direction = Vector2.Zero;
                    // Инициализируем случайное направление
                    //float angle = (float)(Utility.Random.NextDouble() * 2 * Math.PI);
                    //_direction = Vector2.Transform(Vector2.UnitX, Matrix3x2.CreateRotation(angle));
                }
                else
                {
                    _direction = Vector2.Normalize(V);
                }
            }
        }
    }
}
