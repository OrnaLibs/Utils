namespace OrnaLibs.Managers
{
    /// <summary>
    /// Планировщик задач
    /// </summary>
    [Obsolete("Рекомендуется использовать ActionScheduler")]
    public static class TaskScheduler
    {
        private static TimeOnly?[] times = null!;
        private static Action?[] actions = null!;
        private static DateOnly?[] lastRun = null!;
        private static int nullIndex = 0;

        private static int _interval;
        private static Thread _thread = null!;

        /// <summary>
        /// Инициализация планировщика
        /// </summary>
        /// <param name="countTasks">Количество задач</param>
        /// <param name="checkInterval">Интервал между проверками задач в секундах</param>
        public static void Init(int countTasks, int checkInterval = 300)
        {
            times = new TimeOnly?[countTasks];
            actions = new Action?[countTasks];
            lastRun = new DateOnly?[countTasks];
            _interval = checkInterval;
        }

        /// <summary>
        /// Регистрация задачи
        /// </summary>
        /// <param name="time">Время, в которое будет срабатывать действие</param>
        /// <param name="action">Действие</param>
        /// <exception cref="OverflowException">Вызывается, когда отсутствует место для регистрации нового задачи</exception>
        public static void RegisterTask(TimeOnly time, Action action)
        {
            if (nullIndex == actions.Length) throw new OverflowException();
            actions[nullIndex] = action;
            times[nullIndex] = time;
            lastRun[nullIndex] = DateOnly.MinValue;
            nullIndex++;
        }

        /// <summary>
        /// Запуск планировщика
        /// </summary>
        public static void Start()
        {
            _thread = new(Loop)
            {
                IsBackground = true
            };
            _thread.Start();
        }

        private static void Loop()
        {
            while (true)
            {
                try
                {
                    var now = DateTime.Now;
                    for (var i = 0; i < actions.Length; i++)
                    {
                        if (actions[i] is null) continue;
                        if (times[i] <= TimeOnly.FromDateTime(now) && lastRun[i] < DateOnly.FromDateTime(now))
                        {
                            times[i] = TimeOnly.FromDateTime(now);
                            lastRun[i] = DateOnly.FromDateTime(now);
                            actions[i]!.Invoke();
                        }
                    }
                    Thread.Sleep(_interval*1000);
                }
                catch (ThreadInterruptedException)
                {
                    break;
                }
            }
        }

        /// <summary>
        /// Отключение и освобожение планировщика
        /// </summary>
        public static void Dispose() => _thread.Interrupt();
    }
}
