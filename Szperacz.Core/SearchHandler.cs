using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Szperacz.Core
{
    public static class SearchHandler
    {
        private static readonly string pathListPath = "Src/paths.txt";
        private static readonly string[] chartPaths = new string[] { };

        // D:\Development\GitHub\Szperacz\Szperacz.Core\Src\paths.txt
        public static bool SearchWord(string word, string path, bool createChart, bool letterSizeMeans, bool automaticSelection)
        {
            return true;
        }

        public static List<String> GetPaths()
        {
            var lines = File.ReadAllLines(pathListPath);
            var list = new List<string>(lines);
            return list;
        }

        public static List<String> GetChartPaths()
        {
            return new List<string>() {
                @"D:\Development\GitHub\Szperacz\Szperacz.Wpf\Src\chart1.jpg",
                @"D:\Development\GitHub\Szperacz\Szperacz.Wpf\Src\chart2.jpg",
                @"D:\Development\GitHub\Szperacz\Szperacz.Wpf\Src\chart3.jpg"
            };
        }
    }
}
