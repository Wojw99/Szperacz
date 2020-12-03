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
        #region Properties
        public string Path
        {
            get
            {
                return (string)GetValue(TextProperty);
            }
            set
            {
                SetValue(TextProperty, value);
                ChangeImage();
            }
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Path", typeof(string), typeof(ClickableImage), new PropertyMetadata(""));
        #endregion

        public ClickableImage()
        {
            InitializeComponent();
        }

        private void ChangeImage()
        {

        }
    }
}
