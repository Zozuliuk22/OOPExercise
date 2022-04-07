using System.IO;
using Newtonsoft.Json.Linq;


namespace AnkhMorpork
{
    internal static class DataLoader
    {       
        internal static JArray LoadNpcsFromJson(string path)
        {          
            if (!File.Exists(path))
                throw new FileNotFoundException("File into the path is not found.");

            var json = File.ReadAllText(path);            
            return JArray.Parse(json);        
        }        
    }
}
