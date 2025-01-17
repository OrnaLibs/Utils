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

        /// <summary>
        /// Создание автоматически-запускаемого сервиса
        /// </summary>
        /// <param name="id">Идентификатор сервиса</param>
        /// <param name="name">Отображаемое имя сервиса</param>
        /// <param name="path">Путь к исполняемому файла сервиса</param>
        public static void CreateService(string id, string name, string path)
        {
            switch (Environment.OSVersion.Platform)
            {
                case PlatformID.Win32NT:
                    CreateWindowsService(id, name, path);
                    break;
                default:
                    break;
            }
        }
#pragma warning restore CA1416
    }
}
