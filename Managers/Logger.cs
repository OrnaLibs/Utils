namespace OrnaLibs.Managers
{
    public static class Logger
    {
        private static Queue<string> Messages = new();
        private static Thread? thread;
        private static string _directory = null!;

        public static void Init(string directory)
        {
            _directory = directory;
            thread = new Thread(WriteToFile) { IsBackground = true };
            thread.Start();
        }

        public static void Dispose()
        {
            thread?.Interrupt();
        }

        private static void WriteToFile()
        {
            while (true)
            {
                try
                {
                    Thread.Sleep(100);
                    while (Messages.TryDequeue(out var msg))
                        File.AppendAllText($"{_directory}\\{DateTime.Today:yyyyMMdd}.log", $"{msg}\n");
                }
                catch (ThreadInterruptedException)
                {
                    while (Messages.TryDequeue(out var msg))
                        File.AppendAllText($"{_directory}\\{DateTime.Today:yyyyMMdd}.log", $"{msg}\n");
                    break;
                }
            }
        }

        private static void AddRecord(string text, string level, Exception? ex = null) =>
            Messages.Enqueue($"[{DateTime.Now:yyyy'-'MM'-'dd HH':'mm':'ss}] ({level}) {text}{(ex is null ? "" : $"\n{ex}")}");

        public static void Information(string text) => AddRecord(text, "INFO");

        public static void Warning(string text) => AddRecord(text, "WARN");
        public static void Warning(Exception ex, string text) => AddRecord(text, "WARN", ex);

        public static void Error(string text) => AddRecord(text, "ERRO");
        public static void Error(Exception ex, string text) => AddRecord(text, "ERRO", ex);

        public static void Fatal(Exception ex, string text) => AddRecord(text, "CRIT", ex);
    }
}
