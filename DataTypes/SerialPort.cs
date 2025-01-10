using System.Globalization;
using System.Text;

namespace OrnaLibs.DataTypes
{
    public struct SerialPort : IFormattable
    {
        public string Port { get; }
        public string Device { get; }

        public SerialPort(string port, string device)
        {
            Port = port; 
            Device = device;
        }

        public override string ToString() => ToString(null, CultureInfo.InvariantCulture);

        public string ToString(string? format) => ToString(format, CultureInfo.InvariantCulture);

        public string ToString(string? format, IFormatProvider? formatProvider)
        {
            if (format is null || string.IsNullOrWhiteSpace(format)) format = "P";
            var text = new StringBuilder();
            foreach (var ch in format)
            {
                text.Append($"{ch}".ToUpper() switch
                {
                    "P" => Port,
                    "D" => Device,
                    _ => ch
                });
            }
            return text.ToString();
        }
    }
}
