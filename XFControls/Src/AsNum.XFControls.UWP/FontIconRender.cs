using AsNum.XFControls;
using AsNum.XFControls.UWP;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;
using Xamarin.Forms.Platform.UWP;
using W = Windows.UI.Xaml.Controls;


[assembly: ExportRenderer(typeof(FontIcon), typeof(FontIconRender))]
namespace AsNum.XFControls.UWP {
    public class FontIconRender : ViewRenderer<FontIcon, W.FontIcon> {

        protected override void OnElementChanged(ElementChangedEventArgs<FontIcon> e) {
            base.OnElementChanged(e);

            if (e.NewElement == null)
                return;

            this.SetNativeControl(new W.FontIcon());
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e) {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName.Equals(FontIcon.FontFamilyProperty.PropertyName) ||
                e.PropertyName.Equals(FontIcon.FontSizeProperty.PropertyName) ||
                    e.PropertyName.Equals(FontIcon.GlyphProperty.PropertyName) ||
                    e.PropertyName.Equals(FontIcon.ColorProperty.PropertyName)) {

                this.UpdateNativeControl();
            }
        }

        protected override void UpdateNativeControl() {
            base.UpdateNativeControl();

            if (this.Control == null)
                return;

            this.Control.FontFamily = this.Element.FontFamily.ToFontFamily();
            this.Control.Foreground = new SolidColorBrush(this.Element.Color.ToMediaColor());
            this.Control.Glyph = this.Element.Glyph;
            this.Control.FontSize = this.Element.FontSize;
        }

    }
}
