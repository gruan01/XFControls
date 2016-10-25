using Xamarin.Forms;

namespace AsNum.XFControls {
    /// <summary>
    /// DatePicker 扩展
    /// </summary>
    [System.Obsolete("不建议使用")]
    public class DatePickerEx : DatePicker {

        //public static readonly BindableProperty TextColorProperty =
        //    BindableProperty.Create(
        //            "TextColor",
        //            typeof(Color),
        //            typeof(DatePickerEx),
        //            Color.Default
        //            );

        /// <summary>
        /// PlaceHolder 颜色
        /// </summary>
        public static readonly BindableProperty PlaceHolderColorProperty =
            BindableProperty.Create(
                        "PlaceHolderColor",
                        typeof(Color),
                        typeof(DatePickerEx),
                        Color.Default
                        );


        /// <summary>
        /// PlaceHolder 颜色
        /// </summary>
        public static readonly BindableProperty PlaceHolderProperty =
            BindableProperty.Create(
                "PlaceHolder",
                typeof(string),
                typeof(DatePickerEx),
                null
                );


        /// <summary>
        /// 文本大小,默认 15
        /// </summary>
        public static readonly BindableProperty FontSizeProperty =
            BindableProperty.Create(
                "FontSize",
                typeof(double),
                typeof(TimePickerEx),
                15D);


        /// <summary>
        /// 文本水平对齐
        /// </summary>
        public static readonly BindableProperty HorizontalTextAlignmentProperty =
            BindableProperty.Create(
                "HorizontalTextAlignment",
                typeof(TextAlignment),
                typeof(DatePickerEx),
                TextAlignment.Start,
                BindingMode.OneWay
                );

        //public Color TextColor {
        //    get {
        //        return (Color)this.GetValue(TextColorProperty);
        //    }
        //    set {
        //        this.SetValue(TextColorProperty, value);
        //    }
        //}

        /// <summary>
        /// PlaceHolder 颜色
        /// </summary>
        public Color PlaceHolderColor {
            get {
                return (Color)this.GetValue(PlaceHolderColorProperty);
            }
            set {
                this.SetValue(PlaceHolderColorProperty, value);
            }
        }

        /// <summary>
        /// PlaceHolder
        /// </summary>
        public string PlaceHolder {
            get {
                return (string)this.GetValue(PlaceHolderProperty);
            }
            set {
                this.SetValue(PlaceHolderProperty, value);
            }
        }

        /// <summary>
        /// 文本大小
        /// </summary>
        [TypeConverter(typeof(FontSizeConverter))]
        public double FontSize {
            get {
                return (double)this.GetValue(FontSizeProperty);
            }
            set {
                this.SetValue(FontSizeProperty, (object)value);
            }
        }

        /// <summary>
        /// 文本水平对齐
        /// </summary>
        public TextAlignment HorizontalTextAlignment {
            get {
                return (TextAlignment)this.GetValue(HorizontalTextAlignmentProperty);
            }
            set {
                this.SetValue(HorizontalTextAlignmentProperty, value);
            }
        }
    }
}
