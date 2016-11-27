using System;
using System.Linq;
using System.Drawing;

namespace Hunter
{
    /// <summary>
    /// Еда
    /// </summary>
    class Food : Creature
    {
        public Food() : base(0)
        {
            // Инициализируем площадь еды
            Area = 100;
        }
        /// <summary>
        /// Рисовать еду
        /// </summary>
        /// <param name="g">Объект графики</param>
        public override void Draw(Graphics g)
        {
            // Количество вершин
            int vertexes = 5;
            // Угол между вершинами
            var angle = Math.PI * 2 / vertexes;
            // Координаты центра
            var center = new PointF(X, Y);
            // Генерируем координаты вершин
            var points = Enumerable.Range(0, vertexes)
                  .Select(i => PointF.Add(center, new SizeF((float)Math.Sin(i * angle) * _radius, (float)Math.Cos(i * angle) * _radius)));
            // Рисуем полигон по вершинам
            g.FillPolygon(_brush, points.ToArray());
        }
    }
}
