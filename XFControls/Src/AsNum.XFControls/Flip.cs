using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AsNum.XFControls {

    /// <summary>
    /// 左右滑动幻灯片
    /// </summary>
    [ContentProperty("Children")]
    public class Flip : View {

        /// <summary>
        /// 下一侦
        /// </summary>
        public event EventHandler NextRequired;

        /// <summary>
        /// 请求指定侦
        /// </summary>
        public event EventHandler<IndexRequestEventArgs> IndexRequired;


        #region ItemsSource
        /// <summary>
        /// 源
        /// </summary>
        public static readonly BindableProperty ItemsSourceProperty =
            BindableProperty.Create("ItemsSource",
                typeof(IEnumerable),
                typeof(Flip),
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
            var flip = (Flip)bindable;
            flip.WrapItemsSource();
        }
        #endregion

        #region Orientation
        ///// <summary>
        ///// 
        ///// </summary>
        //public static readonly BindableProperty OrientationProperty =
        //    BindableProperty.Create(
        //        "Orientation",
        //        typeof(ScrollOrientation),
        //        typeof(Flip),
        //        ScrollOrientation.Horizontal);

        //public ScrollOrientation Orientation {
        //    get {
        //        return (ScrollOrientation)this.GetValue(OrientationProperty);
        //    }
        //    set {
        //        this.SetValue(OrientationProperty, value);
        //    }
        //}
        #endregion

        #region ItemTemplate
        /// <summary>
        /// 数据模板
        /// </summary>
        public static readonly BindableProperty ItemTemplateProperty =
            BindableProperty.Create(
                "ItemTemplate",
                typeof(DataTemplate),
                typeof(Flip),
                null
                );

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

        #region AutoPlay
        /// <summary>
        /// 是否自动播放
        /// </summary>
        public static readonly BindableProperty AutoPlayProperty =
            BindableProperty.Create(
                    "AutoPlay",
                    typeof(bool),
                    typeof(Flip),
                    false,
                    propertyChanged: AutoPlayChanged
                );

        /// <summary>
        /// 是否自动播放
        /// </summary>
        public bool AutoPlay {
            get {
                return (bool)this.GetValue(AutoPlayProperty);
            }
            set {
                this.SetValue(AutoPlayProperty, value);
            }
        }


        private static void AutoPlayChanged(BindableObject bindable, object oldValue, object newValue) {
            var flip = (Flip)bindable;
            if ((bool)newValue) {
                flip.Play();
            } else {
                flip.Stop();
            }
        }
        #endregion

        #region Interval
        /// <summary>
        /// 播放间隔, 单位毫秒,默认2000
        /// </summary>
        public static readonly BindableProperty IntervalProperty =
            BindableProperty.Create("Interval",
                typeof(int),
                typeof(Flip),
                2000);

        /// <summary>
        /// 播放间隔, 单位毫秒,默认2000
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

        #region ShowIndicator
        /// <summary>
        /// 是否显示指示点
        /// </summary>
        public static readonly BindableProperty ShowIndicatorProperty =
            BindableProperty.Create(
                    "ShowIndicator",
                    typeof(bool),
                    typeof(Flip),
                    true
                );

        /// <summary>
        /// 是否显示指示点
        /// </summary>
        public bool ShowIndicator {
            get {
                return (bool)this.GetValue(ShowIndicatorProperty);
            }
            set {
                this.SetValue(ShowIndicatorProperty, value);
            }
        }
        #endregion

        #region Current
        /// <summary>
        /// 当前侦序号,从0开始
        /// </summary>
        public static readonly BindableProperty CurrentProperty =
            BindableProperty.Create("Current",
                typeof(int),
                typeof(Flip),
                0,
                propertyChanged: CurrentChanged,
                defaultBindingMode: BindingMode.TwoWay
                );

        /// <summary>
        /// 当前侦序号,从0开始
        /// </summary>
        public int Current {
            get {
                return (int)this.GetValue(CurrentProperty);
            }
            set {
                this.SetValue(CurrentProperty, value);
            }
        }

        /// <summary>
        /// 从1开始,区别于 Current, 为了方便在界面上显示序号
        /// </summary>
        public int Index {
            get {
                if (this.Children.Count > 0)
                    return (int)Current + 1;
                else
                    return 0;
            }
        }

        private static void CurrentChanged(BindableObject bindable, object oldValue, object newValue) {
            var flip = (Flip)bindable;
            flip.OnPropertyChanged("Index");

            if (flip.IndexRequired != null && !oldValue.Equals(newValue)) {
                flip.IndexRequired.Invoke(flip, new IndexRequestEventArgs((int)newValue));
            }
        }
        #endregion

        #region Total
        //https://developer.xamarin.com/api/type/Xamarin.Forms.BindablePropertyKey/
        /// <summary>
        /// 条目总数
        /// </summary>
        public static readonly BindablePropertyKey TotalPropertyKey =
            BindableProperty.CreateReadOnly("Total",
                typeof(int),
                typeof(Flip),
                0);

        /// <summary>
        /// 条目总数,为了方便界面显示
        /// </summary>
        public int Total {
            get {
                //注意: TotalPropertyKey.BindableProperty
                return (int)this.GetValue(TotalPropertyKey.BindableProperty);
            }
            private set {
                //注意: TotalPropertyKey
                this.SetValue(TotalPropertyKey, value);
            }
        }

        #endregion


        public ObservableCollection<View> Children {
            get;
        } = new ObservableCollection<View>();


        #region 数据源变动事件
        /// <summary>
        /// 订阅数据源变化通知
        /// </summary>
        private void WrapItemsSource() {
            new NotifyCollectionWrapper(this.ItemsSource,
                            add: (datas, idx) => this.Add(datas, idx),
                            remove: (datas, idx) => this.Remove(datas, idx),
                            reset: () => this.Reset(),
                            finished: () => {
                                this.Total = this.ItemsSource?.Cast<object>().Count() ?? 0;
                            });
        }


        private void Add(IList datas, int idx) {
            var c = this.Children.Count();

            foreach (var d in datas) {
                var i = idx++;
                var v = this.GetChild(d);
                if (i < c) {
                    this.Children.Insert(i, v);
                } else {
                    this.Children.Add(v);
                }
            }
        }

        private void Remove(IList datas, int idx) {
            var headers = this.Children.Skip(idx).Take(datas.Count);

            for (var i = idx; i < datas.Count; i++) {
                this.Children.RemoveAt(i);
            }
        }

        private void Reset() {
            this.Children.Clear();

            if (this.ItemsSource != null) {
                var idx = 0;
                foreach (var d in this.ItemsSource) {
                    var i = idx++;
                    var v = this.GetChild(d);
                    this.Children.Add(v);
                }
            }
        }
        #endregion

        /// <summary>
        /// 根据数据模板,生成子元素
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private View GetChild(object data) {
            View view = null;
            if (this.ItemTemplate != null) {
                view = (View)this.ItemTemplate.CreateContent();
                view.BindingContext = data;
                view.Parent = this;
            } else {
                view = new Label() { Text = "Not Set ItemTemplate" };
            }
            return view;
        }

        protected override void OnSizeAllocated(double width, double height) {
            base.OnSizeAllocated(width, height);

            foreach (var c in this.Children) {
                if (c.Parent == null)
                    c.Parent = this;
                c.Layout(new Rectangle(0, 0, width, height));
            }
        }



        public void Play() {
            this.AutoPlay = true;
            this.InnerPlay();
        }

        public void Stop() {
            this.AutoPlay = false;
        }

        private void InnerPlay() {
            if (this.AutoPlay)
                Task.Delay(this.Interval)
                    .ContinueWith(t => {
                        if (this.NextRequired != null)
                            this.NextRequired.Invoke(this, new EventArgs());
                        this.InnerPlay();
                    });
        }





        public class IndexRequestEventArgs : EventArgs {

            public int Index { get; }

            public IndexRequestEventArgs(int idx) {
                this.Index = idx;
            }
        }
    }

}
