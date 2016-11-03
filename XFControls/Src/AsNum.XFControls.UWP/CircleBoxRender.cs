using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;
using WC = Windows.UI.Xaml.Controls;

namespace AsNum.XFControls.UWP {
    public class CircleBoxRender : ViewRenderer<CircleBox, WC.Border> {

        protected override void OnElementChanged(ElementChangedEventArgs<CircleBox> e) {
            base.OnElementChanged(e);

            this.SetNativeControl(new WC.Border());
            this.UpdateControl();
        }


        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
            base.OnElementPropertyChanged(sender, e);

            this.Element.HorizontalOptions = LayoutOptions.Center;
            this.Element.VerticalOptions = LayoutOptions.Center;

            if (e.PropertyName.Equals(CircleBox.RadiusProperty.PropertyName, StringComparison.OrdinalIgnoreCase)) {
                this.UpdateControl();
            }
        }

        protected override void UpdateNativeControl() {
            base.UpdateNativeControl();
            this.UpdateControl();
        }

        protected override void UpdateBackgroundColor() {
            if (Control != null) {
                Control.Background = this.Element.BackgroundColor.ToBrush();
            }
        }

        private void UpdateControl() {
            if (this.Control == null)
                return;

            var wh = this.Element.Radius * 2;
            this.Control.CornerRadius = new Windows.UI.Xaml.CornerRadius(this.Element.Radius);// new WX.CornerRadius(this.Element.Radius);

            this.Element.WidthRequest = wh;
            this.Element.HeightRequest = wh;
            this.Element.Content.HorizontalOptions = LayoutOptions.Center;
            this.Element.Content.VerticalOptions = LayoutOptions.Center;

            //this.Control.Width = wh;
            //this.Control.Height = wh;
        }
    }
}
