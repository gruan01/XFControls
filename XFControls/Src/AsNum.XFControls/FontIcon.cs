using AsNum.XFControls.Binders;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace AsNum.XFControls {
    /// <summary>
    /// Font 图标
    /// </summary>
    public class FontIcon : View {

        /// <summary>
        /// 字体名称, 字体需要放到平台项目下面
        /// </summary>
        public static readonly BindableProperty FontFamilyProperty =
            BindableProperty.Create("FontFamily",
                typeof(string),
                typeof(FontIcon),
                "");

        /// <summary>
        /// 文字大小,默认12
        /// </summary>
        public static readonly BindableProperty FontSizeProperty =
            BindableProperty.Create("FontSize",
                typeof(double),
                typeof(FontIcon),
                12d);

        /// <summary>
        /// 要显示的文本的代码
        /// </summary>
        public static readonly BindableProperty GlyphProperty =
            BindableProperty.Create("Glyph",
                typeof(string),
                typeof(FontIcon),
                "");


        /// <summary>
        /// 文字颜色,默认 Black
        /// </summary>
        public static readonly BindableProperty ColorProperty =
            BindableProperty.Create(
                "Color",
                typeof(Color),
                typeof(FontIcon),
                Color.Black,
                propertyChanged: Changed);


        /// <summary>
        /// 禁用状态下的文字颜色,默认Gray
        /// </summary>
        public static readonly BindableProperty DisableColorProperty =
            BindableProperty.Create("DisableColor",
                typeof(Color),
                typeof(FontIcon),
                Color.Gray);

        /// <summary>
        /// Tap 命令
        /// </summary>
        public static readonly BindableProperty TapCmdProperty =
            BindableProperty.Create("TapCmd",
                typeof(ICommand),
                typeof(FontIcon),
                null,
                propertyChanged: TapCmdChanged
                );

        /// <summary>
        /// Tap 命令的参数
        /// </summary>
        public static readonly BindableProperty TapCmdParamProperty =
            BindableProperty.Create("TapParam",
                typeof(object),
                typeof(FontIcon),
                null,
                propertyChanged: TapCmdParamChanged);


        /// <summary>
        /// 字体
        /// </summary>
        public string FontFamily {
            get {
                return this.GetValue(FontFamilyProperty) as string;
            }
            set {
                this.SetValue(FontFamilyProperty, value);
            }
        }

        /// <summary>
        /// 字体大小
        /// </summary>
        public double FontSize {
            get {
                return (double)this.GetValue(FontSizeProperty);
            }
            set {
                this.SetValue(FontSizeProperty, value);
            }
        }

        /// <summary>
        /// 要显示的文本的代码
        /// </summary>
        public string Glyph {
            get {
                return this.GetValue(GlyphProperty) as string;
            }
            set {
                this.SetValue(GlyphProperty, value);
            }
        }

        /// <summary>
        /// 当前使用颜色，在 Renderer 中使用
        /// </summary>
        public Color CurrentColor {
            get;
            private set;
        }

        /// <summary>
        /// 文字颜色,默认 Black
        /// </summary>
        public Color Color {
            get {
                return (Color)this.GetValue(ColorProperty);
            }
            set {
                this.SetValue(ColorProperty, value);
            }
        }

        /// <summary>
        /// 禁用状态下的文字颜色,默认 Gray
        /// </summary>
        public Color DisableColor {
            get {
                return (Color)this.GetValue(DisableColorProperty);
            }
            set {
                this.SetValue(DisableColorProperty, value);
            }
        }

        /// <summary>
        /// Tap 命令
        /// </summary>
        public ICommand TapCmd {
            get {
                return (ICommand)this.GetValue(TapCmdProperty);
            }
            set {
                this.SetValue(TapCmdProperty, value);
            }
        }

        /// <summary>
        /// Tap 命令参数
        /// </summary>
        public object TapParam {
            get {
                return this.GetValue(TapCmdParamProperty);
            }
            set {
                this.SetValue(TapCmdParamProperty, value);
            }
        }


        private static void Changed(BindableObject bindable, object oldValue, object newValue) {
            var fi = (FontIcon)bindable;
            fi.UpdateColor();
        }

        private void UpdateColor() {
            this.CurrentColor = this.IsEnabled ? this.Color : this.DisableColor;
            this.OnPropertyChanged("CurrentColor");
        }

        private static void TapCmdParamChanged(BindableObject bindable, object oldValue, object newValue) {
            TapBinder.SetParam(bindable, newValue);
        }

        private static void TapCmdChanged(BindableObject bindable, object oldValue, object newValue) {
            TapBinder.SetCmd(bindable, (ICommand)newValue);
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            base.OnPropertyChanged(propertyName);

            if (propertyName.Equals("IsEnabled")) {
                this.UpdateColor();
            }
        }

		//protected override SizeRequest OnMeasure(double widthConstraint, double heightConstraint)
		//{
		//	//return base.OnMeasure(widthConstraint, heightConstraint);
		//	return new SizeRequest(new Size(this.FontSize, this.FontSize));
		//}
    }
}
