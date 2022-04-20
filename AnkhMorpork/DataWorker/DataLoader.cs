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

        /// <summary>
        /// Load NPC objects from a Json file and return the array of objects.
        /// </summary>
        /// <param name="path">The path to a file with Json objects.</param>
        /// <returns>The array of Json objects.</returns>
        /// <exception cref="ArgumentException">The file into the path doesn't contain any Json object.</exception>
        public JArray LoadNpcsFromJson(string path)
        {
            var json = _fileReader.Read(path);

            if (String.IsNullOrWhiteSpace(json))
                throw new ArgumentException("File doesn't contain any Json object.");

            return JArray.Parse(json);        
        }        
    }
}
