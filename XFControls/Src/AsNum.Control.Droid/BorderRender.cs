using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using AsNum.XFControls;
using AsNum.XFControls.Droid;
using System.ComponentModel;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Border), typeof(BorderRender))]
namespace AsNum.XFControls.Droid {
    public class BorderRender : VisualElementRenderer<Border> {

        private bool IsDisposed = false;

        private GradientDrawable Dab;
        private InsetDrawable InsetDab;
        private Path ClipPath;

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e) {
            base.OnElementPropertyChanged(sender, e);
            this.UpdateBackground(this);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Border> e) {
            base.OnElementChanged(e);
            this.UpdateBackground(this);
        }

        protected override void DispatchDraw(Canvas canvas) {
            if (Element.IsClippedToBorder) {
                canvas.Save(SaveFlags.Clip);
                this.SetClipPath(canvas);
                base.DispatchDraw(canvas);
                canvas.Restore();
            }
            else {
                base.DispatchDraw(canvas);
            }
        }


        //protected override void UpdateBackgroundColor() {
        //    //base.UpdateBackgroundColor();
        //}


        private void UpdateBackground(Android.Views.View view) {
            var border = this.Element;
            var stroke = border.StrokeThickness;
            var corner = border.CornerRadius;
            var padding = border.Padding;

            var context = view.Context;

            if (this.Dab == null) {
                this.Dab = new GradientDrawable();
                this.Dab.SetShape(ShapeType.Rectangle);
            }
            var maxWidth = (int)context.ToPixels(Max(stroke));

            if (maxWidth > 0) {
                this.Dab.SetStroke(maxWidth, border.Stroke.ToAndroid(), 0, 0);
            }

            if ((int)Build.VERSION.SdkInt < 21) {
                //Android 4.4 SetCornerRadii »áºÚÒ»Æ¬
                this.Dab.SetCornerRadius(context.ToPixels(Max(corner)));
            }
            else {
                var ctl = context.ToPixels(corner.TopLeft);
                var ctr = context.ToPixels(corner.TopRight);
                var cbr = context.ToPixels(corner.BottomRight);
                var cbl = context.ToPixels(corner.BottomLeft);

                var corners = new float[] {
                    ctl, ctl,
                    ctr, ctr,
                    cbr, cbr,
                    cbl, cbl
                };

                this.Dab.SetCornerRadii(corners);
            }

            this.Dab.SetColor(border.BackgroundColor.ToAndroid());

            var left = -(int)(maxWidth - context.ToPixels(stroke.Left));
            var top = -(int)(maxWidth - context.ToPixels(stroke.Top));
            var right = -(int)(maxWidth - context.ToPixels(stroke.Right));
            var bottom = -(int)(maxWidth - context.ToPixels(stroke.Bottom));

            if (this.InsetDab == null) {
                this.InsetDab = new InsetDrawable(this.Dab, left, top, right, bottom);
            }

            view.Background = InsetDab;

            view.SetPadding(
                (int)context.ToPixels(stroke.Left + padding.Left),
                (int)context.ToPixels(stroke.Top + padding.Top),
                (int)context.ToPixels(stroke.Right + padding.Right),
                (int)context.ToPixels(stroke.Bottom + padding.Bottom));
        }

        private double Max(Thickness t) {
            return new double[] {
                t.Left,
                t.Top,
                t.Right,
                t.Bottom
            }.Max();
        }

        private double Max(CornerRadius t) {
            return new double[] {
                t.TopLeft,
                t.TopRight,
                t.BottomLeft,
                t.BottomRight
            }.Max();
        }

        private void SetClipPath(Canvas canvas) {
            var br = this;
            this.ClipPath = new Path();
            var corner = br.Element.CornerRadius;
            var tl = (float)corner.TopLeft;
            var tr = (float)corner.TopRight;
            var bbr = (float)corner.BottomRight;
            var bl = (float)corner.BottomLeft;

            //Array of 8 values, 4 pairs of [X,Y] radii
            float[] radius = new float[] {
                tl, tl, tr, tr, bbr, bbr, bl, bl
            };

            int w = (int)br.Width;
            int h = (int)br.Height;

            this.ClipPath.AddRoundRect(new RectF(
                 br.ViewGroup.PaddingLeft,
                 br.ViewGroup.PaddingTop,
                 w - br.ViewGroup.PaddingRight,
                 h - br.ViewGroup.PaddingBottom),
                 radius,
                 Path.Direction.Cw);

            canvas.ClipPath(this.ClipPath);
        }


        protected override void Dispose(bool disposing) {

            if (disposing && !this.IsDisposed) {
                this.IsDisposed = true;

                if (this.Dab != null) {
                    this.Dab.Dispose();
                    this.Dab = null;
                }
                if (this.InsetDab != null) {
                    this.InsetDab.Dispose();
                    this.InsetDab = null;
                }
                if (this.ClipPath != null) {
                    this.ClipPath.Dispose();
                    this.ClipPath = null;
                }
            }
            base.Dispose(disposing);
        }
    }
}