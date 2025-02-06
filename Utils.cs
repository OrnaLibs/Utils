namespace OrnaLibs
{
    public static partial class Utils
    {
#pragma warning disable CA1416
        public static (string Port, string Device)[] GetSerialPorts() =>
            Environment.OSVersion.Platform switch
            {
                PlatformID.Win32NT => GetSerialPortsWindows(),
                _ => []
            };
#pragma warning restore CA1416
    }
}
