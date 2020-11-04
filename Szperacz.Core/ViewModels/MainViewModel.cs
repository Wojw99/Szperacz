﻿using MvvmCross.Commands;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Szperacz.Core.Models;

namespace Szperacz.Core.ViewModels
{
    public class MainViewModel : MvxViewModel
    {
        private string _firstName;
        private string _lastName;
        private ObservableCollection<PersonModel> _personList = new ObservableCollection<PersonModel>();

        public MainViewModel()
        {
            AddGuestCommand = new MvxCommand(AddGuest);
        }

        public bool CanAddGuest => FirstName?.Length > 0 && LastName?.Length > 0;

        public IMvxCommand AddGuestCommand { get; set; }
        public void AddGuest()
        {
            var person = new PersonModel(FirstName, LastName);
            FirstName = String.Empty;
            LastName = String.Empty;
            PersonList.Add(person);
        }

        public ObservableCollection<PersonModel> PersonList
        {
            get { return _personList; }
            set { SetProperty(ref _personList, value); } // sends a signal that the list was changed
        }

        public string FirstName
        {
            get { return _firstName; }
            set 
            { 
                SetProperty(ref _firstName, value);
                RaisePropertyChanged(() => FullName);
                RaisePropertyChanged(() => CanAddGuest);
            }
        }

        public string LastName
        {
            get { return _lastName; }
            set 
            { 
                SetProperty(ref _lastName, value);
                RaisePropertyChanged(() => FullName);
                RaisePropertyChanged(() => CanAddGuest);
            }
        }

        public string FullName => $"{FirstName} {LastName}";
    }
}
