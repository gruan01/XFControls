using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AsNum.XFControls {
    public class RoundButton : Button {

        public static readonly BindableProperty RadiusProperty =
            BindableProperty.Create("Radius",
                typeof(long?),
                typeof(RoundButton),
                null);

    }
}
