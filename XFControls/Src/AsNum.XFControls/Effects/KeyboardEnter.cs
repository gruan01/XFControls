using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace AsNum.XFControls.Effects {
    public class KeyboardEnter {

        #region Cmd
        public static readonly BindableProperty CmdProperty =
            BindableProperty.CreateAttached("Cmd",
                typeof(ICommand),
                typeof(KeyboardEnter),
                null
                );

        public static ICommand GetCmd(BindableObject view) {
            return (ICommand)view.GetValue(CmdProperty);
        }
        #endregion

        #region Param
        public static readonly BindableProperty ParamProperty =
            BindableProperty.CreateAttached("Param",
                typeof(object),
                typeof(KeyboardEnter),
                null
                );

        public static object GetParam(BindableObject view) {
            return view.GetValue(ParamProperty);
        }
        #endregion

        #region Type
        public static readonly BindableProperty TypeProperty =
            BindableProperty.CreateAttached("Type",
                typeof(KeyboardEnterTypes?),
                typeof(KeyboardEnter),
                null,
                propertyChanged: Changed);

        public static void SetType(BindableObject view, KeyboardEnterTypes key) {
            if (view is Entry)
                view.SetValue(TypeProperty, key);
        }

        public static KeyboardEnterTypes GetType(BindableObject view) {
            return (KeyboardEnterTypes)view.GetValue(TypeProperty);
        }

        private static void Changed(BindableObject bindable, object oldValue, object newValue) {
            var view = (View)bindable;
            if (view != null) {
                var effect = view.Effects.FirstOrDefault(e => e is KeyboardEnterEffect);
                if (effect == null) {
                    effect = new KeyboardEnterEffect();
                    view.Effects.Add(effect);
                }
            }
        }
        #endregion

        class KeyboardEnterEffect : RoutingEffect {
            public KeyboardEnterEffect()
                : base("AsNum.KeyboardEnterEffect") {
            }
        }
    }


    public enum KeyboardEnterTypes {
        Default,
        Go,
        Search,
        Send,
        Next,
        Done
    }
}
