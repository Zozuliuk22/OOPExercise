using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using System.Linq;
using AnkhMorpork.NPCs;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json.Linq;


namespace AnkhMorpork
{
    internal static class DataLoader
    {
        internal static IEnumerable<T> LoadNpcsFromXml<T>(string path) where T : class
        {
            var list = new List<T>();
            var xmlSerializer = new XmlSerializer(typeof(T[]));

            using (var fileStream = new FileStream(path, FileMode.OpenOrCreate))
            {
                T[] npcs = xmlSerializer.Deserialize(fileStream) as T[];
                if (npcs is not null)
                    list = npcs.ToList();
            }

            return list;
        }       

        internal static JArray LoadNpcsFromJson(string path)
        {           
            var json = File.ReadAllText(path);
            return JArray.Parse(json);        
        }        
    }
}
