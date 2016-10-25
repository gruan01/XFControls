using Xamarin.Forms;

namespace AsNum.XFControls {
    /// <summary>
    /// 打分条
    /// </summary>
    public class RatingBar : View /*UniformGrid*/ {

        #region IsIndicator
        /// <summary>
        /// 是否仅指示,默认 false
        /// </summary>
        public static readonly BindableProperty IsIndicatorProperty =
            BindableProperty.Create("IsIndicator",
                typeof(bool),
                typeof(RatingBar),
                false);

        /// <summary>
        /// 是否仅指示,默认 false
        /// </summary>
        public bool IsIndicator {
            get {
                return (bool)this.GetValue(IsIndicatorProperty);
            }
            set {
                this.SetValue(IsIndicatorProperty, value);
            }
        }
        #endregion

        #region StarCount
        /// <summary>
        /// 星星数量
        /// </summary>
        public static readonly BindableProperty StarCountProperty =
            BindableProperty.Create("StarCount",
                typeof(int),
                typeof(RatingBar),
                5//,
                 //propertyChanged: StarCountChanged
                );

        //private static void StarCountChanged(BindableObject bindable, object oldValue, object newValue) {
        //    var rb = (RatingBar)bindable;
        //    rb.Update();
        //}

        /// <summary>
        /// 星星数量
        /// </summary>
        public int StarCount {
            get {
                return (int)this.GetValue(StarCountProperty);
            }
            set {
                this.SetValue(StarCountProperty, value);
            }
        }
        #endregion

        #region Rate
        /// <summary>
        /// 当前分数
        /// </summary>
        public static readonly BindableProperty RateProperty =
            BindableProperty.Create("Rate",
                typeof(float),
                typeof(RatingBar),
                0f,
                BindingMode.TwoWay
                );

        //private static void RateChanged(BindableObject bindable, object oldValue, object newValue) {
        //    var rb = (RatingBar)bindable;
        //    rb.Update();
        //}

        /// <summary>
        /// 当前分数
        /// </summary>
        public float Rate {
            get {
                return (float)this.GetValue(RateProperty);
            }
            set {
                this.SetValue(RateProperty, value);
            }
        }
        #endregion

        #region Step
        /// <summary>
        /// 打分步长,默认1
        /// </summary>
        public static readonly BindableProperty StepProperty =
            BindableProperty.Create("Step",
                typeof(float),
                typeof(RatingBar),
                1f);

        /// <summary>
        /// 打分步长,默认1
        /// </summary>
        public float Step {
            get {
                return (float)this.GetValue(StepProperty);
            }
            set {
                this.SetValue(StepProperty, value);
            }
        }
        #endregion

        #region SelectedColor
        public static readonly BindableProperty SelectedColorProperty =
            BindableProperty.Create("SelectedColor",
                typeof(Color),
                typeof(RatingBar),
                Color.Default);

        public Color SelectedColor {
            get {
                return (Color)this.GetValue(SelectedColorProperty);
            }
            set {
                this.SetValue(SelectedColorProperty, value);
            }
        }
        #endregion

        #region UnSelectedColor
        public static readonly BindableProperty UnSelectedColorProperty =
            BindableProperty.Create("UnSelectedColor",
                typeof(Color),
                typeof(RatingBar),
                Color.Default
                );

        public Color UnSelectedColor {
            get {
                return (Color)this.GetValue(UnSelectedColorProperty);
            }
            set {
                this.SetValue(UnSelectedColorProperty, value);
            }
        }
        #endregion

        #region
        //private static readonly ImageSource Checked;
        //private static readonly ImageSource Unchecked;

        //private ObservableCollection<Tmp> Datas = new ObservableCollection<Tmp>();

        //static RatingBar() {
        //    Unchecked = ImageSource.FromResource("AsNum.XFControls.Imgs.Star-Unchecked.png");
        //    Checked = ImageSource.FromResource("AsNum.XFControls.Imgs.Star-Checked.png");
        //}

        //public RatingBar() {
        //    this.FixBy = UniformGridFixBy.Row;
        //    this.Count = 1;
        //    this.ColumnWidth = GridLength.Auto;
        //    this.ItemsSource = this.Datas;
        //    this.ItemTemplate = new RatingBarItemTemplate();
        //    this.Update();
        //}

        //private void Update() {
        //    this.UpdateStarCount();
        //    this.UpdateRate();
        //}

        //private void UpdateStarCount() {
        //    if (this.Datas.Count > this.StarCount) {
        //        for (var i = this.Datas.Count - 1; i > this.StarCount; i--) {
        //            this.Datas.RemoveAt(i);
        //        }
        //    } else if (this.Datas.Count < this.StarCount) {
        //        for (var i = this.Datas.Count; i < this.StarCount; i++) {
        //            this.Datas.Add(new Tmp() {
        //                Index = i,
        //                Value = 0
        //            });
        //        }
        //    }
        //}

        //private void UpdateRate() {
        //    var n = (int)this.Rate;
        //    for (var i = 0; i < n; i++) {
        //        if (i < this.Datas.Count)
        //            this.Datas[i].Value = 1;
        //        else
        //            break;
        //    }

        //    if (this.Datas.Count > n)
        //        this.Datas[n].Value = this.Rate - n;

        //    for (var i = n + 1; i < this.Datas.Count; i++) {
        //        this.Datas[i].Value = 0;
        //    }
        //}

        //public class Tmp {
        //    public int Index {
        //        get; set;
        //    }

        //    public float Value {
        //        get; set;
        //    }
        //}
        #endregion
    }
}
