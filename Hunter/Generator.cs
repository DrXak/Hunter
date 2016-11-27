namespace Hunter
{
    /// <summary>
    /// Генератор объектов
    /// </summary>
    /// <typeparam name="T">Тип игрового объекта для генерации</typeparam>
    class Generator<T> : GameObject
        where T : GameObject, new()
    {
        /// <summary>
        /// Скорость генерации
        /// </summary>
        private int _generateSpeed;
        /// <summary>
        /// Счётчик
        /// </summary>
        private int _counter;

        public Generator(int generatorSpeed)
        {
            _generateSpeed = generatorSpeed;
        }
        /// <summary>
        /// Считаем счётчик и генерируем
        /// </summary>
        protected override void Update()
        {
            _counter++;
            if (_counter >= _generateSpeed)
            {
                _counter = 0;
                Instantiate<T>();
            }
        }
    }
}
