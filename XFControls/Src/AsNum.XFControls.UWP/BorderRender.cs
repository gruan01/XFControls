using AsNum.XFControls;
using AsNum.XFControls.UWP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Platform.UWP;
using WC = Windows.UI.Xaml.Controls;
using WX = Windows.UI.Xaml;
using WM = Windows.UI.Xaml.Media;
using System.ComponentModel;
using XF = Xamarin.Forms;

[assembly: ExportRenderer(typeof(Border), typeof(BorderRender))]
namespace AsNum.XFControls.UWP {
    public class BorderRender : ViewRenderer<Border, WC.Border> {

        protected override void OnElementChanged(ElementChangedEventArgs<Border> e) {
            base.OnElementChanged(e);
            if (e.NewElement != null) {
                SetNativeControl(new WC.Border());
                //UpdateControl();
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e) {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == "Content") {
                PackChild();
            }
            switch (e.PropertyName) {
                case "Content":
                    this.PackChild();
                    break;
                case "Stroke":
                    this.SetBorderBrush();
                    break;
                case "StrokeThickness":
                    this.SetThickness();
                    break;
                case "CornerRadius":
                    this.SetCornerRadius();
                    break;
                case "Padding":
                    this.SetPadding();
                    break;
            }

        }

        // the base class is setting the background to the renderer when Control is null
        protected override void UpdateBackgroundColor() {
            if (this.Control != null) {
                this.Control.Background = (this.Element.BackgroundColor != XF.Color.Default ?
                                                this.Element.BackgroundColor.ToBrush() :
                                                base.Background);
            }
        }

        private void PackChild() {
            if (Element.Content == null)
                return;

            if (Element.Content.GetOrCreateRenderer() == null) {
                Platform.SetRenderer(Element.Content, Platform.GetRenderer(Element.Content));
            }
            var render = Platform.GetRenderer(Element.Content) as WX.UIElement;
            Control.Child = render;
        }

        private void SetCornerRadius() {
            var c = this.Element.CornerRadius;
            this.Control.CornerRadius = new WX.CornerRadius(c.TopLeft, c.TopRight, c.BottomRight, c.BottomLeft);
        }

        private void SetBorderBrush() {
            this.Control.BorderBrush = this.Element.Stroke.ToBrush();
        }

        private void SetThickness() {
            this.Control.BorderThickness = this.Element.StrokeThickness.ToWinPhone();
        }

        private void SetPadding() {
            this.Control.Padding = this.Element.Padding.ToWinPhone();
        }


        protected override void UpdateNativeControl() {
            base.UpdateNativeControl();

            if (this.Control != null) {
                this.SetPadding();
                this.SetBorderBrush();
                this.SetThickness();
                this.SetCornerRadius();
            }
        }

        protected override void Dispose(bool disposing) {
            base.Dispose(disposing);
        }
    }
}
