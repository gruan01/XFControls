using AsNum.XFControls.Binders;
using AsNum.XFControls.Templates;
using System.Collections;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace AsNum.XFControls {
    /// <summary>
    /// 单选按钮组的基类
    /// </summary>
    public abstract class RadioGroupBase : ContentView {

        #region SelectedItem
        /// <summary>
        /// 选中的数据
        /// </summary>
        public static readonly BindableProperty SelectedItemProperty =
            BindableProperty.Create("SelectedItem",
                typeof(object),
                typeof(RadioGroupBase),
                null,
                BindingMode.TwoWay,
                propertyChanged: SelectedItemChanged);

        private static void SelectedItemChanged(BindableObject bindable, object oldValue, object newValue) {
            var rg = (RadioGroupBase)bindable;
            rg.UpdateSelected();
        }

        /// <summary>
        /// 选中的数据
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

        #region itemsSource 数据源
        /// <summary>
        /// 数据源
        /// </summary>
        public static readonly BindableProperty ItemsSourceProperty =
            BindableProperty.Create("ItemsSource",
                typeof(IEnumerable),
                typeof(RadioGroupBase),
                null,
                propertyChanged: ItemsSourceChanged);

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

        private static void ItemsSourceChanged(BindableObject bindable, object oldValue, object newValue) {
            var rg = (RadioGroupBase)bindable;
            rg.Container.Children.Clear();

            //if (newValue != null) {
            //    var source = (IEnumerable<object>)newValue;
            //    rg.Add(source.ToList(), 0);
            //}
            rg.WrapItemsSource();

            rg.UpdateSelected();
        }
        #endregion

        #region DisplayPath
        /// <summary>
        /// 要作为标签文本显示的属性路径
        /// </summary>
        public static readonly BindableProperty DisplayPathProperty =
            BindableProperty.Create("DisplayPath",
                                    typeof(string),
                                    typeof(RadioGroupBase));

        /// <summary>
        /// 要作为标签文本显示的属性路径
        /// </summary>
        public string DisplayPath {
            get {
                return (string)this.GetValue(DisplayPathProperty);
            }
            set {
                this.SetValue(DisplayPathProperty, value);
            }
        }

        #endregion

        #region RadioSize
        /// <summary>
        /// 按钮大小,默认25
        /// </summary>
        private static readonly BindableProperty RadioSizeProperty =
            BindableProperty.Create("Size",
                                    typeof(double),
                                    typeof(RadioGroupBase),
                                    25D
                                    );

        /// <summary>
        /// 按钮大小,默认25
        /// </summary>
        public double RadioSize {
            get {
                return (double)this.GetValue(RadioSizeProperty);
            }
            set {
                this.SetValue(RadioSizeProperty, value);
            }
        }
        #endregion

        #region SelectedItemControlTemplate
        /// <summary>
        /// 选中时的 ControlTemplate
        /// </summary>
        public static readonly BindableProperty SelectedItemControlTemplateProperty =
            BindableProperty.Create("SelectedItemControlTemplate",
                                    typeof(ControlTemplate),
                                    typeof(RadioButtonGroup),
                                    null
                );

        /// <summary>
        /// 选中时的 ControlTemplate
        /// </summary>
        public ControlTemplate SelectedItemControlTemplate {
            get {
                return (ControlTemplate)this.GetValue(SelectedItemControlTemplateProperty);
            }
            set {
                this.SetValue(SelectedItemControlTemplateProperty, value);
            }
        }
        #endregion


        #region UnSelectedItemControlTemplate
        /// <summary>
        /// 未选中时的 ControlTemplate
        /// </summary>
        public static readonly BindableProperty UnSelectedItemControlTemplateProperty =
            BindableProperty.Create("UnSelectedItemControlTemplate",
                                    typeof(ControlTemplate),
                                    typeof(RadioButtonGroup),
                                    null
                );

        /// <summary>
        /// 未选中时的 ControlTemplate
        /// </summary>
        public ControlTemplate UnSelectedItemControlTemplate {
            get {
                return (ControlTemplate)this.GetValue(UnSelectedItemControlTemplateProperty);
            }
            set {
                this.SetValue(UnSelectedItemControlTemplateProperty, value);
            }
        }
        #endregion

        /// <summary>
        /// 内部使用的选中命令
        /// </summary>
        private ICommand SelectedCmd { get; }

        /// <summary>
        /// 当前选中的 Radio
        /// </summary>
        private Radio SelectedRadio = null;

        //private StackLayout Container = null;
        internal Layout<View> Container { get; private set; }

        private static readonly ControlTemplate DefaultControlTemplate = new DefaultControlTemplate();

        /// <summary>
        /// 获取父容器,抽象方法
        /// </summary>
        /// <returns></returns>
        protected abstract Layout<View> GetContainer();

        public RadioGroupBase() {
            this.Container = this.GetContainer();
            this.Content = this.Container;

            this.SelectedCmd = new Command((o) => {
                if (o == null)
                    return;

                var item = (Radio)o;
                if (this.SelectedRadio != null) {
                    this.SelectedRadio.IsSelected = false;
                    this.SelectedRadio.ControlTemplate = this.UnSelectedItemControlTemplate ?? DefaultControlTemplate;
                }

                this.SelectedItem = item.Value;
                this.SelectedRadio = item;
                this.SelectedRadio.ControlTemplate = this.SelectedItemControlTemplate ?? DefaultControlTemplate;

                item.IsSelected = true;
            });

            this.WrapItemsSource();
        }

        private void WrapItemsSource() {
            new NotifyCollectionWrapper(this.ItemsSource,
                add: (datas, idx) => this.Add(datas, idx),
                remove: (datas, idx) => this.Remove(datas, idx),
                reset: () => this.Reset(),
                finished: () => { });
        }

        private void Add(IList datas, int idx) {
            var c = this.Container.Children.Count;

            foreach (var d in datas) {
                var v = this.GetRadio(d);
                if (idx < c)
                    this.Container.Children.Insert(idx++, v);
                else
                    this.Container.Children.Add(v);
            }
        }

        private void Remove(IList datas, int idx) {
            var rms = this.Container.Children.Skip(idx).Take(datas.Count);
            foreach (var rm in rms) {
                this.Container.Children.Remove(rm);
            }
        }

        private void Reset() {
            this.Container.Children.Clear();
            if (this.ItemsSource != null)
                foreach (var d in this.ItemsSource) {
                    var v = this.GetRadio(d);
                    this.Container.Children.Add(v);
                }
        }

        protected virtual Radio GetRadio(object data) {
            Radio item = null;
            if (data is Radio) {
                item = (Radio)data;
            } else {
                item = new Radio();
                item.Value = data;

                if (!string.IsNullOrWhiteSpace(this.DisplayPath)) {
                    //item.Text = Helper.GetProperty<string>(data, this.DisplayPath, "DisplayPath Invalid");
                    //item.SetBinding(Radio.TextProperty, this.DisplayPath);
                    item.SetBinding(Radio.TextProperty, new Binding(this.DisplayPath, source: data));
                } else
                    item.Text = data.ToString();
            }

            item.Size = this.RadioSize;

            if (this.UnSelectedItemControlTemplate != null) {
                item.ControlTemplate = this.UnSelectedItemControlTemplate;
            }

            TapBinder.SetCmd(item, this.SelectedCmd);
            TapBinder.SetParam(item, item);

            return item;
        }

        private void UpdateSelected() {
            var item = this.SelectedItem;
            //TODO 地址引用型数据，这里会找不到相同的值的
            var radio = this.Container.Children.FirstOrDefault(r => ((Radio)r).Value.Equals(item));
            this.SelectedCmd.Execute(radio);
        }


        public enum RadioGroupOrientation {
            Vertical = 0,
            Horizontal = 1,
            HorizontalWrap = 2
        }
    }
}
