using MvvmCross.Core;
using MvvmCross.Platforms.Wpf.Views;
using MvvmCross.Platforms.Wpf.Core;
using System.Globalization;

namespace Szperacz.Wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : MvxApplication
    {
        public App()
        {
            var pl = "pl-PL";
            var en = "en-EN";
            System.Threading.Thread.CurrentThread.CurrentUICulture = new CultureInfo(en);
        }

        protected override void RegisterSetup()
        {
            //System.Threading.Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-EN");
            this.RegisterSetupType<MvxWpfSetup<Core.App>>(); // starting at Szperacz.Core.App
        }
    }
}
