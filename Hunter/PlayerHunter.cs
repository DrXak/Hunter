using System;
using System.Numerics;

namespace Hunter
{
    // Игрок
    class PlayerHunter : Hunter
    {
        // Рекордный радиус
        public float Record { get; private set; } = 0;
        // Конструктор
        public PlayerHunter()
        {
            Position = new Vector2(Scene.Field.Width, Scene.Field.Height) / 2;
        }
        // Обновляем направление движения
        protected override void UpdateHunter()
        {
            // Устанавливаем движение по направлению курсора
            _direction = Vector2.Normalize(Scene.Cursor - Position);
            // Обновляем рекорд
            Record = Math.Max(Radius, Record);
        }
    }
}
