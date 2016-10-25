using Android.Views.InputMethods;
using Android.Widget;
using AsNum.XFControls.Droid.Effects;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using A = AsNum.XFControls.Effects;

//同一个解决方案只允许一个
//[assembly: ResolutionGroupName("AsNum")]
[assembly: ExportEffect(typeof(KeyboardEnterEffect), "KeyboardEnterEffect")]
namespace AsNum.XFControls.Droid.Effects {
    public class KeyboardEnterEffect : PlatformEffect {

        protected override void OnAttached() {
            if (this.Control is EditText) {
                this.Update();
            }
        }

        protected override void OnDetached() {

        }

        protected override void OnElementPropertyChanged(PropertyChangedEventArgs args) {
            base.OnElementPropertyChanged(args);

            if (args.PropertyName.Equals(A.KeyboardEnter.TypeProperty.PropertyName)) {
                this.Update();
            }
        }

        private void Update() {
            var txt = (EditText)this.Control;
            txt.ImeOptions = this.Convert(A.KeyboardEnter.GetType(this.Element));
            txt.EditorAction += Txt_EditorAction;
        }

        private void Txt_EditorAction(object sender, TextView.EditorActionEventArgs e) {
            var cmd = (ICommand)this.Element.GetValue(A.KeyboardEnter.CmdProperty);
            var param = this.Element.GetValue(A.KeyboardEnter.ParamProperty);
            if (cmd != null && cmd.CanExecute(param)) {
                cmd.Execute(param);
            }
        }

        private ImeAction Convert(A.KeyboardEnterTypes key) {
            switch (key) {
                case A.KeyboardEnterTypes.Done:
                    return ImeAction.Done;
                case A.KeyboardEnterTypes.Go:
                    return ImeAction.Go;
                case A.KeyboardEnterTypes.Next:
                    return ImeAction.Next;
                case A.KeyboardEnterTypes.Search:
                    return ImeAction.Search;
                case A.KeyboardEnterTypes.Send:
                    return ImeAction.Send;
                default:
                    return ImeAction.Unspecified;
            }
        }
    }
}