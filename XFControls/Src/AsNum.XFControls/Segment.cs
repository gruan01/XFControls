using AsNum.XFControls.Binders;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace AsNum.XFControls {

    /// <summary>
    /// Segment 模拟
    /// </summary>
    [ContentProperty("Items")]
    public class Segment : ContentView {


        #region IsMutliSelectable
        /// <summary>
        /// 是否可多选
        /// </summary>
        public static readonly BindableProperty IsMutliSelectableProperty =
            BindableProperty.Create("IsMutliSelectable",
                typeof(bool),
                typeof(Segment),
                false);

        /// <summary>
        /// 是否可多选
        /// </summary>
        public bool IsMutliSelectable {
            get {
                return (bool)this.GetValue(IsMutliSelectableProperty);
            }
            set {
                this.SetValue(IsMutliSelectableProperty, value);
            }
        }

        #endregion

        #region SelectedItem
        /// <summary>
        /// 选中的数据
        /// </summary>
        public static readonly BindableProperty SelectedItemProperty =
            BindableProperty.Create("SelectedItem",
                typeof(object),
                typeof(Segment),
                null,
                BindingMode.TwoWay);


        /// <summary>
        /// 选中的数据
        /// </summary>
        public object SelectedItem {
            get {
                return this.GetValue(SelectedItemProperty);
            }
            set {
                this.SetValue(SelectedItemProperty, value);
            }
        }

        #endregion

        #region SelectedItems
        /// <summary>
        /// 选中的数据, 仅作用于 IsMutliSelectable = true
        /// </summary>
        public static readonly BindablePropertyKey SelectedItemsPropertyKey
            = BindableProperty.CreateReadOnly("SelectedItems",
                typeof(IList),
                typeof(Segment),
                new List<object>(),
                BindingMode.TwoWay
                );

        //private static void SelectedItemsChanged(BindableObject bindable, object oldValue, object newValue) {
        //    throw new NotImplementedException();
        //}

        /// <summary>
        /// 选中的数据, 仅作用于 IsMutliSelectable = true
        /// </summary>
        public IList SelectedItems {
            get {
                return (IList)this.GetValue(SelectedItemsPropertyKey.BindableProperty);
            }
            set {
                this.SetValue(SelectedItemsPropertyKey, value);
            }
        }
        #endregion

        #region ItemTemplate
        /// <summary>
        /// 数据模板
        /// </summary>
        public static readonly BindableProperty ItemTemplateProperty =
            BindableProperty.Create("ItemTemplate",
                typeof(DataTemplate),
                typeof(Segment),
                null);

        /// <summary>
        /// 数据模板
        /// </summary>
        public DataTemplate ItemTemplate {
            get {
                return (DataTemplate)this.GetValue(ItemTemplateProperty);
            }
            set {
                this.SetValue(ItemTemplateProperty, value);
            }
        }
        #endregion

        //#region SelectedItemTemplate
        //public static readonly BindableProperty SelectedItemControlTemplateProperty =
        //    BindableProperty.Create("SelectedItemControlTemplate",
        //        typeof(ControlTemplate),
        //        typeof(Segment),
        //        null);

        //public ControlTemplate SelectedItemControlTemplate {
        //    get {
        //        return (ControlTemplate)this.GetValue(SelectedItemControlTemplateProperty);
        //    }
        //    set {
        //        this.SetValue(SelectedItemControlTemplateProperty, value);
        //    }
        //}
        //#endregion

        #region itemsSource 数据源
        /// <summary>
        /// 数据源
        /// </summary>
        public static readonly BindableProperty ItemsSourceProperty =
            BindableProperty.Create("ItemsSource",
                typeof(IEnumerable),
                typeof(Segment),
                null,
                propertyChanged: ItemsSourceChanged);

        /// <summary>
        /// 数据源
        /// </summary>
        public IEnumerable ItemsSource {
            get {
                return (IEnumerable)this.GetValue(ItemsSourceProperty);
            }
            set {
                this.SetValue(ItemsSourceProperty, value);
            }
        }

        private static void ItemsSourceChanged(BindableObject bindable, object oldValue, object newValue) {
            var seg = (Segment)bindable;
            seg.Items.Clear();
            if (newValue != null) {
                var source = (IEnumerable<object>)newValue;
                seg.Add(source.ToList(), 0);
            }
        }
        #endregion

        #region SelectedItemBackgroundColor
        /// <summary>
        /// 选中项的背景颜色
        /// </summary>
        public static readonly BindableProperty SelectedItemBackgroundColorProperty =
            BindableProperty.Create("SelectedItemBackgroundColor",
                typeof(Color),
                typeof(Segment),
                Color.Blue);

        /// <summary>
        /// 选中项的背景颜色
        /// </summary>
        public Color SelectedItemBackgroundColor {
            get {
                return (Color)this.GetValue(SelectedItemBackgroundColorProperty);
            }
            set {
                this.SetValue(SelectedItemBackgroundColorProperty, value);
            }
        }

        #endregion


        private Grid Container;
        public ObservableCollection<SegmentItem> Items {
            get;
        } = new ObservableCollection<SegmentItem>();

        private ICommand SelectedCmd { get; }

        private SegmentItem SelectedSegment = null;

        public Segment() {
            this.Container = new Grid() {
                ColumnSpacing = 0
            };
            this.Content = this.Container;

            this.SelectedCmd = new Command((o) => {
                var item = (SegmentItem)o;

                if (this.IsMutliSelectable) {
                    if (this.SelectedItems.Contains(item.Value)) {
                        this.SelectedItems.Remove(item.Value);
                        item.BackgroundColor = Color.Transparent;
                        item.IsSelected = false;
                        //item.ControlTemplate = null;
                    } else {
                        this.SelectedItems.Add(item.Value);
                        item.BackgroundColor = this.SelectedItemBackgroundColor;
                        item.IsSelected = true;
                        //item.ControlTemplate = this.SelectedItemControlTemplate;
                    }
                } else {
                    if (this.SelectedSegment != null) {
                        this.SelectedSegment.BackgroundColor = Color.Transparent;
                        this.SelectedSegment.IsSelected = false;
                        //item.ControlTemplate = null;
                    }
                    this.SelectedItem = item.Value;
                    this.SelectedSegment = item;
                    item.BackgroundColor = this.SelectedItemBackgroundColor;
                    item.IsSelected = true;
                    //item.ControlTemplate = this.SelectedItemControlTemplate;
                }
            });

            new NotifyCollectionWrapper(this.Items,
                add: (datas, idx) => this.Add(datas, idx),
                remove: (datas, idx) => this.Remove(datas, idx),
                reset: () => this.Reset(),
                finished: () => { });
        }


        private void Add(IList datas, int idx) {
            for (var i = idx; i < datas.Count; i++) {
                if (this.Container.Children.Count > i) {
                    var v = this.Container.Children[i];
                    var c = Grid.GetColumn(v) + datas.Count;
                    Grid.SetColumn(v, c);
                }
            }

            foreach (var d in datas) {
                var v = this.GetSegmentItem(d);
                Grid.SetColumn(v, idx++);
                this.Container.Children.Add(v);
            }

        }

        private void Remove(IList datas, int idx) {
            for (var i = idx; i < datas.Count; i++) {
                if (this.Container.Children.Count > i) {
                    var v = this.Container.Children[i];
                    var c = Grid.GetColumn(v) - datas.Count;
                    Grid.SetColumn(v, c);
                }
            }
        }

        private void Reset() {
            this.Container.Children.Clear();
            var idx = 0;
            foreach (var d in this.Items) {
                var v = this.GetSegmentItem(d);
                Grid.SetColumn(v, idx++);
                this.Container.Children.Add(v);
            }
        }

        private SegmentItem GetSegmentItem(object data) {
            SegmentItem item = null;
            if (data is SegmentItem) {
                item = (SegmentItem)data;
            } else {
                item = new SegmentItem();
                var view = (View)this.ItemTemplate.CreateContent();
                if (view is SegmentItem) {
                    item = (SegmentItem)view;
                } else {
                    item.Content = view;
                }
                item.BindingContext = data;
            }

            item.HorizontalOptions = LayoutOptions.FillAndExpand;
            item.VerticalOptions = LayoutOptions.FillAndExpand;

            item.Content.HorizontalOptions = LayoutOptions.Center;
            item.Content.VerticalOptions = LayoutOptions.Center;

            TapBinder.SetCmd(item, this.SelectedCmd);
            TapBinder.SetParam(item, item);

            return item;
        }
    }

    public class SegmentItem : ContentView {

        #region value
        public static BindableProperty ValueProperty =
            BindableProperty.Create("Value",
                typeof(object),
                typeof(SegmentItem));

        public object Value {
            get {
                return this.GetValue(ValueProperty);
            }
            set {
                this.SetValue(ValueProperty, value);
            }
        }
        #endregion

        #region isSelected
        public static BindableProperty IsSelectedProperty =
            BindableProperty.Create("IsSelected",
                typeof(bool),
                typeof(SegmentItem),
                false,
                BindingMode.TwoWay,
                propertyChanged: IsSelectedChanged
                );

        private static void IsSelectedChanged(BindableObject bindable, object oldValue, object newValue) {

        }

        public bool IsSelected {
            get {
                return (bool)this.GetValue(IsSelectedProperty);
            }
            set {
                this.SetValue(IsSelectedProperty, value);
            }
        }
        #endregion


    }
}
