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
using System.Data;
using WpfDemoBrowser;

namespace DataBinding
{
    /// <summary>
    /// Interaction logic for BoundTreeView.xaml
    /// </summary>

    public partial class BoundTreeViewDataSet : UserControl
    {

        public BoundTreeViewDataSet()
        {
            InitializeComponent();

            DataSet ds = App.StoreDbDataSet.GetCategoriesAndProducts();

            treeCategories.ItemsSource = ds.Tables["Categories"].DefaultView;
        }

    }
}