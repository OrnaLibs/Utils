using System.Text.RegularExpressions;

namespace OrnaLibs.DataTypes
{
    public readonly partial struct SemVersion : IComparable<SemVersion>
    {
        public readonly int Major => _major;
        public readonly int Minor => _minor;
        public readonly int Patch => _patch;

        private readonly int _major;
        private readonly int _minor;
        private readonly int _patch;

        public SemVersion(int major, int minor, int patch)
        {
            _major = major;
            _minor = minor;
            _patch = patch;
        }

        public SemVersion(string? text) : this(0, 0, 0)
        {
            if (text is null) return;
            var match = RegExp().Match(text);
            if (!match.Success) return;
            _major = int.Parse(match.Groups["major"].Value);
            _minor = int.Parse(match.Groups["minor"].Value);
            _patch = int.Parse(match.Groups["patch"].Value);
        }

        public SemVersion(Version vers)
        {
            _major = vers.Major;
            _minor = vers.Minor;
            _patch = vers.Build == -1 ? 0 : vers.Build;
        }

        public readonly int CompareTo(SemVersion other)
        {
            var major = Major.CompareTo(other.Major);
            if (major != 0) return major;
            var minor = Minor.CompareTo(other.Minor);
            if (minor != 0) return minor;
            var patch = Patch.CompareTo(other.Patch);
            //if (major != 0) return major;
            return patch;
        }

        public static bool operator ==(SemVersion left, SemVersion right) =>
            left.CompareTo(right) == 0;

        public static bool operator !=(SemVersion left, SemVersion right) =>
            left.CompareTo(right) != 0;

        public static bool operator >(SemVersion left, SemVersion right) =>
            left.CompareTo(right) > 0;

        public static bool operator <(SemVersion left, SemVersion right) =>
            left.CompareTo(right) < 0;

        public override string ToString() =>
            $"{Major}.{Minor}.{Patch}";

        [GeneratedRegex(@"(?<major>\d+)\.(?<minor>\d+)\.(?<patch>\d+)?.+")]
        internal static partial Regex RegExp();

        public static implicit operator SemVersion(Version vers) => new(vers);
        public static implicit operator Version(SemVersion vers) => new(vers.ToString());
    }
}
