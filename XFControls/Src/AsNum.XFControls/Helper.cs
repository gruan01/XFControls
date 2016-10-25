using Microsoft.CSharp.RuntimeBinder;
using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace AsNum.XFControls {
    internal static class Helper {

        // http://www.cnblogs.com/LoveJenny/archive/2011/07/07/2100416.html
        // http://stackoverflow.com/questions/4939508/get-value-of-c-sharp-dynamic-property-via-string
        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static object GetProperty(object target, string name) {
            if (target == null || name == null)
                return null;

            var site = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(0, name, target.GetType(), new[] { CSharpArgumentInfo.Create(0, null) }));
            return site.Target(site, target);
        }

        public static object TryGetProperty(object target, string name) {
            try {
                return GetProperty(target, name);
            } catch {
                return null;
            }
        }

        public static T GetProperty<T>(object target, string name, T defaultValue = default(T)) {
            try {
                return (T)GetProperty(target, name);
            } catch {
                return defaultValue;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="stm"></param>
        /// <param name="perCount"></param>
        /// <returns></returns>
        public static byte[] GetBytes(this Stream stm, int perCount = 1024) {
            if (stm == null)
                throw new ArgumentNullException("stm");
            if (perCount <= 0)
                throw new ArgumentOutOfRangeException("perCount", "perCount 必须大于等于0");

            if (stm.CanSeek)
                stm.Position = 0;

            byte[] bytes = new byte[stm.Length];
            var offset = 0;
            var count = 0;
            while (0 != (count = stm.Read(bytes, offset, stm.Length - stm.Position > perCount ? perCount : (int)(stm.Length - stm.Position)))) {
                offset += count;
            }

            return bytes;
        }
    }
}
