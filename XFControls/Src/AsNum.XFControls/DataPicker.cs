using System.Collections;
using System.Collections.Generic;
using Xamarin.Forms;

namespace AsNum.XFControls {

    /// <summary>
    /// 数据选择器
    /// </summary>
    public class DataPicker : View {

        #region itemsSource 数据源
        /// <summary>
        /// 数据源
        /// </summary>
        public static readonly BindableProperty ItemsSourceProperty =
            BindableProperty.Create("ItemsSource",
                typeof(IEnumerable),
                typeof(DataPicker),
                null);

        /// <summary>
        /// 数据源
        /// </summary>
        public IEnumerable ItemsSource {
            get {
                return (IEnumerable)this.GetValue(ItemsSourceProperty);
            }
            set {
                this.SetValue(ItemsSourceProperty, value);
            }
        }

        #endregion

        #region SelectedItem

        /// <summary>
        /// 选中项
        /// </summary>
        public static readonly BindableProperty SelectedItemProperty =
            BindableProperty.Create("SelectedItem",
                typeof(object),
                typeof(DataPicker),
                null,
                BindingMode.TwoWay
                );

        /// <summary>
        /// 选中项
        /// </summary>
        public object SelectedItem {
            get {
                return this.GetValue(SelectedItemProperty);
            }
            set {
                this.SetValue(SelectedItemProperty, value);
            }
        }

        #endregion

        #region textColor
        public static readonly BindableProperty TextColorProperty =
            BindableProperty.Create("TextColor",
                typeof(Color),
                typeof(DataPicker),
                Color.Black
                );

        public Color TextColor {
            get {
                return (Color)this.GetValue(TextColorProperty);
            }
            set {
                this.SetValue(TextColorProperty, value);
            }
        }

        #endregion

        #region FontSize
        public static readonly BindableProperty FontSizeProperty =
            BindableProperty.Create("FontSize",
                typeof(float),
                typeof(DataPicker),
                15f
                );

        public float FontSize {
            get {
                return (float)this.GetValue(FontSizeProperty);
            }
            set {
                this.SetValue(FontSizeProperty, value);
            }
        }
        #endregion

        #region DividerColor
        public static readonly BindableProperty DividerColorProperty =
            BindableProperty.Create(
                "DividerColor",
                typeof(Color),
                typeof(DataPicker),
                Color.Gray);


        public Color DividerColor {
            get {
                return (Color)this.GetValue(DividerColorProperty);
            }
            set {
                this.SetValue(DividerColorProperty, value);
            }
        }
        #endregion

        #region
        /// <summary>
        /// 显示值的属性路径
        /// </summary>
        public string DisplayPath { get; set; }

        /// <summary>
        /// 显示值的格式
        /// </summary>
        public string DisplayFormat { get; set; }

        //public Color TextColor { get; set; }
        //public Color DividerColor { get; set; }
        #endregion

        /// <summary>
        /// 将数据源转换为可显示文本集合,用于 Render 中的 NativeControl
        /// </summary>
        public IList<string> StringValues {
            get {

                var lst = new List<string>();

                if (this.ItemsSource != null && !string.IsNullOrWhiteSpace(this.DisplayPath)) {

                    foreach (var d in this.ItemsSource) {
                        lst.Add(Helper.GetProperty(d, this.DisplayPath)?.ToString());
                    }
                } else if (this.ItemsSource != null) {
                    foreach (var d in this.ItemsSource)
                        lst.Add(d.ToString());
                }

                return lst;
            }
        }

        /// <summary>
        /// 选中项的序号
        /// </summary>
        public int SelectedIndex {
            get {
                if (this.SelectedItem != null) {
                    var str = Helper.GetProperty(this.SelectedItem, this.DisplayPath)?.ToString();
                    return this.StringValues.IndexOf(str);
                } else {
                    return -1;
                }
            }
        }
    }
}
