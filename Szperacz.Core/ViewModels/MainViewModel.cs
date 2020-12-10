using MvvmCross.Commands;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Szperacz.Core.Models;
using Ookii.Dialogs.Wpf;
using System.Timers;

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

        private double _messageBoxVisibility = 0;
        private string _messageBoxText = "Nieprawidłowa wartość!";
        private readonly Timer timer = new Timer(2000);

        private ObservableCollection<PathModel> _outputPathList = new ObservableCollection<PathModel>();
        private ObservableCollection<String> _cpuThreadList = new ObservableCollection<String>() { "232", "323", "467" };

        private readonly ObservableCollection<SearchModel> historyList = new ObservableCollection<SearchModel>(HistoryHandler.DeserializeHistoryList());
        private ObservableCollection<string> _pathHistoryList = new ObservableCollection<string>();
        private ObservableCollection<string> _phraseHistoryList = new ObservableCollection<string>();

        public MainViewModel()
        {
            SearchCommand = new MvxCommand(SearchWord);
            FolderCommand = new MvxCommand(SelectFolder);
            ShowGraph1Command = new MvxCommand(ShowGraph1);
            ShowGraph2Command = new MvxCommand(ShowGraph2);

            PathHistoryList = new ObservableCollection<string>(historyList.Select(m => m.FolderPath).Reverse());
            PhraseHistoryList = new ObservableCollection<string>(historyList.Select(m => m.Phrase).Reverse());
        }

        #region Helper Methods
        /// <summary>
        /// Add a new object to the history and refreshes view.
        /// </summary>
        private void AddToHistory(string phrase, string path)
        {
            historyList.Add(new SearchModel(phrase, path));
            PathHistoryList = new ObservableCollection<string>(historyList.Select(m => m.FolderPath).Reverse());
            PhraseHistoryList = new ObservableCollection<string>(historyList.Select(m => m.Phrase).Reverse());
            HistoryHandler.SerializeHistoryList(historyList.ToList());
        }

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

        /// <summary>
        /// Show a warning to the user and count down a timer.
        /// </summary>
        /// <param name="text">Message to the user</param>
        /// <param name="interval">Time of the waring</param>
        private void ShowWarning(string text, double interval = 2000)
        {
            MessageBoxText = text;
            MessageBoxVisibility = 35;
            timer.Elapsed += timerElapsed;
            timer.Interval = interval;
            timer.Start();
        }

        /// <summary>
        /// Occurs when the timer elapsed.
        /// </summary>
        private void timerElapsed(object sender, ElapsedEventArgs e)
        {
            MessageBoxVisibility = 0;
            timer.Stop();
        }
        #endregion

        #region Commands and methods for buttons
        public IMvxCommand ShowGraph1Command { get; set; }
        public IMvxCommand ShowGraph2Command { get; set; }

        /// <summary>
        /// Change the main graph to Graph1
        /// </summary>
        public void ShowGraph1()
        {
            var chart1PathAlias = Chart1Path;
            var chart3PathAlias = Chart3Path;

            Chart3Path = chart1PathAlias;
            Chart1Path = chart3PathAlias;
        }

        /// <summary>
        /// Change the main graph to Graph2
        /// </summary>
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
                AddToHistory(Word, Path);

                if (wordFound)
                {
                    OutputPathList = new ObservableCollection<PathModel>(SearchHandler.GetPaths());

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
                    OutputPathList = new ObservableCollection<PathModel>();
                    ShowWarning("Nie znaleziono szukanej frazy w podanej ścieżce!", 3000);
                }
            }
            else 
            {
                ClearGraphs();
                OutputPathList = new ObservableCollection<PathModel>();
                if (!IsCorrectPath(Path)) ShowWarning("Ścieżka nieprawidłowa!");
                else ShowWarning("Brak podanej frazy!");
            }
        }
        #endregion

        #region Properties
        public string MessageBoxText
        {
            get { return _messageBoxText; }
            set { SetProperty(ref _messageBoxText, value); }
        }

        public double MessageBoxVisibility
        {
            get { return _messageBoxVisibility; }
            set { SetProperty(ref _messageBoxVisibility, value); }
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

        public ObservableCollection<PathModel> OutputPathList
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
            get { return new ObservableCollection<String>(_pathHistoryList.Distinct()); }
            set { SetProperty(ref _pathHistoryList, value); }
        }

        public ObservableCollection<String> PhraseHistoryList
        {
            get { return new ObservableCollection<String>(_phraseHistoryList.Distinct()); }
            set { SetProperty(ref _phraseHistoryList, value); }
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
