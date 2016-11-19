using AsNum.XFControls.Services;
using AsNum.XFControls.UWP.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;


[assembly: Dependency(typeof(ToastImpl))]
namespace AsNum.XFControls.UWP.Services {
    public class ToastImpl : IToast {

        public void Show(string msg, bool longShow = false) {
            try {
                Device.BeginInvokeOnMainThread(() => {
                    Toast.Instance.Show(msg, longShow ? 3000 : 1000);
                });
            } catch (Exception) {

            }
        }

    }
}
