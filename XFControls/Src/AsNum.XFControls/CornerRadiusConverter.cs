using System;
using System.Globalization;
using Xamarin.Forms;

namespace AsNum.XFControls {

    /// <summary>
    /// CornerRadius 的 XAML 转换器
    /// </summary>
    public class CornerRadiusConverter : TypeConverter {

        public override bool CanConvertFrom(Type sourceType) {
            if (sourceType == null) {
                throw new ArgumentNullException("sourceType");
            }
            return sourceType == typeof(string);
        }


        public override object ConvertFromInvariantString(string text) {
            if (text == null) {
                return null;
            }
            
            if (text != null) {
                string[] array = text.Split(new char[]
                {
                    ','
                });

                switch (array.Length) {
                    case 1: {
                            double num;
                            if (double.TryParse(array[0], NumberStyles.Number, CultureInfo.InvariantCulture, out num)) {
                                return new CornerRadius(num);
                            }
                            break;
                        }
                    case 2: {
                            double num;
                            double num2;
                            if (double.TryParse(array[0], NumberStyles.Number, CultureInfo.InvariantCulture, out num) && double.TryParse(array[1], NumberStyles.Number, CultureInfo.InvariantCulture, out num2)) {
                                return new CornerRadius(num, num2);
                            }
                            break;
                        }
                    case 4: {
                            double num;
                            double num2;
                            double right;
                            double bottom;
                            if (double.TryParse(array[0], NumberStyles.Number, CultureInfo.InvariantCulture, out num) && double.TryParse(array[1], NumberStyles.Number, CultureInfo.InvariantCulture, out num2) && double.TryParse(array[2], NumberStyles.Number, CultureInfo.InvariantCulture, out right) && double.TryParse(array[3], NumberStyles.Number, CultureInfo.InvariantCulture, out bottom)) {
                                return new CornerRadius(num, num2, right, bottom);
                            }
                            break;
                        }
                }
            }
            throw new InvalidOperationException(string.Format("Cannot convert \"{0}\" into {1}", new object[]
            {
                text,
                typeof(CornerRadius)
            }));
        }

    }
}
