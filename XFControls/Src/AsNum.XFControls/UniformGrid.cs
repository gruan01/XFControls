using AsNum.XFControls.Binders;
using System;
using System.Collections;
using System.Windows.Input;
using Xamarin.Forms;
using System.Diagnostics;

namespace AsNum.XFControls {
    /// <summary>
    /// 
    /// </summary>
    public class UniformGrid : Grid {

		/// <summary>
		/// 上一次数据源的Item Count
		/// </summary>
		private int _ChildCount = 0;

        #region itemsSource 数据源
        public static readonly BindableProperty ItemsSourceProperty =
            BindableProperty.Create("ItemsSource",
                typeof(IEnumerable),
                typeof(UniformGrid),
                null,
                propertyChanged: ItemsSourceChanged);

        public IEnumerable ItemsSource {
            get {
                return (IEnumerable)this.GetValue(ItemsSourceProperty);
            }
            set {
                this.SetValue(ItemsSourceProperty, value);
            }
        }

        private static void ItemsSourceChanged(BindableObject bindable, object oldValue, object newValue) {
            var g = (UniformGrid)bindable;
            g.WrapItemsSource();
        }
        #endregion

        #region 每行或每列的个数
        public static readonly BindableProperty CountProperty =
            BindableProperty.Create("Count",
                typeof(int),
                typeof(UniformGrid),
                1);

        public int Count {
            get {
                return (int)this.GetValue(CountProperty);
            }
            set {
                this.SetValue(CountProperty, value);
            }
        }
        #endregion

        #region FixBy
        public static readonly BindableProperty FixByProperty =
            BindableProperty.Create("FixBy",
                typeof(UniformGridFixBy),
                typeof(UniformGrid),
                UniformGridFixBy.Column
                );

        public UniformGridFixBy FixBy {
            get {
                return (UniformGridFixBy)this.GetValue(FixByProperty);
            }
            set {
                this.SetValue(FixByProperty, value);
            }
        }
        #endregion

