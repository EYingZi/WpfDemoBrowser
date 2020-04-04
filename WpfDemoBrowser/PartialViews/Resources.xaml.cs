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
using System.Windows.Shapes;

namespace WpfDemoBrowser.PartialViews.Views
{
    /// <summary>
    /// Interaction logic for WindowResources.xaml
    /// </summary>

    public partial class Resources : UserControl
    {

        public Resources()
        {
            InitializeComponent();
        }

        private void cmdChange_Click(object sender, RoutedEventArgs e)
        {
            this.Resources["TileBrush"] = new SolidColorBrush(Colors.LightBlue);

            //ImageBrush brush = (ImageBrush)this.Resources["TileBrush"];
            //brush.Viewport = new Rect(0, 0, 5, 5);
        }
    }
}