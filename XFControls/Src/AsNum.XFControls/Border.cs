using Xamarin.Forms;

namespace AsNum.XFControls {

    /// <summary>
    /// 边框
    /// </summary>
    public class Border : ContentView {

        /// <summary>
        /// 圆角大小
        /// </summary>
        public static readonly BindableProperty CornerRadiusProperty =
            BindableProperty.Create("CornerRadius",
                typeof(CornerRadius),
                typeof(Border),
                default(CornerRadius)
                );


        /// <summary>
        /// 边框颜色
        /// </summary>
        public static readonly BindableProperty StrokeProperty =
            BindableProperty.Create("Stroke",
                typeof(Color),
                typeof(Border),
                Color.Default);



        /// <summary>
        /// 边框厚度
        /// </summary>
        public static readonly BindableProperty StrokeThicknessProperty =
            BindableProperty.Create("StrokeThickness",
                typeof(Thickness),
                typeof(Border),
                default(Thickness)
                );


        /// <summary>
        /// 是否裁剪超出部分
        /// </summary>
        public static readonly BindableProperty IsClippedToBorderProperty =
            BindableProperty.Create("IsClippedToBorder",
                typeof(bool),
                typeof(bool),
                true);


        /// <summary>
        /// 圆角大小
        /// </summary>
        public CornerRadius CornerRadius {
            get {
                return (CornerRadius)base.GetValue(CornerRadiusProperty);
            }
            set {
                base.SetValue(CornerRadiusProperty, value);
            }
        }


        /// <summary>
        /// 边框颜色
        /// </summary>
        public Color Stroke {
            get {
                return (Color)GetValue(StrokeProperty);
            }
            set {
                SetValue(StrokeProperty, value);
            }
        }

        /// <summary>
        /// 边框宽度
        /// </summary>
        public Thickness StrokeThickness {
            get {
                return (Thickness)GetValue(StrokeThicknessProperty);
            }
            set {
                SetValue(StrokeThicknessProperty, value);
            }
        }

        /// <summary>
        /// 是否裁剪超出部分
        /// </summary>
        public bool IsClippedToBorder {
            get {
                return (bool)GetValue(IsClippedToBorderProperty);
            }
            set {
                SetValue(IsClippedToBorderProperty, value);
            }
        }

    }
}
