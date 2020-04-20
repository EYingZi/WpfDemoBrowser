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
            txtSelection2.Text = String.Format(
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
            txtSelection2.Text = sb.ToString();
        }



        private void DatePicker_DateValidationError(object sender, DatePickerDateValidationErrorEventArgs e)
        {
            lblError.Text = "'" + e.Text +
                "' is not a valid value because " + e.Exception.Message;
        }

        private void Calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            // Check all the newly added items.
            foreach (DateTime selectedDate in e.AddedItems)
            {
                if ((selectedDate.DayOfWeek == DayOfWeek.Saturday) ||
                    (selectedDate.DayOfWeek == DayOfWeek.Sunday))
                {
                    lblError.Text = "Weekends are not allowed";

                    ((Calendar)sender).SelectedDates.Remove(selectedDate);
                }
            }

        }
        private void _calendar_OnLoaded(object sender, RoutedEventArgs e)
        {
            _calendar.DisplayMode = CalendarMode.Year;
        }

        //<Calendar Grid.Row="0" Grid.Column= "3" x:Name= "_calendar" DisplayModeChanged= "_calendar_DisplayModeChanged" Loaded= "_calendar_OnLoaded"
        //    DisplayDate="{Binding SelectedMonth, UpdateSourceTrigger=PropertyChanged}" DisplayMode="Month" />
        //private void _calendar_DisplayModeChanged(object sender, CalendarModeChangedEventArgs e)
        //{
        //    _calendar.DisplayMode = CalendarMode.Year;
        //}
        //private void _calendar_OnLoaded(object sender, RoutedEventArgs e)
        //{
        //    _calendar.DisplayMode = CalendarMode.Year;
        //}
    }
}
