namespace OrnaLibs;

public partial struct Application
{
    /// <summary>
    /// Название приложения
    /// </summary>
    public string Name { get; init; }

    /// <summary>
    /// Версия приложения
    /// </summary>
    public Version? Version { get; init; }

    /// <summary>
    /// Отображаемое название приложения
    /// </summary>
    public string DisplayName { get; init; }

    /// <summary>
    /// Разработчик приложения (Организация в GitHub)
    /// </summary>
    public string CompanyName { get; init; }

    /// <summary>
    /// Путь до исполняемого файла
    /// </summary>
    public string AppPath { get; init; }

    /// <summary>
    /// Путь до иконки приложения
    /// </summary>
    public string IconPath { get; init; }

    /// <summary>
    /// Путь до конфигуратора
    /// </summary>
    public string Configurator { get; init; }

    /// <summary>
    /// Путь до деинсталятора
    /// </summary>
    public string Uninstaller { get; init; }

    /// <summary>
    /// Путь до сервис-приложения
    /// </summary>
    public string ServicePath { get; init; }
}
