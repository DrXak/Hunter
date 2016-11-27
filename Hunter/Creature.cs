using System;
using System.Drawing;
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
        protected SolidBrush _brush;
        /// <summary>
        /// Приращение движения существа
        /// </summary>
        protected float _dx = 0, _dy = 0;
        /// <summary>
        /// Скорость движения существа
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
            get { return (int)Area; }
            protected set { Area = value; }
        }
        public Creature(float speed)
        {
            // Инициализируем координаты случайным образом в пределах поля
            X = Utility.Random.Next((int)_radius + 1, Scene.Field.Width - (int)_radius - 1);
            Y = Utility.Random.Next((int)_radius + 1, Scene.Field.Height - (int)_radius - 1);
            // Инициализируем скорость движения
            _speed = speed;
            // Инициализируем приращение движения
            float angle = (float)(Utility.Random.Next(359) / 360.0 * Math.PI);
            _dx = (float)Math.Cos(angle) * _speed;
            _dy = (float)Math.Sin(angle) * _speed;
            //Инициализируем цвет
            _brush = new SolidBrush(Color.FromArgb(Utility.Random.Next(255), Utility.Random.Next(255), Utility.Random.Next(255)));
        }
    }
}
