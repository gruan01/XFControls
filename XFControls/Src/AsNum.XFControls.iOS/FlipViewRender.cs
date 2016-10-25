using AsNum.XFControls;
using AsNum.XFControls.iOS;
using System;
using System.Collections.Generic;
using System.Linq;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Flip), typeof(FlipViewRender))]
namespace AsNum.XFControls.iOS {
    public class FlipViewRender : ViewRenderer<Flip, FlipView> {

        protected override void OnElementChanged(ElementChangedEventArgs<Flip> e) {
            base.OnElementChanged(e);

            if (e.OldElement != null) {
                e.OldElement.NextRequired -= Element_NextRequired;
                e.OldElement.IndexRequired -= Element_IndexRequired;
                e.OldElement.Children.CollectionChanged -= Children_CollectionChanged;
            }

            if (e.NewElement != null) {
                var fv = new FlipView();
                var items = this.GetChildrenViews().ToList();
                fv.SetItems(items);

                this.SetNativeControl(fv);
                this.Control.SizeToFit();

                if (this.Element.ShowIndicator)
                    this.AddSubview(this.Control.PageControl);

                this.Element.NextRequired += Element_NextRequired;
                this.Element.IndexRequired += Element_IndexRequired;
                this.Element.Children.CollectionChanged += Children_CollectionChanged;
                this.Control.PosChanged += Fv_PosChanged;
            }
        }

        private void Children_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e) {
            this.Control.SetItems(this.GetChildrenViews().ToList());
        }

        private void Fv_PosChanged(object sender, FlipViewPosChangedEventArgs e) {
            if (this.Element == null)
                return;

            this.Element.Current = e.Pos;
        }

        private void Element_IndexRequired(object sender, Flip.IndexRequestEventArgs e) {
            Device.BeginInvokeOnMainThread(() => {
                this.Control.Goto(e.Index);
            });
        }

        private void Element_NextRequired(object sender, EventArgs e) {
            Device.BeginInvokeOnMainThread(() => {
                this.Control.Next();
            });
        }

        private IEnumerable<UIView> GetChildrenViews() {
            if (this.Element != null)
                foreach (var v in this.Element.Children) {
                    var render = v.GetOrCreateRenderer();// Platform.CreateRenderer(v);// RendererFactory.GetRenderer(v);
                    yield return render.NativeView;
                }
        }

        //public override SizeRequest GetDesiredSize(double widthConstraint, double heightConstraint) {
        //    this.Control.UpdateLayout(widthConstraint, heightConstraint);
        //    return UIViewExtensions.GetSizeRequest(this.NativeView, widthConstraint, heightConstraint, 44.0, 44.0);
        //}

        public override void LayoutSubviews() {
            base.LayoutSubviews();

            this.Control.UpdateLayout(this.Bounds.Width, this.Bounds.Height);
        }
    }
}