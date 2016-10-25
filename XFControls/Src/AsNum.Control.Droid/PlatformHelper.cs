using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace AsNum.XFControls.Droid {
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