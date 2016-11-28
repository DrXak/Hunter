namespace Hunter
{
    // Генератор объектов
    class Generator<T> : GameObject
        where T : GameObject, new()
    {
        // Скорость генерации
        private int _generateSpeed;
        // Счётчик
        private int _counter;
        // Инициализируем в конструкторе скорость генерации
        public Generator(int generatorSpeed)
        {
            _generateSpeed = generatorSpeed;
        }
        // Считаем счётчик и генерируем
        protected override void Update()
        {
            _counter++;
            if (_counter >= _generateSpeed)
            {
                _counter = 0;
                Instantiate(new T());
            }
        }
    }
}
