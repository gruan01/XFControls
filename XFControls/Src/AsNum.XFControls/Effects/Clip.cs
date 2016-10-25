using System;
using System.Linq;
using Xamarin.Forms;

namespace AsNum.XFControls.Effects {

    [Obsolete("未完成")]
    public class Clip {

        #region Bounds
        public static BindableProperty BoundsProperty =
            BindableProperty.CreateAttached("Bounds",
                typeof(Thickness?),
                typeof(Clip),
                null,
                propertyChanged: Changed
                );

        public static Thickness GetBounds(BindableObject view) {
            return (Thickness)view.GetValue(BoundsProperty);
        }

        private static void Changed(BindableObject bindable, object oldValue, object newValue) {
            var view = (View)bindable;
            if (view != null) {
                var effect = view.Effects.FirstOrDefault(e => e is ClipEffect);
                if (effect == null) {
                    effect = new ClipEffect();
                    view.Effects.Add(effect);
                }
            }
        }
        #endregion


        class ClipEffect : RoutingEffect {
            public ClipEffect()
                : base("AsNum.ClipEffect") {
            }
        }
    }
}
