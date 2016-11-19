using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Example {
    public partial class FlipExample : ContentPage {

        public IEnumerable<FlipItem> FlipSource { get; }
            = new List<FlipItem>() {
                new FlipItem() {
                    Desc = "合肥火车站小旅馆生存录",
                    Img = "http://imgsize.ph.126.net/?imgurl=http://dingyue.nosdn.127.net/VMlSQ5f5qzJoVIQa5dobBEfiS2T1qvb1=rsqQSodsIity1477383717798compressflag.jpg_750x380x1x45.jpg&enlarge=true"
                },
                new FlipItem() {
                    Desc = "2017国考人数达133.8万 近历史最高值",
                    Img = "http://imgsize.ph.126.net/?imgurl=http://cms-bucket.nosdn.127.net/b17ee3f6b826475c86d9d4a54754b01020161025072256.jpeg_750x380x1x45.jpg&enlarge=true"
                }
            };

        public FlipExample() {
            InitializeComponent();

            this.BindingContext = this;
        }
    }
}
