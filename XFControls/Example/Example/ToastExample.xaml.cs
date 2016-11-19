using AsNum.XFControls.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Example {
    public partial class ToastExample : ContentPage {

        public ICommand Show { get; }

        public ToastExample() {
            InitializeComponent();

            this.Show = new Command((o) => {
                var msg = (string)o;
                if (string.IsNullOrWhiteSpace(msg))
                    msg = "Not set toast message";

                DependencyService.Get<IToast>()
                    .Show(msg, true);
            });


            this.BindingContext = this;
        }
    }
}
