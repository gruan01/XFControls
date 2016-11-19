using Android.Widget;
using AsNum.XFControls.Droid.Services;
using AsNum.XFControls.Services;
using System;
using Xamarin.Forms;

[assembly: Dependency(typeof(ToastImpl))]
namespace AsNum.XFControls.Droid.Services {
    public class ToastImpl : IToast {

        public void Show(string msg, bool longShow = false) {
            try {
                //Looper.PrepareMainLooper();
                //var toast = Toast.MakeText(Forms.Context, msg, longShow ? ToastLength.Long : ToastLength.Short);
                //toast.Show();
                //Looper.Loop();
                Device.BeginInvokeOnMainThread(() => {
                    var toast = Toast.MakeText(Forms.Context, msg, longShow ? ToastLength.Long : ToastLength.Short);
                    toast.Show();
                    toast.Dispose();
                });
            }
            catch (Exception) {

            }
        }

    }
}