using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using XF = Xamarin.Forms;

namespace AsNum.XFControls.UWP {
    internal static class Helper {
        public static Brush ToBrush(this XF.Color color) {
            return new SolidColorBrush(color.ToMediaColor());
        }

        public static Color ToMediaColor(this XF.Color color) {
            return Color.FromArgb((byte)(color.A * 255), (byte)(color.R * 255), (byte)(color.G * 255), (byte)(color.B * 255));
        }

        public static Thickness ToWinPhone(this XF.Thickness t) {
            return new Thickness(t.Left, t.Top, t.Right, t.Bottom);
        }

        //public static T FindChildControl<T>(this DependencyObject control, string ctrlName) where T : UIElement {
        //    int childNumber = VisualTreeHelper.GetChildrenCount(control);
        //    for (int i = 0; i < childNumber; i++) {
        //        DependencyObject child = VisualTreeHelper.GetChild(control, i);
        //        FrameworkElement fe = child as FrameworkElement;
        //        // Not a framework element or is null
        //        if (fe == null) return default(T);


        //        if (child is T && fe.Name == ctrlName) {
        //            // Found the control so return
        //            return child as T;
        //        } else {
        //            // Not found it - search children
        //            T nextLevel = FindChildControl<T>(child, ctrlName);
        //            if (nextLevel != null)
        //                return nextLevel;
        //        }
        //    }
        //    return default(T);
        //}

        //public static FrameworkElement GetRootPage(this FrameworkElement control) {
        //    var p = control.Parent;
        //    while (p != null) {
        //        if (typeof(Page).IsInstanceOfType(p)) {
        //            break;
        //        } else
        //            p = (p as FrameworkElement).Parent;
        //    }

        //    return p as FrameworkElement;
        //}

        public static FontFamily ToFontFamily(this string ff) {
            //font file must in assets folder.
            ff = Regex.Replace(ff, "(^/Assets/)|(^Assets/)", "");

            // ff like : FontAwesome.otf
            // Full Path must like : Assets/Fonts/FontAwesome.otf#FontAwesome
            // font name must same as font file name
            var fontName = Path.GetFileNameWithoutExtension(ff);
            // not have prefix "/", if have preifx "/", Path.Combin will return fail path.
            string path = string.Format("Assets/{0}", ff);
            if (File.Exists(Path.Combine(AppContext.BaseDirectory, path))) {
                return new FontFamily(string.Format("/{0}#{1}", path, fontName));
            } else
                return FontFamily.XamlAutoFontFamily;
        }
    }
}
