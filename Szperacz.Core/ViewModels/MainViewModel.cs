using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Text;
using Szperacz.Core.Models;

namespace Szperacz.Core.ViewModels
{
    public class MainViewModel : MvxViewModel
    {
        private string _path;
        private string _word;
        private ObservableCollection<String> _outputPathList = new ObservableCollection<String>();

        public MainViewModel()
        {
            SearchCommand = new MvxCommand(SearchWord);
        }

        // Connected to the search button
        public IMvxCommand SearchCommand { get; set; }
        public void SearchWord()
        {
            if(IsCorrectPath(Path) && Word.Length > 0)
            {
                var pathsWithWord = SearchHandler.SearchWord(Word, Path);
                OutputPathList = new ObservableCollection<String>(pathsWithWord);
            }
            else
            {
                // TODO: send a warning messege to the user
            }
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

        public ObservableCollection<String> OutputPathList
        {
            get { return _outputPathList; }
            set { SetProperty(ref _outputPathList, value); }
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
    }
}
