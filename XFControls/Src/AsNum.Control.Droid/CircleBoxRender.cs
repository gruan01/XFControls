using Android.Graphics;
using Android.OS;
using AsNum.XFControls;
using AsNum.XFControls.Droid;
using Java.Lang;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CircleBox), typeof(CircleBoxRender))]
namespace AsNum.XFControls.Droid {
    public class CircleBoxRender : VisualElementRenderer<CircleBox> {

        private static bool Flag;

        //private static Paint Paint;

        private Android.Graphics.Color BgColor;

        static CircleBoxRender() {
            //clipPath with hardware acceleration is only supported in API level 18 and higher
            Flag = (int)Build.VERSION.SdkInt >= 18;
            //if (!Flag) {
            //    Paint = new Paint(PaintFlags.AntiAlias | PaintFlags.FilterBitmap);
            //}
        }



        protected override void OnElementChanged(ElementChangedEventArgs<CircleBox> e) {
            base.OnElementChanged(e);
            this.Element.HorizontalOptions = LayoutOptions.Center;
            this.Element.VerticalOptions = LayoutOptions.Center;

            if (this.Element.Content != null) {
                this.Element.Content.HorizontalOptions = LayoutOptions.Center;
                this.Element.Content.VerticalOptions = LayoutOptions.Center;

                //if (!Flag) {
                //    this.BgColor = this.Element.BackgroundColor.ToAndroid();
                //    this.Element.BackgroundColor = Xamarin.Forms.Color.Transparent;
                //}
            }


            //这种方法虽然可以解决 ClipPath 的问题，但是锯齿明显
            //if (!Flag)
            //    this.SetLayerType(Android.Views.LayerType.Software, Paint);

        }

        protected override void UpdateBackgroundColor() {
            if (!Flag && !this.Element.BackgroundColor.Equals(Xamarin.Forms.Color.Transparent)) {
                this.BgColor = this.Element.BackgroundColor.ToAndroid();
                this.Element.BackgroundColor = Xamarin.Forms.Color.Transparent;
            }
            base.UpdateBackgroundColor();
        }

        public override void Draw(Canvas canvas) {
            if (Flag) {
                //抗锯齿
                canvas.DrawFilter = new PaintFlagsDrawFilter(PaintFlags.AntiAlias | PaintFlags.FilterBitmap, PaintFlags.AntiAlias | PaintFlags.FilterBitmap);
                var density = this.Context.Resources.DisplayMetrics.Density;
                var path = new Path();
                path.AddCircle(canvas.Width / 2, canvas.Height / 2, (float)this.Element.Radius * density, Path.Direction.Ccw);
                canvas.ClipPath(path, Region.Op.Intersect);
                canvas.DrawColor(this.Element.BackgroundColor.ToAndroid());
                path.Dispose();
            }
            else {
                var bmp = this.GetClip(canvas.Width, canvas.Height, this.BgColor);
                var paint = new Paint(PaintFlags.AntiAlias);
                canvas.DrawBitmap(bmp, 0, 0, paint);
                paint.Dispose();
                bmp.Dispose();
            }
            base.Draw(canvas);
        }

        /// <summary>
        /// http://stackoverflow.com/questions/16889815/canvas-clippath-only-works-on-android-emulator
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        private Bitmap GetClip(int width, int height, Android.Graphics.Color color) {
            Bitmap output = Bitmap.CreateBitmap(width, height, Bitmap.Config.Argb8888);
            Canvas canvas = new Canvas(output);
            var paint = new Paint();
            var rect = new Rect(0, 0, width, height);
            paint.Color = color;
            paint.SetStyle(Paint.Style.Fill);
            paint.AntiAlias = true;
            canvas.DrawARGB(0, 0, 0, 0);
            //paint.setColor(color);
            canvas.DrawCircle(
                            width / 2,
                            height / 2,
                            Math.Max(width, height) / 2,
                            paint);
            // change the parameters accordin to your needs.
            paint.SetXfermode(new PorterDuffXfermode(PorterDuff.Mode.SrcIn));
            //canvas.DrawBitmap(bitmap, rect, rect, paint);
            return output;
        }
    }

}