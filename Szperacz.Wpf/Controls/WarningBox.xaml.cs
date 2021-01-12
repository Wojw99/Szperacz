using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
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
    /// Logika interakcji dla klasy WarningBox.xaml
    /// </summary>
    public partial class WarningBox : UserControl
    {
        public object WarningNotFound
        {
            get { return (object)GetValue(WarningNotFoundProperty); }
            set { SetValue(WarningNotFoundProperty, value); }
        }

        public static readonly DependencyProperty WarningNotFoundProperty =
            DependencyProperty.Register("WarningNotFound", typeof(object), typeof(WarningBox), new PropertyMetadata(0));

        public object WarningPhraseIncorrect
        {
            get { return (object)GetValue(WarningPhraseIncorrectProperty); }
            set { SetValue(WarningPhraseIncorrectProperty, value); }
        }

        public static readonly DependencyProperty WarningPhraseIncorrectProperty =
            DependencyProperty.Register("WarningPhraseIncorrect", typeof(object), typeof(WarningBox), new PropertyMetadata(0));

        public object WarningPathIncorrect
        {
            get { return (object)GetValue(WarningPathIncorrectProperty); }
            set { SetValue(WarningPathIncorrectProperty, value); }
        }

        public static readonly DependencyProperty WarningPathIncorrectProperty =
            DependencyProperty.Register("WarningPathIncorrect", typeof(object), typeof(WarningBox), new PropertyMetadata(0));

        public WarningBox()
        {
            InitializeComponent();
        }
    }
}
