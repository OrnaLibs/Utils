using Microsoft.Win32;
using System.Runtime.Versioning;

namespace OrnaLibs.Extensions
{ 
    [SupportedOSPlatform("windows")]
    public static class RegistryExtensions
    {

        public static RegistryKey CreateRecurseKey(this RegistryKey parent, string path)
        {
            var elem = path.Split('\\');
            if (elem.Length == 0) return parent;
            var key = parent.OpenSubKey(elem[0], true) ?? parent.CreateSubKey(elem[0], true);
            return key.CreateRecurseKey(elem[1..]);
        }

        // todo избавиться от рекурсии
        private static RegistryKey CreateRecurseKey(this RegistryKey parent, params string[] pathParts)
        {
            if (pathParts.Length == 0) return parent;
            var key = parent.OpenSubKey(pathParts[0], true) ?? parent.CreateSubKey(pathParts[0], true);
            return key.CreateRecurseKey(pathParts[1..]);
        }
    }
}
