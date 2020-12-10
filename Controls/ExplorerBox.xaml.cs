using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Szperacz.Wpf.Controls
{
    /// <summary>
    /// Logika interakcji dla klasy ExplorerBox.xaml
    /// </summary>
    public partial class ExplorerBox : UserControl
    {
        #region Properies
        public object BorderColor
        {
            get { return (object)GetValue(BorderColorProperty); }
            set { SetValue(BorderColorProperty, value); }
        }

        public object TextWatermark
        {
            get { return (object)GetValue(TextWatermarkProperty); }
            set { SetValue(TextWatermarkProperty, value); }
        }

        public object TextInside
        {
            get { return (object)GetValue(TextInsideProperty); }
            set { SetValue(TextInsideProperty, value); }
        }

        public object ExItemSource
        {
            get { return (object)GetValue(ExItemSourceProperty); }
            set { SetValue(ExItemSourceProperty, value); }
        }
        #endregion

        #region Dependencies
        public static readonly DependencyProperty BorderColorProperty =
            DependencyProperty.Register("BorderColor", typeof(object), typeof(ExplorerBox), new PropertyMetadata(0));

        public static readonly DependencyProperty TextWatermarkProperty =
            DependencyProperty.Register("TextWatermark", typeof(object), typeof(ExplorerBox), new PropertyMetadata(0));

        public static readonly DependencyProperty TextInsideProperty =
            DependencyProperty.Register("TextInside", typeof(object), typeof(ExplorerBox), new PropertyMetadata(0));

        public static readonly DependencyProperty ExItemSourceProperty =
            DependencyProperty.Register("ExItemSource", typeof(object), typeof(ExplorerBox), new PropertyMetadata(0));
        #endregion

        public ExplorerBox()
        {
            InitializeComponent();
            BorderColor = Brushes.Gray;
        }

        public void Alert()
        {
            BorderColor = Brushes.Red;
        }

        private void combo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(combo.SelectedIndex > -1)
            {
                TextInside = combo.SelectedItem.ToString();
            }
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

            BorderColor = Brushes.Gray;
            combo.SelectedIndex = -1;
        }
    }
}
