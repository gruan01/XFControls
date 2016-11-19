using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Media;
using WC = Windows.UI.Xaml.Controls;
using WX = Windows.UI.Xaml;

namespace AsNum.XFControls.UWP {
    public class Toast : IDisposable {


        private static Lazy<Toast> _Instance = new Lazy<Toast>(() => new Toast());

        private CancellationTokenSource CTS = new CancellationTokenSource();

        public static Toast Instance {
            get {
                return _Instance.Value;
            }
        }


        private bool IsShowing = false;

        private Grid Container = new Grid();

        private Popup Popup = null;


        private Toast() {
            this.Popup = new Popup() {
                Child = new WC.Border() {
                    Background = new SolidColorBrush(Color.FromArgb(200, 0, 0, 0)),
                    CornerRadius = new WX.CornerRadius(10),
                    Padding = new WX.Thickness(5),
                    HorizontalAlignment = WX.HorizontalAlignment.Center,
                    VerticalAlignment = WX.VerticalAlignment.Center,
                    Child = this.Container,
                }
            };

            this.Container.SizeChanged += Container_SizeChanged;
        }

        void Container_SizeChanged(object sender, WX.SizeChangedEventArgs e) {
            this.UpdatePos(Positions.Bottom);
        }

        public void Show(string msg, int delay = 1000, Positions pos = Positions.Bottom) {
            if (this.IsShowing) {
                this.CTS.Cancel();
                this.CTS.Dispose();
                this.CTS = null;
                this.CTS = new CancellationTokenSource();
            }
            this.IsShowing = true;


            this.Popup.IsOpen = true;
            this.Container.Children.Clear();
            this.Container.Children.Add(new TextBlock() {
                Text = msg,
                Foreground = new SolidColorBrush(Colors.White),
                FontSize = 12
            });

            Task.Delay(delay)
                .ContinueWith(t => {
                    this.Dismiss();
                }, this.CTS.Token);
        }

        public async void Dismiss() {
            await this.Popup.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.High, () => {
                this.Popup.IsOpen = false;
                this.IsShowing = false;
            });
        }


        private void UpdatePos(Positions pos) {
            var size = WX.Window.Current.Bounds;
            var y = 0D;
            switch (pos) {
                case Positions.Top:
                    y = 80;
                    break;
                case Positions.Center:
                    y = size.Height / 2;
                    break;
                case Positions.Bottom:
                    y = size.Height - 80;
                    break;
            }

            this.Popup.VerticalOffset = y;
            //this.Popup.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Center;
            this.Popup.HorizontalOffset = (size.Width - this.Container.ActualWidth) / 2;
        }

        #region dispose
        public void Dispose() {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }


        ~Toast() {
            this.Dispose(false);
        }


        private bool isDisposed = false;
        private void Dispose(bool flag) {
            if (!isDisposed) {
                if (flag) {
                    if (this.CTS != null) {
                        this.CTS.Dispose();
                    }
                }
                isDisposed = true;
            }
        }
        #endregion
    }

    public enum Positions {
        Top,
        Center,
        Bottom
    }
}
