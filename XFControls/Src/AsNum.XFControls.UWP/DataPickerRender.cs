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

            var ctl = new DataPicker();
            //ctl.ItemsSource = this.Element.ItemsSource;
            ctl.ItemsSource = this.Element.StringValues;
            ctl.FontSize = this.Element.FontSize;
            ctl.TextColor = this.Element.TextColor.ToMediaColor();
            ctl.DividerColor = this.Element.DividerColor.ToMediaColor();
            //ctl.DisplayMemberPath = this.Element.DisplayPath;
            this.SetNativeControl(ctl);
        }


        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e) {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName.Equals(AC.DataPicker.ItemsSourceProperty.PropertyName)) {
                this.Control.ItemsSource = this.Element.ItemsSource;
            }else if (e.PropertyName.Equals(AC.DataPicker.FontSizeProperty.PropertyName)) {
                this.Control.FontSize = this.Element.FontSize;
            } else if (e.PropertyName.Equals(AC.DataPicker.TextColorProperty.PropertyName)) {
                this.Control.TextColor = this.Element.TextColor.ToMediaColor();
            } else if (e.PropertyName.Equals(AC.DataPicker.DividerColorProperty.PropertyName)) {
                this.Control.DividerColor = this.Element.DividerColor.ToMediaColor();
            }
        }
    }
}
