using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfDemoBrowser.PartialViews.Views
{
    /// <summary>
    /// LayoutPanels.xaml 的交互逻辑
    /// </summary>
    public partial class ControlsW : UserControl
    {
        public ControlsW()
        {
            InitializeComponent();

        }

        // TooltipsAndPopup
        private void run_MouseEnter(object sender, MouseEventArgs e)
        {
            popLink.IsOpen = true;
        }
        private void lnk_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(((Hyperlink)sender).NavigateUri.ToString());
        }


        private void LineUp(object sender, RoutedEventArgs e)
        {
            scroller.LineUp();
        }
        private void LineDown(object sender, RoutedEventArgs e)
        {
            scroller.LineDown();
        }
        private void PageUp(object sender, RoutedEventArgs e)
        {
            scroller.PageUp();
        }
        private void PageDown(object sender, RoutedEventArgs e)
        {
            scroller.PageDown();
        }


        private void txt_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (txtSelection == null) return;
            txtSelection.Text = String.Format(
                "Selection from {0} to {1} is \"{2}\"",
                txt.SelectionStart, txt.SelectionLength, txt.SelectedText);
        }
    }
}
