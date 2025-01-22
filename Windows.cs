﻿using Microsoft.Win32;
using OrnaLibs.DataTypes;
using System.Diagnostics;
using System.Runtime.Versioning;
using System.Text;

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
        private static SerialPort[] GetSerialPortsWindows()
        {
            using var registry = Registry.LocalMachine.OpenSubKey(@"HARDWARE\DEVICEMAP\SERIALCOMM")!;
            if (registry is null) return [];
            var names = registry.GetValueNames();
            var ports = new SerialPort[names.Length];
            for (var i = 0; i < names.Length; i++)
                ports[i] = new SerialPort((string)registry.GetValue(names[i])!, names[i].Split('\\')[^1]);
            return ports;
        }
        [SupportedOSPlatform("windows")]
        private static void CreateWindowsService(string name, string displayName, string path)
        {
            var builder = new StringBuilder("New-Service");
            builder.AppendFormat(" -Name \"{0}\"", name);
            builder.AppendFormat(" -DisplayName \"{0}\"", displayName);
            builder.AppendFormat(" -BinaryPathName \"{0}\"", path);
            builder.AppendFormat(" -StartupType \"{0}\"", "Automatic");
            PowerShell(builder.ToString());
        }
    }
}
