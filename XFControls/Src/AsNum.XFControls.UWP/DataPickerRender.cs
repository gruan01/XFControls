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


[assembly: ExportRenderer(typeof(AC.DataPicker), typeof(DataPickerRender))]
namespace AsNum.XFControls.UWP {
    public class DataPickerRender : ViewRenderer<AC.DataPicker, DataPicker> {

        protected override void OnElementChanged(ElementChangedEventArgs<AC.DataPicker> e) {
            base.OnElementChanged(e);

            var ctl = new DataPicker();
            ctl.ItemsSource = this.Element.ItemsSource;
            ctl.FontSize = this.Element.FontSize;
            ctl.TextColor = this.Element.TextColor.ToMediaColor();
            ctl.DividerColor = this.Element.DividerColor.ToMediaColor();
            this.SetNativeControl(ctl);
        }

    }
}
