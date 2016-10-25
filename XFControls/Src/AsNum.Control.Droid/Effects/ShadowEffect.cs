using Android.Graphics;
using Android.OS;
using Android.Widget;
using AsNum.XFControls.Droid.Effects;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using A = AsNum.XFControls.Effects;

[assembly: ResolutionGroupName("AsNum")]
[assembly: ExportEffect(typeof(ShadowEffect), "ShadowEffect")]
namespace AsNum.XFControls.Droid.Effects {

    //https://developer.xamarin.com/guides/xamarin-forms/effects/passing-parameters/clr-properties/

    public class ShadowEffect : PlatformEffect {

        private Paint Paint;

        protected override void OnAttached() {
            this.Paint = new Paint();
            this.Update();
        }

        protected override void OnDetached() {
            if (this.Paint != null)
                this.Paint.Dispose();
        }

        protected override void OnElementPropertyChanged(PropertyChangedEventArgs e) {
            base.OnElementPropertyChanged(e);

            if (e.PropertyName.Equals(A.Shadow.RadiusProperty.PropertyName)
                || e.PropertyName.Equals(A.Shadow.ColorProperty.PropertyName)
                || e.PropertyName.Equals(A.Shadow.XProperty)
                || e.PropertyName.Equals(A.Shadow.YProperty)) {

                this.Update();
            }
        }

        private void Update() {
            var radius = Forms.Context.ToPixels(A.Shadow.GetRadius(this.Element));
            var x = Forms.Context.ToPixels(A.Shadow.GetX(this.Element));
            var y = Forms.Context.ToPixels(A.Shadow.GetY(this.Element));
            var color = A.Shadow.GetColor(this.Element);

            if (this.Control != null && this.Control is TextView) {
                ((TextView)Control).SetShadowLayer(radius, x, y, color.ToAndroid());
            } else if (this.Container != null) {
                if ((int)Build.VERSION.SdkInt >= 21)
                    this.Container.Elevation = radius;
                //else {
                //    this.Paint.SetShadowLayer(radius, x, y, color.ToAndroid());
                //    this.Container.SetLayerType(Android.Views.LayerType.Software, this.Paint);
                //    this.Container.SetLayerPaint(this.Paint);
                //}
            }
        }
    }
}