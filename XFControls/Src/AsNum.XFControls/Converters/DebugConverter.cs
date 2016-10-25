using System;
using System.Globalization;
using Xamarin.Forms;

namespace AsNum.XFControls.Converters {
    internal class DebugConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
