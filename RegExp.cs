using System.Text.RegularExpressions;

namespace OrnaLibs
{
    internal static partial class RegExp
    {
        [GeneratedRegex(@"^(?<user>[\w\d-\.]+)@(?<domain>([\w-]+\.)+[\w-]{2,4})$")]
        public static partial Regex Email();
        [GeneratedRegex(@"^(?<begin>([0-1]\d|2[0-3])\:[0-5]\d)\-(?<end>([0-1]\d|2[0-4])\:[0-5]\d)$")]
        public static partial Regex TimeRange();
    }
}
