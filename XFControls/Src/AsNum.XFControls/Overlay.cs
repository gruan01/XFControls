using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AsNum.XFControls {
    /// <summary>
    /// 覆盖层
    /// </summary>
    [ContentProperty("Content")]
    public partial class Overlay : View {

        //public BindableProperty IsVisibleProperty =
            //BindableProperty.Create("IsVisible",
            //    typeof(bool),
            //    typeof(Overlay),
            //    false);

        /// <summary>
        /// 是否显示遮罩层
        /// </summary>
        public BindableProperty WithMaskProperty =
            BindableProperty.Create("WithMask",
                typeof(bool),
                typeof(Overlay),
                true);

        /// <summary>
        /// 遮罩层颜色
        /// </summary>
        public BindableProperty MaskColorProperty =
            BindableProperty.Create("MaskColor",
                typeof(Color),
                typeof(Overlay),
                Color.FromHex("02333333"));//前两位是透明度


        public BindableProperty ContentProperty =
            BindableProperty.Create("Content",
                typeof(View),
                typeof(Overlay),
                null);



        //public bool IsVisible {
        //    get {
        //        return (bool)this.GetValue(IsVisibleProperty);
        //    }
        //    set {
        //        this.SetValue(IsVisibleProperty, value);
        //    }
        //}

        public bool WithMask {
            get {
                return (bool)this.GetValue(WithMaskProperty);
            }
            set {
                this.SetValue(WithMaskProperty, value);
            }
        }

        public Color MaskColor {
            get {
                return (Color)this.GetValue(MaskColorProperty);
            }
            set {
                this.SetValue(MaskColorProperty, value);
            }
        }

        public View Content {
            get {
                return (View)this.GetValue(ContentProperty);
            }
            set {
                this.SetValue(ContentProperty, value);
            }
        }

        private static void IsShowChanged(BindableObject bindable, object oldValue, object newValue) {
            var flag = (bool)newValue;
            var overlay = (Overlay)bindable;
            overlay.IsVisible = flag;
        }

        public void Show() {
            this.IsVisible = true;
        }

        public void Hide() {
            this.IsVisible = false;
        }

        public Overlay() {
            this.IsVisible = false;
        }
    }
}