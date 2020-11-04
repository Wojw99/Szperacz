using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using Szperacz.Core.ViewModels;

namespace Szperacz.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            RegisterAppStart<MainViewModel>(); // register the MainViewModel as place to start
        }
    }
}
