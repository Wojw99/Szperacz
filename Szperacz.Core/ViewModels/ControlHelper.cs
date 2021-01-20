using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using WindowsInput;
using WindowsInput.Native;

namespace Szperacz.Core.ViewModels
{
    public static class ControlHelper
    {
        private static readonly int txtOpenTime = 500;
        private static readonly int pdfOpenTime = 1300;
        private static readonly int docxOpenTime = 3000;
        public static string WordToFind { get; set; } = "";
        public static string FolderPath { get; set; } = "";
        public static bool AutomaticSelection { get; set; } = false;

        public static bool IsCorrectPathAndWord()
        {
            var drives = DriveInfo.GetDrives();
            var driveNames = new List<string>();

            foreach (var d in drives) driveNames.Add(d.Name);

            if (FolderPath.Length < 4) return false;
            if (!driveNames.Contains(FolderPath.Substring(0, 3))) return false;
            if (WordToFind == String.Empty) return false;

            return true;
        }

        /// <summary>
        /// Simulates searching in pdf/txt/docx files.
        /// </summary>
        public static void SimulateCtrlPlusF()
        {
            var sim = new InputSimulator();
            var wordToType = WordToFind;
            sim.Keyboard.ModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_F)
                .TextEntry(wordToType)
                .KeyPress(VirtualKeyCode.RETURN)
                .Sleep(200)
                .KeyPress(VirtualKeyCode.CANCEL);
        }

        /// <summary>
        /// Open a file with given path.
        /// </summary>
        /// <param name="path">Path to the file</param>
        public static async Task OpenTxtFile(string path)
        {
            var startInfo = new ProcessStartInfo();

            startInfo.FileName = path;
            startInfo.Arguments = "\"" + path + "\"";
            startInfo.UseShellExecute = true;

            Process.Start(startInfo);

            if (AutomaticSelection)
            {
                if (path.Contains(".txt")) await Task.Delay(txtOpenTime);
                else if (path.Contains(".pdf")) await Task.Delay(pdfOpenTime);
                else if (path.Contains(".docx")) await Task.Delay(docxOpenTime);
                SimulateCtrlPlusF();
            }
        }
    }

    enum MessageType
    {
        PhraseIncorrect,
        PathIncorrect,
        PhraseNotFound
    }
}
