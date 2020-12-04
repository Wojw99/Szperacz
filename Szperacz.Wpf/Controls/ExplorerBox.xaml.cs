using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace Szperacz.Wpf.Controls
{
    /// <summary>
    /// Logika interakcji dla klasy ExplorerBox.xaml
    /// </summary>
    public partial class ExplorerBox : UserControl
    {
        public object TextWatermark
        {
            get { return (object)GetValue(TextWatermarkProperty); }
            set { SetValue(TextWatermarkProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TextWatermark.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextWatermarkProperty =
            DependencyProperty.Register("TextWatermark", typeof(object), typeof(ExplorerBox), new PropertyMetadata(0));

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
        }

        private void combo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TextInside = combo.SelectedItem.ToString();
        }

        private void textBoxPath_GotFocus(object sender, RoutedEventArgs e)
        {
            textBlockWatermark.Visibility = Visibility.Hidden;
        }

        private void textBoxPath_LostFocus(object sender, RoutedEventArgs e)
        {
            if(textBoxPath.Text == "")
                textBlockWatermark.Visibility = Visibility.Visible;
        }

        private void textBoxPath_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (textBoxPath.Text == "")
            {
                textBlockWatermark.Visibility = Visibility.Visible;
            }
            else
            {
                textBlockWatermark.Visibility = Visibility.Hidden;
            }
        }
    }
}