        #region ItemTemplate 标签页数据模板
        public static readonly BindableProperty ItemTemplateProperty =
            BindableProperty.Create("ItemTemplate",
                typeof(DataTemplate),
                typeof(UniformGrid),
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

        #region ItemTemplateSelector 标签页模板选择器
        public static readonly BindableProperty ItemTemplateSelectorProperty =
            BindableProperty.Create("ItemTemplateSelector",
                typeof(DataTemplateSelector),
                typeof(UniformGrid),
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

        #region ItemTapedCmd
        public static readonly BindableProperty ItemTapedCmdProperty =
            BindableProperty.Create("ItemTapedCmd",
                typeof(ICommand),
                typeof(UniformGrid),
                propertyChanged: ItemTapedCmdChanged
                );

        private static void ItemTapedCmdChanged(BindableObject bindable, object oldValue, object newValue) {
            var g = (UniformGrid)bindable;
            var flag = (newValue != null || g.ItemTapedCmd != null);
            foreach (var v in g.Children) {
                TapBinder.SetWithFeedback(v, flag);
            }
        }

        public ICommand ItemTapedCmd {
            get {
                return (ICommand)this.GetValue(ItemTapedCmdProperty);
            }
            set {
                this.SetValue(ItemTapedCmdProperty, value);
            }
        }
        #endregion

        #region AutoColumnWidth
        /// <summary>
        /// 列宽度，默认 Star, 即均分
        /// </summary>
        public static readonly BindableProperty ColumnWidthProperty =
            BindableProperty.Create("ColumnWidth",
                typeof(GridLength),
                typeof(UniformGrid),
                GridLength.Star
                );

        public GridLength ColumnWidth {
            get {
                return (GridLength)this.GetValue(ColumnWidthProperty);
            }
            set {
                this.SetValue(ColumnWidthProperty, value);
            }
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="idx"></param>
        /// <returns></returns>
        private View GetChild(object data) {

            View child = null;
            if (this.ItemTemplate != null || this.ItemTemplateSelector != null) {
                if (this.ItemTemplateSelector != null) {
                    child = (View)this.ItemTemplateSelector.SelectTemplate(data, this).CreateContent();
                } else if (this.ItemTemplate != null) {
                    child = (View)this.ItemTemplate.CreateContent();
                }

                if (child != null)
                    child.BindingContext = data;
            }
            if (child == null)
                child = new Label() { Text = "Not set ItemTemplate" };

            child.BindingContext = data;
            child.SetBinding(TapBinder.CmdProperty, new Binding(nameof(this.ItemTapedCmd), source: this));
            TapBinder.SetParam(child, data);

            return child;
        }

        #region 数据源变动事件
        /// <summary>
        /// 订阅数据源变化通知
        /// </summary>
        private void WrapItemsSource() {
            new NotifyCollectionWrapper(
                this.ItemsSource,
                add: (datas, idx) => this.Add(datas, idx),
                remove: (datas, idx) => this.Remove(datas, idx),
                reset: () => this.Reset(),
                finished: () => {
                    this.UpdateRowCol();
                    this.UpdateChildrenRowCol();
                },
                begin: () => {
                    this.UpdateRowCol();
                });
        }




        private void UpdateRowCol() {
            var c = this.Children.Count;
			if (c == 0 || c == _ChildCount)
                return;

			this._ChildCount = c;

            var rc = (int)Math.Ceiling((double)c / this.Count);
            if (rc == 0)
                rc = 1;
            var row = this.FixBy == UniformGridFixBy.Row ? this.Count : rc;
            var col = this.FixBy == UniformGridFixBy.Column ? this.Count : rc;

            this.ColumnDefinitions = new ColumnDefinitionCollection();
            this.RowDefinitions = new RowDefinitionCollection();

            for (var i = 0; i < row; i++) {
                this.RowDefinitions.Add(new RowDefinition() {
                    Height = GridLength.Auto
                });
            }

            for (var i = 0; i < col; i++) {
                var cf = new ColumnDefinition();
                cf.SetBinding(ColumnDefinition.WidthProperty, new Binding(nameof(this.ColumnWidth), source: this));
                this.ColumnDefinitions.Add(cf);
            }
        }

        private void UpdateChildGridRowCol(View child, int i) {
            int r, c;

            if (this.FixBy == UniformGridFixBy.Column) {
                r = i / this.Count;
                c = i % this.Count;
            } else {
                r = i % this.Count;
                c = i / this.Count;
            }

            Grid.SetRow(child, r);
            Grid.SetColumn(child, c);
        }

        private void UpdateChildrenRowCol() {
			this.BatchBegin();
            int i = 0;
            foreach (var c in this.Children) {
                this.UpdateChildGridRowCol(c, i++);
            }
			this.BatchCommit();
			this.UpdateChildrenLayout();
        }

        private void Add(object d, int i) {
            var v = this.GetChild(d);
            this.Children.Add(v);
        }

        private void Insert(object d, int i) {
            var v = this.GetChild(d);
            this.Children.Insert(i, v);
        }

        private void Remove(int i) {
            this.Children.RemoveAt(i);
        }

        private void Add(IList datas, int idx) {
            var c = this.Children.Count;

            foreach (var d in datas) {
                var i = idx++;
                if (i < c) {
                    this.Insert(d, i);
                } else {
                    this.Add(d, i);
                }
            }
        }

        private void Remove(IList datas, int idx) {
            for (var i = idx; i < datas.Count; i++) {
                this.Remove(i);
            }
        }

        private void Reset() {
            this.Children.Clear();

            if (this.ItemsSource != null) {
                var idx = 0;
                foreach (var d in this.ItemsSource) {
                    this.Add(d, idx++);
                }
            }
        }
        #endregion
    }


    public enum UniformGridFixBy {
        Row,
        Column
    }
}
