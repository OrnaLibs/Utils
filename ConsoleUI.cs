using OrnaLibs.DataTypes;
using System.Globalization;

namespace OrnaLibs
{
    public static class ConsoleUI
    {
        public static string TextBox(string title, string defaultValue = "", Predicate<string>? validate = null)
        {
            string res;
            do
            {
                Console.Clear();
                Console.Write($"{title}{(defaultValue == "" ? "" : $" (По-умолчанию: {defaultValue})")}: ");
                res = Console.ReadLine() ?? defaultValue;
            } while (validate is { } && !validate.Invoke(res));
            return res;
        }

        public static int NumberBox(string title, int defaultValue = 0, int min = 0, int max = 100)
        {
            Console.Clear();
            Console.Write($"{title} [{min}{(defaultValue == 0?"":$"..{defaultValue}")}..{max}]: ");
            var res = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(res) && defaultValue != 0) return defaultValue;
            if (int.TryParse(res, CultureInfo.InvariantCulture, out var i) && i >= min && i <= max) return i;
            return NumberBox(title, defaultValue, min, max);
        }

        public static void Error(string text)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(text);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static int Select(string title, string[] items)
        {
            if(items.Length == 0) throw new ArgumentNullException("items");
            Console.CursorVisible = false;
            int index = 0;
            ConsoleKey key;
            Console.Clear();
            Console.WriteLine($"{title}: ");
            for (var i = 0; i < items.Length; i++)
                Console.WriteLine($"{(index == i ? ">" : " ")} {items[i]}");
            do
            {
                key = Console.ReadKey().Key;
                if (key == ConsoleKey.DownArrow && index < items.Length - 1)
                    SelectItem(index, ++index);
                else if (key == ConsoleKey.UpArrow && index > 0)
                    SelectItem(index, --index);
                else if (key == ConsoleKey.PageDown)
                {
                    SelectItem(index, items.Length - 1);
                    index = items.Length - 1;
                }
                else if (key == ConsoleKey.PageUp)
                {
                    SelectItem(index, 0);
                    index = 0;
                }
            } while (key != ConsoleKey.Enter);
            return index;
        }

        private static void SelectItem(int clearIndex, int placeIndex)
        {
            Console.SetCursorPosition(0, clearIndex+1);
            Console.Write(' ');
            Console.SetCursorPosition(0, placeIndex+1);
            Console.Write('>');
        }

        public static DateOnly DateBox(string title, string format = "yyyy'/'MM'/'dd")
        {
            DateOnly d;
            string? res;
            do
            {
                Console.Clear();
                Console.Write($"{title} (Формат: {format.Replace("'", "").ToUpper()}): ");
                res = Console.ReadLine();
                if (res is null) return DateOnly.FromDateTime(DateTime.Today);
            } while (!DateOnly.TryParseExact(res, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out d));
            return d;
        }

        public static TimeOnly TimeBox(string title, string format = "HH:mm")
        {
            TimeOnly t;
            string? res;
            do
            {
                Console.Clear();
                Console.Write($"{title} (Формат: {format.Replace("'", "").ToUpper()}): ");
                res = Console.ReadLine();
                if (res is null) return TimeOnly.FromDateTime(DateTime.Today);
            } while (!TimeOnly.TryParseExact(res, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out t));
            return t;
        }

        public static bool Ask(string title, bool defaultValue)
        {
            ConsoleKey key;
            Console.Clear();
            ConsoleKey[] waitKeys = [ConsoleKey.Enter, ConsoleKey.L, ConsoleKey.Y];
            Console.Write($"{title}? [{(defaultValue ? "Д" : "д")}/{(defaultValue ? "н" : "Н")}]");
            var pos = Console.GetCursorPosition().Left;
            do
            {
                key = Console.ReadKey().Key;
                Console.SetCursorPosition(pos, 0);
                Console.Write(' ');
                Console.SetCursorPosition(pos, 0);
            } while (!waitKeys.Contains(key));

            return key switch
            {
                (ConsoleKey.L) => true,
                (ConsoleKey.Y) => false,
                _ => defaultValue,
            };
        }
    }
}
