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

namespace Controls
{
    /// <summary>
    /// Interaction logic for ImageList.xaml
    /// </summary>

    public partial class ListControlTest : UserControl
    {

        public ListControlTest()
        {
            InitializeComponent();
        }

        private void list_SelectionChanged(object sender, RoutedEventArgs e)
        {
            
        }

        // CheckBoxList
        private void checkBoxList_SelectionChanged(object sender, RoutedEventArgs e)
        {
            // Select when checkbox portion is clicked (optional).
            if (e.OriginalSource is CheckBox)
            {
                checkBoxList.SelectedItem = e.OriginalSource;
            }

            if (checkBoxList.SelectedItem == null) return;
            txtSelection.Text = String.Format(
                "You chose item at position {0}.\r\nChecked state is {1}.",
                checkBoxList.SelectedIndex,
                ((CheckBox)checkBoxList.SelectedItem).IsChecked);
        }

        private void cmd_CheckAllItems(object sender, RoutedEventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            foreach (CheckBox item in checkBoxList.Items)
            {
                if (item.IsChecked == true)
                {
                    sb.Append(
                        item.Content + " is checked.");
                    sb.Append("\r\n");
                }
            }
            txtSelection.Text = sb.ToString();
        }
    }
}