using System.Globalization;

namespace OrnaLibs.DataTypes
{
    public struct TimeRange
    {
        public TimeOnly Begin { get; set; }
        public TimeOnly End { get; set; }

        public TimeRange(TimeOnly begin, TimeOnly end)
        {
            Begin = begin;
            End = end;
        }

        public TimeRange(DateTime begin, DateTime end)
        {
            Begin = TimeOnly.FromDateTime(begin);
            End = TimeOnly.FromDateTime(end);
        }

        private TimeRange(string begin, string end)
        {
            Begin = TimeOnly.ParseExact(begin, "HH':'mm", CultureInfo.InvariantCulture);
            End = TimeOnly.ParseExact(end, "HH':'mm", CultureInfo.InvariantCulture);
        }

        public static TimeRange Parse(string text)
        {
            if (TryParse(text, out var res))
                return res;
            throw new FormatException();
        }

        public static bool TryParse(string text, out TimeRange range)
        {
            range = new TimeRange();
            var match = RegExp.TimeRange().Match(text);
            if (!match.Success) return false;
            range = new TimeRange(match.Groups["begin"].Value, match.Groups["end"].Value);
            return true;
        }
    }
}
