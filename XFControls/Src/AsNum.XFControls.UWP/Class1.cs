using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace AsNum.XFControls.UWP {
    public class Control4 : Control {
        TextBlock txt;

        public Control4() {
            DefaultStyleKey = typeof(Control4);
        }

        //public string Text { set; get; }


        public string Text {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register(
                "Text",
                typeof(string),
                typeof(Control4),
                new PropertyMetadata(""));


        #region TextColor
        public static readonly DependencyProperty TextColorProperty =
            DependencyProperty.Register(
                "TextColor",
                typeof(Color),
                typeof(Control4),
                new PropertyMetadata(Colors.Black)
                );


        public Color TextColor {
            get {
                return (Color)this.GetValue(TextColorProperty);
            }
            set {
                this.SetValue(TextColorProperty, value);
            }
        }
        #endregion


        protected override void OnApplyTemplate() {
            base.OnApplyTemplate();

            txt = GetTemplateChild("txt") as TextBlock;
        }
    }
}
