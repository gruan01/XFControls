using Android.Widget;
using System;
using Xamarin.Forms.Platform.Android;

//[assembly: ExportEffect(typeof(ClipEffect), "ClipEffect")]
namespace AsNum.XFControls.Droid.Effects {

    [Obsolete("Î´Íê³É")]
    public class ClipEffect : PlatformEffect {
        protected override void OnAttached() {
            //this.Control.ClipBounds = new Android.Graphics.Rect(0, 0, 50, 0);
            var img = (ImageView)this.Control;
            img.Drawable.SetBounds(0, 0, 10, 0);
            img.Drawable.InvalidateSelf();
        }

        protected override void OnDetached() {

        }
    }
}