using AsNum.XFControls.Behaviors;
using AsNum.XFControls.Binders;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using Xamarin.Forms;

namespace AsNum.XFControls {
    /// <summary>
    /// 
    /// </summary>
    [System.Obsolete("请使用 TabView 替换")]
    public class TabbedView : ContentView {

        #region itemsSource 数据源
        public static readonly BindableProperty ItemsSourceProperty =
            BindableProperty.Create("ItemsSource",
                typeof(IEnumerable<ISelectable>),
                typeof(TabbedView),
                Enumerable.Empty<ISelectable>(),//保证 ItemsSource 不为NULL
                propertyChanged: ItemsSourceChanged);

        public IEnumerable<ISelectable> ItemsSource {
            get {
                return (IEnumerable<ISelectable>)this.GetValue(ItemsSourceProperty);
            }
            set {
                if (value != null) {
                    //保证数据源中,没有 null
                    var source = value.Cast<object>().Where(s => s != null);
                    this.SetValue(ItemsSourceProperty, source);
                } else {
                    //保证数据源不为 NULL
                    SetValue(ItemsSourceProperty, Enumerable.Empty<object>());
                }
            }
        }

        private static void ItemsSourceChanged(BindableObject bindable, object oldValue, object newValue) {
            var tv = (TabbedView)bindable;
            tv.UpdateTabs();
            tv.UpdateChildren();

            if (newValue is INotifyCollectionChanged) {
                var newCollection = (INotifyCollectionChanged)newValue;
                tv.InitCollection(newCollection);
            }
        }
        #endregion

        #region TabContainerTemplate
        public static readonly BindableProperty TabContainerTemplateProperty =
            BindableProperty.Create("TabContainerTemplate",
                typeof(ControlTemplate),
                typeof(TabbedView),
                null,
                BindingMode.Default,
                propertyChanged: TabContainerChanged);

        public ControlTemplate TabContainerTemplate {
            get {
                return (ControlTemplate)this.GetValue(TabContainerTemplateProperty);
            }
            set {
                this.SetValue(TabContainerTemplateProperty, value);
            }
        }

        private static void TabContainerChanged(BindableObject bindable, object oldValue, object newValue) {
            var tv = (TabbedView)bindable;
            tv.TabContainer.ControlTemplate = (ControlTemplate)newValue;
        }

        #endregion

        #region TabTemplate 标签模板
        public static readonly BindableProperty TabTemplateProperty =
            BindableProperty.Create("TabTemplate",
                typeof(DataTemplate),
                typeof(TabbedView),
                null,
                propertyChanged: TabTemplateChanged);

        public DataTemplate TabTemplate {
            get {
                return (DataTemplate)GetValue(TabTemplateProperty);
            }
            set {
                SetValue(TabTemplateProperty, value);
            }
        }

        private static void TabTemplateChanged(BindableObject bindable, object oldValue, object newValue) {
            var tv = (TabbedView)bindable;
            tv.UpdateTabs();
        }
        #endregion

        #region ItemTemplate 数据模板
        public static readonly BindableProperty ItemTemplateProperty =
            BindableProperty.Create("ItemTemplate",
                typeof(DataTemplate),
                typeof(TabbedView),
                null);

        public DataTemplate ItemTemplate {
            get {
                return (DataTemplate)GetValue(ItemTemplateProperty);
            }
            set {
                SetValue(ItemTemplateProperty, value);
            }
        }
        #endregion

        #region itemTemplateSelector 模板选择器
        public static readonly BindableProperty ItemTemplateSelectorProperty =
            BindableProperty.Create("ItemTemplateSelector",
                typeof(DataTemplateSelector),
                typeof(TabbedView),
                null);

        public DataTemplateSelector ItemTemplateSelector {
            get {
                return (DataTemplateSelector)GetValue(ItemTemplateSelectorProperty);
            }
            set {
                SetValue(ItemTemplateSelectorProperty, value);
            }
        }
        #endregion

        #region TabTemplateSelector 标签模板选择器
        public static readonly BindableProperty TabTemplateSelectorProperty =
            BindableProperty.Create("TabTemplateSelector",
                typeof(DataTemplateSelector),
                typeof(TabbedView),
                null);

