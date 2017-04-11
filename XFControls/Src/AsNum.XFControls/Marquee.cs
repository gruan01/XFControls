using System.Collections;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AsNum.XFControls {
    public enum MarqueeDirect
    {
        //从左到右
        LeftToRight ,
        //从右到左
        RightToLeft,
        //从下往上
        DownToUp,
        //从上往下
        UpToDown
    }
    /// <summary>
    /// 跑马灯
    /// </summary>
    public class Marquee : AbsoluteLayout {

        #region itemsSource 数据源

        /// <summary>
        /// 数据源
        /// </summary>
        public static readonly BindableProperty ItemsSourceProperty =
            BindableProperty.Create("ItemsSource",
                typeof(IEnumerable),
                typeof(Marquee),
                null,
                propertyChanged: ItemsSourceChanged);

        /// <summary>
        /// 数据源
        /// </summary>
        public IEnumerable ItemsSource {
            get {
                return (IList)this.GetValue(ItemsSourceProperty);
            }
            set {
                this.SetValue(ItemsSourceProperty, value);
            }
        }

        private static void ItemsSourceChanged(BindableObject bindable, object oldValue, object newValue) {
            var tv = (Marquee)bindable;
            tv.UpdateChildren();

            if (newValue is INotifyCollectionChanged) {
                var newCollection = (INotifyCollectionChanged)newValue;
                tv.InitCollection(newCollection);
            }
        }
        #endregion

        #region ItemTemplate 数据模板
        /// <summary>
        /// 数据模板
        /// </summary>
        public static readonly BindableProperty ItemTemplateProperty =
            BindableProperty.Create("ItemTemplate",
                typeof(DataTemplate),
                typeof(Marquee)
                );

        /// <summary>
        /// 数据模板
        /// </summary>
        public DataTemplate ItemTemplate {
            get {
                return (DataTemplate)GetValue(ItemTemplateProperty);
            }
            set {
                SetValue(ItemTemplateProperty, value);
            }
        }
        #endregion

        #region Interval
        /// <summary>
        /// 切换间隔, 单位毫秒,默认3000
        /// </summary>
        public static readonly BindableProperty IntervalProperty =
            BindableProperty.Create("Interval",
                typeof(int),
                typeof(Marquee),
                3000);

        /// <summary>
        /// 切换间隔, 单位毫秒,默认3000
        /// </summary>
        public int Interval {
            get {
                return (int)this.GetValue(IntervalProperty);
            }
            set {
                this.SetValue(IntervalProperty, value);
            }
        }
        #endregion

        #region 设置方向
        private MarqueeDirect _Direct = MarqueeDirect.DownToUp;
        public MarqueeDirect Direct { get { return _Direct; } set { _Direct = value; } }
        #endregion

        private int? _current = null;
        /// <summary>
        /// 当前序号
        /// </summary>
        private int? Current {
            get {
                return this._current;
            }
            set {
                this._current = value < 0 ? 0 : value >= this.Children.Count ? 0 : value;
            }
        }

        /// <summary>
        /// 是否正在运行
        /// </summary>
        private bool IsRunning = false;

        public Marquee() {
            //可视范围之外的内容不可见
            this.IsClippedToBounds = true;
            this.ChildAdded += Marquee_ChildAdded;
            //this.Loop();
        }

        private async Task Animate(View view, bool isCurrent) {
            //if (isCurrent)
            view.IsVisible = true;

            Rectangle beginRect = Rectangle.Zero;
            Rectangle endRect = Rectangle.Zero;

            //设置起始和终止值             
            beginRect = setbeginRect(isCurrent, _Direct);
            endRect = setendRect(isCurrent, _Direct);

            view.Layout(beginRect);
            await view.LayoutTo(endRect, easing: Easing.Linear)
            .ContinueWith(t => {
                //BUG 会使填充失效
                view.IsVisible = isCurrent;
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private Rectangle setbeginRect(bool iscurrent, MarqueeDirect Direct)
        {
            if (iscurrent)
            {
                if (Direct == MarqueeDirect.DownToUp)
                {
                    return new Rectangle(0, this.Bounds.Height, this.Bounds.Width, this.Bounds.Height);
                }
                else if (Direct == MarqueeDirect.LeftToRight)
                {
                    return new Rectangle(-this.Bounds.Width, 0, this.Bounds.Width, this.Bounds.Height);
                }
                else if (Direct == MarqueeDirect.RightToLeft)
                {
                    return new Rectangle(this.Bounds.Width, 0, this.Bounds.Width, this.Bounds.Height);
                }
                else if (Direct == MarqueeDirect.UpToDown)
                {
                    return new Rectangle(0, -this.Bounds.Height, this.Bounds.Width, this.Bounds.Height);
                }
                else
                {
                    return new Rectangle(0, this.Bounds.Height, this.Bounds.Width, this.Bounds.Height);
                }
            }
            else
            {
                return new Rectangle(0, 0, this.Bounds.Width, this.Bounds.Height);
            }

        }

        private Rectangle setendRect(bool iscurrent, MarqueeDirect Direct)
        {
            if (iscurrent)
            {
                return new Rectangle(0, 0, this.Bounds.Width, this.Bounds.Height);
            }
            else
            {
                if (Direct == MarqueeDirect.DownToUp)
                {
                    return new Rectangle(0, -this.Bounds.Height, this.Bounds.Width, this.Bounds.Height);
                }
                else if (Direct == MarqueeDirect.LeftToRight)
                {
                    return new Rectangle(this.Bounds.Width, 0, this.Bounds.Width, this.Bounds.Height);
                }
                else if (Direct == MarqueeDirect.RightToLeft)
                {
                    return new Rectangle(-this.Bounds.Width, 0, this.Bounds.Width, this.Bounds.Height);
                }
                else if (Direct == MarqueeDirect.UpToDown)
                {
                    return new Rectangle(0, this.Height, this.Bounds.Width, this.Bounds.Height);
                }
                else
                {
                    return new Rectangle(0, -this.Bounds.Height, this.Bounds.Width, this.Bounds.Height);
                }
            }
        }

        private void Begin() {
            if (this.IsRunning)
                return;
            else
                this.Run();
        }

        private async void Run() {
            if (this.Children.Count > 0) {
                this.IsRunning = true;

                if (this.Current.HasValue) {
                    var outEle = this.Children[this.Current.Value];
                    await this.Animate(outEle, false);
                    this.Current++;
                } else {
                    this.Current = 0;
                }

                var inEle = this.Children[this.Current.Value];
                await this.Animate(inEle, true);
            }

            await Task.Delay(this.Interval)
                    .ContinueWith(t => this.Run(), TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void Marquee_ChildAdded(object sender, ElementEventArgs e) {
            this.InitChildView((View)e.Element);
            this.Begin();
        }

        private void UpdateChildren() {
            this.Children.Clear();
            if (this.ItemsSource == null)
                return;

            var source = this.ItemsSource.Cast<object>();
            foreach (var d in this.ItemsSource) {
                var view = this.GetChildView(d);
                this.Children.Add(view);
            }
        }

        private View GetChildView(object data) {
            View view = null;
            if (this.ItemTemplate != null) {
                if (this.ItemTemplate != null)
                    view = (View)this.ItemTemplate.CreateContent();

                if (view != null) {
                    view.BindingContext = data;
                }
            }

            if (view == null) {
                view = new Label() { Text = data?.GetType().FullName };
            }
            return view;
        }

        private void InitChildView(View view) {
            view.IsVisible = false;
            view.VerticalOptions = LayoutOptions.CenterAndExpand;
            view.HorizontalOptions = LayoutOptions.StartAndExpand;
            ////父容器的可视范围之外
            view.Layout(new Rectangle(0, -this.Bounds.Height, this.Bounds.Width, this.Bounds.Height));
        }

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
                    this.UpdateChildren();
                    break;
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

                this.Children.Insert(startIdx, view);
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
                this.Children.RemoveAt(startIdx);
                startIdx++;
            }
        }
    }
}
