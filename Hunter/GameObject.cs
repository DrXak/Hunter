using System.Drawing;
using System.Threading;
using System;
using System.Numerics;

namespace Hunter
{
    /// <summary>
    /// Игровой объект
    /// </summary>
    class GameObject
    {
        public event EventHandler Destroing;
        /// <summary>
        /// Координаты объекта
        /// </summary>
        public Vector2 Position;
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
                _thread.Start();
                _thread.IsBackground = true;
            }
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
            GameObject go;
            lock (Scene.GameObjects)
            {
                Scene.GameObjects.TryGetValue(guid, out go);
                if (go != null)
                {
                    go.Stop();
                    go.Destroing?.Invoke(go, EventArgs.Empty);
                    Scene.GameObjects.TryRemove(guid, out go);
                }
                return go != null;
            }
        }
        public static void Instantiate<T>(int count = 1)
            where T : GameObject, new()
        {
            for (int i = 0; i < count; i++)
            {
                T go = new T();
                lock (Scene.GameObjects)
                {
                    Scene.GameObjects.TryAdd(Guid.NewGuid(), go);
                }
                go.Start();
            }
        }
        public static void Instantiate(GameObject go)
        {
            lock (Scene.GameObjects)
            {
                Scene.GameObjects.TryAdd(Guid.NewGuid(), go);
            }
            go.Start();
        }
    }
}
