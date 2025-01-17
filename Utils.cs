using OrnaLibs.DataTypes;

namespace OrnaLibs
{
    public static partial class Utils
    {
#pragma warning disable CA1416
        public static SerialPort[] GetSerialPorts() =>
            Environment.OSVersion.Platform switch
            {
                PlatformID.Win32NT => GetSerialPortsWindows(),
                _ => []
            };
#pragma warning restore CA1416
    }
}
