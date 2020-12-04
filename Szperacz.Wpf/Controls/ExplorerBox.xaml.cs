using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Logika interakcji dla klasy ExplorerBox.xaml
    /// </summary>
    public partial class ExplorerBox : UserControl
    {
        public object TextInside
        {
            get { return (object)GetValue(TextInsideProperty); }
            set { SetValue(TextInsideProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TextInside.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextInsideProperty =
            DependencyProperty.Register("TextInside", typeof(object), typeof(ExplorerBox), new PropertyMetadata(0));

        public object ExItemSource
        {
            get { return (object)GetValue(ExItemSourceProperty); }
            set { SetValue(ExItemSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ExItemSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ExItemSourceProperty =
            DependencyProperty.Register("ExItemSource", typeof(object), typeof(ExplorerBox), new PropertyMetadata(0));

        public ExplorerBox()
        {
            InitializeComponent();
            ExItemSource = new ObservableCollection<String>() { "232", "323", "467" };
        }

        private void combo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TextInside = combo.SelectedItem.ToString();
        }
    }
}
