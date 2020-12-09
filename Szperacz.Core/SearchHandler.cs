using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using Szperacz.Core.Models;

namespace Szperacz.Core
{
    /// <summary>
    /// Contains methods to operate with the python script to search files.
    /// </summary>
    public static class SearchHandler
    {
        private static readonly string pathListPath = "Src/paths.txt";
        private static readonly string[] chartPaths = new string[] { };

        /// <summary>
        /// Gives arguments to the python script and turn on it.
        /// </summary>
        /// <returns></returns>
        public static bool SearchWord(string word, string path, bool createChart, bool letterSizeMeans, bool automaticSelection)
        {
            return true;
        }

        /// <summary>
        /// Reads paths searched by the python script.
        /// </summary>
        /// <returns>List of PathModel</returns>
        public static List<PathModel> GetPaths()
        {
            string text = "";

            using (StreamReader r = new StreamReader(pathListPath, Encoding.UTF8))
            {
                text = r.ReadToEnd();
                Debug.WriteLine(text);
            }

            var lines = text.Split('\n');
            var list = new List<PathModel>();

            foreach (var l in lines)
            {
                var elems = l.Replace(" ", "").Split(';');
                var model = new PathModel(elems[0], int.Parse(elems[1]), elems[2]);
                list.Add(model);
            }

            return list;
        }

        /// <summary>
        /// Reads paths to images created by the python script.
        /// </summary>
        /// <returns>List of strings which contains filenames.</returns>
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