        public DataTemplateSelector TabTemplateSelector {
            get {
                return (DataTemplateSelector)GetValue(TabTemplateSelectorProperty);
            }
            set {
                SetValue(TabTemplateSelectorProperty, value);
            }
        }
        #endregion

        #region selectedItem 选中的数据
        public static readonly BindableProperty SelectedItemProperty =
            BindableProperty.Create("SelectedItem",
                typeof(ISelectable),
                typeof(TabbedView),
                null,
                BindingMode.TwoWay,
                propertyChanged: SelectedItemChanged);

        public ISelectable SelectedItem {
            get {
                return (ISelectable)GetValue(SelectedItemProperty);
            }
            set {
                SetValue(SelectedItemProperty, value);
            }
        }

        private static void SelectedItemChanged(BindableObject bindable, object oldValue, object newValue) {
            var tv = (TabbedView)bindable;
            var flag = true;

            if (oldValue == null) {

                //重新进入的时候，oldValue 为 null, 但是 itemsSource 中有 IsSelected 为 true 的
                // Bug ??
                var restoreSelected = tv.ItemsSource.FirstOrDefault(t => t.IsSelected);
                if (restoreSelected != null) {
                    flag = false;
                    restoreSelected.IsSelected = true;
                    restoreSelected.NotifyOfPropertyChange("IsSelected");

                    if (newValue != null && !restoreSelected.Equals(newValue)) {
                        ((ISelectable)newValue).IsSelected = false;
                        ((ISelectable)newValue).NotifyOfPropertyChange("IsSelected");
                        //更新选中项，必须
                        tv.SelectedItem = restoreSelected;
                    }
                }
            }


            if (flag) {
                if (oldValue != null) {
                    ((ISelectable)oldValue).IsSelected = false;
                    ((ISelectable)oldValue).NotifyOfPropertyChange("IsSelected");
                }
                if (newValue != null) {
                    ((ISelectable)newValue).IsSelected = true;
                    ((ISelectable)newValue).NotifyOfPropertyChange("IsSelected");
                }
            }
        }
        #endregion

        #region tabPosition 标签位置
        public static readonly BindableProperty TabPositionProperty =
            BindableProperty.Create("TabPosition",
                typeof(TabPositions),
                typeof(TabbedView),
                TabPositions.Top,
                propertyChanged: TabPositionChanged);

        public TabPositions TabPosition {
            get {
                return (TabPositions)(this.GetValue(TabPositionProperty));
            }
            set {
                this.SetValue(TabPositionProperty, value);
            }
        }

        private static void TabPositionChanged(BindableObject bindable, object oldValue, object newValue) {
            var tv = (TabbedView)bindable;
            tv.UpdateTabPosition();
        }
        #endregion


        /// <summary>
        /// 子视图容器
        /// </summary>
        private Grid ChildrenContainer = null;


        private ContentView TabContainer = null;

        /// <summary>
        /// 标签容器的父容器
        /// </summary>
        private ScrollView TabScroller = null;

        /// <summary>
        /// 内层标签容器
        /// </summary>
        private StackLayout TabInnerContainer = null;

        /// <summary>
        /// 选中命令
        /// </summary>
        private Command SelectedCmd = null;


        public TabbedView() {

            this.SelectedCmd = new Command(o => {
                var model = (ISelectable)o;

                this.SelectedItem = model;
                if (model != null && model.SelectedCommand != null)
                    model.SelectedCommand.Execute(null);
            });

            this.PrepareLayout();
        }

