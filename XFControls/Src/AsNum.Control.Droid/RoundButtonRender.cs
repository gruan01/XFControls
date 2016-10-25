using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Graphics.Drawables;
using Android.Graphics.Drawables.Shapes;

namespace AsNum.XFControls.Droid {
    public class RoundButtonRender : ButtonRenderer {

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Button> e) {
            base.OnElementChanged(e);

            //var corners = new float[] {
            //        (float)this.Element.BorderRadius.CornerRadius.TopLeft,
            //        (float)border.CornerRadius.TopLeft,

            //        (float)border.CornerRadius.TopRight,
            //        (float)border.CornerRadius.TopRight,

            //        (float)border.CornerRadius.BottomRight,
            //        (float)border.CornerRadius.BottomRight,

            //        (float)border.CornerRadius.BottomLeft,
            //        (float)border.CornerRadius.BottomLeft
            //    };

            //var sharp = new RoundRectShape();
            var drb = new ShapeDrawable();
        }

    }
}