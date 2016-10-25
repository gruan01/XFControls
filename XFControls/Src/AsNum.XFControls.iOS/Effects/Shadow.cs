using AsNum.XFControls.Effects;
using AsNum.XFControls.iOS.Effects;
using CoreGraphics;
using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;


[assembly: ResolutionGroupName("AsNum")]
[assembly: ExportEffect(typeof(ShadowEffect), "ShadowEffect")]
namespace AsNum.XFControls.iOS.Effects {


    public class ShadowEffect : PlatformEffect {
        private CGSize _originalOffset;
        private CGColor _originalColor;
        private float _originalOpacity;
        private nfloat _originalRadius;

        protected override void OnAttached() {
            if (this.Container == null)
                return;
            this._originalOffset = this.Container.Layer.ShadowOffset;
            this._originalColor = this.Container.Layer.ShadowColor;
            this._originalOpacity = this.Container.Layer.ShadowOpacity;
            this._originalRadius = this.Container.Layer.ShadowRadius;
            this.UpdateShadow();
        }

        protected override void OnDetached() {
            if (this.Container == null)
                return;
            this.Container.Layer.ShadowColor = this._originalColor;
            this.Container.Layer.ShadowOffset = this._originalOffset;
            this.Container.Layer.ShadowOpacity = this._originalOpacity;
            this.Container.Layer.ShadowRadius = this._originalRadius;
        }

        protected override void OnElementPropertyChanged(PropertyChangedEventArgs e) {
            base.OnElementPropertyChanged(e);
            if (e.PropertyName.Equals(Shadow.RadiusProperty.PropertyName)
                || e.PropertyName.Equals(Shadow.ColorProperty.PropertyName)
                || e.PropertyName.Equals(Shadow.XProperty)
                || e.PropertyName.Equals(Shadow.YProperty)) {
                this.UpdateShadow();
            }
        }

        private void UpdateShadow() {
            this.Container.Layer.ShadowOpacity = 1f;

            var radius = Shadow.GetRadius(this.Element);
            var x = Shadow.GetX(this.Element);
            var y = Shadow.GetY(this.Element);
            var color = Shadow.GetColor(this.Element);

            this.Container.Layer.ShadowRadius = radius;
            this.Container.Layer.ShadowColor = ColorExtensions.ToCGColor(color);
            this.Container.Layer.ShadowOffset = new CGSize(x, y);
        }
    }
}
