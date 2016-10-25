using CoreGraphics;
using Foundation;
using System;
using System.Collections.Generic;
using System.Text;
using UIKit;

namespace AsNum.XFControls.iOS {


	public class RatingBarRateChangeEventArgs : EventArgs { 
		public float Rate { get; set; }
	}


    /// <summary>
    /// https://onevcat.com/2013/04/using-blending-in-ios/
    /// https://github.com/saiwu-bigkoo/iOS-RatingBar
    /// </summary>
    public class RatingBar : UIView {

		public event EventHandler<RatingBarRateChangeEventArgs> RateChanged;

        /// <summary>
        /// 星星数
        /// </summary>
        public int StarNum { get; set; } = 5;

        private float _rate = 0;
        public float Rate {
            get {
                return this._rate;
            }
            set {
                _rate = value < 0 ? 0 : (value > this.StarNum ? this.StarNum : value);
                this.UpdateRateView();
            }
        }

        /// <summary>
        /// 是否带动画
        /// </summary>
        public bool WithAnimation { get; set; } = true;

        /// <summary>
        /// 动画时长
        /// </summary>
        public int AnimationInterval { get; set; } = 200;

        /// <summary>
        /// 评分时是否允许不是整颗星星
        /// </summary>
        public bool Incomplete { get; set; } = false;

        /// <summary>
        /// 是否仅显示
        /// </summary>
        public bool IsIndicator { get; set; } = false;


        /// <summary>
        /// 步长
        /// </summary>
        public float Step { get; set; } = 1;


        private static readonly UIImage ImgSelected;
        private static readonly UIImage ImgUnSelected;

        private UIView ForegroundView { get; set; }
        private UIView BackgroundView { get; set; }

        static RatingBar() {
            var asm = typeof(RatingBar).Assembly;
            ImgSelected = UIImage.FromResource(asm, "AsNum.XFControls.iOS.Imgs.star_light.png");
            ImgUnSelected = UIImage.FromResource(asm, "AsNum.XFControls.iOS.Imgs.star_dark.png");
        }

        private void Build() {
            this.ForegroundView = this.CreateRatingView(ImgSelected);
            this.BackgroundView = this.CreateRatingView(ImgUnSelected);
            this.UpdateRateView();
            this.AddSubviews(this.BackgroundView, this.ForegroundView);

            var tap = new UITapGestureRecognizer(this, new ObjCRuntime.Selector("tapRateView:"));
            tap.NumberOfTapsRequired = 1;
            this.AddGestureRecognizer(tap);

        }

        private UIView CreateRatingView(UIImage img) {
            var view = new UIView() {
                Frame = this.Bounds,
                ClipsToBounds = true,
                BackgroundColor = UIColor.Clear
            };

            for (var i = 0; i < this.StarNum; i++) {
                var x = i * this.Bounds.Size.Width / this.StarNum;
                var y = 0;
                var w = this.Bounds.Size.Width / StarNum;
                var h = this.Bounds.Size.Height;

                var imgView = new UIImageView(img) {
                    ContentMode = UIViewContentMode.ScaleAspectFit,
                    Frame = new CGRect(x, y, w, h)
                };
                view.AddSubview(imgView);
            };

            return view;
        }

        private void UpdateRateView() {
            var s = this.Rate / this.StarNum;
			var w = this.Bounds.Size.Width * s;
			var h = this.Bounds.Size.Height;

			if (w > 0 && h > 0)
            	this.ForegroundView.Frame = new CGRect(0, 0, w, h);
        }


        [Export("tapRateView:")]
        private void Tap(UITapGestureRecognizer tap) {
            if (this.IsIndicator)
                return;

            var p = tap.LocationInView(this);
            var offset = p.X;

			var s = (float)(offset / (this.Bounds.Size.Width / StarNum));
			this.Rate = this.Incomplete ? s : (float)Math.Ceiling(s);

			if (this.RateChanged != null)
				this.RateChanged.Invoke(this, new RatingBarRateChangeEventArgs() { Rate = this.Rate });

			//Step 不应该用在 Tap事件中
            //var s = (float)(offset / (this.Bounds.Size.Width / StarNum));

            //if (s < this.Step)
            //    s = this.Step;
            //else {
            //    var n = s / this.Step;
            //    if (n != (int)n) {
            //        var a = ((int)n) * this.Step;
            //        s = a;
            //    }
            //}
            //this.Rate = this.Incomplete ? s : (float)Math.Round(s);
        }


        public override void LayoutSubviews() {
            base.LayoutSubviews();

            this.Build();
            var interval = this.WithAnimation ? this.AnimationInterval : 0;
            //开启动画改变foregroundRatingView可见范围
            UIView.Animate(interval, () => {
                this.UpdateRateView();
            });
        }


		public void UpdateLayout(double width, double height)
		{
			this.Frame = new CGRect(0, 0, width, height);
			this.SetNeedsLayout();
		}
    }
}
