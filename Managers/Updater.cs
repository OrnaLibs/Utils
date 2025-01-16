using System.Diagnostics;
using System.Text;
using System.Text.Json.Nodes;

namespace OrnaLibs.Managers;

public static class Updater
{
    private static Thread? thread;
    private static string _execArgs = null!;
    private static string _account = null!;
    private static string _repo = null!;
    private static int _interval;
    private static Version _version = null!;

    public static void Init(string account, string repo, Version version, int interval = 3600, string execArgs = "")
    {
        _execArgs = execArgs;
        _account = account;
        _repo = repo;
        _version = version;
        _interval = interval;
        thread = new Thread(Checker().Start) { IsBackground = true };
        thread.Start();
    }

    public static void Dispose() => thread?.Interrupt();

    private static async Task Checker()
    {
        while (true)
        {
            try
            {
                var info = await IsInstalledLastVersion();
                if (info.Item1)
                {
                    var path = await DownloadFile(info.Item2, info.Item3);
                    Process.Start(path, _execArgs);
                }
                Thread.Sleep(_interval * 1000);
            }
            catch (ThreadInterruptedException)
            {
                break;
            }
        }
    }

    private static async Task<(bool, string, string)> IsInstalledLastVersion()
    {
        using var http = new HttpClient();
        var resp = await http.GetAsync(UpdaterConstants.VersionUrl);
        var body = await resp.Content.ReadAsStringAsync();
        var json = JsonNode.Parse(body);
        var projInfo = json?[_repo];
        if (projInfo is null) return (false, "", "");
        if (!Version.TryParse(projInfo["version"]?.GetValue<string>(), out var lastVersion) || lastVersion > _version)
            return (false, "", "");
        var id = projInfo["id"]?.GetValue<string>()!;
        var token = projInfo["token"]?.GetValue<string>()!;
        return (true, id, token);
    }

    internal static async Task<string> DownloadFile(string id, string token)
    {
        var url = new StringBuilder(UpdaterConstants.UpdaterUrl);
        url.Replace("%org%", _account);
        url.Replace("%repo%", _repo);
        url.Replace("%id%", id);

        var req = new HttpRequestMessage(HttpMethod.Get, url.ToString());
        req.Headers.Add("Authorization", $"token {token}");

        using var http = new HttpClient();
        var resp = await http.SendAsync(req);
        var path = $"{Path.GetTempPath()}.exe";
        using var fs = new FileStream(path!, FileMode.Create);
        await resp.Content.CopyToAsync(fs);
        return path;
    }
}
