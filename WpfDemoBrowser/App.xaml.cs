﻿using Animation;
using BehaviorTest;
using ControlTemplates;
using CustomControlsClient;
using DataBinding;
using Drawing;
using DrawingIn3D;
using MenusAndToolbars;
using WpfDemoBrowser.PartialViews.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Unity;
using SoundAndVideo;
using StoreDatabase;
using Styles;
using System.Windows;
using WpfDemoBrowser.Views;
using CS;

namespace WpfDemoBrowser
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : PrismApplication
    {
        private static StoreDb storeDb = new StoreDb();
        public static StoreDb StoreDb {
            get { return storeDb; }
        }

        private static StoreDbDataSet storeDbDataSet = new StoreDbDataSet();
        public static StoreDbDataSet StoreDbDataSet {
            get { return storeDbDataSet; }
        }

        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // LayoutPanels
            containerRegistry.RegisterForNavigation<LayoutPanels>();

            // RoutedEvents
            containerRegistry.RegisterForNavigation<RoutedEventsW>();

            // Controls
            containerRegistry.RegisterForNavigation<ControlsW>();

            // DataBinding
            containerRegistry.RegisterForNavigation<ElementBinding>();

            // Commands
            containerRegistry.RegisterForNavigation<CommandsW>();

            // Resources
            containerRegistry.RegisterForNavigation<Resources>();

            // Styles
            containerRegistry.RegisterForNavigation<ReuseFontWithResources>();
            containerRegistry.RegisterForNavigation<ReuseFontWithStyles>();
            containerRegistry.RegisterForNavigation<Styles.EventSetter>();
            containerRegistry.RegisterForNavigation<StyleInheritance>();
            containerRegistry.RegisterForNavigation<AutomaticStyles>();
            containerRegistry.RegisterForNavigation<SimpleTriggers>();
            containerRegistry.RegisterForNavigation<EventTriggers>();

            // Behaviors
            containerRegistry.RegisterForNavigation<PlayMediaTest>();
            containerRegistry.RegisterForNavigation<FadeInAndOutTest>();
            containerRegistry.RegisterForNavigation<DragInCanvasTest>();

            // Drawing.C12
            containerRegistry.RegisterForNavigation<C12>();

            // Drawing.C13
            containerRegistry.RegisterForNavigation<C13>();

            // Drawing.C14
            containerRegistry.RegisterForNavigation<C14>();

            // Animation.C15
            containerRegistry.RegisterForNavigation<C15W>();

            containerRegistry.RegisterForNavigation<CodeAnimation>();
            containerRegistry.RegisterForNavigation<XamlAnimation>();
            containerRegistry.RegisterForNavigation<AnimationInStyle>();
            containerRegistry.RegisterForNavigation<AnimationPlayer>();
            containerRegistry.RegisterForNavigation<ImageWipe>();
            containerRegistry.RegisterForNavigation<Easing>();
            containerRegistry.RegisterForNavigation<CustomEasingFunction>();
            containerRegistry.RegisterForNavigation<FrameRates>();
            containerRegistry.RegisterForNavigation<CachingTest>();
            containerRegistry.RegisterForNavigation<CachingTest2>();

            // Animation.C16
            containerRegistry.RegisterForNavigation<RotateButton>();
            containerRegistry.RegisterForNavigation<RotateButtonWithLayout>();
            containerRegistry.RegisterForNavigation<ExpandElement>();
            containerRegistry.RegisterForNavigation<AnimateRadialGradient>();
            containerRegistry.RegisterForNavigation<AnimateVisual>();
            containerRegistry.RegisterForNavigation<BlurringButtons>();
            containerRegistry.RegisterForNavigation<ExpandElement2>();
            containerRegistry.RegisterForNavigation<KeySplineAnimation>();
            containerRegistry.RegisterForNavigation<PathBasedAnimation>();
            containerRegistry.RegisterForNavigation<FrameBasedAnimation>();

            // ControlTemplates
            containerRegistry.RegisterForNavigation<SimpleCustomButton>();
            containerRegistry.RegisterForNavigation<GradientButtonTest>();
            containerRegistry.RegisterForNavigation<ButtonTemplate>();
            containerRegistry.RegisterForNavigation<MultiPartTemplates>();

            // CustomControls
            containerRegistry.RegisterForNavigation<ColorPickerUserControlTest>();
            containerRegistry.RegisterForNavigation<ColorPickerTwoWays>();
            containerRegistry.RegisterForNavigation<FlipPanelTest>();
            containerRegistry.RegisterForNavigation<FlipPanelAlternateTemplate>();
            containerRegistry.RegisterForNavigation<MaskedTextBoxTest>();
            containerRegistry.RegisterForNavigation<WrapBreakPanelTest>();
            containerRegistry.RegisterForNavigation<CustomDrawnElementTest>();
            containerRegistry.RegisterForNavigation<CustomDrawnElementChrome>();

            // DataBinding.C19
            containerRegistry.RegisterForNavigation<BindProductObject>();
            containerRegistry.RegisterForNavigation<EditProductObject>();
            containerRegistry.RegisterForNavigation<BindToCollection>();
            containerRegistry.RegisterForNavigation<BindToDataSet>();
            containerRegistry.RegisterForNavigation<BindToLinqFilteredCollection>();
            containerRegistry.RegisterForNavigation<VirtualizationTest>();
            containerRegistry.RegisterForNavigation<ValidationTest>();
            containerRegistry.RegisterForNavigation<BindingGroupValidation>();
            containerRegistry.RegisterForNavigation<BindToObjectDataProvider>();
            containerRegistry.RegisterForNavigation<BindToXmlDataProvider>();

            // DataBinding.C20
            containerRegistry.RegisterForNavigation<ValueConverter>();
            containerRegistry.RegisterForNavigation<MoreValueConverters>();
            containerRegistry.RegisterForNavigation<ListStyles>();
            containerRegistry.RegisterForNavigation<RadioButtonList>();
            containerRegistry.RegisterForNavigation<DataBinding.CheckBoxList>();
            containerRegistry.RegisterForNavigation<VariedStyles>();
            containerRegistry.RegisterForNavigation<DataTemplateList>();
            containerRegistry.RegisterForNavigation<DataTemplateByType>();
            containerRegistry.RegisterForNavigation<DataTemplateImages>();
            containerRegistry.RegisterForNavigation<DataTemplateControls>();
            containerRegistry.RegisterForNavigation<VariedTemplates>();
            containerRegistry.RegisterForNavigation<ExpandingDataTemplate>();
            containerRegistry.RegisterForNavigation<WrappedList>();
            containerRegistry.RegisterForNavigation<ComboBoxSelectionBox>();

            // DataBinding.C21
            containerRegistry.RegisterForNavigation<NavigateCollection>();
            containerRegistry.RegisterForNavigation<FilterCollection>();
            containerRegistry.RegisterForNavigation<FilterDataSet>();
            containerRegistry.RegisterForNavigation<GroupList>();
            containerRegistry.RegisterForNavigation<GroupInRanges>();

            // DataBinding.C22
            containerRegistry.RegisterForNavigation<BasicListView>();
            containerRegistry.RegisterForNavigation<AdvancedListView>();
            containerRegistry.RegisterForNavigation<CustomListViewTest>();
            containerRegistry.RegisterForNavigation<BoundTreeView>();
            containerRegistry.RegisterForNavigation<BoundTreeViewDataSet>();
            containerRegistry.RegisterForNavigation<DirectoryTreeView>();
            containerRegistry.RegisterForNavigation<DataGridTest>();
            containerRegistry.RegisterForNavigation<DataGridRowDetails>();
            containerRegistry.RegisterForNavigation<DataGridEditing>();
            containerRegistry.RegisterForNavigation<DataGridGrouping>();

            // MenusAndToolbars
            containerRegistry.RegisterForNavigation<MenusAndToolbarsTest>();
            
            // SoundAndVideo
            containerRegistry.RegisterForNavigation<SoundPlayerTest>();
            containerRegistry.RegisterForNavigation<MultipleSounds>();
            containerRegistry.RegisterForNavigation<CodePlayback>();
            containerRegistry.RegisterForNavigation<DeclarativePlayback>();
            containerRegistry.RegisterForNavigation<SynchronizedAnimation>();
            containerRegistry.RegisterForNavigation<Video>();
            containerRegistry.RegisterForNavigation<AnimatedVideoWindow>();
            containerRegistry.RegisterForNavigation<SpeechSynthesis>();
            containerRegistry.RegisterForNavigation<SpeechRecognition>();

            // DrawingIn3D
            containerRegistry.RegisterForNavigation<OneTriangleMesh>();
            containerRegistry.RegisterForNavigation<CubeMesh>();
            containerRegistry.RegisterForNavigation<PersonIn3D>();
            containerRegistry.RegisterForNavigation<Materials>();
            containerRegistry.RegisterForNavigation<TextureMapping>();
            containerRegistry.RegisterForNavigation<VideoIn3D>();
            containerRegistry.RegisterForNavigation<HitTesting>();
            containerRegistry.RegisterForNavigation<AnimatedRing>();
            containerRegistry.RegisterForNavigation<TrackballRing>();
            containerRegistry.RegisterForNavigation<ElementsIn3D>();

            // CSharp
            containerRegistry.RegisterForNavigation<CSharp>();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            
        }
    }
}
