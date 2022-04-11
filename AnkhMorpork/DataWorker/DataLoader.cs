using System;
using Newtonsoft.Json.Linq;

namespace AnkhMorpork
{
    public class DataLoader
    {
        private IFileReader _fileReader;

        public DataLoader(IFileReader fileReader = null)
        {
            _fileReader = fileReader ?? new FileReader();
        }

        public JArray LoadNpcsFromJson(string path)
        {
            var json = _fileReader.Read(path);

            if (String.IsNullOrWhiteSpace(json))
                throw new ArgumentException("File doesn't contain any Json object.");

            return JArray.Parse(json);        
        }        
    }
}
