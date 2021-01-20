using MvvmCross.Platforms.Wpf.Views;
using Szperacz.Core;

namespace Szperacz.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MvxWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MvxWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //SearchHandler.DeleteAllTempFiles();
        }
    }
}
