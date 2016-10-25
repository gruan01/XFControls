using Android.Animation;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Views;
using Android.Views.Animations;
using AsNum.XFControls.Droid.Effects;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;


//https://github.com/siriscac/RippleView/blob/master/RippleView/src/com/indris/material/RippleView.java
[assembly: ExportEffect(typeof(TapEffect), "TapEffect")]
namespace AsNum.XFControls.Droid.Effects {
    public class TapEffect : PlatformEffect {
        private RippleDrawable Drb;

        private ValueAnimator Anim;

        protected override void OnAttached() {

            var radius = 100;// Math.Max(this.Container.Width, this.Container.Height);

            this.Drb = new RippleDrawable(this.Container.Background);
            this.Container.Background = this.Drb;

            this.Container.Touch += Container_Touch;

            this.Anim = ObjectAnimator.OfInt(1, radius);
            this.Anim.Update += (s, arg) => this.Drb.Radius = (int)arg.Animation.AnimatedValue;
            this.Anim.SetInterpolator(new AccelerateDecelerateInterpolator());
            this.Anim.SetDuration(300);
        }

        private void Container_Touch(object sender, Android.Views.View.TouchEventArgs e) {
            if (!this.Container.Background.Equals(this.Drb))
                this.Container.Background = this.Drb;

            this.Drb.X = e.Event.GetX();
            this.Drb.Y = e.Event.GetY();

            switch (e.Event.Action) {
                case MotionEventActions.Down:
                    this.Anim.Cancel();
                    this.Anim.Start();
                    break;
                case MotionEventActions.Move:
                    //this.Drb.InvalidateSelf();
                    break;
                default:
                    this.Anim.Cancel();
                    this.Anim.Reverse();
                    break;
            }

            e.Handled = false;
        }

        protected override void OnDetached() {
            if (this.Container != null)
                //Cannot access a disposed object
                try {
                    this.Container.Touch -= Container_Touch;
                }
                catch { }

            if (this.Drb != null)
                this.Drb.Dispose();
            if (this.Anim != null)
                this.Anim.Dispose();
        }


        class RippleDrawable : Android.Support.V7.Graphics.Drawable.DrawableWrapper {
            public float X { get; set; }

            public float Y { get; set; }

            private Path Path = new Path();

            private Paint Paint;

            private int _radius = 0;
            public int Radius {
                get {
                    return this._radius;
                }
                set {
                    this._radius = value;
					//TODO 优化
                    var rg = new RadialGradient(this.X,
                        this.Y,
                        value,
                        Android.Graphics.Color.White,
                        Android.Graphics.Color.Transparent,
                        Shader.TileMode.Mirror
                        );

                    this.Paint.SetShader(rg);
                    this.InvalidateSelf();
                }
            }



            public override int Opacity {
                get {
                    return 0;
                }
            }

            private EmptyDrawable DefaultDrb = new EmptyDrawable();

            public RippleDrawable(Drawable drb) : base(drb) {
                if (this.WrappedDrawable == null)
                    this.WrappedDrawable = new EmptyDrawable();


                this.Paint = new Paint(PaintFlags.AntiAlias);
                this.Paint.Alpha = 100;
            }

            public override void Draw(Canvas canvas) {

                if (WrappedDrawable != null) {
                    this.WrappedDrawable.Draw(canvas);
                }

                canvas.Save(SaveFlags.Clip);
                this.Path.Reset();
                this.Path.AddCircle(this.X, this.Y, this._radius, Path.Direction.Cw);
                canvas.ClipPath(this.Path);
                canvas.Restore();

                canvas.DrawCircle(this.X, this.Y, this._radius, this.Paint);
            }

            public override void SetAlpha(int alpha) {
                this.Paint.Alpha = alpha;
            }

            public override void SetColorFilter(ColorFilter colorFilter) {
            }

            protected override void Dispose(bool disposing) {
                base.Dispose(disposing);

                if (this.Paint != null)
                    this.Paint.Dispose();
                if (this.Path != null)
                    this.Path.Dispose();
                //if (this.WrappedDrawable != null)
                //    this.WrappedDrawable.Dispose();
            }
        }

        class EmptyDrawable : Drawable {
            public override int Opacity {
                get {
                    return 0;
                }
            }

            public override void Draw(Canvas canvas) {

            }

            public override void SetAlpha(int alpha) {

            }

            public override void SetColorFilter(ColorFilter colorFilter) {

            }
        }
    }
}