using Microsoft.Win32;
using System.Runtime.Versioning;
using System.Text;

namespace OrnaLibs
{
    [SupportedOSPlatform("windows")]
    public static class RegistryKeys
    {
        public static RegistryKey LocalMachineUninstallX86
        {
            get
            {
                var path = new StringBuilder();
                path.Append(@"SOFTWARE\");
                if (Environment.Is64BitOperatingSystem) path.Append("WOW6432Node\\");
                path.Append(@"Microsoft\Windows\CurrentVersion\Uninstall\");
                return Registry.LocalMachine.OpenSubKey(path.ToString(), true)!;
            }
        }
        public static RegistryKey LocalMachineUninstall
        {
            get
            {
                var path = new StringBuilder();
                path.Append(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\");
                return Registry.LocalMachine.OpenSubKey(path.ToString(), true)!;
            }
        }
    }
}
