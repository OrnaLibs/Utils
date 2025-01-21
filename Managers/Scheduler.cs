namespace OrnaLibs.Managers
{
    public static class Scheduler
    {
        private static TimeOnly?[] times = null!;
        private static Action?[] actions = null!;
        private static DateOnly?[] lastRun = null!;
        private static int nullIndex = 0;

        private static int _interval;
        private static Thread _thread = null!;

        public static void Init(int countTasks, int checkInterval = 300)
        {
            times = new TimeOnly?[countTasks];
            actions = new Action?[countTasks];
            lastRun = new DateOnly?[countTasks];
            _interval = checkInterval;
        }

        public static void RegisterTask(TimeOnly time, Action action)
        {
            if (nullIndex == actions.Length) throw new OverflowException();
            actions[nullIndex] = action;
            times[nullIndex] = time;
            lastRun[nullIndex] = DateOnly.MinValue;
            nullIndex++;
        }

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

        public static void Dispose() => _thread.Interrupt();
    }
}
