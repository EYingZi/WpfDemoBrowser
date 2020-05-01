using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
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
    public partial class CommandsW : UserControl
    {
        public CommandsW()
        {
            //ApplicationCommands.New.Text = "Completely New";

            InitializeComponent();

            //CommandBinding bindingNew = new CommandBinding(ApplicationCommands.New);
            //bindingNew.Executed += NewCommand;

            //this.CommandBindings.Add(bindingNew);


            //---

            // Create bindings.
            CommandBinding binding;
            binding = new CommandBinding(ApplicationCommands.New);
            binding.Executed += NewCommand2;
            this.CommandBindings.Add(binding);

            binding = new CommandBinding(ApplicationCommands.Open);
            binding.Executed += OpenCommand;
            this.CommandBindings.Add(binding);

            binding = new CommandBinding(ApplicationCommands.Save);
            binding.Executed += SaveCommand_Executed;
            binding.CanExecute += SaveCommand_CanExecute;
            this.CommandBindings.Add(binding);


            // NoCommandTextBox

            ntxt.CommandBindings.Add(new CommandBinding(ApplicationCommands.Cut, null, SuppressCommand));

            ntxt.InputBindings.Add(new KeyBinding(ApplicationCommands.NotACommand, Key.C, ModifierKeys.Control));
            //txt.ContextMenu = null;
        }

        private void NewCommand(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("New command triggered by " + e.Source.ToString());
        }

        private void cmdDoCommand_Click(object sender, RoutedEventArgs e)
        {
            this.CommandBindings[0].Command.Execute(null);
            // ApplicationCommands.New.Execute(null, (Button)sender);
        }




        private void NewCommand2(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("New command triggered with " + e.Source.ToString());
            isDirty = false;
        }

        private void OpenCommand(object sender, ExecutedRoutedEventArgs e)
        {
            isDirty = false;
        }

        private void SaveCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("Save command triggered with " + e.Source.ToString());
            isDirty = false;
        }

        private bool isDirty = false;
        private void txt_TextChanged(object sender, RoutedEventArgs e)
        {
            isDirty = true;
        }

        private void SaveCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = isDirty;
        }




        private void RequeryCommand(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("Requery");
        }



        private void SuppressCommand(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = false;
            e.Handled = true;
        }


        public static RoutedCommand FontUpdateCommand = new RoutedCommand();

        //The ExecutedRoutedEvent Handler
        //if the command target is the TextBox, changes the fontsize to that
        //of the value passed in through the Command Parameter
        public void SliderUpdateExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            TextBox source = sender as TextBox;

            if (source != null)
            {
                if (e.Parameter != null)
                {
                    try
                    {
                        if ((int)e.Parameter > 0 && (int)e.Parameter <= 60)
                        {
                            source.FontSize = (int)e.Parameter;
                        }
                    }
                    catch
                    {
                        MessageBox.Show("in Command \n Parameter: " + e.Parameter);
                    }

                }
            }
        }

        //The CanExecuteRoutedEvent Handler
        //if the Command Source is a TextBox, then set CanExecute to ture;
        //otherwise, set it to false
        public void SliderUpdateCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            TextBox source = sender as TextBox;

            if (source != null)
            {
                if (source.IsReadOnly)
                {
                    e.CanExecute = false;
                }
                else
                {
                    e.CanExecute = true;
                }
            }
        }

        //if the Readonly Box is checked, we need to force the CommandManager
        //to raise the RequerySuggested event.  This will cause the Command Source
        //to query the command to see if it can execute or not.
        public void OnReadOnlyChecked(object sender, RoutedEventArgs e)
        {
            if (txtBoxTarget != null)
            {
                txtBoxTarget.IsReadOnly = true;
                CommandManager.InvalidateRequerySuggested();
            }
        }

        //if the Readonly Box is checked, we need to force the CommandManager
        //to raise the RequerySuggested event.  This will cause the Command Source
        //to query the command to see if it can execute or not.
        public void OnReadOnlyUnChecked(object sender, RoutedEventArgs e)
        {
            if (txtBoxTarget != null)
            {
                txtBoxTarget.IsReadOnly = false;
                CommandManager.InvalidateRequerySuggested();
            }
        }




        private void SaveCommand(object sender, ExecutedRoutedEventArgs e)
        {
            string text = ((TextBox)sender).Text;
            MessageBox.Show("About to save: " + text);
            isDirty2[sender] = false;
        }

        private Dictionary<Object, bool> isDirty2 = new Dictionary<Object, bool>();
        private void txt_TextChanged2(object sender, RoutedEventArgs e)
        {
            isDirty2[sender] = true;
        }

        private void SaveCommand_CanExecute2(object sender, CanExecuteRoutedEventArgs e)
        {
            if (isDirty2.ContainsKey(sender) && isDirty2[sender] == true)
            {
                e.CanExecute = true;
            }
            else
            {
                e.CanExecute = false;
            }
        }
    }



    public class DataCommands
    {
        private static RoutedUICommand requery;
        static DataCommands()
        {
            InputGestureCollection inputs = new InputGestureCollection();
            inputs.Add(new KeyGesture(Key.R, ModifierKeys.Control, "Ctrl+R"));
            requery = new RoutedUICommand(
              "Requery", "Requery", typeof(DataCommands), inputs);
        }

        public static RoutedUICommand Requery
        {
            get { return requery; }
        }
    }




    //Converter to convert the Slider value property to an int
    [ValueConversion(typeof(double), typeof(int))]
    public class FontStringValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string fontSize = (string)value;
            int intFont;

            try
            {
                intFont = Int32.Parse(fontSize);
                return intFont;
            }
            catch (FormatException e)
            {
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }

    //Converter to convert the Slider value property to an int
    [ValueConversion(typeof(double), typeof(int))]
    public class FontDoubleValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double fontSize = (double)value;
            return (int)fontSize;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
