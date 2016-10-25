using AsNum.XFControls;
using AsNum.XFControls.iOS;
using CoreAnimation;
using CoreGraphics;
using System;
using System.ComponentModel;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Border), typeof(BorderRender))]
namespace AsNum.XFControls.iOS {
    public class BorderRender : VisualElementRenderer<Border> {

        private CALayer[] borderLayers = new CALayer[4];

        protected override void OnElementChanged(ElementChangedEventArgs<Border> e) {
            base.OnElementChanged(e);
            if (e.NewElement != null) {
                this.SetupLayer();
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e) {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == VisualElement.BackgroundColorProperty.PropertyName ||
                e.PropertyName == Border.StrokeProperty.PropertyName ||
                e.PropertyName == Border.StrokeThicknessProperty.PropertyName ||
                e.PropertyName == Border.CornerRadiusProperty.PropertyName ||
                e.PropertyName == Border.WidthProperty.PropertyName ||
                e.PropertyName == Border.HeightProperty.PropertyName) {
                this.SetupLayer();
            }
        }

        private void SetupLayer() {
            if (Element == null || Element.Width <= 0 || Element.Height <= 0) {
                return;
            }

            

            Layer.CornerRadius = (nfloat)Element.CornerRadius.TopLeft;
            if (Element.BackgroundColor != Color.Default) {
                Layer.BackgroundColor = Element.BackgroundColor.ToCGColor();
            } else {
                Layer.BackgroundColor = UIColor.White.CGColor;
            }

            Layer.BorderColor = Element.Stroke.ToCGColor();
            Layer.MasksToBounds = true;

            UpdateBorderLayer(BorderPosition.Left, (nfloat)Element.StrokeThickness.Left);
            UpdateBorderLayer(BorderPosition.Top, (nfloat)Element.StrokeThickness.Top);
            UpdateBorderLayer(BorderPosition.Right, (nfloat)Element.StrokeThickness.Right);
            UpdateBorderLayer(BorderPosition.Bottom, (nfloat)Element.StrokeThickness.Bottom);

            Layer.RasterizationScale = UIScreen.MainScreen.Scale;
            Layer.ShouldRasterize = true;
            Layer.BorderColor = Element.Stroke.ToCGColor();
            Layer.BorderWidth = (float)Element.StrokeThickness.Left;
        }

        enum BorderPosition {
            Left,
            Top,
            Right,
            Bottom
        }

        void UpdateBorderLayer(BorderPosition borderPosition, nfloat thickness) {
            var borderLayer = borderLayers[(int)borderPosition];
            if (thickness <= 0) {
                if (borderLayer != null) {
                    borderLayer.RemoveFromSuperLayer();
                    borderLayers[(int)borderPosition] = null;
                }
            } else {
                if (borderLayer == null) {
                    borderLayer = new CALayer();
                    Layer.AddSublayer(borderLayer);
                    borderLayers[(int)borderPosition] = borderLayer;
                }

                switch (borderPosition) {
                    case BorderPosition.Left:
                        borderLayer.Frame = new CGRect(0, 0, thickness, (nfloat)Element.Height);
                        break;
                    case BorderPosition.Top:
                        borderLayer.Frame = new CGRect(0, 0, (nfloat)Element.Width, thickness);
                        break;
                    case BorderPosition.Right:
                        borderLayer.Frame = new CGRect((nfloat)Element.Width - thickness, 0, thickness, (nfloat)Element.Height);
                        break;
                    case BorderPosition.Bottom:
                        borderLayer.Frame = new CGRect(0, (nfloat)Element.Height - thickness, (nfloat)Element.Width, thickness);
                        break;
                }


                borderLayer.BackgroundColor = Element.Stroke.ToCGColor();
                borderLayer.CornerRadius = (nfloat)Element.CornerRadius.TopLeft;
            }
        }

    }
}