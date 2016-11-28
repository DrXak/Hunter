using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Numerics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hunter
{
    /// <summary>
    /// Существо, базовый класс для еды и хищника
    /// </summary>
    class Creature : GameObject
    {
        /// <summary>
        /// Цвет существа
        /// </summary>
        public SolidBrush Brush;
        /// <summary>
        /// Направление движения
        /// </summary>
        protected Vector2 _direction;
        /// <summary>
        /// Скорость движения
        /// </summary>
        protected float _speed = 0;
        /// <summary>
        /// Радиус существа
        /// </summary>
        protected float _radius
        {
            get { return (float)Math.Sqrt(Area / Math.PI); }
        }
        /// <summary>
        /// Площадь существа
        /// </summary>
        public float Area = 0;
        /// <summary>
        /// Слой зарисовки
        /// </summary>
        public override int Layer
        {
            get { return (int)_radius; }
            protected set {}
        }
        public Creature(float speed, float area)
        {
            // Инициализируем скорость движения
            _speed = speed;
            // Инициализируем площадь
            Area = area;
            // Инициализируем координаты случайным образом в пределах поля
            Position.X = Utility.Random.Next((int)_radius, Scene.Field.Width - (int)_radius);
            Position.Y = Utility.Random.Next((int)_radius, Scene.Field.Height - (int)_radius);
            // Инициализируем направление движения
            float angle = (float)(Utility.Random.NextDouble() * 2 * Math.PI);
            _direction = Vector2.Transform(Vector2.UnitX, Matrix3x2.CreateRotation(angle));
            //Инициализируем цвет
            Brush = new SolidBrush(Color.FromArgb(Utility.Random.Next(255), Utility.Random.Next(255), Utility.Random.Next(255)));
        }
    }
}
