using Xamarin.Forms;

namespace AsNum.XFControls {
    /// <summary>
    /// 选项卡标签
    /// </summary>
    public class TabPageView : ContentView {

        #region IsSelected
        public static readonly BindableProperty IsSelectedProperty =
            BindableProperty.Create("IsSelected",
                typeof(bool),
                typeof(TabPageView),
                false
                );

        public bool IsSelected {
            get {
                return (bool)this.GetValue(IsSelectedProperty);
            }
            set {
                this.SetValue(IsSelectedProperty, value);
            }
        }
        #endregion

        #region Index
        internal static BindableProperty IndexProperty =
            BindableProperty.Create("Index",
                typeof(int),
                typeof(TabPageView),
                0);

        public int Index {
            get {
                return (int)this.GetValue(IndexProperty);
            }
            set {
                this.SetValue(IndexProperty, value);
            }
        }
        #endregion

        #region Header
        public static readonly BindableProperty HeaderProperty =
            BindableProperty.Create("Header",
                typeof(ContentView),
                typeof(TabPageView),
                new ContentView()
                );

        public ContentView Header {
            get {
                return (ContentView)this.GetValue(HeaderProperty);
            }
            set {
                this.SetValue(HeaderProperty, value);
            }
        }
        #endregion

        #region Title
        public static readonly BindableProperty TitleProperty =
            BindableProperty.Create("Title",
                typeof(string),
                typeof(TabPageView),
                propertyChanged: TitleChanged);

        public string Title {
            get {
                return (string)this.GetValue(TitleProperty);
            }
            set {
                this.SetValue(TitleProperty, value);
            }
        }

        private static void TitleChanged(BindableObject bindable, object oldValue, object newValue) {
            var pv = (TabPageView)bindable;
            pv.Header.Content = new Label() {
                Text = (string)newValue
            };
        }
        #endregion

        #region TabPosition
        public static readonly BindableProperty TabPositionProperty =
            BindableProperty.Create("TabPosition",
                typeof(TabViewPositions),
                typeof(TabPageView),
                TabViewPositions.Top);

        public TabViewPositions TabPosition {
            get {
                return (TabViewPositions)this.GetValue(TabPositionProperty);
            }
            set {
                this.SetValue(TabPositionProperty, value);
            }
        }
        #endregion
    }
}
