using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Xaml;
using WC = Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.System.Profile;
using Windows.UI.Xaml.Input;
using Windows.Devices.Input;

namespace AsNum.XFControls.UWP {

    [TemplatePart(Name = "r1", Type = typeof(WC.RowDefinition))]
    [TemplatePart(Name = "r3", Type = typeof(WC.RowDefinition))]
    [TemplatePart(Name = "sc", Type = typeof(WC.ScrollViewer))]
    [TemplatePart(Name = "border", Type = typeof(WC.Border))]
    public class DataPicker : WC.ItemsControl {

        public event EventHandler<SelectedChangedEventArgs> SelectedChanged;

        #region DividerColor
        public static readonly DependencyProperty DividerColorProperty =
            DependencyProperty.Register(nameof(DividerColor),
                typeof(Color),
                typeof(DataPicker),
                PropertyMetadata.Create(Colors.Gray, DividerColorChanged)
                );

        public Color DividerColor {
            get {
                return (Color)this.GetValue(DividerColorProperty);
            }
            set {
                this.SetValue(DividerColorProperty, value);
            }
        }

        private static void DividerColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {

        }
        #endregion

        #region TextColor
        public static readonly DependencyProperty TextColorProperty =
            DependencyProperty.Register(nameof(TextColor),
                typeof(Color),
                typeof(DataPicker),
                PropertyMetadata.Create(Colors.Black, TextColorChanged)
                );


        public Color TextColor {
            get {
                return (Color)this.GetValue(TextColorProperty);
            }
            set {
                this.SetValue(TextColorProperty, value);
            }
        }

        private static void TextColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var dp = (DataPicker)d;
            dp.TextBrush.Color = (Color)e.NewValue;
            dp.UpdateLayout();
        }
        #endregion

        #region RowHeight
        public static readonly DependencyProperty RowHeightProperty =
            DependencyProperty.Register(nameof(RowHeight),
                typeof(double),
                typeof(DataPicker),
                PropertyMetadata.Create(40D)
                );

        public double RowHeight {
            get {
                return (double)this.GetValue(RowHeightProperty);
            }
            set {
                this.SetValue(RowHeightProperty, value);
            }
        }

        #endregion


        #region SelectedIndex
        public static readonly DependencyProperty SelectedIndexProperty =
            DependencyProperty.Register(nameof(SelectedIndex),
                typeof(int),
                typeof(DataPicker),
                PropertyMetadata.Create(-1, SelectedIndexChanged)
                );


        public int SelectedIndex {
            get {
                return (int)this.GetValue(SelectedIndexProperty);
            }
            set {
                this.SetValue(SelectedIndexProperty, value);
            }
        }

        private static void SelectedIndexChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var dp = (DataPicker)d;
            if (dp.isManual)
                return;

