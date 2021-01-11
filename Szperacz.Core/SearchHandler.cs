using IronPython.Hosting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Szperacz.Core.Models;

namespace Szperacz.Core
{
    /// <summary>
    /// Contains methods to operate with the python script to search files.
    /// </summary>
    public static class SearchHandler
    {
        private static readonly string pathListPath = "Src/paths.txt";
        private static readonly string configPath = "Src/config.txt";
        private static readonly string connectorPath = "Src/connector.txt";
        private static readonly string[] chartPaths = new string[] { };

        /// <summary>
        /// Gives arguments to the python script and turn on it.
        /// </summary>
        /// <returns></returns>
        public static async void SearchWord(string word, string path, bool createChart, bool letterSizeMeans, bool automaticSelection, int cpuThreadNumber)
        {
            var list = new List<String> { word, path, Convert.ToInt32(letterSizeMeans).ToString(), cpuThreadNumber.ToString(), Convert.ToInt32(automaticSelection).ToString()};
            File.WriteAllLines(configPath, list);

            ProcessStartInfo start = new ProcessStartInfo();
            var scriptPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "plik.py");
            var appdata = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

            start.FileName = Path.Combine(appdata, @"Programs\Python\Python38\python.exe");
            start.UseShellExecute = false;
            start.WorkingDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Src");
            start.Arguments = "plik.py";
            start.RedirectStandardOutput = true;
            start.RedirectStandardError = true;
            start.LoadUserProfile = true;

            using (Process process = Process.Start(start))
            {
                using (StreamReader reader = process.StandardOutput)
                {
                    string stderr = process.StandardError.ReadToEnd(); 
                    string result = reader.ReadToEnd(); 
                    Debug.WriteLine(stderr);
                    Debug.WriteLine(result);
                }
            }
        }

        private static void SaveToConfig(List<String> list)
        {
            File.WriteAllLines(configPath, list);
        }

        /// <summary>
        /// Reads paths searched by the python script.
        /// </summary>
        /// <returns>List of PathModel</returns>
        public static List<PathModel> GetPaths()
        {
            string text = "";

            using (StreamReader r = new StreamReader(connectorPath, Encoding.UTF8))
            {
                text = r.ReadToEnd();
            }

            var lines = text.Split('\n');
            var list = new List<PathModel>();

            for(int i = 0; i < lines.Length - 1; i++)
            {
                var elems = lines[i].Split(';');

                if(elems.Length > 2)
                {
                    var model = new PathModel(elems[0], int.Parse(elems[1]), PhrasesMaker(elems[2]));
                    list.Add(model);
                }
                else
                {
                    var model = new PathModel(elems[0], int.Parse(elems[1]), new List<String>() { "" } );
                    list.Add(model);
                }
            }

            return list;
        }

        private static List<String> PhrasesMaker(string text)
        {
            var formatedText = text.Replace('[', ' ').Replace(']', ' ').Trim();
            var array = formatedText.Split(',');

            if (array.Length > 0)
            {
                return new List<string>(array);
            }

            return new List<string>();
        }

        /// <summary>
        /// Reads paths to images created by the python script.
        /// </summary>
        /// <returns>List of strings which contains filenames.</returns>
        public static List<String> GetChartPaths()
        {
            return new List<string>() {
                @"D:\Development\GitHub\Szperacz\Szperacz.Wpf\Src\chart1.png",
                @"D:\Development\GitHub\Szperacz\Szperacz.Wpf\Src\chart2.png",
                @"D:\Development\GitHub\Szperacz\Szperacz.Wpf\Src\chart3.jpg"
            };
        }
    }
}
