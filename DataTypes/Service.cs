using System.Diagnostics;

namespace OrnaLibs.DataTypes
{
    public class Service(string id)
    {

        public void Create(string path, string name) =>
            Execute($"create {id} binPath= \"{path}\" DisplayName= \"{name}\" start= auto");

        public void Stop() => Execute($"stop {id}");

        public void Start() => Execute($"start {id}");

        public void Delete() => Execute($"delete {id}");


        private static void Execute(string command)
        {
            var info = new ProcessStartInfo
            {
                FileName = "sc",
                Arguments = command,
                Verb = "runas",
                UseShellExecute = true,
                CreateNoWindow = true,
            };
            Process.Start(info)!.WaitForExit();
        }
    }
}
