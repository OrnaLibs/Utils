namespace OrnaLibs.DataTypes
{
    public struct RequiredDirectory(string path)
    {
        private void RecursiveCreate()
        {
            var elems = new Queue<string>(path.Split(Path.DirectorySeparatorChar));
            var _path = elems.Dequeue();
            do
            {
                _path = Path.Combine(_path, elems.Dequeue());
                if(!Directory.Exists(_path))
                    Directory.CreateDirectory(_path);
            }
            while (elems.Count > 0);
        }

        public override string ToString()
        {
            if (!Directory.Exists(path))
                RecursiveCreate();
            return path;
        }

        public static implicit operator RequiredDirectory(string path) => new(path);
        public static implicit operator string(RequiredDirectory dir) => dir.ToString();
    }
}
