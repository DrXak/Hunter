using System.Drawing;
using System.Threading;
using System;
using System.Numerics;

namespace Hunter
{
    // Игровой объект
    class GameObject
    {
        // Событие удаления игрового объекта
        public event EventHandler Destroing;
        // Координаты объекта
        public Vector2 Position;
        // Поток объекта
        private Thread _thread;
        // Текущее состояние потока
        private bool _isAlive = false;
        // Слой на котором рисуется объект
        public virtual int Layer { get; protected set; } = 0;
        // Основная функция потока
        private void Updater()
        {
            // Бесконечный цикл пока поток живой и сцена позволяет
            while (Scene.IsActing && _isAlive)
            {
                // Блокируем доступ к объекту
                lock (this)
                {
                    // Обновляем данные
                    Update();
                }
                // И ждём следующего раза
                Thread.Sleep(10);
            }
        }
        // Функция обновления данных, служит для переопределения в дочерних классах
        protected virtual void Update()
        {
        }
        // Запустить поток
        public void Start()
        {
            // Устанавливаем что поток живой
            _isAlive = true;
            // Если поток уже закончил свою работу или ещё не начинал, то создаём новый
            if (_thread == null || !_thread.IsAlive)
            {
                _thread = new Thread(Updater);
                _thread.Start();
                _thread.IsBackground = true;
            }
        }
        //  Остановить поток
        public void Stop()
        {
            _isAlive = false;
        }
        // Рисовать объект, служит для переопределения в дочерних классах
        public virtual void Draw(Graphics g)
        {
        }
        // Удалить игровой объект
        public static void Destroy(GameObject go)
        {
            // Проверяем что нам не передали пустышку
            if (go != null)
            {
                // Удаляем из списка
                Scene.GameObjects.Remove(go);
                // Останавливаем работу объекта
                go.Stop();
                // Вызываем событие удаления у объекта
                go.Destroing?.Invoke(go, EventArgs.Empty);
            }
        }
        // Создать игровой объект
        public static void Instantiate(GameObject go)
        {
            // Блокируем доступ к списку игровых объектов
            lock (Scene.GameObjects)
            {
                // Добавляем новый объект к списку
                Scene.GameObjects.Add(go);
            }
            // Запускаем игровой объект
            go.Start();
        }
    }
}
