using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using MvvmCross;
using System.Text;
using System.Windows;
using Szperacz.Core.Models;

using Ookii.Dialogs.Wpf;
using MvvmCross.Base;

namespace Szperacz.Core.ViewModels
{
    public class MainViewModel : MvxViewModel
    {
        private string _chart1Path = "";
        private string _chart2Path = "";
        private string _chart3Path = "";

        private string _path = "";
        private string _word = "";
        private bool _createChart = false;
        private bool _letterSizeMeans = true;
        private bool _automaticSelection = false;

        private ObservableCollection<String> _outputPathList = new ObservableCollection<String>();
        private ObservableCollection<String> _cpuThreadList = new ObservableCollection<String>() { "232", "323", "467" };
        private ObservableCollection<String> _pathHistoryList = new ObservableCollection<String>() 
        {
            @"C:\Program Files (x86)\Microsoft Visual Studio\2019",
            @"C:\Users\woj19\AppData",
            @"D:\Users\Obrazy" 
        };
        private ObservableCollection<String> _wordHistoryList = new ObservableCollection<String>() 
        { 
            "Słowo Roku 2020", 
            "Papież", 
            "Lubię Javascript!" 
        };

        public MainViewModel()
        {
            SearchCommand = new MvxCommand(SearchWord);
            FolderCommand = new MvxCommand(SelectFolder);
            ShowGraph1Command = new MvxCommand(ShowGraph1);
            ShowGraph2Command = new MvxCommand(ShowGraph2);
        }

        #region Helper Methods
        private bool IsCorrectPath(string path)
        {
            var drives = DriveInfo.GetDrives();
            var driveNames = new List<string>();

            foreach (var d in drives) driveNames.Add(d.Name);

            if (path.Length < 4) return false;
            if (!driveNames.Contains(path.Substring(0, 3))) return false;

            return true;
        }

        private void ClearGraphs()
        {
            Chart1Path = "";
            Chart2Path = "";
            Chart3Path = "";
        }
        #endregion

        #region Commands and methods for buttons
        public IMvxCommand ShowGraph1Command { get; set; }
        public IMvxCommand ShowGraph2Command { get; set; }

        public void ShowGraph1()
        {
            var chart1PathAlias = Chart1Path;
            var chart3PathAlias = Chart3Path;

            Chart3Path = chart1PathAlias;
            Chart1Path = chart3PathAlias;
        }

        public void ShowGraph2()
        {
            var chart2PathAlias = Chart2Path;
            var chart3PathAlias = Chart3Path;

            Chart2Path = chart3PathAlias;
            Chart3Path = chart2PathAlias;
        }

        // Connected to the select folder button
        public IMvxCommand FolderCommand { get; set; }
        
        /// <summary>
        /// Show folder browser dialog and geth path from user
        /// </summary>
        public void SelectFolder()
        {
            var browser = new VistaFolderBrowserDialog();

            browser.ShowDialog();
            Path = browser.SelectedPath;
        }

        // Connected to the search button
        public IMvxCommand SearchCommand { get; set; }
      
        /// <summary>
        /// Check if path is correct and search for given word
        /// </summary>
        public void SearchWord()
        {
            if (IsCorrectPath(Path) && Word.Length > 0)
            {
                var wordFound = SearchHandler.SearchWord(Word, Path, CreateChart, LetterSizeMeans, AutomaticSelection);

                if(wordFound)
                {
                    Debug.WriteLine("Debug");
                    OutputPathList = new ObservableCollection<String>(SearchHandler.GetPaths());
                }
                if (CreateChart)
                {
                    var graphPaths = SearchHandler.GetChartPaths();
                    Chart1Path = graphPaths[0];
                    Chart2Path = graphPaths[1];
                    Chart3Path = graphPaths[2];
                }
                else
                {
                    ClearGraphs();
                }
            }
            else
            {
                ClearGraphs();
                OutputPathList = new ObservableCollection<String>();
            }
        }
        #endregion

        #region Properties
        public string FolderImgSource
        {
            get 
            {
                var uri = new Uri(@"/Src/folder.png", UriKind.Relative);
                return uri.ToString(); 
            }
        }

        public string Chart1Path
        {
            get { return _chart1Path; }
            set { SetProperty(ref _chart1Path, value); }
        }

        public string Chart2Path
        {
            get { return _chart2Path; }
            set { SetProperty(ref _chart2Path, value); }
        }

        public string Chart3Path
        {
            get { return _chart3Path; }
            set { SetProperty(ref _chart3Path, value); }
        }

        public ObservableCollection<String> OutputPathList
        {
            get { return _outputPathList; }
            set { SetProperty(ref _outputPathList, value); }
        }

        public ObservableCollection<String> CpuThreadList
        {
            get { return _cpuThreadList; }
            set { SetProperty(ref _cpuThreadList, value); }
        }

        public ObservableCollection<String> PathHistoryList
        {
            get { return _pathHistoryList; }
            set { SetProperty(ref _pathHistoryList, value); }
        }

        public ObservableCollection<String> WordHistoryList
        {
            get { return _wordHistoryList; }
            set { SetProperty(ref _wordHistoryList, value); }
        }

        public bool CreateChart
        {
            get { return _createChart; }
            set { _createChart = value; }
        }

        public bool LetterSizeMeans
        {
            get { return _letterSizeMeans; }
            set { _letterSizeMeans = value; }
        }

        public bool AutomaticSelection
        {
            get { return _automaticSelection; }
            set { _automaticSelection = value; }
        }

        public string Path
        {
            get { return _path; }
            set
            {
                SetProperty(ref _path, value);
            }
        }

        public string Word
        {
            get { return _word; }
            set
            {
                SetProperty(ref _word, value);
            }
        }
        #endregion
    }
}
