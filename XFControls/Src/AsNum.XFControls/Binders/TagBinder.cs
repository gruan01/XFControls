using Xamarin.Forms;

namespace AsNum.XFControls.Binders {
    public class TagBinder {

        public static readonly BindableProperty TagProperty =
            BindableProperty.Create("Tag",
                typeof(object),
                typeof(TagBinder),
                null);

        public static object GetTag(BindableObject bindable) {
            return bindable.GetValue(TagProperty);
        }

        public static void SetTag(BindableObject bindable, object value) {
            bindable.SetValue(TagProperty, value);
        }
    }
}
