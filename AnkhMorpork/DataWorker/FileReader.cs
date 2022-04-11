using System.IO;

namespace AnkhMorpork
{
    public class FileReader : IFileReader
    {
        public string Read(string path)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException("File into the path is not found.");

            return File.ReadAllText(path);
        }
    }
}
