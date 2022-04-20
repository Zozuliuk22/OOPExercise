using System;
using System.Text.RegularExpressions;

namespace AnkhMorpork.Constants
{
    public static class Paths
    {
        private const string _pattern = @"bin.*";
        private static string _defaultPath = Regex.Replace(AppContext.BaseDirectory, _pattern, "");
        public static string pathToAssassinsJsonFile = _defaultPath + @"InputData\assassins.json";
        public static string pathToBeggarsJsonFile = _defaultPath + @"InputData\beggars.json";
        public static string pathToFoolsJsonFile = _defaultPath + @"InputData\fools.json";
    }
}
