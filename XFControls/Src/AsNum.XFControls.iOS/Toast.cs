using Foundation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UIKit;
using Xamarin.Forms;
using System.Threading;

namespace AsNum.XFControls.iOS {
    public class Toast : NSObject {

        private static Lazy<Toast> _Instance = new Lazy<Toast>(() => new Toast());

		private CancellationTokenSource CTS = new CancellationTokenSource();

		private bool IsShowing = false;

        public static Toast Instance {
            get {
                return _Instance.Value;
            }
        }


        private Toast() {

        }


        private Lazy<UIView> Container = new Lazy<UIView>(() => {
            var view = new UIView();
            view.Layer.CornerRadius = 10f;
            view.Layer.BackgroundColor = new CoreGraphics.CGColor(0, 0, 0, 0.55f);
            //view.AutoresizingMask = UIViewAutoresizing.All;
			view.Layer.ShadowRadius = 5f;
			view.Layer.ShadowColor = new CoreGraphics.CGColor(0, 0, 0, 0.75f);
			//view.Layer.ShadowOffset = new CoreGraphics.CGSize(0, 0);
			//view.AutosizesSubviews = true;
            return view;
        });

        private UIView SubView = null;

        public void SetContent(UIView view) {
            if (this.SubView != null) {
                this.SubView.RemoveFromSuperview();
            }
			//view.ContentMode = UIViewContentMode.Bottom;
			view.SizeToFit();
            this.SubView = view;
            this.Container.Value.AddSubview(view);

			var window = UIApplication.SharedApplication.KeyWindow;
			this.Container.Value.Frame = new CoreGraphics.CGRect(0, 0, view.Frame.Width + 10, view.Frame.Height + 10);
			this.Container.Value.Center = new CoreGraphics.CGPoint(window.Center.X, window.Center.Y);

			var x = this.Container.Value.Frame.Width / 2;
			var y = this.Container.Value.Frame.Height / 2;

			this.SubView.Center = new CoreGraphics.CGPoint(x, y);
			//this.Container.Value.SizeToFit();
        }

		private CoreGraphics.CGPoint GetCenter( Positions pos ) { 
			var window = UIApplication.SharedApplication.KeyWindow;
			nfloat y = 0f;
			switch (pos) {
				case Positions.Top:
					y = 80;
					break;
				case Positions.Center:
					y = window.Center.Y;
					break;
				case Positions.Bottom:
					y = window.Frame.Bottom - 80;
					break;
			}
			return new CoreGraphics.CGPoint(window.Center.X, y);
		}
			

        public void Show(Positions pos = Positions.Bottom, Durations duration = Durations.Short) {
			if (this.IsShowing)
			{
				this.CTS.Cancel();
				this.CTS.Dispose();
				this.CTS = null;
				this.CTS = new CancellationTokenSource();
			}
			else {
				var window = UIApplication.SharedApplication.KeyWindow;
				window.AddSubview(this.Container.Value);				
			}
			this.IsShowing = true;

			this.Container.Value.Center = this.GetCenter(pos);

			var ms = duration == Durations.Long ? 5000 : 2000;

			Task.Delay(ms)
			    .ContinueWith(t =>
				{
					this.Dismiss();
				}
			    ,this.CTS.Token);
        }

        public void Dismiss() {
			Device.BeginInvokeOnMainThread(()=>
			{
				this.Container.Value.RemoveFromSuperview();
				this.IsShowing = false;
			});
        }


        #region dispose
        private bool IsDisposed = false;

        protected override void Dispose(bool disposing) {
            if (disposing && !this.IsDisposed) {
                this.IsDisposed = true;

                if (this.Container != null && this.Container.IsValueCreated) {
                    this.Container.Value.Dispose();
                    this.Container = null;
                }

				if (this.CTS != null)
				{
					this.CTS.Dispose();
				}
            }

            base.Dispose(disposing);
        }
        #endregion

        public enum Positions {
            Top,
            Center,
            Bottom
        }

        public enum Durations {
            Long,
            Short
        }
    }
}
