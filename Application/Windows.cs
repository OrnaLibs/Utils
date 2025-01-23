using Microsoft.Win32;
using System.Runtime.Versioning;

namespace OrnaLibs;

public partial struct Application
{
    [SupportedOSPlatform("windows")]
    private void RegistrationWindows()
    {
        using var reg = RegistryKeys.LocalMachineUninstallX86.CreateSubKey(Name, true);
        reg.SetValue("Publisher", CompanyName, RegistryValueKind.String);
        reg.SetValue("EstimatedSize", new FileInfo(AppPath).Length / 1024, RegistryValueKind.DWord);
        reg.SetValue("InstallLocation", Path.GetDirectoryName(AppPath)!, RegistryValueKind.String);
        if (Version is { })
            reg.SetValue("DisplayVersion", Version.ToString(), RegistryValueKind.String);
        if (!string.IsNullOrWhiteSpace(DisplayName))
            reg.SetValue("DisplayName", DisplayName, RegistryValueKind.String);
        if (!string.IsNullOrWhiteSpace(IconPath))
            reg.SetValue("DisplayIcon", IconPath, RegistryValueKind.String);
        if (!string.IsNullOrWhiteSpace(Configurator))
            reg.SetValue("ModifyPath", Configurator, RegistryValueKind.String);
        if (!string.IsNullOrWhiteSpace(Uninstaller))
            reg.SetValue("UninstallString", Uninstaller, RegistryValueKind.String);
    }
}
