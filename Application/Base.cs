namespace OrnaLibs;

/// <summary>
/// Информация о приложении
/// </summary>
public partial struct Application
{
    /// <summary>
    /// Регистрация приложения
    /// </summary>
    public void Registration()
    {
        if (OperatingSystem.IsWindows())
            RegistrationWindows();
    }

    /// <summary>
    /// Удаление записи о приложении
    /// </summary>
    public void Unregistration()
    {
        if (OperatingSystem.IsWindows())
            RegistryKeys.LocalMachineUninstallX86.DeleteSubKey(Name);
    }

    /// <summary>
    /// Регистрация сервиса
    /// </summary>
    public void RegistrationService()
    {
        if (string.IsNullOrWhiteSpace(ServicePath))
            throw new ArgumentNullException(nameof(ServicePath));
        if (OperatingSystem.IsWindows())
            CreateWindowsService();
    }

    /// <summary>
    /// Удаление сервиса
    /// </summary>
    public void UnregistrationService()
    {
        if (string.IsNullOrWhiteSpace(ServicePath))
            throw new ArgumentNullException(nameof(ServicePath));
        if (OperatingSystem.IsWindows())
            Utils.CMD($"sc stop {Name} && sc delete {Name}");
    }
}
