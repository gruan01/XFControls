using System;
using Xamarin.Forms;

namespace AsNum.XFControls.Behaviors {
    public abstract class BindableBehavior<T> : Behavior<T> where T : BindableObject {
        public T AssociatedObject { get; private set; }

        protected override void OnAttachedTo(T view) {
            base.OnAttachedTo(view);

            this.AssociatedObject = view;

            if (view.BindingContext != null)
                this.BindingContext = view.BindingContext;

            view.BindingContextChanged += OnBindingContextChanged;
        }

        private void OnBindingContextChanged(object sender, EventArgs e) {
            this.OnBindingContextChanged();
        }

        protected override void OnDetachingFrom(T view) {
            view.BindingContextChanged -= this.OnBindingContextChanged;
        }

        protected override void OnBindingContextChanged() {
            base.OnBindingContextChanged();
            this.BindingContext = this.AssociatedObject.BindingContext;
        }
    }

    public abstract class BindableBehavoir : BindableBehavior<VisualElement> {

    }
}
