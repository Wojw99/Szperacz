using System;
using System.Collections.Generic;
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
    }
}
