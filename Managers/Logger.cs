namespace OrnaLibs.Managers
{
    public static class Logger
    {
        private static Queue<string> Messages = new();
        private static Thread? thread;
        private static string _directory = null!;
        private static int _interval;

        public static bool SetShowInConsole(bool showInConsole) =>
            _showInConsole = showInConsole;

        private static bool _showInConsole = false;

        public static void Init(string directory, int interval = 100, bool showInConsole = false)
        {
            _directory = directory;
            _showInConsole = showInConsole;
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
                    Thread.Sleep(_interval);
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

        private static void AddRecord(string text, string level, Exception? ex = null)
        {
            var msg = $"[{DateTime.Now:yyyy'-'MM'-'dd HH':'mm':'ss}] ({level}) {text}{(ex is null ? "" : $"\n\t{ex}")}";
            if (_showInConsole) Console.WriteLine(msg);
            Messages.Enqueue(msg);
        }

        public static void Information(string text) => AddRecord(text, "INFO");

        public static void Warning(string text) => AddRecord(text, "WARN");
        public static void Warning(Exception ex, string text) => AddRecord(text, "WARN", ex);

        public static void Error(string text) => AddRecord(text, "ERRO");
        public static void Error(Exception ex, string text) => AddRecord(text, "ERRO", ex);

        public static void Fatal(Exception ex, string text) => AddRecord(text, "CRIT", ex);
    }
}
