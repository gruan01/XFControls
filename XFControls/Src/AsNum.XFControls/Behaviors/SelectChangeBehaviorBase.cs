using Xamarin.Forms;

namespace AsNum.XFControls.Behaviors {
    public abstract class SelectChangeBehaviorBase : BindableBehavior<VisualElement> {

        #region IsSelected
        public static readonly BindableProperty IsSelectedProperty =
            BindableProperty.Create("IsSelected",
                typeof(bool),
                typeof(SelectChangeBehaviorBase),
                false,
                BindingMode.Default,
                propertyChanged: IsSelectedChanged);

        public bool IsSelected {
            get {
                return (bool)GetValue(IsSelectedProperty);
            }
            set {
                SetValue(IsSelectedProperty, value);
            }
        }

        private static void IsSelectedChanged(BindableObject bindable, object oldvalue, object newvalue) {
            var behavior = (SelectChangeBehaviorBase)bindable;

            behavior.OnSelectedChanged();
        }
        #endregion


        protected virtual void OnSelectedChanged() {

        }
    }
}
