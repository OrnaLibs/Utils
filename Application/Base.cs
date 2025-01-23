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
}
