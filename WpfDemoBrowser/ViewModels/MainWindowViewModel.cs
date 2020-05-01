using Animation;
using BehaviorTest;
using Commands;
using ControlTemplates;
using CustomControlsClient;
using DataBinding;
using Drawing;
using DrawingIn3D;
using MenusAndToolbars;
using WpfDemoBrowser.PartialViews.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using SoundAndVideo;
using Styles;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using WpfDemoBrowser.Models;
using CS;

namespace WpfDemoBrowser.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly IRegionManager regionManager;

        private string title = "WPF Demo Browser";
        public string Title {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        private ObservableCollection<MyTreeViewItem> menuItems;
        public ObservableCollection<MyTreeViewItem> MenuItems {
            get { return menuItems; }
            set { SetProperty(ref menuItems, value); }
        }

        public MainWindowViewModel(IRegionManager regionManager)
        {
            this.regionManager = regionManager;

            SelectedChangedCommand = new DelegateCommand<TreeView>(SelectedChangedCommandExcute);

            MenuItems = new ObservableCollection<MyTreeViewItem>()
            {
                new MyTreeViewItem("CSharp", nameof(CSharp)),
                new MyTreeViewItem("3. 布局 +", nameof(LayoutPanels)),
                new MyTreeViewItem("4. 依赖项属性 -"),
                new MyTreeViewItem("5. 路由事件 +", nameof(RoutedEventsW)),
                //{
                //    MenuItems=new ObservableCollection<MyTreeViewItem>()
                //    {
                //        new MyTreeViewItem("5.2.2 冒泡路由事件", nameof(BubbledLabelClick)),
                //        new MyTreeViewItem("5.2.5 隧道路由事件", nameof(TunneledKeyPress)),
                //        new MyTreeViewItem("ButtonMouseUpEvent", nameof(ButtonMouseUpEvent)),
                //        new MyTreeViewItem("KeyPressEvents", nameof(KeyPressEvents)),
                //        new MyTreeViewItem("OnlyNumbers", nameof(OnlyNumbers)),
                //        new MyTreeViewItem("KeyModifiers", nameof(KeyModifiers)),
                //        new MyTreeViewItem("5.4.2 焦点", nameof(Focus)),
                //        new MyTreeViewItem("5.5 鼠标输入", nameof(MousePosition)),
                //        new MyTreeViewItem("5.5.3 鼠标拖放", nameof(DragAndDrop)),
                //    }
                //},
                new MyTreeViewItem("6. 控件 +", nameof(ControlsW)),
                //{
                //    MenuItems = new ObservableCollection<MyTreeViewItem>()
                //    {
                //        new MyTreeViewItem("6.1.2 字体", nameof(FontTest)),
                //        new MyTreeViewItem("6.2 内容控件", nameof(ContentControlTest)),
                //        new MyTreeViewItem("6.2.6 工具提示", nameof(TooltipsAndPopup)),
                //        new MyTreeViewItem("6.3 特殊容器", nameof(ScrollableTextBoxColumn)),
                //        new MyTreeViewItem("6.4 文本控件", nameof(TextBoxTest)),
                //        new MyTreeViewItem("6.5 列表控件", nameof(ListControlTest)),
                //        new MyTreeViewItem("6.6 基于范围的控件", nameof(RangeBase)),
                //        new MyTreeViewItem("6.7 日期控件", nameof(DateControls)),
                //    }
                //},
                new MyTreeViewItem("7. Application类 -"),
                new MyTreeViewItem("8. 元素绑定 +", nameof(ElementBinding)),
                new MyTreeViewItem("9. 命令", nameof(CommandsW))
                {
                    MenuItems = new ObservableCollection<MyTreeViewItem>()
                    {
                        //new MyTreeViewItem("TestNewCommand", nameof(TestNewCommand)),
                        //new MyTreeViewItem("SimpleDocument", nameof(SimpleDocument)),
                        //new MyTreeViewItem("CustomCommand", nameof(CustomCommand)),
                        //new MyTreeViewItem("NoCommandTextBox", nameof(NoCommandTextBox)),
                        //new MyTreeViewItem("CustomControlWithCommand", nameof(CustomControlWithCommand)),
                        //new MyTreeViewItem("TwoDocument", nameof(TwoDocument)),
                        new MyTreeViewItem("MonitorCommands", nameof(MonitorCommands))
                    }
                },
                new MyTreeViewItem("10. 资源 +", nameof(Resources)),
                new MyTreeViewItem("11. 样式和行为")
                {
                    MenuItems=new ObservableCollection<MyTreeViewItem>()
                    {
                        new MyTreeViewItem("11.1 样式")
                        {
                            MenuItems = new ObservableCollection<MyTreeViewItem>()
                            {
                                new MyTreeViewItem("重用字体通过资源", nameof(ReuseFontWithResources)),
                                new MyTreeViewItem("重用字体通过样式", nameof(ReuseFontWithStyles)),
                                new MyTreeViewItem("11.1.3 关联事件处理程序", nameof(EventSetter)),
                                new MyTreeViewItem("11.1.4 多层样式", nameof(StyleInheritance)),
                                new MyTreeViewItem("11.1.5 通过类型自动应用样式", nameof(AutomaticStyles)),
                                new MyTreeViewItem("11.2.1 简单触发器", nameof(SimpleTriggers)),
                                new MyTreeViewItem("11.2.2 事件触发器", nameof(EventTriggers))
                            }
                        },
                        new MyTreeViewItem("11.3 行为")
                        {
                            MenuItems = new ObservableCollection<MyTreeViewItem>()
                            {
                                new MyTreeViewItem("PlayMediaTest", nameof(PlayMediaTest)),
                                new MyTreeViewItem("FadeInAndOutTest", nameof(FadeInAndOutTest)),
                                new MyTreeViewItem("DragInCanvasTest", nameof(DragInCanvasTest))
                            }
                        },
                    }
                },
                new MyTreeViewItem("12-14. 图画")
                {
                    MenuItems = new ObservableCollection<MyTreeViewItem>()
                    {
                        new MyTreeViewItem("12. 形状、画刷和变换", nameof(C12)),
                        new MyTreeViewItem("13. 几何图形和图画", nameof(C13)),
                        new MyTreeViewItem("14. 效果和可视化对象", nameof(C14)),
                    }
                },
                new MyTreeViewItem("15-16. 动画")
                {
                    MenuItems = new ObservableCollection<MyTreeViewItem>()
                    {
                        new MyTreeViewItem("15. 动画基础")
                        {
                            MenuItems=new ObservableCollection<MyTreeViewItem>()
                            {
                                new MyTreeViewItem("CodeAnimation", nameof(CodeAnimation)),
                                new MyTreeViewItem("XamlAnimation", nameof(XamlAnimation)),
                                new MyTreeViewItem("AnimationInStyle", nameof(AnimationInStyle)),
                                new MyTreeViewItem("AnimationPlayer", nameof(AnimationPlayer)),
                                new MyTreeViewItem("ImageWipe", nameof(ImageWipe)),
                                new MyTreeViewItem("Easing", nameof(Easing)),
                                new MyTreeViewItem("CustomEasingFunction", nameof(CustomEasingFunction)),
                                new MyTreeViewItem("FrameRates", nameof(FrameRates)),
                                new MyTreeViewItem("CachingTest", nameof(CachingTest)),
                                new MyTreeViewItem("CachingTest2", nameof(CachingTest2)),
                            }
                        },
                        new MyTreeViewItem("16. 高级动画")
                        {
                            MenuItems=new ObservableCollection<MyTreeViewItem>()
                            {
                                new MyTreeViewItem("RotateButton", nameof(RotateButton)),
                                new MyTreeViewItem("RotateButtonWithLayout", nameof(RotateButtonWithLayout)),
                                new MyTreeViewItem("ExpandElement", nameof(ExpandElement)),
                                new MyTreeViewItem("AnimateRadialGradient", nameof(AnimateRadialGradient)),
                                new MyTreeViewItem("AnimateVisual", nameof(AnimateVisual)),
                                new MyTreeViewItem("BlurringButtons", nameof(BlurringButtons)),
                                new MyTreeViewItem("ExpandElement2", nameof(ExpandElement2)),
                                new MyTreeViewItem("KeySplineAnimation", nameof(KeySplineAnimation)),
                                new MyTreeViewItem("PathBasedAnimation", nameof(PathBasedAnimation)),
                                new MyTreeViewItem("FrameBasedAnimation", nameof(FrameBasedAnimation)),
                            }
                        }

                    }
                },
                new MyTreeViewItem("17. 控件模板")
                {
                    MenuItems = new ObservableCollection<MyTreeViewItem>()
                    {
                        new MyTreeViewItem("SimpleCustomButton", nameof(SimpleCustomButton)),
                        new MyTreeViewItem("GradientButtonTest", nameof(GradientButtonTest)),
                        new MyTreeViewItem("ButtonTemplate", nameof(ButtonTemplate)),
                        new MyTreeViewItem("MultiPartTemplates", nameof(MultiPartTemplates)),
                    }
                },
                new MyTreeViewItem("18. 自定义元素")
                {
                    MenuItems = new ObservableCollection<MyTreeViewItem>()
                    {
                        new MyTreeViewItem("ColorPickerUserControlTest", nameof(ColorPickerUserControlTest)),
                        new MyTreeViewItem("ColorPickerTwoWays", nameof(ColorPickerTwoWays)),
                        new MyTreeViewItem("FlipPanelTest", nameof(FlipPanelTest)),
                        new MyTreeViewItem("FlipPanelAlternateTemplate", nameof(FlipPanelAlternateTemplate)),
                        new MyTreeViewItem("MaskedTextBoxTest", nameof(MaskedTextBoxTest)),
                        new MyTreeViewItem("WrapBreakPanelTest", nameof(WrapBreakPanelTest)),
                        new MyTreeViewItem("CustomDrawnElementTest", nameof(CustomDrawnElementTest)),
                        new MyTreeViewItem("CustomDrawnElementChrome", nameof(CustomDrawnElementChrome)),
                    }
                },
                new MyTreeViewItem("19-22. 数据")
                {
                    MenuItems = new ObservableCollection<MyTreeViewItem>()
                    {
                        new MyTreeViewItem("19. 数据绑定")
                        {
                            MenuItems=new ObservableCollection<MyTreeViewItem>()
                            {
                                new MyTreeViewItem("BindProductObject", nameof(BindProductObject)),
                                new MyTreeViewItem("EditProductObject", nameof(EditProductObject)),
                                new MyTreeViewItem("BindToCollection", nameof(BindToCollection)),
                                new MyTreeViewItem("BindToDataSet", nameof(BindToDataSet)),
                                new MyTreeViewItem("BindToLinqFilteredCollection", nameof(BindToLinqFilteredCollection)),
                                new MyTreeViewItem("VirtualizationTest", nameof(VirtualizationTest)),
                                new MyTreeViewItem("ValidationTest", nameof(ValidationTest)),
                                new MyTreeViewItem("BindingGroupValidation", nameof(BindingGroupValidation)),
                                new MyTreeViewItem("BindToObjectDataProvider", nameof(BindToObjectDataProvider)),
                                new MyTreeViewItem("BindToXmlDataProvider", nameof(BindToXmlDataProvider)),
                            }
                        },
                        new MyTreeViewItem("20. 格式化绑定的数据")
                        {
                            MenuItems=new ObservableCollection<MyTreeViewItem>()
                            {
                                new MyTreeViewItem("ValueConverter", nameof(ValueConverter)),
                                new MyTreeViewItem("MoreValueConverters", nameof(MoreValueConverters)),
                                new MyTreeViewItem("ListStyles", nameof(ListStyles)),
                                new MyTreeViewItem("RadioButtonList", nameof(RadioButtonList)),
                                new MyTreeViewItem("CheckBoxList", nameof(DataBinding.CheckBoxList)),
                                new MyTreeViewItem("VariedStyles", nameof(VariedStyles)),
                                new MyTreeViewItem("DataTemplateList", nameof(DataTemplateList)),
                                new MyTreeViewItem("DataTemplateByType", nameof(DataTemplateByType)),
                                new MyTreeViewItem("DataTemplateImages", nameof(DataTemplateImages)),
                                new MyTreeViewItem("DataTemplateControls", nameof(DataTemplateControls)),
                                new MyTreeViewItem("VariedTemplates", nameof(VariedTemplates)),
                                new MyTreeViewItem("ExpandingDataTemplate", nameof(ExpandingDataTemplate)),
                                new MyTreeViewItem("WrappedList", nameof(WrappedList)),
                                new MyTreeViewItem("ComboBoxSelectionBox", nameof(ComboBoxSelectionBox)),
                            }
                        },
                        new MyTreeViewItem("21. 数据视图")
                        {
                            MenuItems=new ObservableCollection<MyTreeViewItem>()
                            {
                                new MyTreeViewItem("NavigateCollection", nameof(NavigateCollection)),
                                new MyTreeViewItem("FilterCollection", nameof(FilterCollection)),
                                new MyTreeViewItem("FilterDataSet", nameof(FilterDataSet)),
                                new MyTreeViewItem("GroupList", nameof(GroupList)),
                                new MyTreeViewItem("GroupInRanges", nameof(GroupInRanges)),
                            }
                        },
                        new MyTreeViewItem("22. 列表、树和网格")
                        {
                            MenuItems=new ObservableCollection<MyTreeViewItem>()
                            {
                                new MyTreeViewItem("BasicListView", nameof(BasicListView)),
                                new MyTreeViewItem("AdvancedListView", nameof(AdvancedListView)),
                                new MyTreeViewItem("CustomListViewTest", nameof(CustomListViewTest)),
                                new MyTreeViewItem("BoundTreeView", nameof(BoundTreeView)),
                                new MyTreeViewItem("BoundTreeViewDataSet", nameof(BoundTreeViewDataSet)),
                                new MyTreeViewItem("DirectoryTreeView", nameof(DirectoryTreeView)),
                                new MyTreeViewItem("DataGridTest", nameof(DataGridTest)),
                                new MyTreeViewItem("DataGridRowDetails", nameof(DataGridRowDetails)),
                                new MyTreeViewItem("DataGridEditing", nameof(DataGridEditing)),
                                new MyTreeViewItem("DataGridGrouping", nameof(DataGridGrouping)),
                            }
                        }
                    }
                },
                new MyTreeViewItem("23. 窗口 -"),
                new MyTreeViewItem("24. 页面和导航 -"),
                new MyTreeViewItem("25. 菜单、工具栏和功能区")
                {
                    MenuItems=new ObservableCollection<MyTreeViewItem>()
                    {
                        new MyTreeViewItem("菜单、工具栏和功能区", nameof(MenusAndToolbarsTest)),
                    }
                },
                new MyTreeViewItem("26. 声音和视频")
                {
                    MenuItems=new ObservableCollection<MyTreeViewItem>()
                    {
                        new MyTreeViewItem("SoundPlayerTest", nameof(SoundPlayerTest)),
                        new MyTreeViewItem("MultipleSounds", nameof(MultipleSounds)),
                        new MyTreeViewItem("CodePlayback", nameof(CodePlayback)),
                        new MyTreeViewItem("DeclarativePlayback", nameof(DeclarativePlayback)),
                        new MyTreeViewItem("SynchronizedAnimation", nameof(SynchronizedAnimation)),
                        new MyTreeViewItem("Video", nameof(Video)),
                        new MyTreeViewItem("AnimatedVideoWindow", nameof(AnimatedVideoWindow)),
                        new MyTreeViewItem("SpeechSynthesis", nameof(SpeechSynthesis)),
                        new MyTreeViewItem("SpeechRecognition", nameof(SpeechRecognition)),
                    }
                },
                new MyTreeViewItem("27. 3D绘图")
                {
                    MenuItems=new ObservableCollection<MyTreeViewItem>
                    {
                        new MyTreeViewItem("OneTriangleMesh", nameof(OneTriangleMesh)),
                        new MyTreeViewItem("CubeMesh", nameof(CubeMesh)),
                        new MyTreeViewItem("PersonIn3D", nameof(PersonIn3D)),
                        new MyTreeViewItem("Materials", nameof(Materials)),
                        new MyTreeViewItem("TextureMapping", nameof(TextureMapping)),
                        new MyTreeViewItem("VideoIn3D", nameof(VideoIn3D)),
                        new MyTreeViewItem("HitTesting", nameof(HitTesting)),
                        new MyTreeViewItem("AnimatedRing", nameof(AnimatedRing)),
                        new MyTreeViewItem("TrackballRing", nameof(TrackballRing)),
                        new MyTreeViewItem("ElementsIn3D", nameof(ElementsIn3D)),
                    }
                },
                new MyTreeViewItem("28. 文档 -"),
                new MyTreeViewItem("29. 打印 -"),
                new MyTreeViewItem("30. 与Windows窗体进行交互 -"),
                new MyTreeViewItem("31. 多线程 -"),
                new MyTreeViewItem("32. 插件模型 -"),
                new MyTreeViewItem("16. 17. 23. 25 -")
            };
        }

        public DelegateCommand<TreeView> SelectedChangedCommand { get; private set; }
        private void SelectedChangedCommandExcute(TreeView sender)
        {
            var selectedItem = sender.SelectedItem as MyTreeViewItem;
            var tag = selectedItem.Tag;
            if (string.IsNullOrEmpty(tag))
                return;

            regionManager.RequestNavigate("ContentRegion", tag);

            Title = selectedItem.Header;
        }
    }
}
