using Xamarin.Forms;

namespace AsNum.XFControls.Behaviors {
    public class VisibilityBehavior : SelectChangeBehaviorBase {

        protected override void OnAttachedTo(VisualElement view) {
            base.OnAttachedTo(view);

            view.SetBinding(VisualElement.IsVisibleProperty, new Binding(nameof(this.IsSelected), source: this));
        }

        protected override void OnDetachingFrom(VisualElement view) {
            base.OnDetachingFrom(view);
            view.RemoveBinding(VisualElement.IsVisibleProperty);
        }
    }
}
