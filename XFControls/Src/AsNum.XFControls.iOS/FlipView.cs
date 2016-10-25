using CoreGraphics;
using Foundation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using UIKit;

namespace AsNum.XFControls.iOS {
    [Register("FlipView")]
    public class FlipView : UIScrollView {

        public event EventHandler<FlipViewPosChangedEventArgs> PosChanged = null;

        public UIPageControl PageControl { get; set; }

        private List<UIView> Views = new List<UIView>();

        private nfloat? TargetX = null;

        public FlipView()
            : base() {
            Initialize();
            this.SetUp();
        }

        public FlipView(RectangleF bounds)
            : base(bounds) {
            Initialize();
            this.SetUp();
        }

        void Initialize() {
            //BackgroundColor = UIColor.Red;
        }

        private void SetUp() {
            //Hide Scroll bar
            this.ShowsHorizontalScrollIndicator = false;
            this.ShowsVerticalScrollIndicator = false;

            //一次翻一页，自动在边界处停止
            this.PagingEnabled = true;

            this.Bounces = false;
            this.BouncesZoom = false;

            this.PageControl = new UIPageControl() {
                CurrentPageIndicatorTintColor = UIColor.White,
                PageIndicatorTintColor = UIColor.Gray
            };
            //this.AddSubview(this.PageControl);
            this.Scrolled += FlipView_Scrolled;
        }

        void FlipView_Scrolled(object sender, EventArgs e) {
            var pageWidth = this.Frame.Size.Width;
            var page = (int)Math.Floor((this.ContentOffset.X - pageWidth / 2) / pageWidth) + 1;
            this.PageControl.CurrentPage = page;
            //Debugger.Log(1, "FlipView", page.ToString());

            //左右滑动时，不知道目标页是哪个
            //只能设置 Next / GoTo 的目标位置
            //滚动到目标位置后，把 TargetX 设置为 null
            //当左右滑动的时候， TargetX 一直是 null，这样就可以判断到底是滑动，还是程序跳转了
            if (this.ContentOffset.X == this.TargetX) {
                this.TargetX = null;
            }

            if (TargetX == null) {
                if (this.PosChanged != null) {
                    this.PosChanged.Invoke(this, new FlipViewPosChangedEventArgs() {
                        Pos = page
                    });
                }
            }
        }

        public void SetItems(List<UIView> items) {
            if (items == null || items.Count == 0) {

            } else {
                for (var i = 0; i < items.Count; i++) {
                    var v = new UIView();
                    var item = items[i];
                    this.Views.Add(item);
                    v.AddSubview(item);
                    this.AddSubview(v);
                }
                this.PageControl.Pages = items.Count;
            }
        }

        public void UpdateLayout(double width, double height) {
            this.Frame = new CGRect(0, 0, width, height);
            this.PageControl.Frame = new CGRect(0, height - 20, width, 20);
            //this.BringSubviewToFront(this.PageControl);
            this.SetNeedsLayout();
        }

        public override void LayoutSubviews() {
            base.LayoutSubviews();

            var w = this.Frame.Size.Width;
            var h = this.Frame.Size.Height;
            for (var i = 0; i < this.Views.Count; i++) {
                var v = this.Subviews[i];

                var rect = new CGRect(w * i, 0, w, h);
                v.Frame = rect;
            }

            this.ContentSize = new CGSize(w * this.Views.Count, h);
        }

        public void Next() {
            var offset = this.ContentOffset;
            offset.X += this.Frame.Size.Width;
            if (offset.X >= this.ContentSize.Width)
                offset.X = 0;

            this.TargetX = offset.X;

            this.SetContentOffset(offset, true);
        }

        public void Goto(int idx) {
            var offset = this.ContentOffset;
            offset.X = this.Frame.Size.Width * idx;
            if (offset.X >= this.ContentSize.Width)
                offset.X = 0;

            this.TargetX = offset.X;

            this.SetContentOffset(offset, true);
        }

    }

    public class FlipViewPosChangedEventArgs : EventArgs {
        public int Pos { get; set; }
    }
}