using System.Text.RegularExpressions;

namespace OrnaLibs
{
    internal static partial class RegExp
    {
        [GeneratedRegex(@"^(?<user>[\w\d-\.]+)@(?<domain>([\w-]+\.)+[\w-]{2,4})$")]
        public static partial Regex Email();
    }
}
