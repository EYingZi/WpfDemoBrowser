using Prism.Mvvm;
using System.Collections.ObjectModel;

namespace WpfDemoBrowser.Models
{
    public class MyTreeViewItem : BindableBase
    {
        public MyTreeViewItem(string header, string tag=null, bool isExpanded=false)
        {
            Header = header;
            Tag = tag;
            MenuItems = new ObservableCollection<MyTreeViewItem>();

            IsExpanded = isExpanded;
        }

        public string Header { get; set; }

        public string Tag { get; set; }

        public bool IsExpanded { get; set; }

        private ObservableCollection<MyTreeViewItem> menuItems;
        public ObservableCollection<MyTreeViewItem> MenuItems {
            get { return menuItems; }
            set { SetProperty(ref menuItems, value); }
        }
    }
}
