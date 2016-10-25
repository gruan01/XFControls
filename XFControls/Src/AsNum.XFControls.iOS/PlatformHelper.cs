using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

namespace AsNum.XFControls.iOS {
    internal static class PlatformHelper {

        public static IVisualElementRenderer GetOrCreateRenderer(this VisualElement bindable) {
            var renderer = Platform.GetRenderer(bindable);
            if (renderer == null) {
                renderer = Platform.CreateRenderer(bindable);
                Platform.SetRenderer(bindable, renderer);
            }
            return renderer;
        }

    }
}