        /// <summary>
        /// 布局
        /// </summary>
        private void PrepareLayout() {
            #region 
            var grid = new Grid();
            this.Content = grid;

            grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            grid.RowDefinitions.Add(new RowDefinition());
            grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });

            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });

            this.ChildrenContainer = new Grid();
            grid.Children.Add(this.ChildrenContainer);


            this.TabContainer = new ContentView();
            grid.Children.Add(this.TabContainer);

            this.TabScroller = new ScrollView();
            this.TabContainer.Content = this.TabScroller;

            this.TabInnerContainer = new StackLayout() {
                Spacing = 0
            };
            this.TabScroller.Content = this.TabInnerContainer;

            this.UpdateTabPosition();
            this.UpdateChildrenPosition();
            #endregion
        }

        /// <summary>
        /// 更新标签位置
        /// </summary>
        private void UpdateTabPosition() {
            int row = 0, col = 0, colSpan = 1, rowSpan = 1;
            ScrollOrientation orientation = ScrollOrientation.Horizontal;
            StackOrientation orientation2 = StackOrientation.Horizontal;
            switch (this.TabPosition) {
                case TabPositions.Top:
                    row = 0;
                    col = 0;
                    colSpan = 3;
                    rowSpan = 1;
                    orientation = ScrollOrientation.Horizontal;
                    orientation2 = StackOrientation.Horizontal;
                    break;
                case TabPositions.Bottom:
                    row = 2;
                    col = 0;
                    colSpan = 3;
                    rowSpan = 1;
                    orientation = ScrollOrientation.Horizontal;
                    orientation2 = StackOrientation.Horizontal;
                    break;
                case TabPositions.Left:
                    row = 0;
                    col = 0;
                    rowSpan = 3;
                    colSpan = 1;
                    orientation = ScrollOrientation.Vertical;
                    orientation2 = StackOrientation.Vertical;
                    break;
                case TabPositions.Right:
                    row = 0;
                    col = 2;
                    rowSpan = 3;
                    colSpan = 1;
                    orientation = ScrollOrientation.Vertical;
                    orientation2 = StackOrientation.Vertical;
                    break;
            }

            this.TabScroller.Orientation = orientation;
            this.TabScroller.HorizontalOptions = LayoutOptions.Fill;
            this.TabScroller.VerticalOptions = LayoutOptions.Fill;

            this.TabInnerContainer.Orientation = orientation2;
            if (this.TabInnerContainer.Orientation == StackOrientation.Horizontal) {
                this.TabInnerContainer.HorizontalOptions = LayoutOptions.Center;
                this.TabInnerContainer.VerticalOptions = LayoutOptions.Center;
            } else {
                this.TabInnerContainer.HorizontalOptions = LayoutOptions.Center;
                this.TabInnerContainer.VerticalOptions = LayoutOptions.Start;
            }

            Grid.SetRow(this.TabContainer, row);
            Grid.SetColumn(this.TabContainer, col);
            Grid.SetRowSpan(this.TabContainer, rowSpan);
            Grid.SetColumnSpan(this.TabContainer, colSpan);
        }

        /// <summary>
        /// 更新主体位置
        /// </summary>
        private void UpdateChildrenPosition() {
            int row = 0, col = 0, colSpan = 0, rowSpan = 0;

            switch (this.TabPosition) {
                case TabPositions.Top:
                    row = 1;
                    col = 0;
                    colSpan = 3;
                    rowSpan = 2;
                    break;
                case TabPositions.Bottom:
                    row = 0;
                    col = 0;
                    colSpan = 3;
                    rowSpan = 2;
                    break;
                case TabPositions.Left:
                    row = 0;
                    col = 1;
                    rowSpan = 3;
                    colSpan = 2;
                    break;
                case TabPositions.Right:
                    row = 0;
                    col = 0;
                    rowSpan = 3;
                    colSpan = 2;
                    break;
            }
            Grid.SetRow(this.ChildrenContainer, row);
            Grid.SetColumn(this.ChildrenContainer, col);
            Grid.SetRowSpan(this.ChildrenContainer, rowSpan);
            Grid.SetColumnSpan(this.ChildrenContainer, colSpan);
        }

        public enum TabPositions {
            Top,
            Bottom,
            Left,
            Right
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="collection"></param>
        private void InitCollection(INotifyCollectionChanged collection) {
            if (collection != null)
                collection.CollectionChanged += Collection_CollectionChanged;
        }

        private void Collection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e) {
            switch (e.Action) {
                case NotifyCollectionChangedAction.Add:
                    this.InsertChildren(e.NewItems, e.NewStartingIndex);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    this.RemoveChildren(e.OldItems, e.OldStartingIndex);
                    break;
                case NotifyCollectionChangedAction.Move:
                    Debugger.Break();
                    break;
                case NotifyCollectionChangedAction.Replace:
                    Debugger.Break();
                    break;
                case NotifyCollectionChangedAction.Reset:
                    this.UpdateTabs();
                    this.UpdateChildren();
                    break;
            }
        }

        /// <summary>
        /// 更新子元素
        /// </summary>
        private void UpdateChildren() {
            this.ChildrenContainer.Children.Clear();

            var source = this.ItemsSource.Cast<object>();
            foreach (var d in source) {
                View view = this.GetChildView(d);
                this.ChildrenContainer.Children.Add(view);
            }

            if (this.SelectedItem == null) {
                this.SelectedCmd.Execute(source.FirstOrDefault());
            }

        }

        /// <summary>
        /// 更新标签
        /// </summary>
        private void UpdateTabs() {
            this.TabInnerContainer.Children.Clear();
            foreach (var d in this.ItemsSource) {
                View tabView = this.GetTabView(d);
                this.TabInnerContainer.Children.Add(tabView);
            }
        }

        /// <summary>
        /// 数据源更新(插入)
        /// </summary>
        /// <param name="datas"></param>
        /// <param name="startIdx"></param>
        private void InsertChildren(IEnumerable datas, int startIdx = 0) {
            if (datas == null)
                return;

            foreach (var d in datas) {
                var view = this.GetChildView(d);
                var tabView = this.GetTabView(d);

                this.ChildrenContainer.Children.Insert(startIdx, view);
                this.TabInnerContainer.Children.Insert(startIdx, tabView);
                startIdx++;
            }
        }

        /// <summary>
        /// 数据源更新(删除)
        /// </summary>
        /// <param name="datas"></param>
        /// <param name="startIdx"></param>
        private void RemoveChildren(IList datas, int startIdx) {
            if (datas == null)
                return;

            foreach (var d in datas) {
                this.ChildrenContainer.Children.RemoveAt(startIdx);
                this.TabInnerContainer.Children.RemoveAt(startIdx);
                startIdx++;
            }
        }

        /// <summary>
        /// 从模板/模板选择器创建子视图
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private View GetChildView(object data) {
            View view = null;
            if (this.ItemTemplate != null || this.ItemTemplateSelector != null) {
                if (this.ItemTemplateSelector != null)
                    view = (View)this.ItemTemplateSelector.SelectTemplate(data, null).CreateContent();
                else if (this.ItemTemplate != null)
                    view = (View)this.ItemTemplate.CreateContent();

                if (view != null) {
                    view.BindingContext = data;
                }
            }

            if (view == null)
                view = new Label() { Text = "111" };

            //this.ChildrenContainer.Children.Add(view);
            this.SetFade(view, data);
            return view;
        }

        /// <summary>
        /// 设置淡入淡出
        /// </summary>
        /// <param name="view"></param>
        /// <param name="data"></param>
        private void SetFade(View view, object data) {
            var behavior = new FadeBehavior();
            behavior.SetBinding(FadeBehavior.IsSelectedProperty, "IsSelected", BindingMode.TwoWay);
            view.Behaviors.Add(behavior);
        }

        /// <summary>
        /// 从模板/模板选择器创建标签
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private View GetTabView(object data) {
            View view = null;

            if (this.TabTemplate != null || this.TabTemplateSelector != null) {
                //优先使用 TemplateSelector
                if (this.TabTemplateSelector != null) {
                    // SelectTemplate 的第二个参数，即 TemplateSelector 的 OnSelectTemplate 方法的 container 参数
                    view = (View)this.TabTemplateSelector.SelectTemplate(data, this).CreateContent();
                } else if (this.TabTemplate != null)
                    view = (View)this.TabTemplate.CreateContent();

                if (view != null) {
                    //上下文
                    view.BindingContext = data;
                }
            }

            if (view == null)
                view = new Label() { Text = "Tab" };

            TapBinder.SetCmd(view, this.SelectedCmd);
            TapBinder.SetParam(view, data);

            return view;
        }


    }
}
