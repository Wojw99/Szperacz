using MvvmCross.Platforms.Wpf.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Szperacz.Core;
using Szperacz.Core.ViewModels;

namespace Szperacz.Wpf.Views
{
    /// <summary>
    /// Logika interakcji dla klasy MainView.xaml
    /// </summary>
    public partial class MainView : MvxWpfView
    {
        public MainView()
        {
            InitializeComponent();
        }

        private void Button_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            borderWait.Visibility = Visibility.Visible;
        }

        private void MvxWpfView_MouseMove(object sender, MouseEventArgs e)
        {
            borderWait.Visibility = Visibility.Hidden;
        }
    }
}
