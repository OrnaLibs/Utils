using Microsoft.Win32;
using OrnaLibs.DataTypes;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.Versioning;
using System.Security.Principal;

namespace OrnaLibs
{
    public static partial class Utils
    {
        /// <summary>
        /// Выполнение скрипта в PowerShell
        /// </summary>
        [SupportedOSPlatform("windows")]
        public static void PowerShell(string script)
        {
            var info = new ProcessStartInfo
            {
                FileName = "powershell",
                Arguments = $"-WindowStyle hidden -Command \"{script.Replace('"', '\'')}\"",
                Verb = "runas",
                CreateNoWindow = true,
                UseShellExecute = true
            };
            Process.Start(info);
        }

        [SupportedOSPlatform("windows")]
        public static SerialPort[] GetSerialPorts()
        {
            using var registry = Registry.LocalMachine.OpenSubKey(@"HARDWARE\DEVICEMAP\SERIALCOMM")!;
            var names = registry.GetValueNames();
            var ports = new SerialPort[names.Length];
            for (var i = 0; i < names.Length; i++)
                ports[i] = new SerialPort((string)registry.GetValue(names[i])!, names[i].Split('\\')[^1]);
            return ports;
        }
    }
}
