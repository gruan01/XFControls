using AsNum.XFControls;
using AsNum.XFControls.UWP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Xamarin.Forms.Platform.UWP;


[assembly: ExportRenderer(typeof(Flip), typeof(FlipRender))]
namespace AsNum.XFControls.UWP {
    public class FlipRender : ViewRenderer<Flip, FlipView> {


        //https://developer.microsoft.com/en-us/windows/uwp-community-toolkit/controls/rotatortile

        protected override void OnElementChanged(ElementChangedEventArgs<Flip> e) {
            if (e.NewElement == null)
                return;

            var ctl = new FlipView();
            foreach (var c in this.Element.Children) {
                var render = Platform.CreateRenderer(c);
                ctl.Items.Add(render);
            }
            this.SetNativeControl(ctl);
        }
    }
}
