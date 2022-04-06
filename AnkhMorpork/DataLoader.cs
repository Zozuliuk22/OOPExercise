using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using System.Linq;

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
                T[]? assassins = xmlSerializer.Deserialize(fileStream) as T[];
                if (assassins is not null)
                    list = assassins.ToList();
            }

            return list;
        }
    }
}
