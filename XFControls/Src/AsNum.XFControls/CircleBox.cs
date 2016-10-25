using System;
using Xamarin.Forms;

namespace AsNum.XFControls {
    /// <summary>
    /// 园形BOX
    /// </summary>
    public class CircleBox : ContentView {

        /// <summary>
        /// 半径,默认 40
        /// </summary>
        public static readonly BindableProperty RadiusProperty =
            BindableProperty.Create(
                "Radius",
                typeof(double),
                typeof(CircleBox),
                40d
                );

        /// <summary>
        /// 半径,默认 40
        /// </summary>
        public double Radius {
            get {
                return (Double)base.GetValue(RadiusProperty);
            }
            set {
                base.SetValue(RadiusProperty, value);
            }
        }


        public CircleBox() {
            this.HorizontalOptions = LayoutOptions.Center;
            this.VerticalOptions = LayoutOptions.Center;
        }

        protected override SizeRequest OnMeasure(double widthConstraint, double heightConstraint) {
            //设置高宽为半径的2倍
            var w = this.Radius * 2;
            return new SizeRequest(new Size(w, w));
        }


        //protected override SizeRequest OnSizeRequest(double widthConstraint, double heightConstraint) {
        //    return base.OnSizeRequest(widthConstraint, heightConstraint);
        //    var w = this.Radius * 2;
        //    return new SizeRequest(new Size(w, w));
        //}
    }
}
