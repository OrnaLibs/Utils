using Microsoft.Win32;
using System.Runtime.Versioning;

namespace OrnaLibs
{
    [SupportedOSPlatform("windows")]
    public static class RegistryKeys
    {
        public static class HKLM
        {
            public static RegistryKey Software => Registry.LocalMachine.Software();

            public static RegistryKey SoftwareX86 => Software.Software86();

            public static RegistryKey Uninstall => Software.UninstallPath();

            public static RegistryKey UninstallX86 => SoftwareX86.UninstallPath();

            public static RegistryKey Hardware => Registry.LocalMachine.OpenSubKey("HARDWARE")!;

            public static RegistryKey DeviceMap => Hardware.OpenSubKey("DEVICEMAP")!;

            public static RegistryKey? SerialPorts => DeviceMap.OpenSubKey("SERIALCOMM");
        }

        public static class HKCU
        {
            public static RegistryKey Software => Registry.CurrentUser.Software();

            public static RegistryKey SoftwareX86 => Software.Software86();

            public static RegistryKey Uninstall => Software.UninstallPath();

            public static RegistryKey UninstallX86 => SoftwareX86.UninstallPath();
        }

        public static class HKCC
        {
            public static RegistryKey Software => Registry.CurrentConfig.Software();
        }

        #region Исправление дублей
        private static RegistryKey UninstallPath(this RegistryKey software) => 
            software.OpenSubKey(@"Microsoft\Windows\CurrentVersion\Uninstall", true)!;

        private static RegistryKey Software86(this RegistryKey software) =>
            Environment.Is64BitOperatingSystem ? software.OpenSubKey("WOW6432Node", true)! : software;

        private static RegistryKey Software(this RegistryKey hkey) => hkey.OpenSubKey("Software", true)!;
        #endregion
    }
}
