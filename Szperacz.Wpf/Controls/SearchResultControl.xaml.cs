using System;
using System.Diagnostics;
using System.Windows;
using Szperacz.Core.ViewModels;

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
                await ControlHelper.OpenTxtFile(PathResult as string);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + PathResult.ToString());
            }
        }

        private void SearchResultControl_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            MessageBox.Show(ControlHelper.WordToFind);
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
