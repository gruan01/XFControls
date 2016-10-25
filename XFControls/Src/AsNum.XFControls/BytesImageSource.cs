using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AsNum.XFControls {
    public class BytesImageSource : StreamImageSource {

        public BytesImageSource(byte[] bytes) {
            this.Stream = cancel => {
                var msm = new MemoryStream(bytes);
                return Task.FromResult<Stream>(msm);
            };
        }
    }
}
