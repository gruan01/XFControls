using System.Threading.Tasks;
using Xamarin.Forms;

namespace AsNum.XFControls.Behaviors {

    //https://adventuresinxamarinforms.com/2015/04/20/animating-the-tabbedview/
    public class FadeBehavior : SelectChangeBehaviorBase {

        public uint FadeInAnimationLength { get; set; }

        public uint FadeOutAnimationLength { get; set; }

        public FadeBehavior() {
            FadeInAnimationLength = 250;
            FadeOutAnimationLength = 350;
        }

        protected override void OnAttachedTo(VisualElement visualElement) {
            base.OnAttachedTo(visualElement);

            if (!this.IsSelected) {
                visualElement.Opacity = 0;
                visualElement.IsVisible = false;
            }
        }

		protected override void OnSelectedChanged()
		{
			base.OnSelectedChanged();

			this.Animate();
		}

        private void Animate() {

            if (this.IsSelected)
                AssociatedObject.IsVisible = true;

            AssociatedObject.FadeTo(
                this.IsSelected ? 1 : 0,
                this.IsSelected ? FadeInAnimationLength : FadeOutAnimationLength,
                Easing.Linear
                ).ContinueWith(x => {
                    if (!IsSelected)
                        AssociatedObject.IsVisible = false;
                }, TaskScheduler.FromCurrentSynchronizationContext());
        }
    }
}
