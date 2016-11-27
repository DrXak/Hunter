using System.Drawing;
using System.Threading;
using System;

namespace Hunter
{
    /// <summary>
    /// Игровой объект
    /// </summary>
    class GameObject
    {
        /// <summary>
        /// Координаты объекта
        /// </summary>
        public float X = 0, Y = 0;
        /// <summary>
        /// Поток объекта
        /// </summary>
        private Thread _thread;
        /// <summary>
        /// Текущее состояние потока
        /// </summary>
        private bool _isAlive;
        /// <summary>
        /// Слой объекта
        /// </summary>
        public virtual int Layer { get; protected set; } = 0;

        public GameObject()
        {
            // Инициализируем поток
            _isAlive = false;
        }
        /// <summary>
        /// Основная функция потока
        /// </summary>
        private void Worker()
        {
            // Бесконечный цикл пока поток живой
            while (Scene.IsAlive && _isAlive)
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
        /// <summary>
        /// Функция обновления данных, служит для переопределения в дочерних классах
        /// </summary>
        protected virtual void Update()
        {
        }
        /// <summary>
        /// Запустить поток
        /// </summary>
        public void Start()
        {
            _isAlive = true;
            if (_thread == null || !_thread.IsAlive)
            {
                _thread = new Thread(Worker);
                _thread.IsBackground = true;
            }
            _thread.Start();
        }
        /// <summary>
        ///  Остановить поток
        /// </summary>
        public void Stop()
        {
            _isAlive = false;
        }
        /// <summary>
        /// Рисовать объект, служит для переопределения в дочерних классах
        /// </summary>
        /// <param name="g">Объект графики</param>
        public virtual void Draw(Graphics g)
        {
        }
        public static bool Destroy(Guid guid)
        {
            GameObject tmp;
            Scene.GameObjects[guid]?.Stop();
            return Scene.GameObjects.TryRemove(guid, out tmp);
        }
        public static void Instantiate<T>(int count = 1)
            where T : GameObject, new()
        {
            for (int i = 0; i < count; i++)
            {
                T go = new T();
                Scene.GameObjects.TryAdd(Guid.NewGuid(), go);
                go.Start();
            }
        }
        public static void Instantiate(GameObject go)
        {
            Scene.GameObjects.TryAdd(Guid.NewGuid(), go);
            go.Start();
        }
    }
}
