using AsNum.XFControls;
using AsNum.XFControls.UWP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Xamarin.Forms.Platform.UWP;
using AC = AsNum.XFControls;
using System.ComponentModel;

[assembly: ExportRenderer(typeof(AC.DataPicker), typeof(DataPickerRender))]
namespace AsNum.XFControls.UWP {
    public class DataPickerRender : ViewRenderer<AC.DataPicker, DataPicker> {

        protected override void OnElementChanged(ElementChangedEventArgs<AC.DataPicker> e) {
            base.OnElementChanged(e);

            if (this.Control != null) {
                this.Control.SelectedChanged -= this.Ctl_SelectedChanged;
            }

            var ctl = new DataPicker();
            ctl.FontSize = this.Element.FontSize;
            ctl.TextColor = this.Element.TextColor.ToMediaColor();
            ctl.DividerColor = this.Element.DividerColor.ToMediaColor();
            ctl.SelectedChanged += Ctl_SelectedChanged;

            this.SetNativeControl(ctl);
            this.UpdateItemsSource();
        }

        private void Ctl_SelectedChanged(object sender, DataPicker.SelectedChangedEventArgs e) {
            if (this.Element.ItemsSource != null)
                this.Element.SelectedItem = this.Element.ItemsSource.Cast<object>().ElementAt(e.SelectedIndex);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e) {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName.Equals(AC.DataPicker.ItemsSourceProperty.PropertyName)) {
                //this.Control.ItemsSource = this.Element.StringValues;
                this.UpdateItemsSource();
            } else if (e.PropertyName.Equals(AC.DataPicker.FontSizeProperty.PropertyName)) {
                this.Control.FontSize = this.Element.FontSize;
            } else if (e.PropertyName.Equals(AC.DataPicker.TextColorProperty.PropertyName)) {
                this.Control.TextColor = this.Element.TextColor.ToMediaColor();
            } else if (e.PropertyName.Equals(AC.DataPicker.DividerColorProperty.PropertyName)) {
                this.Control.DividerColor = this.Element.DividerColor.ToMediaColor();
            }
        }

        private void UpdateItemsSource() {
            this.Control.ItemsSource = this.Element.StringValues;
            this.Control.SelectedIndex = 0;
        }
    }
}
