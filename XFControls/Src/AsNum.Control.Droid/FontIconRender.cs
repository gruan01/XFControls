using Android.Views;
using Android.Widget;
using AsNum.XFControls;
using AsNum.XFControls.Droid;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;


[assembly: ExportRenderer(typeof(FontIcon), typeof(FontIconRender))]
namespace AsNum.XFControls.Droid {

    public class FontIconRender : ViewRenderer<FontIcon, TextView> {

        protected override void OnElementChanged(ElementChangedEventArgs<FontIcon> e) {
            base.OnElementChanged(e);

            if (e.NewElement != null) {
                var control = new TextView(this.Context);
                //var vi = (LayoutInflater)this.Context.GetSystemService(Context.LayoutInflaterService);
                control.Gravity = GravityFlags.CenterHorizontal | GravityFlags.CenterVertical;
                this.SetNativeControl(control);
                this.UpdateNativeControl();
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e) {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName.Equals(FontIcon.FontFamilyProperty.PropertyName) ||
                e.PropertyName.Equals(FontIcon.FontSizeProperty.PropertyName) ||
                    e.PropertyName.Equals(FontIcon.GlyphProperty.PropertyName) ||
                    e.PropertyName.Equals(FontIcon.ColorProperty.PropertyName) ||
                    e.PropertyName.Equals("CurrentColor")) {

                this.UpdateNativeControl();
            }
        }

        private void UpdateNativeControl() {
            var txt = this.Control;
            txt.Typeface = this.Element.FontFamily.ToTypeface();// Typeface.CreateFromAsset(Forms.Context.Assets, this.Element.FontFamily);
            txt.Text = this.Element.Glyph;
            txt.SetTextColor(this.Element.CurrentColor.ToAndroid());
            txt.TextSize = (float)this.Element.FontSize;

            ////
            //txt.Clickable = this.Element.IsEnabled;
            //this.Control.Enabled = this.Element.IsEnabled;
        }
    }
}
