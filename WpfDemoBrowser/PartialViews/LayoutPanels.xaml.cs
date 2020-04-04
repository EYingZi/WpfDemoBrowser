using System;
using System.Collections.Generic;
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
    public partial class LayoutPanels : UserControl
    {
        public LayoutPanels()
        {
            InitializeComponent();

            // ModularContent
            //AddHandler(CheckBox.CheckedEvent, new RoutedEventHandler(chk_Checked));
            //AddHandler(CheckBox.UncheckedEvent, new RoutedEventHandler(chk_Unchecked));

            // SimpleInkCanvas
            foreach (InkCanvasEditingMode mode in Enum.GetValues(typeof(InkCanvasEditingMode)))
            {
                lstEditingMode.Items.Add(mode);
                lstEditingMode.SelectedItem = inkCanvas.EditingMode;
            }
        }

        // LocalizableText
        private void chkLongText_Checked(object sender, RoutedEventArgs e)
        {
            cmdPrev.Content = " <- Go to the Previous Window ";
            cmdNext.Content = " Go to the Next Window -> ";
        }

        private void chkLongText_Unchecked(object sender, RoutedEventArgs e)
        {
            cmdPrev.Content = "Prev";
            cmdNext.Content = "Next";
        }

        // ModularContent
        private void chk_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox chk = (CheckBox)e.OriginalSource;
            DependencyObject obj = LogicalTreeHelper.FindLogicalNode(this, chk.Content.ToString());
            ((FrameworkElement)obj).Visibility = Visibility.Visible;
        }

        private void chk_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox chk = (CheckBox)e.OriginalSource;
            DependencyObject obj = LogicalTreeHelper.FindLogicalNode(this, chk.Content.ToString());
            ((FrameworkElement)obj).Visibility = Visibility.Collapsed;
        }
    }
}
