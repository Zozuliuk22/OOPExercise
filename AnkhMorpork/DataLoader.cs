using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using System.Linq;
using AnkhMorpork.NPCs;
using System.Runtime.Serialization;
using System.Text.Json;

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

        internal static IEnumerable<BeggarNpc> CreateBeggarsConstants()
        {
            return new List<BeggarNpc>()
            {
                new BeggarNpc("John", Enums.BeggarsPractice.Twitchers),
                new BeggarNpc("Elleon", Enums.BeggarsPractice.Droolers),
                new BeggarNpc("Bobby", Enums.BeggarsPractice.Dribblers),
                new BeggarNpc("George", Enums.BeggarsPractice.Mumblers),
                new BeggarNpc("Shyam", Enums.BeggarsPractice.Mutterers),
                new BeggarNpc("Ronnie", Enums.BeggarsPractice.WalkingAlongShouters),
                new BeggarNpc("Katerina", Enums.BeggarsPractice.Demanders),
                new BeggarNpc("Tyron", Enums.BeggarsPractice.JimmyCaller),
                new BeggarNpc("Onur", Enums.BeggarsPractice.EightpenceForMeal),
                new BeggarNpc("Nadia", Enums.BeggarsPractice.TuppenceForTea),
                new BeggarNpc("Mohsin", Enums.BeggarsPractice.BeerNeeders)
            };
        }

        internal static IEnumerable<FoolNpc> CreateFoolsConstants()
        {
            return new List<FoolNpc>()
            {
                new FoolNpc("Dillon", Enums.FoolsPractice.Muggins),
                new FoolNpc("Zoey", Enums.FoolsPractice.Gull),
                new FoolNpc("Humphrey", Enums.FoolsPractice.Dupe),
                new FoolNpc("Stephan", Enums.FoolsPractice.Butt),
                new FoolNpc("Chloe-Louise", Enums.FoolsPractice.Fool),
                new FoolNpc("Montel", Enums.FoolsPractice.Tomfool),
                new FoolNpc("Lynn", Enums.FoolsPractice.StupidFool),
                new FoolNpc("Iylah", Enums.FoolsPractice.ArchFool),
                new FoolNpc("Jozef", Enums.FoolsPractice.CompleteFool)
            };
        }

        internal static IEnumerable<AssassinNpc> CreateAssassinsConstants()
        {
            return new List<AssassinNpc>()
            {
               new AssassinNpc("Black Widow"),
               new AssassinNpc("Mockingjay"),
               new AssassinNpc("Lonely Barman"),
               new AssassinNpc("Robot Arlye"),
               new AssassinNpc("Sniper Ghost")
            };
        }
    }
}
