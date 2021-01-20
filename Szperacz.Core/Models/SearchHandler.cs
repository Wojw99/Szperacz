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
        private static readonly string configPath = "Src/config.txt";
        private static readonly string connectorPath = "Src/connector.txt";
        private static readonly List<string> chartPaths = new List<string>() {
                AppDomain.CurrentDomain.BaseDirectory + @"Src\axisChart.png",
                AppDomain.CurrentDomain.BaseDirectory + @"Src\pieChart.png",
                AppDomain.CurrentDomain.BaseDirectory + @"Src\barChart.png"};
        private static List<string> tmpFiles = new List<string>();

        private static void CopyFiles()
        {
            foreach(var s in chartPaths)
            {
                string copy = Path.GetTempFileName().Replace(".tmp", $"{tmpFiles.Count}.tmp");
                File.Copy(s, copy);
                tmpFiles.Add(copy);
            }
        }

        public static void DeleteAllTempFiles()
        {
            foreach (string file in tmpFiles)
            {
                File.Delete(file);
            }
        }

        /// <summary>
        /// Gives arguments to the python script and turn on it.
        /// </summary>
        /// <returns></returns>
        public static void SearchWord(string word, string path, bool createChart, bool letterSizeMeans, bool automaticSelection, int cpuThreadNumber)
        {
            // Write properties (settings) to the config file 
            var list = new List<String> { word, path, Convert.ToInt32(letterSizeMeans).ToString(), cpuThreadNumber.ToString(), Convert.ToInt32(automaticSelection).ToString()};
            File.WriteAllLines(configPath, list);

            ProcessStartInfo start = new ProcessStartInfo();

            // Localize python.exe
            var appdata = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            start.FileName = Path.Combine(appdata, @"Programs\Python\Python38\python.exe");

            // Initial settings to the ProcessStartInfo
            start.UseShellExecute = false;
            start.WorkingDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Src");
            start.Arguments = "plik.py";
            start.RedirectStandardOutput = true;
            start.RedirectStandardError = true;
            start.LoadUserProfile = true;
            start.CreateNoWindow = true;
            start.Verb = "runas";

            //await Task.Run(() => Process.Start(start));
            // Start the proccess and get the output from python script
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

            CopyFiles();
        }

        /// <summary>
        /// Check if the python script found a word. Must be used after the use of the SearchWord method.
        /// </summary>
        /// <returns>True if found, false if not</returns>
        public static bool WordFound()
        {
            var text = File.ReadAllText(connectorPath);

            if(text != String.Empty)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Reads paths from connector file (searched by the python script, after use of SearchWord()).
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

        /// <summary>
        /// Unserialize list of matching phrases.
        /// </summary>
        /// <returns></returns>
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
                tmpFiles[tmpFiles.Count - 3],
                tmpFiles[tmpFiles.Count - 2],
                tmpFiles[tmpFiles.Count - 1]
            };
            //return new List<string>() {
            //    AppDomain.CurrentDomain.BaseDirectory + @"Src\axisChart.png",
            //    AppDomain.CurrentDomain.BaseDirectory + @"Src\pieChart.png",
            //    AppDomain.CurrentDomain.BaseDirectory + @"Src\barChart.png"
            //};
        }
    }
}
