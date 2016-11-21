using AsNum.XFControls;
using AsNum.XFControls.UWP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WC = Windows.UI.Xaml.Controls;
using Xamarin.Forms.Platform.UWP;
using System.Collections.Specialized;
using Xamarin.Forms;

[assembly: ExportRenderer(typeof(Flip), typeof(FlipRender))]
namespace AsNum.XFControls.UWP {
    public class FlipRender : ViewRenderer<Flip, WC.FlipView> {


        //https://developer.microsoft.com/en-us/windows/uwp-community-toolkit/controls/rotatortile

        protected override void OnElementChanged(ElementChangedEventArgs<Flip> e) {
            if (e.OldElement != null) {
                e.OldElement.Children.CollectionChanged -= this.Children_CollectionChanged;
                e.OldElement.NextRequired -= this.Element_NextRequired;
                e.OldElement.IndexRequired -= this.Element_IndexRequired;
            }

            if (e.NewElement == null)
                return;

            var ctl = new WC.FlipView();
            foreach (var c in this.Element.Children) {
                var render = Platform.CreateRenderer(c);
                ctl.Items.Add(render);
            }
            this.SetNativeControl(ctl);

            this.Element.NextRequired += Element_NextRequired;
            this.Element.IndexRequired += Element_IndexRequired;
            this.Element.Children.CollectionChanged += Children_CollectionChanged;
        }

        private void Element_IndexRequired(object sender, Flip.IndexRequestEventArgs e) {
            Device.BeginInvokeOnMainThread(() => {
                this.Control.SelectedIndex = e.Index >= this.Control.Items.Count ? 0 : e.Index;
            });
        }

        private void Element_NextRequired(object sender, EventArgs e) {
            Device.BeginInvokeOnMainThread(() => {
                var i = this.Control.SelectedIndex + 1;
                this.Control.SelectedIndex = i >= this.Control.Items.Count ? 0 : i;
            });
        }

        private void Children_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e) {

        }

        //private IEnumerable<object> GetChildrenViews() {
        //    if (this.Element != null)
        //        foreach (var v in this.Element.Children) {
        //            var render = v.GetOrCreateRenderer();// Platform.CreateRenderer(v);// RendererFactory.GetRenderer(v);
        //            yield return render;
        //        }
        //}
    }
}
