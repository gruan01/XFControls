using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms.Platform.Android;
using AW = Android.Widget;
using Xamarin.Forms;
using AsNum.XFControls;
using AsNum.XFControls.Droid;
using System.ComponentModel;

[assembly: ExportRenderer(typeof(Overlay), typeof(OverlayRender))]
namespace AsNum.XFControls.Droid {
    public class OverlayRender : VisualElementRenderer<Overlay> {

        private PopupWindow Pop = null;
        private AW.FrameLayout Frame = null;
        private IVisualElementRenderer Render = null;

        protected override void OnElementChanged(ElementChangedEventArgs<Overlay> e) {
            base.OnElementChanged(e);

            if (e.NewElement != null) {
                this.Pop = new PopupWindow(this.Context);
                this.Pop.Height = ViewGroup.LayoutParams.MatchParent;
                this.Pop.Width = ViewGroup.LayoutParams.MatchParent;

                this.Frame = new AW.FrameLayout(this.Context);
                this.Frame.SetBackgroundColor(this.Element.MaskColor.ToAndroid());


                this.Pop.ContentView = this.Frame;
                this.Pop.Focusable = true;

                this.UpdateVisible();
                this.UpdateContent();
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e) {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName.Equals(Xamarin.Forms.View.IsVisibleProperty.PropertyName)) {
                this.UpdateVisible();
            } else if (e.PropertyName.Equals(AsNum.XFControls.Overlay.IsVisibleProperty.PropertyName)) {
                this.UpdateContent();
            }
        }

        private void UpdateVisible() {
            if (this.Element.IsVisible) {
                this.Pop.ShowAsDropDown(this.Frame, 0, 0);
                this.UpdateElementLayout();
            } else {
                this.Pop.Dismiss();
            }
        }

        private void UpdateContent() {
            if (this.Element.Content != null) {
                this.Render = Platform.CreateRenderer(this.Element.Content);
                this.Element.Content.Parent = this.Element;
                this.Frame.AddView(this.Render.ViewGroup, LayoutParams.MatchParent, LayoutParams.MatchParent);
            }
        }

        private void UpdateElementLayout() {
            this.Render.UpdateLayout();
            var size = this.Render.Element.GetSizeRequest(2000, 2000);
            this.Render.Element.Layout(new Rectangle(0, 0, size.Request.Width, size.Request.Height));
        }
    }
}