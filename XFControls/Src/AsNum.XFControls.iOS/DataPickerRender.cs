using AsNum.XFControls;
using AsNum.XFControls.iOS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

///http://www.gooorack.com/2013/07/18/xamarin-uipickerview-as-a-combobox/
[assembly: ExportRenderer(typeof(DataPicker), typeof(DataPickerRender))]
namespace AsNum.XFControls.iOS {
    public class DataPickerRender : ViewRenderer<DataPicker, UIPickerView> {

        protected override void OnElementChanged(ElementChangedEventArgs<DataPicker> e) {
            base.OnElementChanged(e);

            if (e.NewElement != null) {
                var picker = new UIPickerView();
                picker.ShowSelectionIndicator = true;
                this.SetNativeControl(picker);
                this.UpdatePicker();
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e) {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName.Equals(DataPicker.ItemsSourceProperty.PropertyName)) {
                this.UpdatePicker();
            }
        }

        public void UpdatePicker() {
            if (this.Element.ItemsSource != null) {
                var model = new DataPickerModel(this.Element.StringValues);
                model.PickerChanged += Model_PickerChanged;
                this.Control.Model = model;

                if (this.Element.SelectedIndex >= 0)
                    model.Selected(this.Control, this.Element.SelectedIndex, 0);
                else
                    model.Selected(this.Control, 0, 0);
            }
        }

        private void Model_PickerChanged(object sender, PickerChangedEventArgs e) {
            this.UpdateSelectedItem(e.SelectedIndex);
        }

        private void UpdateSelectedItem(int idx) {
            this.Element.SelectedItem = this.Element.ItemsSource.Cast<object>().ElementAt(idx);
        }
    }

    public class DataPickerModel : UIPickerViewModel {
        public IList<string> values;

        public event EventHandler<PickerChangedEventArgs> PickerChanged;

        public DataPickerModel(IList<string> values) {
            this.values = values;
        }

        public override nint GetComponentCount(UIPickerView picker) {
            return 1;
        }

        public override nint GetRowsInComponent(UIPickerView pickerView, nint component) {
            return values.Count;
        }


        public override string GetTitle(UIPickerView pickerView, nint row, nint component) {
            return values[(int)row].ToString();
        }


        public override nfloat GetRowHeight(UIPickerView pickerView, nint component) {
            return 40f;
        }


        public override void Selected(UIPickerView pickerView, nint row, nint component) {
            if (this.PickerChanged != null) {
                this.PickerChanged(this, new PickerChangedEventArgs { SelectedIndex = (int)row });
            }
        }
    }

    public class PickerChangedEventArgs : EventArgs {
        public int SelectedIndex { get; set; }
    }
}
