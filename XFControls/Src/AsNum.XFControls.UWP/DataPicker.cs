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

namespace AsNum.XFControls.UWP {

    [TemplatePart(Name = "r1", Type = typeof(WC.RowDefinition))]
    [TemplatePart(Name = "r3", Type = typeof(WC.RowDefinition))]
    [TemplatePart(Name = "sc", Type = typeof(WC.ScrollViewer))]
    [TemplatePart(Name = "border", Type = typeof(WC.Border))]
    public class DataPicker : WC.ItemsControl {

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

        private WC.ScrollViewer sc;
        private WC.RowDefinition r1, r3;
        private WC.Border border;

        private SolidColorBrush TextBrush;

        private double MARGIN = 0;

        public DataPicker() {
            this.DefaultStyleKey = typeof(DataPicker);

            this.FontSize = 15;

            this.TextBrush = new SolidColorBrush(this.TextColor);
        }


        protected override void OnApplyTemplate() {
            base.OnApplyTemplate();

            sc = (WC.ScrollViewer)this.GetTemplateChild("sc");
            r1 = (WC.RowDefinition)this.GetTemplateChild("r1");
            r3 = (WC.RowDefinition)this.GetTemplateChild("r3");
            border = (WC.Border)this.GetTemplateChild("border");

            sc.ViewChanged += Sc_ViewChanged;
        }

        private void Sc_ViewChanged(object sender, WC.ScrollViewerViewChangedEventArgs e) {
            var gt = border.TransformToVisual(this);
            var bounds = gt.TransformBounds(new Rect(0, 0, this.ActualWidth, this.RowHeight));
            var elements = VisualTreeHelper.FindElementsInHostCoordinates(bounds, this.ItemsPanelRoot, true);
            var c = elements.Count();
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

    }
}
