using Android.Graphics.Drawables;
using Android.Support.V4.Graphics.Drawable;
using AsNum.XFControls;
using AsNum.XFControls.Droid;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using AG = Android.Graphics;
using AW = Android.Widget;

[assembly: ExportRenderer(typeof(RatingBar), typeof(RatingBarRender))]
namespace AsNum.XFControls.Droid {
    public class RatingBarRender : ViewRenderer<RatingBar, AW.LinearLayout> {

        private bool IsDisposed = false;

        private AW.RatingBar RB = null;

        protected override void OnElementChanged(ElementChangedEventArgs<RatingBar> e) {
            base.OnElementChanged(e);

            if (this.RB != null) {
                this.RB.RatingBarChange -= this.RB_RatingBarChange;
            }

            this.RB = new AW.RatingBar(Forms.Context) {
                //放到Update 中，会导致应用挂起
                //也就是说，运行中，不能修改 Step
                StepSize = this.Element.Step
            };
            this.RB.RatingBarChange += RB_RatingBarChange;

            var liner = new AW.LinearLayout(Forms.Context);

            this.SetNativeControl(liner);
            this.Control.AddView(this.RB);

            this.RB.RatingBarChange += Control_RatingBarChange;
            this.Update();
        }

        private void RB_RatingBarChange(object sender, AW.RatingBar.RatingBarChangeEventArgs e) {
            this.Element.Rate = e.Rating;
        }

        private void Control_RatingBarChange(object sender, AW.RatingBar.RatingBarChangeEventArgs e) {
            this.Element.Rate = e.Rating;
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e) {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName.Equals(RatingBar.IsIndicatorProperty.PropertyName) ||
                e.PropertyName.Equals(RatingBar.StarCountProperty.PropertyName) ||
                e.PropertyName.Equals(RatingBar.RateProperty.PropertyName) ||
                e.PropertyName.Equals(RatingBar.StepProperty.PropertyName) ||
                e.PropertyName.Equals(RatingBar.SelectedColorProperty.PropertyName) ||
                e.PropertyName.Equals(RatingBar.UnSelectedColorProperty.PropertyName)
                ) {

                this.Update();
            }
        }


        protected void Update() {
            this.RB.IsIndicator = this.Element.IsIndicator;
            this.RB.NumStars = this.Element.StarCount;
            this.RB.Rating = this.Element.Rate;

            AG.Color? c1 = null;
            if (!this.Element.SelectedColor.Equals(Color.Default))
                c1 = this.Element.SelectedColor.ToAndroid();
            AG.Color? c2 = null;
            if (!this.Element.UnSelectedColor.Equals(Color.Default))
                c2 = this.Element.UnSelectedColor.ToAndroid();

            this.SetColor(c1, c2);

            //this.RB.Max = this.Element.StarCount;
            //this.Control.StepSize = 0.5F;
            //this.Control.SecondaryProgress = this.Element.StarCount;
        }


        private Drawable Wrap(Drawable drb, int tint) {
            var compat = DrawableCompat.Wrap(drb);
            DrawableCompat.SetTint(compat, tint);
            return compat;
        }

        /// <summary>
        /// http://stackoverflow.com/questions/2446270/android-ratingbar-change-star-colors
        /// </summary>
        /// <param name="fColor"></param>
        /// <param name="bColor"></param>
        private void SetColor(AG.Color? fColor, AG.Color? bColor) {

            if (fColor == null && bColor == null)
                return;

            if (this.RB.ProgressDrawable is LayerDrawable) {
                var pDrb = (LayerDrawable)this.RB.ProgressDrawable;

                var drb0 = pDrb.GetDrawable(0);
                var drb1 = pDrb.GetDrawable(1);
                var drb2 = pDrb.GetDrawable(2);

                var drawables = new Drawable[3];
                //Foreground
                drawables[2] = fColor.HasValue ? this.Wrap(drb2, fColor.Value) : drb2;

                drawables[1] = drb1;

                //Background
                drawables[0] = bColor.HasValue ? this.Wrap(drb0, bColor.Value) : drb0;

                var layerDrawable = new LayerDrawable(drawables);

                this.RB.ProgressDrawable = layerDrawable;
            } else {
                if (fColor.HasValue) {
                    var progressDrawable = this.RB.ProgressDrawable;
                    var compat = DrawableCompat.Wrap(progressDrawable);
                    DrawableCompat.SetTint(compat, fColor.Value);
                    this.RB.ProgressDrawable = compat;
                }
            }
        }

        protected override void Dispose(bool disposing) {
            if (disposing && !this.IsDisposed) {
                if (this.RB != null) {
                    this.RB.Dispose();
                    this.RB = null;
                }
            }
            base.Dispose(disposing);
        }
    }
}