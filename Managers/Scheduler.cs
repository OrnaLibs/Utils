namespace OrnaLibs.Managers
{
    public static class Scheduler
    {
        private static TimeOnly?[] times = null!;
        private static Action?[] actions = null!;
        private static DateOnly?[] lastRun = null!;

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
            var index = FirstNullIndex(actions);
            if (index == -1) throw new OverflowException();
            actions[index] = action;
            times[index] = time;
            lastRun[index] = DateOnly.MinValue;
        }

        public static void Start()
        {
            _thread = new(Loop)
            {
                IsBackground = true
            };
            _thread.Start();
        }

        private static int FirstNullIndex(object?[] arr)
        {
            for (var i = 0; i < arr.Length; i++)
                if (arr[i] is null) return i;
            return -1;
        }

        private static void Loop()
        {
            while (true)
            {
                try
                {
                    for (var i = 0; i < actions.Length; i++)
                    {
                        if (actions[i] is null) continue;
                        if (times[i] <= TimeOnly.FromDateTime(DateTime.Now) && lastRun[i] < DateOnly.FromDateTime(DateTime.Today))
                            actions[i]!.Invoke();
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
