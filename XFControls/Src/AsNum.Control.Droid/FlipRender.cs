using Android.Graphics.Drawables;
using Android.Graphics.Drawables.Shapes;
using Android.Support.V4.View;
using Android.Widget;
using AsNum.XFControls;
using AsNum.XFControls.Droid;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using AV = Android.Views;
using AW = Android.Widget;

[assembly: ExportRenderer(typeof(Flip), typeof(FlipRender))]
namespace AsNum.XFControls.Droid {
    public class FlipRender : ViewRenderer<Flip, AW.RelativeLayout> {

        private bool IsDisposed = false;

        private ViewPager VP = null;
        private LinearLayout PointsContainer = null;
        private int LastPos = 0;
        private FlipViewAdapter Adapter = null;
        //private AW.RelativeLayout Root = null;

        private static readonly Color DefaultPointColor = Color.Gray;

        protected override void OnElementChanged(ElementChangedEventArgs<Flip> e) {
            base.OnElementChanged(e);
            if (e.OldElement != null) {
                e.OldElement.Children.CollectionChanged -= this.Children_CollectionChanged;
                e.OldElement.NextRequired -= this.Element_NextRequired;
                e.OldElement.IndexRequired -= this.Element_IndexRequired;
            }

            if (this.Element == null)
                return;

            var Root = new AW.RelativeLayout(this.Context);
            this.VP = new ViewPager(this.Context);
            this.VP.PageSelected += VP_PageSelected;

            //如果传入的 items 是 IEnumerable 类型的 (未ToList) , 会一直去计算那个 IEnumerable , 可断点到 GetChildrenViews 里, 会一直在那里执行, 从而导致子视图不显示
            this.Adapter = new FlipViewAdapter(this.VP);
            Adapter.SetItems(this.GetChildrenViews().ToList());
            Adapter.PosChanged += Adapter_PosChanged;
            this.VP.Adapter = Adapter;
            this.VP.AddOnPageChangeListener(Adapter);
            Root.AddView(this.VP, LayoutParams.MatchParent, LayoutParams.MatchParent);

            this.PointsContainer = new LinearLayout(this.Context);
            this.PointsContainer.Orientation = Orientation.Horizontal;

            var lp = new AW.RelativeLayout.LayoutParams(LayoutParams.WrapContent, 20);
            lp.AddRule(LayoutRules.AlignParentBottom);
            lp.AddRule(LayoutRules.CenterHorizontal);
            Root.AddView(this.PointsContainer, lp);

            this.SetNativeControl(Root);

            if (this.Element.ShowIndicator)
                this.SetPoints();

            Root.Invalidate();
            Root.RequestLayout();

            this.Element.NextRequired += Element_NextRequired;
            this.Element.IndexRequired += Element_IndexRequired;

            this.Element.Children.CollectionChanged += Children_CollectionChanged;
        }

        private void Children_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e) {
            if (this.Adapter != null)
                Device.BeginInvokeOnMainThread(() => {
                    this.Adapter.SetItems(this.GetChildrenViews().ToList());
                    this.Adapter.NotifyDataSetChanged();

                    if (this.Element != null)
                        if (this.Element.ShowIndicator)
                            this.SetPoints();
                });
        }

        private void Adapter_PosChanged(object sender, FlipViewAdapter.PosChangedEventArgs e) {
            if (this.Element == null)
                return;

            this.Element.Current = e.Pos;
        }

        private void Element_IndexRequired(object sender, Flip.IndexRequestEventArgs e) {
            if (this.VP != null) {
                //Device.BeginInvokeOnMainThread(() => {
                var adapter = ((FlipViewAdapter)this.VP.Adapter);
                adapter.Goto(e.Index);
                //});
            }
        }

        private void Element_NextRequired(object sender, EventArgs e) {
            Device.BeginInvokeOnMainThread(() => {
                if (this.VP != null)
                    ((FlipViewAdapter)this.VP.Adapter).Next();
            });
        }

        private void VP_PageSelected(object sender, ViewPager.PageSelectedEventArgs e) {
            if (this.Element == null)
                return;

            this.SetPointColor(this.LastPos);
            var realPos = e.Position % this.Element.Children.Count;
            this.SetPointColor(realPos, Color.White);
        }

        private IEnumerable<AV.View> GetChildrenViews() {
            if (this.Element != null) {
                foreach (var v in this.Element.Children) {
                    //var render = RendererFactory.GetRenderer(v);
                    var render = v.GetOrCreateRenderer(); //Platform.CreateRenderer(v);
                    if (render.ViewGroup.Parent == null) {
                        var c = new AW.FrameLayout(this.Context);
                        //c.SetBackgroundColor(Color.Blue.ToAndroid());
                        c.AddView(render.ViewGroup, LayoutParams.MatchParent, LayoutParams.MatchParent);
                        yield return c;
                    }
                    else
                        yield return (AV.View)render.ViewGroup.Parent;
                }
            }
        }

        private void SetPoints() {
            this.PointsContainer.RemoveAllViews();

            var lp = new LinearLayout.LayoutParams(10, 10);
            lp.LeftMargin = 5;
            lp.RightMargin = 5;

            var shape = new OvalShape();
            shape.Resize(10, 10);
            var dr = new ShapeDrawable(shape);
            dr.Paint.Color = DefaultPointColor.ToAndroid();

            for (var i = 0; i < this.Element.Children.Count; i++) {
                var v = new AV.View(this.Context);
                //v.SetBackgroundDrawable(dr);
                v.Background = dr;

                this.PointsContainer.AddView(v, lp);
            }
        }

        private void SetPointColor(int idx, Color? color = null) {
            var point = this.PointsContainer.GetChildAt(idx);
            if (point != null) {
                var shape = new OvalShape();
                var dr = new ShapeDrawable(shape);
                dr.Paint.Color = (color ?? DefaultPointColor).ToAndroid();
                //point.SetBackgroundDrawable(dr);
                point.Background = dr;
            }
            this.LastPos = idx;
        }

        protected override void Dispose(bool disposing) {

            if (disposing && !this.IsDisposed) {
                this.IsDisposed = true;

                if (this.Element != null) {
                    this.Element.Children.CollectionChanged -= this.Children_CollectionChanged;
                    this.Element.NextRequired -= this.Element_NextRequired;
                    this.Element.IndexRequired -= this.Element_IndexRequired;
                }

                if (this.VP != null) {
                    this.VP.RemoveFromParent();
                    this.VP.Dispose();
                    this.VP = null;
                }

                if (this.Adapter != null) {
                    this.Adapter.Dispose();
                    this.Adapter = null;
                }

                if (this.PointsContainer != null) {
                    this.PointsContainer.RemoveFromParent();
                    this.PointsContainer.Dispose();
                    this.PointsContainer = null;
                }

                // Root 做为 Control ,应该由 base 释放
                //if (this.Root != null) {
                //    this.Root.Dispose();
                //    this.Root = null;
                //}
            }

            base.Dispose(disposing);
        }
    }
}