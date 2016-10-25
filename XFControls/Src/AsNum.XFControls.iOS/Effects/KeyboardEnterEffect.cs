using AsNum.XFControls.iOS.Effects;
using System;
using System.ComponentModel;
using System.Windows.Input;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using A = AsNum.XFControls.Effects;

//同一个解决方案只允许一个
//[assembly: ResolutionGroupName("AsNum")]
[assembly: ExportEffect(typeof(KeyboardEnterEffect), "KeyboardEnterEffect")]
namespace AsNum.XFControls.iOS.Effects {
    public class KeyboardEnterEffect : PlatformEffect {
        protected override void OnAttached() {
            if (this.Control is UITextField) {
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
            var txt = (UITextField)this.Control;

            txt.ReturnKeyType = this.Convert(A.KeyboardEnter.GetType(this.Element));
            //TODO
            txt.PrimaryActionTriggered += Txt_PrimaryActionTriggered;
        }

        private void Txt_PrimaryActionTriggered(object sender, EventArgs e) {
            var cmd = (ICommand)this.Element.GetValue(A.KeyboardEnter.CmdProperty);
            var param = this.Element.GetValue(A.KeyboardEnter.ParamProperty);
            if (cmd != null && cmd.CanExecute(param)) {
                cmd.Execute(param);
            }
        }

        private UIReturnKeyType Convert(A.KeyboardEnterTypes key) {
            switch (key) {
                case A.KeyboardEnterTypes.Done:
                    return UIReturnKeyType.Done;
                case A.KeyboardEnterTypes.Go:
                    return UIReturnKeyType.Go;
                case A.KeyboardEnterTypes.Next:
                    return UIReturnKeyType.Next;
                case A.KeyboardEnterTypes.Search:
                    return UIReturnKeyType.Search;
                case A.KeyboardEnterTypes.Send:
                    return UIReturnKeyType.Send;
                default:
                    return UIReturnKeyType.Default;
            }
        }
    }
}
