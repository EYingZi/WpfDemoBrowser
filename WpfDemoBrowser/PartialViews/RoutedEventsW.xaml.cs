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
    public partial class RoutedEventsW : UserControl
    {
        public RoutedEventsW()
        {
            InitializeComponent();

            // ButtonMouseUpEvent
            cmd.AddHandler(Button.MouseUpEvent, new RoutedEventHandler(Backdoor), true);
        }


        protected int BubbledEventCounter = 0;

        private void SomethingClicked(object sender, RoutedEventArgs e)
        {
            BubbledEventCounter++;
            string message = $"#{BubbledEventCounter}:\r\n" +
                $" Sender: {sender}\r\n" +
                $" Source: {e.Source}\r\n" +
                $" Original Source: {e.OriginalSource}";
            lstMessages.Items.Add(message);
            e.Handled = (bool)chkHandle.IsChecked;
        }

        private void cmdClear_Click(object sender, RoutedEventArgs e)
        {
            BubbledEventCounter = 0;
            lstMessages.Items.Clear();
        }



        protected int TunneledEventCounter = 0;

        private void SomeKeyPressed(object sender, RoutedEventArgs e)
        {
            TunneledEventCounter++;
            string message = $"#{TunneledEventCounter}:\r\n" +
                $" Sender: {sender}\r\n" +
                $" Source: {e.Source}\r\n" +
                $" Original Source: {e.OriginalSource}\r\n" +
                $" Event: {e.RoutedEvent}";
            lstMessages2.Items.Add(message);
            e.Handled = (bool)chkHandle2.IsChecked;
        }

        private void cmdClear2_Click(object sender, RoutedEventArgs e)
        {
            TunneledEventCounter = 0;
            lstMessages2.Items.Clear();
        }



        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("The Button.Click event occurred. This may have been triggered with the keyboard.");
        }

        private void NeverCalled(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("You didn't see this message. That would be impossible.");
        }

        private void Backdoor(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("The (handled) Button.MouseUp event occurred.");
        }




        private void KeyEvent(object sender, KeyEventArgs e)
        {
            if ((bool)chkIgnoreRepeat.IsChecked && e.IsRepeat) return;

            string message = $"At: {e.Timestamp}" +
                $"Event: {e.RoutedEvent} " +
                $" Key: {e.Key}";
            lstMessages3.Items.Add(message);
        }

        private void TextInput(object sender, TextCompositionEventArgs e)
        {
            string message = $"At: {e.Timestamp}" +
                $"Event: {e.RoutedEvent} " +
                $" Text: {e.Text}";
            lstMessages3.Items.Add(message);
        }

        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            string message =
                "Event: " + e.RoutedEvent;
            lstMessages3.Items.Add(message);
        }

        private void cmdClear3_Click(object sender, RoutedEventArgs e)
        {
            lstMessages3.Items.Clear();
        }



        private void pnl_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            short val;
            if (!Int16.TryParse(e.Text, out val))
            {
                e.Handled = true;
            }
        }

        private void pnl_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }




        private void KeyEvent2(object sender, KeyEventArgs e)
        {

            lblInfo.Text = "Modifiers: " +
                e.KeyboardDevice.Modifiers.ToString();

            if ((e.KeyboardDevice.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                lblInfo.Text += "\r\nYou held the Control key.";
            }
        }

        private void CheckShift(object sender, RoutedEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftShift))
            {
                lblInfo.Text = "The left Shift is held down.";
            }
            else
            {
                lblInfo.Text = "The left Shift is not held down.";
            }

        }

        // Focus
        // System.Windows.Window
        //protected override void OnActivated(EventArgs e)
        //{
        //    base.OnActivated(e);
        //    cmdFocus.Focus();
        //}



        private void cmdCapture_Click(object sender, RoutedEventArgs e)
        {
            this.AddHandler(
                   Mouse.LostMouseCaptureEvent,
                   new RoutedEventHandler(this.LostCapture));
            Mouse.Capture(rect);

            cmdCapture.Content = "[ Mouse is now captured ... ]";
        }

        private void MouseMoved(object sender, MouseEventArgs e)
        {
            Point pt = e.GetPosition(this);
            lblInfo2.Text =
                String.Format("You are at ({0},{1}) in window coordinates",
                pt.X, pt.Y);
        }

        private void LostCapture(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Lost capture");
            cmdCapture.Content = "Capture the Mouse";
        }




        private void lblSource_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Label lbl = (Label)sender;
            DragDrop.DoDragDrop(lbl, lbl.Content, DragDropEffects.Copy);
        }

        private void lblTarget_Drop(object sender, DragEventArgs e)
        {
            ((Label)sender).Content = e.Data.GetData(DataFormats.Text);
        }

        private void lblTarget_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
                e.Effects = DragDropEffects.Copy;
            else
                e.Effects = DragDropEffects.None;
        }


    }
}
