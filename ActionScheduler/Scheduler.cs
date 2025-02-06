namespace OrnaLibs.ActionScheduler
{
    // todo добавить учитывание последного запуска
    // todo добавить запись и чтение даты и времени последного запуска
    public static class Scheduler
    {
        private readonly static Dictionary<string, IExpectedAction> actions = [];

        private static Thread? thread = new(Loop) { IsBackground = true };
        private static CancellationTokenSource? source;
        private static CancellationToken token;

        public static void Start()
        {
#pragma warning disable CA1513
            if (thread is null) throw new ObjectDisposedException(nameof(Scheduler));
#pragma warning restore CA1513
            source?.Dispose();
            source = new CancellationTokenSource();
            token = source.Token;
            thread.Start();
        }

        private static void Loop()
        {
            while (!token.IsCancellationRequested)
            {
                try
                {
                    Thread.Sleep(1000);
                    var _actions = new List<IExpectedAction>(actions.Values);
                    if (_actions.Count == 0) return;
                    foreach (var act in _actions)
                        act.TryExecute();
                }
                catch (Exception ex)
                {
                    if (ex is not ThreadAbortException && ex is not ThreadInterruptedException) throw;
                }
            }
        }

        public static void Add(string id, IExpectedAction action)
        {
            if (!actions.TryAdd(id, action))
                throw new ArgumentException(null, nameof(id));
        }

        public static void Clear() => actions.Clear();

        public static void Stop() => source?.Cancel();

        public static void Dispose()
        {
            thread?.Interrupt();
            thread = null;
        }
    }
}
