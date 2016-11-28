using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using System.Drawing.Drawing2D;
using System.Numerics;

namespace Hunter
{
    // Игровая сцена
    class Scene
    {
        // Список игровых объектов
        public static List<GameObject> GameObjects;
        // Цвет фона
        public Color BackgroundColor;
        // Буфер рисования
        private Bitmap _buffer;
        // Объект графики
        private Graphics _graphics;
        // Поток рисования
        private Thread drawingThread;
        // Игровая область
        public static Control Field { get; private set; }
        // Индикатор игровых действий
        public static bool IsActing { get; private set; } = false;
        // Индикатор рисования
        public static bool IsDrawing { get; private set; } = true;
        // Игровой объект игрока
        public Player Player { get; private set; }
        // Координаты курсора
        public static Vector2 Cursor;
        // Конструктор
        public Scene(Control control)
        {
            // Инициализируем поля объекта
            GameObjects = new List<GameObject>();
            BackgroundColor = Color.Black;
            Field = control;
            _buffer = new Bitmap(Field.Width, Field.Height);
            _graphics = Graphics.FromImage(_buffer);
            // Берём на себя перерисовку
            Field.Paint += OnPaint;
            // Создаём игровые объекты
            for (int i = 0; i < 10; i++)
                GameObject.Instantiate(new Hunter());
            GameObject.Instantiate(new Generator<Hunter>(100));
            for (int i = 0; i < 100; i++)
                GameObject.Instantiate(new Food());
            GameObject.Instantiate(new Generator<Food>(20));
            Player = new Player();
            Player.Destroing += Player_Destroing;
            GameObject.Instantiate(Player);
            // Запускаем сцену
            StartDrawing();
            StartActing();
        }
        // Функция обратного вызова при удалении игрока
        private void Player_Destroing(object sender, EventArgs e)
        {
            // Останавливаем всякое действие в сцене
            StopActing();
            // Выводим надпись о поражении
            GameObject.Instantiate(new EndGameLabel());
        }
        // Вывести буфер рисования в окно
        private void OnPaint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImageUnscaled(_buffer, Point.Empty);
        }
        // Функция рисования
        private void Drawer()
        {
            // Продолжаем пока разрешено рисовать
            while (IsDrawing)
            {
                // Устанавливаем высокое разрешение
                _graphics.CompositingQuality = CompositingQuality.HighQuality;
                // Устанавливаем высокое качество линий
                _graphics.SmoothingMode = SmoothingMode.AntiAlias;
                // Очищаем экран
                _graphics.Clear(BackgroundColor);
                // Блокируем доступ к списку игровых объектов
                lock (GameObjects)
                {
                    // Проходимся по игровым объектам в порядке возрастания слоя рисования
                    foreach (var item in GameObjects.OrderBy(x => x.Layer))
                    {
                        // Рисуем объект
                        item.Draw(_graphics);
                    }
                }
                // Информируем что можно выводить нарисованное
                Field.Invalidate();
                // Ждём следующего раза
                Thread.Sleep(12);
            }
        }
        // Начать рисование
        private void StartDrawing()
        {
            // Устанавливаем индикатор рисования
            IsDrawing = true;
            // Создаём поток рисования
            drawingThread = new Thread(Drawer);
            // Устанавливаем фоновый режим потока
            // Это чтобы потоки закрылись при закрытии основного потока
            drawingThread.IsBackground = true;
            // Запускаем поток
            drawingThread.Start();
        }
        // Начать действие
        public void StartActing()
        {
            // Устанавливаем индикатор действий
            IsActing = true;
            // Блокируем список игровых объектов
            lock (GameObjects)
            {
                // Запускаем все
                foreach (var item in GameObjects)
                {
                    item.Start();
                }
            }
        }
        // Остановить действия
        public void StopActing()
        {
            IsActing = false;
        }
        // Остановить рисование
        public void StopDrawing()
        {
            IsDrawing = false;
        }
    }
}
