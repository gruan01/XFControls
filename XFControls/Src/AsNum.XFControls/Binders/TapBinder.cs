using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace AsNum.XFControls.Binders {
    public class TapBinder {

        #region Command
        public static readonly BindableProperty CmdProperty =
            BindableProperty.CreateAttached("Cmd",
                typeof(ICommand),
                typeof(TapBinder),
                null,
                propertyChanged: Changed);

        public static void SetCmd(BindableObject view, ICommand cmd) {
            view.SetValue(CmdProperty, cmd);
        }

        public static ICommand GetCmd(BindableObject view) {
            return (ICommand)view.GetValue(CmdProperty);
        }
        #endregion

        #region Param
        public static readonly BindableProperty ParamProperty =
            BindableProperty.CreateAttached("Param",
                typeof(object),
                typeof(TapBinder),
                null,
                propertyChanged: Changed);

        public static void SetParam(BindableObject view, object param) {
            view.SetValue(ParamProperty, param);
        }

        public static object GetParam(BindableObject view) {
            return view.GetValue(ParamProperty);
        }
        #endregion

        #region WithFeedback
        public static readonly BindableProperty WithFeedbackProperty =
            BindableProperty.CreateAttached("WithFeedback",
                typeof(bool),
                typeof(TapBinder),
                true,
                propertyChanged: Changed
                );

        public static void SetWithFeedback(BindableObject view, bool withFeedback) {
            view.SetValue(WithFeedbackProperty, withFeedback);
        }

        public static bool GetWithFeedback(BindableObject view) {
            return (bool)view.GetValue(WithFeedbackProperty);
        }
        #endregion

        private static void Changed(BindableObject bindable, object oldValue, object newValue) {
            var view = (View)bindable;

            var gesture = (TapGestureRecognizer)view.GestureRecognizers.FirstOrDefault(g => g is TapGestureRecognizer);

            if (gesture == null) {
                gesture = new TapGestureRecognizer();
                view.GestureRecognizers.Add(gesture);
            }
            gesture.Command = GetCmd(view);
            gesture.CommandParameter = GetParam(view);

            if (GetWithFeedback(bindable)) {
                var effect = view.Effects.FirstOrDefault(e => e is TapEffect);
                if (effect == null) {
                    effect = new TapEffect();
                    view.Effects.Add(effect);
                }
            }
            else {
                view.Effects.Remove(view.Effects.FirstOrDefault(e => e is TapEffect));
            }
        }




        class TapEffect : RoutingEffect {
            public TapEffect()
                : base("AsNum.TapEffect") {
            }
        }
    }
}
