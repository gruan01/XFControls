using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Util;
using Android.Views;
using Android.Widget;
using AsNum.XFControls;
using AsNum.XFControls.Droid;
using Java.Lang;
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
            picker.WrapSelectorWheel = false;
            picker.DescendantFocusability = DescendantFocusability.BlockDescendants;

            this.SetNativeControl(picker);
            this.Control.ValueChanged += Control_ValueChanged;
            this.UpdateApperance(this.Element.TextColor.ToAndroid(), Context.ToPixels(this.Element.FontSize));
            this.UpdateDividerColor(this.Element.DividerColor.ToAndroid());
            this.UpdateDatas();
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e) {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName.Equals(DataPicker.ItemsSourceProperty.PropertyName)) {
                this.UpdateDatas();

            } else if (e.PropertyName.Equals(DataPicker.FontSizeProperty.PropertyName) ||
                    e.PropertyName.Equals(DataPicker.TextColorProperty.PropertyName)) {

                this.UpdateApperance(this.Element.TextColor.ToAndroid(), Context.ToPixels(this.Element.FontSize));


            } else if (e.PropertyName.Equals(DataPicker.DividerColorProperty.PropertyName)) {

                this.UpdateDividerColor(this.Element.DividerColor.ToAndroid());
            }
        }


        private void UpdateDatas() {
            if (this.Element.ItemsSource != null) {
                var c = this.Element.ItemsSource.Cast<object>().Count() - 1;
                var cc = this.Control.MaxValue;

                if (c <= cc) {
                    this.Control.MaxValue = c;
                    this.Control.SetDisplayedValues(this.Element.StringValues.ToArray());
                } else {
                    this.Control.SetDisplayedValues(this.Element.StringValues.ToArray());
                    this.Control.MaxValue = c;
                }

                this.Control.MinValue = 0;

                this.Control.Value = this.Element.SelectedIndex;
                this.UpdateSelectedItem(this.Control.Value);
            }
        }


        private void Control_ValueChanged(object sender, NumberPicker.ValueChangeEventArgs e) {
            this.UpdateSelectedItem(e.NewVal);
        }

        private void UpdateSelectedItem(int idx) {
            this.Element.SelectedItem = this.Element.ItemsSource.Cast<object>().ElementAt(idx);
        }


        private void UpdateDividerColor(Android.Graphics.Color color) {
            try {
                var fld = this.Control.Class.GetDeclaredField("mSelectionDivider");
                fld.Accessible = true;

                var d = (Drawable)fld.Get(this.Control);
                d.SetColorFilter(color, PorterDuff.Mode.SrcAtop);
                d.InvalidateSelf();
                this.Control.PostInvalidate(); // Drawable is dirty

            } catch (Exception e) {

            }
        }


        /// <summary>
        /// http://stackoverflow.com/questions/22962075/change-the-text-color-of-numberpicker
        /// </summary>
        /// <param name="numberPicker"></param>
        /// <param name="txtColor"></param>
        /// <param name="complexUnitType"></param>
        /// <param name="textSize"></param>
        /// <param name="style"></param>
        /// <returns></returns>
        private void UpdateApperance(
            Android.Graphics.Color txtColor,
            float textSize
            ) {
            int count = this.Control.ChildCount;
            for (int i = 0; i < count; i++) {
                var child = this.Control.GetChildAt(i);
                if (child is EditText) {
                    try {
                        var fld = this.Control.Class.GetDeclaredField("mSelectorWheelPaint");
                        fld.Accessible = true;


                        var edt = (EditText)child;
                        edt.SetTextSize(ComplexUnitType.Px, textSize);
                        edt.SetTypeface(edt.Typeface, TypefaceStyle.Normal);
                        edt.SetTextColor(txtColor);

                        var paint = (Paint)fld.Get(this.Control);
                        paint.TextSize = TypedValue.ApplyDimension(ComplexUnitType.Px, textSize, this.Control.Resources.DisplayMetrics);
                        paint.Color = txtColor;
                        paint.SetTypeface(edt.Typeface);
                    } catch {

                    }
                }
            }
            this.Control.Invalidate();
        }
    }
}