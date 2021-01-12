using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using Szperacz.Core.ViewModels;
using WindowsInput;
using WindowsInput.Native;

namespace Szperacz.Wpf.Controls
{
    /// <summary>
    /// Logika interakcji dla klasy SearchResultControl.xaml
    /// </summary>
    public partial class SearchResultControl : System.Windows.Controls.UserControl
    {
        public SearchResultControl()
        {
            InitializeComponent();
        }

        private async void OpeningFile_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var startInfo = new ProcessStartInfo();      

                startInfo.FileName = PathResult.ToString();
                startInfo.Arguments = "\"" + PathResult.ToString() + "\"";
                startInfo.UseShellExecute = true;

                Process.Start(startInfo);
                await Task.Delay(500);

                var path = PathResult as string;
                if (path.Contains(".txt"))
                {
                    SimulateCtrlPlusF();
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message + "\n" + PathResult.ToString());
            }
        }

        private void SimulateCtrlPlusF()
        {
            var sim = new InputSimulator();
            var wordToType = ControlHelper.WordToFind;
            sim.Keyboard.ModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_F)
                .TextEntry(wordToType)
                .KeyPress(VirtualKeyCode.RETURN)
                .Sleep(200)
                .KeyPress(VirtualKeyCode.CANCEL);
        }

        private void SearchResultControl_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            //SendKeys.SendWait("{ENTER}");
            System.Windows.MessageBox.Show(ControlHelper.WordToFind);
        }

        private void comboBoxPhrases_Loaded(object sender, RoutedEventArgs e)
        {
            //var list = new List<string>((IEnumerable<string>)comboBoxPhrases.Items.SourceCollection);
            
            if (comboBoxPhrases.Items.GetItemAt(0).ToString() == String.Empty)
            {
                comboBoxPhrases.Visibility = Visibility.Collapsed;
            }
        }

        #region Properties, dependencies
        public object ComboItems
        {
            get { return (object)GetValue(ComboItemsProperty); }
            set { SetValue(ComboItemsProperty, value); }
        }

        public static readonly DependencyProperty ComboItemsProperty =
            DependencyProperty.Register("ComboItems", typeof(object), typeof(SearchResultControl), new PropertyMetadata(0));

        public object PathResult
        {
            get { return (object)GetValue(PathResultProperty); }
            set { SetValue(PathResultProperty, value); }
        }

        public static readonly DependencyProperty PathResultProperty =
            DependencyProperty.Register("PathResult", typeof(object), typeof(SearchResultControl), new PropertyMetadata(0));

        //public object ButtonOpenClick
        //{
        //    get { return (object)GetValue(ButtonOpenClickProperty); }
        //    set { SetValue(ButtonOpenClickProperty, value); }
        //}

        //public static readonly DependencyProperty ButtonOpenClickProperty =
        //    DependencyProperty.Register("ButtonOpenClick", typeof(object), typeof(SearchResultControl), new PropertyMetadata(0));

        public object PhraseAmountResult
        {
            get { return (object)GetValue(PhraseAmountResultProperty); }
            set { SetValue(PhraseAmountResultProperty, value); }
        }


        public static readonly DependencyProperty PhraseAmountResultProperty =
            DependencyProperty.Register("PhraseAmountResult", typeof(object), typeof(SearchResultControl), new PropertyMetadata(0));

        public object MatchPhraseResult
        {
            get { return (object)GetValue(MatchPhraseResultProperty); }
            set { SetValue(MatchPhraseResultProperty, value); }
        }


        public static readonly DependencyProperty MatchPhraseResultProperty =
            DependencyProperty.Register("MatchPhraseResult", typeof(object), typeof(SearchResultControl), new PropertyMetadata(0));

        #endregion
    }
}
