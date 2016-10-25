using AsNum.XFControls;
using AsNum.XFControls.iOS;
using CoreAnimation;
using CoreGraphics;
using System;
using System.Drawing;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CircleBox), typeof(CircleBoxRender))]
namespace AsNum.XFControls.iOS {

    public class CircleBoxRender : VisualElementRenderer<CircleBox> {
        protected override void OnElementChanged(ElementChangedEventArgs<CircleBox> e) {
            base.OnElementChanged(e);
            //this.SetLayout();
        }

        private CGColor BgColor;

        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
            base.OnElementPropertyChanged(sender, e);

            this.Element.HorizontalOptions = LayoutOptions.Center;
            this.Element.VerticalOptions = LayoutOptions.Center;

            if (this.Element.Content != null) {
                this.Element.Content.HorizontalOptions = LayoutOptions.Center;
                this.Element.Content.VerticalOptions = LayoutOptions.Center;

                this.UpdateCircle();
            }
        }



        private void UpdateCircle() {
            double width = ((VisualElement)this.Element).Width;
            double height = ((VisualElement)this.Element).Height;
            if (width <= 0.0 || height <= 0.0)
                return;
            double num = Math.Min(width, height);
            double x = width > num ? (width - num) / 2.0 : 0.0;
            double y = height > num ? (height - num) / 2.0 : 0.0;
            this.Layer.Mask = (CALayer)new CAShapeLayer() {
                Path = CGPath.EllipseFromRect(new CGRect(x, y, num, num))
            };
        }


        protected override void SetBackgroundColor(Color color) {
            //base.SetBackgroundColor(color);
            this.BgColor = this.Element.BackgroundColor.ToCGColor();
            base.SetBackgroundColor(Color.Transparent);
        }

        public override void Draw(CoreGraphics.CGRect rect) {
            //base.Draw(rect);

            var currentContext = UIGraphics.GetCurrentContext();
            var properRect = AdjustForThickness(rect);
            HandleShapeDraw(currentContext, properRect);
        }

        protected RectangleF AdjustForThickness(CGRect rect) {
            var x = rect.X + Element.Padding.Left;
            var y = rect.Y + Element.Padding.Top;
            var width = rect.Width - Element.Padding.HorizontalThickness;
            var height = rect.Height - Element.Padding.VerticalThickness;
            return new RectangleF((float)x, (float)y, (float)width, (float)height);
        }

        protected virtual void HandleShapeDraw(CGContext currentContext, RectangleF rect) {
            var centerX = rect.X + (rect.Width / 2);
            var centerY = rect.Y + (rect.Height / 2);
            var radius = rect.Width / 2;
            var startAngle = 0;
            var endAngle = (float)(Math.PI * 2);

            HandleStandardDraw(currentContext, rect, () => currentContext.AddArc(centerX, centerY, radius, startAngle, endAngle, true));
        }

        /// <summary>
        /// A simple method for handling our drawing of the shape. This method is called differently for each type of shape
        /// </summary>
        /// <param name="currentContext">Current context.</param>
        /// <param name="rect">Rect.</param>
        /// <param name="createPathForShape">Create path for shape.</param>
        protected virtual void HandleStandardDraw(CGContext currentContext, RectangleF rect, Action createPathForShape) {
            currentContext.SetFillColor(this.BgColor);
            createPathForShape();
            currentContext.DrawPath(CGPathDrawingMode.Fill);
        }
    }
}