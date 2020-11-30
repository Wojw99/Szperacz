using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows;
using Szperacz.Core.Models;
using Ookii.Dialogs.Wpf;

namespace Szperacz.Core.ViewModels
{
    public class MainViewModel : MvxViewModel
    {
        private string _path = "";
        private string _word = "";
        private bool _createChart = false;
        private bool _letterSizeMeans = true;
        private bool _automaticSelection = false;
        private ObservableCollection<String> _outputPathList = new ObservableCollection<String>();

        public MainViewModel()
        {
            SearchCommand = new MvxCommand(SearchWord);
            FolderCommand = new MvxCommand(SelectFolder);
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
        #endregion

        #region Commands and methods for buttons
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
                var pathsWithWord = SearchHandler.SearchWord(Word, Path, CreateChart, LetterSizeMeans, AutomaticSelection);
                OutputPathList = new ObservableCollection<String>(pathsWithWord);
            }
            else
            {
                OutputPathList = new ObservableCollection<String>();
                // TODO: send a warning messege to the user
            }
        }
        #endregion

        #region Properties
        public ObservableCollection<String> OutputPathList
        {
            get { return _outputPathList; }
            set { SetProperty(ref _outputPathList, value); }
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
