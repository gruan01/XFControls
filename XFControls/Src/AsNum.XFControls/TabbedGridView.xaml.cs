using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace AsNum.XFControls {

    //https://adventuresinxamarinforms.com/2015/04/20/animating-the-tabbedview/
    public partial class TabbedGridView : Grid {
        private readonly IDictionary<ISelectable, View> _views = new Dictionary<ISelectable, View>();

        public TabbedGridView() {
            InitializeComponent();
            itemsView.SelectedItemChanged += HandleSelectedItemViewChanged;
        }


        public static readonly BindableProperty ItemsSourceProperty =
            BindableProperty.Create("ItemsSource",
                typeof(IEnumerable<ISelectable>),
                typeof(TabbedGridView),
                null,
                BindingMode.TwoWay,
                propertyChanged: ItemsSourceChanged);

        public IEnumerable<ISelectable> ItemsSource {
            get { return (IEnumerable<ISelectable>)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }





        public static readonly BindableProperty BackgroundTemplateProperty =
            BindableProperty.Create("BackgroundTemplate",
                typeof(DataTemplate),
                typeof(TabbedGridView),
                default(DataTemplate),
                BindingMode.TwoWay);

        public DataTemplate BackgroundTemplate {
            get { return (DataTemplate)GetValue(BackgroundTemplateProperty); }
            set { SetValue(BackgroundTemplateProperty, value); }
        }



        public static readonly BindableProperty TabTemplateProperty =
            BindableProperty.Create("TabTemplate",
                typeof(DataTemplate),
                typeof(TabbedGridView),
                default(DataTemplate),
                BindingMode.TwoWay,
                propertyChanged: TabTemplateChanged);

        public DataTemplate TabTemplate {
            get { return (DataTemplate)GetValue(TabTemplateProperty); }
            set { SetValue(TabTemplateProperty, value); }
        }




        public static readonly BindableProperty ItemTemplateProperty =
            BindableProperty.Create("ItemTemplate",
                typeof(DataTemplate),
                typeof(TabbedGridView),
                default(DataTemplate),
                BindingMode.TwoWay);

        public DataTemplate ItemTemplate {
            get { return (DataTemplate)GetValue(ItemTemplateProperty); }
            set { SetValue(ItemTemplateProperty, value); }
        }


        public static readonly BindableProperty ItemTemplateSelectorProperty =
            BindableProperty.Create("ItemTemplateSelector",
                typeof(DataTemplateSelector),
                typeof(TabbedGridView),
                null,
                BindingMode.Default);

        public DataTemplateSelector ItemTemplateSelector {
            get { return (DataTemplateSelector)GetValue(ItemTemplateSelectorProperty); }
            set { SetValue(ItemTemplateSelectorProperty, value); }
        }



        public static readonly BindableProperty SelectedItemProperty =
            BindableProperty.Create("SelectedItem",
                typeof(ISelectable),
                typeof(TabbedGridView),
                null,
                BindingMode.TwoWay,
                propertyChanged: SelectedItemChanged);

        public ISelectable SelectedItem {
            get { return (ISelectable)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        private static void ItemsSourceChanged(BindableObject bindable, object oldValue, object newValue) {
            if (Equals(newValue, oldValue))
                return;
            var tgv = (TabbedGridView)bindable;
            tgv.SetItemsSource((IEnumerable<ISelectable>)newValue);
        }

        private static void SelectedItemChanged(BindableObject bindable, object oldValue, object newValue) {
            if (Equals(newValue, oldValue))
                return;
            var tgv = (TabbedGridView)bindable;
            tgv.AddItemView((ISelectable)newValue);
        }

        private static void TabTemplateChanged(BindableObject bindable, object oldValue, object newValue) {
            var tgv = (TabbedGridView)bindable;
            tgv.itemsView.ItemTemplate = (DataTemplate)newValue;
        }

        private void SetItemsSource(IEnumerable<ISelectable> itemsSource) {
            if (itemsSource == null)
                return;
            var items = itemsSource.ToList();

            itemsView.ItemsSource = items;

            foreach (var selectable in items) {
                if (BackgroundTemplate != null) {
                    var backgroundView = GetBackgroundView(selectable);
                    backgroundGrid.Children.Add(backgroundView);
                }

                //if (Device.OS != TargetPlatform.WinPhone)
                AddItemView(selectable);
            }
        }

        private void AddItemView(ISelectable item) {
            View view = null;
            if (!_views.TryGetValue(item, out view)) {
                //view = ItemTemplate != null ? (View)ItemTemplate.CreateContent() : (ItemTemplateSelector != null ? ItemTemplateSelector.SelectTemplate(item, null).CreateContent() as View : null);
                if (this.ItemTemplate != null) {
                    view = (View)this.ItemTemplate.CreateContent();
                } else if (this.ItemTemplateSelector != null) {
                    view = (View)this.ItemTemplateSelector.SelectTemplate(item, null).CreateContent();
                }


                if (view == null)
                    return;

                view.BindingContext = item;

                AddFadeBehavior(view);

                contentGrid.Children.Add(view);

                _views.Add(item, view);
            }
        }

        private View GetBackgroundView(ISelectable item) {
            var view = BackgroundTemplate.CreateContent() as View;
            if (view == null)
                return null;

            view.BindingContext = item;
            AddFadeBehavior(view);
            return view;
        }

        private void AddFadeBehavior(View view) {
            var behavior = new FadeBehavior();
            view.Behaviors.Add(behavior);
            behavior.SetBinding(FadeBehavior.IsSelectedProperty, "IsSelected");
        }

        private void HandleSelectedItemViewChanged(object sender, EventArgs e) {
            SelectedItem = itemsView.SelectedItem as ISelectable;
        }
    }
}
