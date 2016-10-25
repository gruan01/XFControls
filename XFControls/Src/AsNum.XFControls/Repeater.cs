using AsNum.XFControls.Binders;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace AsNum.XFControls {

    public class RepeaterTapEventArgs : EventArgs {
        public object SelectedItem {
            get; set;
        }
    }

    /// <summary>
    /// Repeater
    /// </summary>
    public class Repeater : Layout<View> {

        public event EventHandler<RepeaterTapEventArgs> ItemTaped;

        #region ItemTemplate
        /// <summary>
        /// 数据模板
        /// </summary>
        public static readonly BindableProperty ItemTemplateProperty =
            BindableProperty.Create("ItemTemplate",
                typeof(DataTemplate),
                typeof(Repeater),
                null
                );

        /// <summary>
        /// 数据模板
        /// </summary>
        public DataTemplate ItemTemplate {
            get {
                return this.GetValue(ItemTemplateProperty) as DataTemplate;
            }

            set {
                this.SetValue(ItemTemplateProperty, value);
            }
        }
        #endregion

        #region ItemsSource
        /// <summary>
        /// 数据源
        /// </summary>
        public static readonly BindableProperty ItemsSourceProperty =
            BindableProperty.Create("ItemsSource",
                typeof(IEnumerable),
                typeof(Repeater),
                null,
                BindingMode.OneWay,
                propertyChanged: ItemsChanged);


        /// <summary>
        /// 数据源
        /// </summary>
        public IEnumerable ItemsSource {
            get {
                return this.GetValue(ItemsSourceProperty) as IEnumerable;
            }
            set {
                this.SetValue(ItemsSourceProperty, value);
            }
        }

        private static void ItemsChanged(BindableObject bindable, object oldValue, object newValue) {
            var rp = (Repeater)bindable;
			rp.InitCollection(newValue);
        }
        #endregion

        #region SelectedItem
        /// <summary>
        /// 选中的数据
        /// </summary>
        public static readonly BindableProperty SelectedItemProperty =
            BindableProperty.Create("SelectedItem",
                typeof(object),
                typeof(Repeater),
                null,
                defaultBindingMode: BindingMode.TwoWay,
                propertyChanged: SelectedItemChanged
                );


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

        private static void SelectedItemChanged(BindableObject bindable, object oldValue, object newValue) {
            var rp = (Repeater)bindable;
            if (rp.ItemTaped != null) {
                rp.ItemTaped.Invoke(rp, new RepeaterTapEventArgs() {
                    SelectedItem = rp.SelectedItem
                });
            }
        }
        #endregion

        #region ItemTapedCmd
        /// <summary>
        /// Tap 命令
        /// </summary>
        public static readonly BindableProperty ItemTapedCmdProperty =
            BindableProperty.Create("ItemTapedCmd",
                typeof(ICommand),
                typeof(Repeater),
                propertyChanged: ItemTapedCmdChanged
                );

        private static void ItemTapedCmdChanged(BindableObject bindable, object oldValue, object newValue) {
            var repeater = (Repeater)bindable;
            var flag = (newValue != null || repeater.ItemTapedCmd != null);
            foreach (var v in repeater.Container.Children) {
                TapBinder.SetWithFeedback(v, flag);
            }
        }

        /// <summary>
        /// Tap 命令
        /// </summary>
        public ICommand ItemTapedCmd {
            get {
                return (ICommand)this.GetValue(ItemTapedCmdProperty);
            }
            set {
                this.SetValue(ItemTapedCmdProperty, value);
            }
        }
        #endregion

        #region itemTemplateSelector 模板选择器
        /// <summary>
        /// 模板选择器
        /// </summary>
        public static readonly BindableProperty ItemTemplateSelectorProperty =
            BindableProperty.Create("ItemTemplateSelector",
                typeof(DataTemplateSelector),
                typeof(Repeater),
                null);

        /// <summary>
        /// 模板选择器
        /// </summary>
        public DataTemplateSelector ItemTemplateSelector {
            get {
                return (DataTemplateSelector)GetValue(ItemTemplateSelectorProperty);
            }
            set {
                SetValue(ItemTemplateSelectorProperty, value);
            }
        }
        #endregion

        #region Orientation
        /// <summary>
        /// 方向
        /// </summary>
        public static readonly BindableProperty OrientationProperty =
            BindableProperty.Create(nameof(Orientation),
                typeof(RepeaterOrientation),
                typeof(Repeater),
                RepeaterOrientation.HorizontalWrap,
                propertyChanged: OrientationChanged
                );

        /// <summary>
        /// 方向
        /// </summary>
        public RepeaterOrientation Orientation {
            get {
                return (RepeaterOrientation)this.GetValue(OrientationProperty);
            }
            set {
                this.SetValue(OrientationProperty, value);
            }
        }

        private static void OrientationChanged(BindableObject bindable, object oldValue, object newValue) {
            var repeater = (Repeater)bindable;
            repeater.SetContainer();
        }

        #endregion

        private Command TapCmd { get; }

        private Layout<View> Container { get; set; }

        public Repeater() {
            this.SetContainer();
            this.TapCmd = new Command(o => {
                this.SelectedItem = o;
                if (this.ItemTapedCmd != null && this.ItemTapedCmd.CanExecute(o)) {
                    this.ItemTapedCmd.Execute(o);
                }
            });
        }


        private void SetContainer() {
            this.BatchBegin();
            var old = this.Container;
            IList<View> subViews = null;
            if (old != null) {
                subViews = old.Children;
            }

            var container = RepeaterContainerFactory.Get(this.Orientation);
            this.Container = container.Layout;
            this.Children.Add(this.Container);

            if (subViews != null) {
                foreach (var sub in subViews) {
                    sub.Parent = null;
                    this.Container.Children.Add(sub);
                }
            }

            if (old != null) {
                this.Children.Remove(old);
            }

            this.BatchCommit();
        }

		private void InitCollection(object datas) {
            new NotifyCollectionWrapper(datas,
                add: this.Add,
                remove: this.Remove,
                reset: () => {
                    this.RemoveAll();
                    this.Add(this.ItemsSource);
                });
        }

        private void Add(IEnumerable datas, int startIdx = 0) {
            if (datas == null)
                return;

            foreach (var d in datas) {
                var v = this.GetChildView(d);
                this.Container.Children.Insert(startIdx++, v);
            }
        }

        private void Remove(IList datas, int startIdx) {
            if (datas == null)
                return;

            foreach (var d in datas) {
                this.Container.Children.RemoveAt(startIdx++);
            }
        }

        private void RemoveAll() {
            var children = this.Container.Children.ToList();
            foreach (var c in children)
                this.Container.Children.Remove(c);
        }


        /// <summary>
        /// 从模板/模板选择器创建子视图
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private View GetChildView(object data) {
            View view = null;
            if (this.ItemTemplate != null || this.ItemTemplateSelector != null) {
                if (this.ItemTemplateSelector != null) {
                    // SelectTemplate 的第二个参数，即 TemplateSelector 的 OnSelectTemplate 方法的 container 参数
                    view = (View)this.ItemTemplateSelector.SelectTemplate(data, this).CreateContent();
                } else if (this.ItemTemplate != null)
                    view = (View)this.ItemTemplate.CreateContent();

                if (view != null) {
                    view.BindingContext = data;
                    TapBinder.SetCmd(view, this.TapCmd);
                    TapBinder.SetParam(view, data);
                    if (this.ItemTaped == null && this.ItemTapedCmd == null) {
                        TapBinder.SetWithFeedback(view, false);
                    }
                }
            }

            if (view == null)
                view = new Label() { Text = "111" };

            return view;
        }


        protected override void LayoutChildren(double x, double y, double width, double height) {
			this.Container.Layout(new Rectangle(x, y, width, height));
        }

		////触发 SizeAllocated -> OnSizeAllocated
		//this.ForceLayout();

		////触发 ForceLayout
		//this.InvalidateLayout();

		//var a = (Width <= 0 || Height <= 0 || !IsVisible /*|| !IsNativeStateConsistent || DisableLayout*/);

		//检查是否需要 LayoutChildren -> LayoutChildren
		//this.UpdateChildrenLayout();


		protected override SizeRequest OnMeasure(double widthConstraint, double heightConstraint) {
			var size = this.Container.Measure(widthConstraint, heightConstraint);
			// return new SizeRequest 而不是 base.OnMeaseure, 否则在 IOS 下会分配空间失败。从而导至 LayoutChildren 不被触发
			return new SizeRequest(size.Request);
            //return base.OnMeasure(size.Request.Width, size.Request.Height);
        }
    }

    public enum RepeaterOrientation {
        /// <summary>
        /// 垂直
        /// </summary>
        Vertical = 0,
        /// <summary>
        /// 水平
        /// </summary>
        Horizontal = 1,
        /// <summary>
        /// 水平,自动换行
        /// </summary>
        HorizontalWrap = 2
    }
}
