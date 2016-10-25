using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AsNum.XFControls {

    //https://adventuresinxamarinforms.com/2014/12/19/creating-a-xamarin-forms-app-part-10-designing-and-developing-the-user-interface/
    public partial class ItemsView : ScrollView {
        private readonly ICommand _selectedCommand;

        public ItemsView() {
            InitializeComponent();

            _selectedCommand = new Command<object>(item => SelectedItem = item);
        }

        public event EventHandler SelectedItemChanged;


        public static readonly BindableProperty ItemsSourceProperty =
            BindableProperty.Create("ItemsSource",
                typeof(IList),
                typeof(ItemsView),
                null,
                BindingMode.TwoWay,
                propertyChanged: ItemsSourceChanged);

        public IList ItemsSource {
            get { return (IList)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }



        public static readonly BindableProperty SelectedItemProperty =
            BindableProperty.Create("SelectedItem",
                typeof(object),
                typeof(ItemsView),
                null,
                BindingMode.TwoWay,
                propertyChanged: OnSelectedItemChanged);

        public object SelectedItem {
            get { return (object)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }





        public static readonly BindableProperty ItemTemplateProperty =
            BindableProperty.Create("ItemTemplate",
                typeof(DataTemplate),
                typeof(ItemsView),
                null,
                BindingMode.Default
                );

        public DataTemplate ItemTemplate {
            get { return (DataTemplate)GetValue(ItemTemplateProperty); }
            set { SetValue(ItemTemplateProperty, value); }
        }

        private static void ItemsSourceChanged(BindableObject bindable, object oldValue, object newValue) {
            var itemsLayout = (ItemsView)bindable;
            itemsLayout.SetItems();
        }

        private void SetItems() {
            stackLayout.Children.Clear();

            if (ItemsSource == null)
                return;

            foreach (var item in ItemsSource)
                stackLayout.Children.Add(GetItemView(item));

            SelectedItem = ItemsSource.Cast<object>().FirstOrDefault();
        }

        private View GetItemView(object item) {
            var content = ItemTemplate.CreateContent();
            var view = content as View;
            view.BindingContext = item;

            view.GestureRecognizers.Add(new TapGestureRecognizer {
                Command = _selectedCommand,
                CommandParameter = item
            });

            return view;
        }

        private static void OnSelectedItemChanged(BindableObject bindable, object oldValue, object newValue) {
            var itemsView = (ItemsView)bindable;

            if (newValue == oldValue)
                return;

            var items = itemsView.ItemsSource.OfType<ISelectable>();

            foreach (var item in items)
                item.IsSelected = item == newValue;

            var handler = itemsView.SelectedItemChanged;
            if (handler != null)
                handler(itemsView, EventArgs.Empty);
        }
    }
}