            dp.ScheduleUpdatePosition((int)e.NewValue);
        }
        #endregion


        private WC.ScrollViewer sc;
        private WC.RowDefinition r1, r3;
        private WC.Border border;

        private SolidColorBrush TextBrush;

        public DataPicker() {
            this.DefaultStyleKey = typeof(DataPicker);

            this.TextBrush = new SolidColorBrush(this.TextColor);
            this.RegisterPropertyChangedCallback(FontSizeProperty, FontSizeChanged);
            this.RegisterPropertyChangedCallback(ItemsSourceProperty, ItemsSourceChanged);
        }

        private void ItemsSourceChanged(DependencyObject sender, DependencyProperty dp) {
            this.SelectedIndex = 0;
            this.ScheduleUpdatePosition(0);
            if (this.SelectedChanged != null)
                this.SelectedChanged.Invoke(this, new SelectedChangedEventArgs() {
                    SelectedIndex = 0
                });
        }

        private void FontSizeChanged(DependencyObject sender, DependencyProperty dp) {

        }

        protected override void OnApplyTemplate() {
            base.OnApplyTemplate();

            sc = (WC.ScrollViewer)this.GetTemplateChild("sc");
            r1 = (WC.RowDefinition)this.GetTemplateChild("r1");
            r3 = (WC.RowDefinition)this.GetTemplateChild("r3");
            border = (WC.Border)this.GetTemplateChild("border");

            sc.ViewChanged += Sc_ViewChanged;

            this.PointerEntered += DataPicker_PointerEntered;
            this.PointerExited += DataPicker_PointerExited;
        }

        private void DataPicker_PointerExited(object sender, PointerRoutedEventArgs e) {
            this.sc.VerticalScrollBarVisibility = WC.ScrollBarVisibility.Hidden;
        }

        private void DataPicker_PointerEntered(object sender, PointerRoutedEventArgs e) {
            this.sc.VerticalScrollBarVisibility =
                e.Pointer.PointerDeviceType == PointerDeviceType.Mouse
                    ? WC.ScrollBarVisibility.Auto
                    : WC.ScrollBarVisibility.Hidden;
        }

        private bool isInnerChange = false;
        private bool isManual = false;
        private async void Sc_ViewChanged(object sender, WC.ScrollViewerViewChangedEventArgs e) {
            if (e.IsIntermediate || isInnerChange)
                return;

            var borderRange = border.TransformToVisual(this).TransformBounds(new Rect(0, 0, border.ActualWidth, border.ActualHeight));

            //foreach (FrameworkElement c in this.ItemsPanelRoot.Children) {
            for (var i = 0; i < this.ItemsPanelRoot.Children.Count; i++) {
                var c = (FrameworkElement)this.ItemsPanelRoot.Children[i];

                var childCenterPoint = c.TransformToVisual(this).TransformPoint(new Point(c.ActualWidth / 2, c.ActualHeight / 2));

                if (borderRange.Contains(childCenterPoint)) {
                    var childTopPoint = c.TransformToVisual(this).TransformPoint(new Point(0, 0));
                    var offsetY = (sc.VerticalOffset + childTopPoint.Y - borderRange.Y);

                    await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () => {
                        this.isInnerChange = true;
                        sc.ChangeView(null, offsetY, null);
                    });

                    if (this.SelectedChanged != null) {
                        this.isManual = true;
                        this.SelectedIndex = i;
                        this.SelectedChanged.Invoke(this, new SelectedChangedEventArgs() {
                            SelectedIndex = i
                        });
                    }

                    break;
                }
            }

            this.isManual = false;
            this.isInnerChange = false;
        }


        private void ScheduleUpdatePosition(int idx) {
            Task.Delay(100).ContinueWith(async t => {
                await this.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () => {
                    this.UpdatePosition(idx);
                });
            });
        }

        private async void UpdatePosition(int idx) {
            if (idx < 0)
                idx = 0;

            if (this.ItemsPanelRoot == null || idx >= this.ItemsPanelRoot.Children.Count)
                return;


            var borderRange = border.TransformToVisual(this).TransformBounds(new Rect(0, 0, border.ActualWidth, border.ActualHeight));
            var c = (FrameworkElement)this.ItemsPanelRoot.Children[idx];
            var childTopPoint = c.TransformToVisual(this).TransformPoint(new Point(0, 0));
            var offsetY = (sc.VerticalOffset + childTopPoint.Y - borderRange.Y);

            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () => {
                this.isInnerChange = true;
                sc.ChangeView(null, offsetY, null);
            });

            this.isInnerChange = false;
        }

        protected override Size MeasureOverride(Size availableSize) {
            base.MeasureOverride(availableSize);


            var h = (availableSize.Height - this.RowHeight) / 2;
            if (h < 0)
                h = 0;

            r1.Height = new GridLength(h);
            r3.Height = new GridLength(h);

            return availableSize;
        }


        protected override void PrepareContainerForItemOverride(DependencyObject element, object item) {
            base.PrepareContainerForItemOverride(element, item);
            var ele = (WC.ContentPresenter)element;
            ele.Height = this.RowHeight;

            ele.FontSize = this.FontSize;
            ele.Foreground = this.TextBrush;
        }

        public class SelectedChangedEventArgs : EventArgs {

            public int SelectedIndex {
                get; set;
            }

        }
    }
}
