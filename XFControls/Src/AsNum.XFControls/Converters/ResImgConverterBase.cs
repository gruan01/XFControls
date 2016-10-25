using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using Xamarin.Forms;

namespace AsNum.XFControls.Converters {
    /// <summary>
    /// <remark>
    /// 由于GetCallingAssembly返回的是调用处的 Assembly, 
    /// 在 Xaml 的 Converter 中指定，会返回 Xamarin.Forms 程序集
    /// 在 Xaml 中也不方便使用 Assembly
    /// 所以，这里是抽象类，如果需要，需要在使用的地方继承这个类
    /// </remark>
    /// </summary>
    public abstract class ResImgConverterBase : IValueConverter {

        public virtual Assembly Asm {
            get {
                return this.GetType().GetTypeInfo().Assembly;
            }
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            var source = (string)value;
            if (string.IsNullOrWhiteSpace(source))
                return null;

            //BUG, 不能使用已经关闭的 Stream
            //var stm = this.Asm.GetManifestResourceStream(source);
            //return ImageSource.FromStream(() => stm);

            using(var stm = this.Asm.GetManifestResourceStream(source)) {
                var bytes = stm.GetBytes();
                return ImageSource.FromStream(() => new MemoryStream(bytes));
            }

            ////ImageSource.FromResource 会去反射获取 CallingAssembly
            ////但是如果在这里调用 ImageSource.FromResource, CallingAssembly 就变成当前这个类所在的 Assembly 了。
            //var callingAssemblyMethod = typeof(Assembly).GetTypeInfo().GetDeclaredMethod("GetCallingAssembly");
            //if (callingAssemblyMethod != null) {
            //    var asm = (Assembly)callingAssemblyMethod.Invoke(null, new object[0]);
            //    //var ress = asm.GetManifestResourceNames();
            //    var stm = asm.GetManifestResourceStream(source);
            //    return ImageSource.FromStream(() => {
            //        return stm;
            //    });
            //} else {
            //    var img = ImageSource.FromResource(source);
            //    return img;
            //}

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
