using Android.Util;
using AsNum.XFControls;
using AsNum.XFControls.Droid;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(TimePickerEx), typeof(TimePickerExRender))]
namespace AsNum.XFControls.Droid {
    public class TimePickerExRender : TimePickerRenderer {

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.TimePicker> e) {
            base.OnElementChanged(e);

            this.UpdateTextColor();
            this.UpdatePlaceHolder();
            this.UpdateFont();
            this.UpdateAlignment();

            //this.Control.SetPadding(0, 20, 0, 20);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e) {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName.Equals(TimePickerEx.TextColorProperty.PropertyName)) {
                this.UpdateTextColor();
            }
            else if (e.PropertyName.Equals(TimePickerEx.FontSizeProperty)) {
                this.UpdateFont();
            }
            else if (e.PropertyName.Equals(TimePickerEx.HorizontalTextAlignmentProperty)) {
                this.UpdateAlignment();
            }
        }


        private void UpdateTextColor() {
            var ele = (TimePickerEx)this.Element;
            this.Control.SetTextColor(ele.TextColor.ToAndroid());
        }

        private void UpdatePlaceHolder() {
            var ele = (TimePickerEx)this.Element;
            this.Control.Hint = ele.PlaceHolder ?? "";
            this.Control.SetHintTextColor(ele.PlaceHolderColor.ToAndroid());
        }

        private void UpdateFont() {
            this.Control.SetTextSize(ComplexUnitType.Sp, (float)((TimePickerEx)this.Element).FontSize);
        }

        private void UpdateAlignment() {
            this.Control.Gravity =
                ((TimePickerEx)this.Element).HorizontalTextAlignment.ToHorizontalGravityFlags();
        }
    }
}