using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace AsNum.XFControls.UWP {

    [TemplatePart(Name = "r1", Type = typeof(RowDefinition))]
    [TemplatePart(Name = "r3", Type = typeof(RowDefinition))]
    [TemplatePart(Name = "ic", Type = typeof(ItemsControl))]
    [TemplatePart(Name = "sc", Type = typeof(ScrollViewer))]
    public class DataPicker : ItemsControl {

        //#region ItemsSource
        //public static readonly DependencyProperty ItemsSourceProperty =
        //    DependencyProperty.Register(nameof(ItemsSource),
        //        typeof(object),
        //        typeof(DataPicker),
        //        PropertyMetadata.Create(Enumerable.Empty<object>())
        //        );

        //public object ItemsSource {
        //    get {
        //        return this.GetValue(ItemsSourceProperty);
        //    }
        //    set {
        //        this.SetValue(ItemsSourceProperty, value);
        //    }
        //}

        //#endregion

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
            
        }
        #endregion

        #region RowHeight
        public static readonly DependencyProperty RowHeightProperty =
            DependencyProperty.Register(nameof(RowHeight),
                typeof(int),
                typeof(DataPicker),
                PropertyMetadata.Create(40)
                );

        public int RowHeight {
            get {
                return (int)this.GetValue(RowHeightProperty);
            }
            set {
                this.SetValue(RowHeightProperty, value);
            }
        }

        #endregion

        private ScrollViewer sc;
        private ItemsControl ic;
        private RowDefinition r1, r3;

        public DataPicker() {
            this.DefaultStyleKey = typeof(DataPicker);

            this.FontSize = 15;
        }

        protected override void OnApplyTemplate() {
            base.OnApplyTemplate();

            sc = (ScrollViewer)this.GetTemplateChild("sc");
            ic = (ItemsControl)this.GetTemplateChild("ic");
            r1 = (RowDefinition)this.GetTemplateChild("r1");
            r3 = (RowDefinition)this.GetTemplateChild("r3");


        }


        protected override Size MeasureOverride(Size availableSize) {
            var size = availableSize;// base.MeasureOverride(availableSize);

            //var h = (size.Height - this.RowHeight) / 2;
            //if (h < 0)
            //    h = 0;

            //r1.Height = new GridLength(h);
            //r3.Height = new GridLength(h);

            return size;
        }
    }
}
