using Android.Util;
using AsNum.XFControls;
using AsNum.XFControls.Droid;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(PickerEx), typeof(PickerExRender))]
namespace AsNum.XFControls.Droid {

    public class PickerExRender : Xamarin.Forms.Platform.Android.AppCompat.PickerRenderer {


        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e) {
            base.OnElementChanged(e);

            if (this.Element != null) {
                var ele = (PickerEx)this.Element;
                this.UpdateAlign();
                this.UpdateFont();
                this.UpdateSelected();
                this.UpdateDefaultIndex();

                //this.Control.SetPadding(0, 20, 0, 20);

                ((ObservableCollection<string>)e.NewElement.Items).CollectionChanged += PickerExRender_CollectionChanged;
            }
            if(e.OldElement != null) {
                ((ObservableCollection<string>)e.OldElement.Items).CollectionChanged -= PickerExRender_CollectionChanged;
            }
        }

        private void PickerExRender_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e) {
            this.UpdateDefaultIndex();
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e) {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName.Equals(Picker.SelectedIndexProperty.PropertyName)) {
                this.UpdateSelected();
            }
            else if (e.PropertyName.Equals(PickerEx.TitleProperty.PropertyName)) {
                this.UpdateTitle();
            }
            else if (e.PropertyName.Equals(PickerEx.HorizontalTextAlignmentProperty.PropertyName)) {
                this.UpdateAlign();
            }
            else if (e.PropertyName.Equals(PickerEx.FontSizeProperty.PropertyName) ||
               e.PropertyName.Equals(PickerEx.TextColorProperty.PropertyName)) {
                this.UpdateFont();
            }
            else if (e.PropertyName.Equals(PickerEx.DefaultIndexProperty.PropertyName)) {
                this.UpdateDefaultIndex();
            }
        }

        private void UpdateTitle() {
            this.Control.Hint = this.Element.Title;
        }

        private void UpdateAlign() {
            var ele = (PickerEx)this.Element;
            this.Control.Gravity =
                ele.HorizontalTextAlignment.ToHorizontalGravityFlags();
        }

        private void UpdateFont() {
            var ele = (PickerEx)this.Element;
            this.Control.SetTextColor(ele.TextColor.ToAndroid());
            this.Control.SetTextSize(ComplexUnitType.Sp, (float)ele.FontSize);
        }

        private void UpdateSelected() {
            var ele = (PickerEx)this.Element;
            if (ele.Items == null || ele.SelectedIndex < 0 || ele.SelectedIndex >= ele.Items.Count) {
                ele.SelectedItem = null;
            }
            else {
                ele.SelectedItem = ele.Items[ele.SelectedIndex];
            }
        }

        private void UpdateDefaultIndex() {
            var ele = (PickerEx)this.Element;
            if (this.Element.SelectedIndex == -1
                && this.Element.Items != null
                && ele.DefaultIndex >= 0
                && ele.DefaultIndex < ele.Items.Count) {

                ele.SelectedIndex = ele.DefaultIndex;

            }

        }
    }

}