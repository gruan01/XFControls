using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace AsNum.XFControls.UWP {
    public class Color2BrushConverter : IValueConverter {

        public object Convert(object value, Type targetType, object parameter, string language) {

            var c = (Color)value;
            return new SolidColorBrush(c);
        }


        public object ConvertBack(object value, Type targetType, object parameter, string language) {
            throw new NotImplementedException();
        }
    }
}
