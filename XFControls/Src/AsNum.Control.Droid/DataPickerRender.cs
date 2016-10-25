using Android.Views;
using Android.Widget;
using AsNum.XFControls;
using AsNum.XFControls.Droid;
using System.ComponentModel;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(DataPicker), typeof(DataPickerRender))]
namespace AsNum.XFControls.Droid {
    public class DataPickerRender : ViewRenderer<DataPicker, NumberPicker> {

        protected override void OnElementChanged(ElementChangedEventArgs<DataPicker> e) {
            base.OnElementChanged(e);

            if (this.Control != null)
                this.Control.ValueChanged -= Control_ValueChanged;


            var picker = new NumberPicker(this.Context);
            //var picker = new ColorNumberPicker(this.Context, this.Element.TextColor.ToAndroid(), this.Element.DividerColor.ToAndroid());
            picker.WrapSelectorWheel = false;
            picker.DescendantFocusability = DescendantFocusability.BlockDescendants;


            this.SetNativeControl(picker);
            this.Control.ValueChanged += Control_ValueChanged;
            this.UpdatePicker();
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e) {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName.Equals(DataPicker.ItemsSourceProperty.PropertyName)) {
                this.UpdatePicker();
            }
        }


        private void UpdatePicker() {
            if (this.Element.ItemsSource != null) {
                var c = this.Element.ItemsSource.Cast<object>().Count() - 1;
                var cc = this.Control.MaxValue;

                if (c <= cc) {
                    this.Control.MaxValue = c;
                    this.Control.SetDisplayedValues(this.Element.StringValues.ToArray());
                }
                else {
                    this.Control.SetDisplayedValues(this.Element.StringValues.ToArray());
                    this.Control.MaxValue = c;
                }

                this.Control.MinValue = 0;

                this.Control.Value = this.Element.SelectedIndex;
                this.UpdateSelectedItem(this.Control.Value);
                //this.UpdatePickerColor();
            }
        }


        private void Control_ValueChanged(object sender, NumberPicker.ValueChangeEventArgs e) {
            this.UpdateSelectedItem(e.NewVal);
        }

        private void UpdateSelectedItem(int idx) {
            this.Element.SelectedItem = this.Element.ItemsSource.Cast<object>().ElementAt(idx);
        }

        //private void UpdatePickerColor() {
        //    for (var i = 0; i < this.Control.ChildCount; i++) {
        //        var c = this.Control.GetChildAt(i);
        //        if (c is EditText) {
        //            var edt = (EditText)c;
        //            edt.SetTextColor(this.Element.TextColor.ToAndroid());
        //        }
        //    }
        //}
    }
}