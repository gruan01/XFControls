using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Example
{
	public partial class FlipExample : ContentPage
	{

		//public IEnumerable<FlipItem> FlipSource { get; }
		//	= new List<FlipItem>() {
		//		new FlipItem() {
		//			Desc = "合肥火车站小旅馆生存录",
		//			Img = "http://imgsize.ph.126.net/?imgurl=http://dingyue.nosdn.127.net/VMlSQ5f5qzJoVIQa5dobBEfiS2T1qvb1=rsqQSodsIity1477383717798compressflag.jpg_750x380x1x45.jpg&enlarge=true"
		//		},
		//		new FlipItem() {
		//			Desc = "2017国考人数达133.8万 近历史最高值",
		//			Img = "http://imgsize.ph.126.net/?imgurl=http://cms-bucket.nosdn.127.net/b17ee3f6b826475c86d9d4a54754b01020161025072256.jpeg_750x380x1x45.jpg&enlarge=true"
		//		},
		//					new FlipItem() {
		//			Desc = "新西兰发生8.0级地震 官方发海啸预警",
		//			Img = "http://imgsize.ph.126.net/?imgurl=http://cms-bucket.nosdn.127.net/a5ec6facbff046b6a5af74dabdbb91b020161113210803.jpeg_750x380x1x45.jpg&enlarge=truee"
		//		},
		//					new FlipItem() {
		//			Desc = "武汉长江漂水葫芦 堆积岸边如\"草坪\"",
		//			Img = "http://imgsize.ph.126.net/?imgurl=http://cms-bucket.nosdn.127.net/913464aa81f34ddd91e4a1ef8a072f5920161113090513.jpeg_750x380x1x45.jpg&enlarge=true"
		//		},
		//					new FlipItem() {
		//			Desc = "江苏办人才交流会 千名台湾青年求职",
		//			Img = "http://imgsize.ph.126.net/?imgurl=http://cms-bucket.nosdn.127.net/2350bd7bb2514c67a2077965f4068c5e20161113080628.jpeg_750x380x1x45.jpg&enlarge=true"
		//		},
		//					new FlipItem() {
		//			Desc = "中国歼10女飞行员余旭牺牲 生前照曝光",
		//			Img = "http://imgsize.ph.126.net/?imgurl=http://cms-bucket.nosdn.127.net/d7c4402fb07f4b7aba023e6082e20f4520161113011158.jpeg_750x380x1x45.jpg&enlarge=true"
		//		}
		//	};

		public IEnumerable<FlipItem> FlipSource { get; }
			= new List<FlipItem>() {
				new FlipItem() {
					Desc = "合肥火车站小旅馆生存录",
					Img = "i1.jpg"
				},
				new FlipItem() {
					Desc = "2017国考人数达133.8万 近历史最高值",
					Img = "i2.jpg"
				},
							new FlipItem() {
					Desc = "新西兰发生8.0级地震 官方发海啸预警",
					Img = "i3.jpg"
				},
							new FlipItem() {
					Desc = "武汉长江漂水葫芦 堆积岸边如\"草坪\"",
					Img = "i4.jpg"
				},
							new FlipItem() {
					Desc = "江苏办人才交流会 千名台湾青年求职",
					Img = "i5.jpg"
				},
							new FlipItem() {
					Desc = "中国歼10女飞行员余旭牺牲 生前照曝光",
					Img = "i6.jpg"
				}
			};

		public FlipExample()
		{
			InitializeComponent();

			this.BindingContext = this;
		}
	}
}
