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
        public static List<GameObject> GameObjects { get; set; }
        // Блокировщик ресурса
        public static ReaderWriterLock Lock { get; private set; } = new ReaderWriterLock();
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
        public PlayerHunter Player { get; private set; }
        // FPS
        public FPS FPS { get; private set; }
        // Координаты курсора
        public static Vector2 Cursor;
        // Задержка между кадрами
        public static int DrawWaitTime = 12;
        public static int ActionWaitTime = 12;
        // Конструктор
        public Scene(Control control)
        {
            // Инициализируем поля объекта
            GameObjects = new List<GameObject>();
            BackgroundColor = Color.Black;
            Field = control;
            _buffer = new Bitmap(Field.Width, Field.Height);
            _graphics = Graphics.FromImage(_buffer);

            // Создаём игровые объекты
            for (int i = 0; i < 10; i++)
                GameObject.Instantiate(new BotHunter());
            GameObject.Instantiate(new Generator<BotHunter>(10));
            for (int i = 0; i < 100; i++)
                GameObject.Instantiate(new Food());
            GameObject.Instantiate(new Generator<Food>(1));
            Player = new PlayerHunter();
            Player.Destroing += Player_Destroing;
            GameObject.Instantiate(Player);
            FPS = new FPS();
            GameObject.Instantiate(FPS);

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
            lock (_buffer)
            {
                e.Graphics.DrawImageUnscaled(_buffer, Point.Empty);
            }
        }
        // Функция рисования
        private void Drawer()
        {
            // Продолжаем пока разрешено рисовать
            while (IsDrawing)
            {
                lock (_buffer)
                {
                    // Устанавливаем высокое разрешение
                    _graphics.CompositingQuality = CompositingQuality.HighQuality;
                    // Устанавливаем высокое качество линий
                    _graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    // Очищаем экран
                    _graphics.Clear(BackgroundColor);
                    // Блокируем доступ к списку игровых объектов
                    Lock.AcquireReaderLock(-1);
                    // Проходимся по игровым объектам в порядке возрастания слоя рисования
                    foreach (var item in GameObjects.OrderBy(x => x.Layer))
                    {
                        // Рисуем объект
                        item.Draw(_graphics);
                    }
                    Lock.ReleaseReaderLock();
                }
                // Информируем что можно выводить нарисованное
                Field.Invalidate();
                // Ждём следующего раза
                Thread.Sleep(DrawWaitTime);
            }
        }
        // Начать рисование
        private void StartDrawing()
        {
            // Берём ответственность за отрисовку на себя
            Field.Paint += OnPaint;
            // Устанавливаем индикатор рисования
            IsDrawing = true;
            // Создаём поток рисования
            drawingThread = new Thread(Drawer);
            // Устанавливаем фоновый режим потока
            // Это чтобы потоки закрылись при закрытии основного потока
            drawingThread.IsBackground = true;
            drawingThread.Name = "scene";
            // Запускаем поток
            drawingThread.Start();
        }
        // Начать действие
        public void StartActing()
        {
            // Устанавливаем индикатор действий
            IsActing = true;
            // Блокируем список игровых объектов
            Lock.AcquireReaderLock(-1);
            // Запускаем все
            foreach (var item in GameObjects)
            {
                item.Start();
            }
            Lock.ReleaseReaderLock();
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
            Field.Paint -= OnPaint;
        }
    }
}
