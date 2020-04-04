using Prism.Mvvm;
using System.Collections.ObjectModel;

namespace WpfDemoBrowser.Models
{
    public class MyTreeViewItem : BindableBase
    {
        public MyTreeViewItem(string header, string tag=null)
        {
            Header = header;
            Tag = tag;
            MenuItems = new ObservableCollection<MyTreeViewItem>();
        }

        public string Header { get; set; }

        public string Tag { get; set; }

        private ObservableCollection<MyTreeViewItem> menuItems;
        public ObservableCollection<MyTreeViewItem> MenuItems {
            get { return menuItems; }
            set { SetProperty(ref menuItems, value); }
        }
    }
}
