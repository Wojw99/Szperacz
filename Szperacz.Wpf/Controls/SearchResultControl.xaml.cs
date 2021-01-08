using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Szperacz.Core.ViewModels;

namespace Szperacz.Wpf.Controls
{
    /// <summary>
    /// Logika interakcji dla klasy SearchResultControl.xaml
    /// </summary>
    public partial class SearchResultControl : UserControl
    {
        public SearchResultControl()
        {
            InitializeComponent();
        }

        private void OpeningFile_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var startInfo = new ProcessStartInfo();

                startInfo.FileName = PathResult.ToString();
                startInfo.Arguments = "\"" + PathResult.ToString() + "\"";
                startInfo.UseShellExecute = true;

                Process.Start(startInfo);

                MessageBox.Show(ControlHelper.WordToFind);

                // znajdź(ControlHelper.WordToFind)
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + PathResult.ToString());
            }
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

        public object ButtonOpenClick
        {
            get { return (object)GetValue(ButtonOpenClickProperty); }
            set { SetValue(ButtonOpenClickProperty, value); }
        }

        public static readonly DependencyProperty ButtonOpenClickProperty =
            DependencyProperty.Register("ButtonOpenClick", typeof(object), typeof(SearchResultControl), new PropertyMetadata(0));

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
