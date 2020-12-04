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
    /// Logika interakcji dla klasy ClickableImage.xaml
    /// </summary>
    public partial class ClickableImage : UserControl
    {

        public object ImagePath
        {
            get { return (object)GetValue(ImagePathProperty); }
            set 
            {
                back.Background = Brushes.Gray;
                SetValue(ImagePathProperty, value);
            }
        }

        // Using a DependencyProperty as the backing store for Source.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ImagePathProperty =
            DependencyProperty.Register("ImagePath", typeof(object), typeof(ClickableImage), new PropertyMetadata(0));

        public ClickableImage()
        {
            InitializeComponent();
        }
    }
}
