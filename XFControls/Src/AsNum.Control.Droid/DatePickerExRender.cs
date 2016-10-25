using Android.Util;
using AsNum.XFControls;
using AsNum.XFControls.Droid;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(DatePickerEx), typeof(DatePickerExRender))]
//[assembly: ExportRenderer(typeof(Xamarin.Forms.DatePicker), typeof(DatePickerExRender))]
namespace AsNum.XFControls.Droid {
    public class DatePickerExRender : DatePickerRenderer {

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.DatePicker> e) {
            base.OnElementChanged(e);

            this.UpdateTextColor();
            this.UpdatePlaceHolder();
            this.UpdateFont();
            this.UpdateAlignment();

            //this.Control.SetPadding(0, 20, 0, 20);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e) {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName.Equals(DatePickerEx.TextColorProperty.PropertyName)) {
                this.UpdateTextColor();
            }
            else if (e.PropertyName.Equals(DatePickerEx.FontSizeProperty)) {
                this.UpdateFont();
            }
            else if (e.PropertyName.Equals(DatePickerEx.HorizontalTextAlignmentProperty)) {
                this.UpdateAlignment();
            }
        }


        private void UpdateTextColor() {
            var ele = (DatePickerEx)this.Element;
            this.Control.SetTextColor(ele.TextColor.ToAndroid());
        }

        private void UpdatePlaceHolder() {
            var ele = (DatePickerEx)this.Element;
            this.Control.Hint = ele.PlaceHolder ?? "";
            this.Control.SetHintTextColor(ele.PlaceHolderColor.ToAndroid());
        }

        private void UpdateFont() {
            this.Control.SetTextSize(ComplexUnitType.Sp, (float)((DatePickerEx)this.Element).FontSize);
        }

        private void UpdateAlignment() {
            this.Control.Gravity = ((DatePickerEx)this.Element).HorizontalTextAlignment.ToHorizontalGravityFlags();
        }
    }
}