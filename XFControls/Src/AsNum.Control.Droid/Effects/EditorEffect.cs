using AsNum.XFControls.Binders;
using AsNum.XFControls.Droid.Effects;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportEffect(typeof(EditorEffect), "EditorEffect")]
namespace AsNum.XFControls.Droid.Effects {

    public class EditorEffect : PlatformEffect {

        protected override void OnAttached() {
            if (this.Element is Editor) {
                var ctrl = (EditorEditText)this.Control;
                ctrl.Hint = EditorBinder.GetPlaceHolder(this.Element);
                ctrl.SetHintTextColor(EditorBinder.GetPlaceHolderColor(this.Element).ToAndroid());
            }
        }

        protected override void OnDetached() {

        }
    }
}