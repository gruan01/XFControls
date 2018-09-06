using AsNum.XFControls;
using AsNum.XFControls.iOS;
using CoreGraphics;
using System;
using System.ComponentModel;
using System.Drawing;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;


[assembly: ExportRenderer(typeof(MyFrame), typeof(FrameRender))]
namespace AsNum.XFControls.iOS
{
    public class FrameRender : VisualElementRenderer<Xamarin.Forms.Frame>
    {


        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Frame> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement == null)
                return;
            this.SetupLayer();
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (!(e.PropertyName == VisualElement.BackgroundColorProperty.PropertyName) && !(e.PropertyName == Xamarin.Forms.Frame.OutlineColorProperty.PropertyName) && !(e.PropertyName == Xamarin.Forms.Frame.HasShadowProperty.PropertyName))
                return;
            this.SetupLayer();
        }


        private void SetupLayer()
        {
            this.Layer.CornerRadius = (nfloat)2;

            if (this.Element.BackgroundColor == Color.Default)
                this.Layer.BackgroundColor = UIColor.White.CGColor;
            else
                this.Layer.BackgroundColor = ColorExtensions.ToCGColor(this.Element.BackgroundColor);

            if (this.Element.HasShadow)
            {
                this.Layer.ShadowRadius = (nfloat)2;
                this.Layer.ShadowColor = UIColor.Black.CGColor;
                this.Layer.ShadowOpacity = 0.5f;
                this.Layer.ShadowOffset = (CGSize)new SizeF();
            }
            else
                this.Layer.ShadowOpacity = 0.0f;

            if (this.Element.OutlineColor == Color.Default)
            {
                this.Layer.BorderColor = UIColor.Clear.CGColor;
            }
            else
            {
                this.Layer.BorderColor = ColorExtensions.ToCGColor(this.Element.OutlineColor);
                this.Layer.BorderWidth = (nfloat)1;
            }

            this.Layer.RasterizationScale = UIScreen.MainScreen.Scale;
            this.Layer.ShouldRasterize = true;
        }

    }
}
