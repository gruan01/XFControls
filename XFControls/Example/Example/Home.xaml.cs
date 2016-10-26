using AsNum.XFControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using System.Windows.Input;
using System.ComponentModel;

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


        public IEnumerable<TabViewItem> TabViewSource { get; }
            = new List<TabViewItem>() {
                new TabViewItem() { Glyph = (char)0xe65e, Title = "生鲜果蔬" },
                new TabViewItem() { Glyph = (char)0xe61a, Title = "粮油米面" },
                new TabViewItem() { Glyph = (char)0xe64b, Title = "坚果副食" },
                new TabViewItem() { Glyph = (char)0xe608, Title = "鸡鸭禽蛋" },
                new TabViewItem() { Glyph = (char)0xe886, Title = "猪牛羊肉" },
                new TabViewItem() { Glyph = (char)0xe610, Title = "南北小吃" }
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



    /// <summary>
    /// Notice : TabView require DataSource's item implementation ISelectable
    /// </summary>
    public class TabViewItem : ISelectable, INotifyPropertyChanged {

        public char Glyph { get; set; }

        public bool IsSelected {
            get; set;
        }

        public ICommand SelectedCommand {
            get; set;
        }

        public string Title { get; set; }

        public ICommand UnSelectedCommand {
            get; set;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyOfPropertyChange(string propertyName) {
            this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
