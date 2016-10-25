using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Example {
    public partial class Home : ContentPage {

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

        public IEnumerable<string> MarqueeSource { get; }
            = new List<string>() {
                "女子乐队“九野”：她们唱出女人的心酸",
                "跟着伊一天悦一起激情巴厘岛",
                "【股市大直播】季报给力！这些股利润惊人"
            };

        public Home() {
            InitializeComponent();

            this.BindingContext = this;
        }
    }


    public class FlipItem {

        public string Desc { get; set; }

        public string Img { get; set; }
    }
}
