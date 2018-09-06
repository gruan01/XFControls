using AsNum.XFControls.Binders;
using LabelHtml.Forms.Plugin.Abstractions;
using System.Windows.Input;
using Xamarin.Forms;

namespace AsNum.XFControls
{
    /// <summary>
    /// 单选按钮(模拟)
    /// </summary>
    public class Radio : ContentView
    {


        #region value
        /// <summary>
        /// 单选项的值
        /// </summary>
        public static BindableProperty ValueProperty =
            BindableProperty.Create("Value",
                typeof(object),
                typeof(Radio));

        /// <summary>
        /// 单选项的值
        /// </summary>
        public object Value
        {
            get
            {
                return this.GetValue(ValueProperty);
            }
            set
            {
                this.SetValue(ValueProperty, value);
            }
        }
        #endregion

        #region isSelected
        /// <summary>
        /// 是否选中
        /// </summary>
        public static BindableProperty IsSelectedProperty =
            BindableProperty.Create("IsSelected",
                typeof(bool),
                typeof(Radio),
                false,
                BindingMode.TwoWay,
                propertyChanged: IsSelectedChanged
                );

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bindable"></param>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        private static void IsSelectedChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var radio = (Radio)bindable;
            var source = (bool)newValue ? radio.OnImg : radio.OffImg;
            radio.Icon.Source = source;
        }

        /// <summary>
        /// 是否选中
        /// </summary>
        public bool IsSelected
        {
            get
            {
                return (bool)this.GetValue(IsSelectedProperty);
            }
            set
            {
                this.SetValue(IsSelectedProperty, value);
            }
        }
        #endregion

        #region Text
        /// <summary>
        /// 标签文本
        /// </summary>
        public static readonly BindableProperty TextProperty =
            BindableProperty.Create("Text",
                                    typeof(string),
                                    typeof(Radio),
                                    "",
                                    propertyChanged: TextChanged
                                    );

        private static void TextChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var radio = (Radio)bindable;
            radio.Lbl.Text = (string)newValue;
        }

        /// <summary>
        /// 标签文本
        /// </summary>
        public string Text
        {
            get
            {
                return (string)this.GetValue(TextProperty);
            }
            set
            {
                this.SetValue(TextProperty, value);
            }
        }
        #endregion

        #region TextAlignment
        /// <summary>
        /// 标签文本的对齐方式
        /// </summary>
        public static readonly BindableProperty TextAlignmentProperty =
            BindableProperty.Create("TextAlignment",
                typeof(TextAlignment),
                typeof(Radio),
                TextAlignment.Start
                );

        /// <summary>
        /// 标签文本的对齐方式
        /// </summary>
        public TextAlignment TextAlignment
        {
            get
            {
                return (TextAlignment)this.GetValue(TextAlignmentProperty);
            }
            set
            {
                this.SetValue(TextAlignmentProperty, value);
            }
        }
        #endregion

        #region textColor
        public static readonly BindableProperty TextColorProperty =
            BindableProperty.Create("TextColor",
                typeof(Color),
                typeof(Radio),
                Color.Black,
                propertyChanged: ColorTextChanged);

        public Color TextColor
        {
            get
            {
                return (Color)this.GetValue(TextColorProperty);
            }
            set
            {
                this.SetValue(TextColorProperty, value);
            }
        }
        private static void ColorTextChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var chk = (Radio)bindable;
            chk.Lbl.TextColor = (Color)newValue;
        }
        #endregion

        #region Size
        /// <summary>
        /// 单选按钮的大小, 对标签文本无效
        /// </summary>
        internal static readonly BindableProperty SizeProperty =
            BindableProperty.Create("Size",
                                    typeof(double),
                                    typeof(Radio),
                                    25D,
                                    propertyChanged: IconSizeChanged);
        /// <summary>
        /// 单选按钮的大小, 对标签文本无效
        /// </summary>
        internal double Size
        {
            get
            {
                return (double)this.GetValue(SizeProperty);
            }
            set
            {
                this.SetValue(SizeProperty, value);
            }
        }

        private static void IconSizeChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var chk = (Radio)bindable;
            chk.Icon.WidthRequest = chk.Icon.HeightRequest = (double)newValue;

        }
        #endregion

        #region ShowRadio
        /// <summary>
        /// 是否显示按钮图标(用于RadioButton)
        /// </summary>
        public static readonly BindableProperty ShowRadioProperty =
            BindableProperty.Create("ShowRadio",
                                    typeof(bool),
                                    typeof(Radio),
                                    true
                                    );
        /// <summary>
        /// 是否显示按钮图标(用于RadioButton)
        /// </summary>
        public bool ShowRadio
        {
            get
            {
                return (bool)this.GetValue(ShowRadioProperty);
            }
            set
            {
                this.SetValue(ShowRadioProperty, value);
            }
        }
        #endregion


        #region ImgSource
        public static readonly BindableProperty OnImgProperty =
            BindableProperty.Create("OnImg",
                typeof(ImageSource),
                typeof(Radio),
                ImageSource.FromResource("AsNum.XFControls.Imgs.Radio-Checked.png"),
                propertyChanged: ImgSourceChanged
                );

        public ImageSource OnImg
        {
            get
            {
                return (ImageSource)this.GetValue(OnImgProperty);
            }
            set
            {
                this.SetValue(OnImgProperty, value);
            }
        }

        public static readonly BindableProperty OffImgProperty =
            BindableProperty.Create("OffImg",
                typeof(ImageSource),
                typeof(Radio),
                ImageSource.FromResource("AsNum.XFControls.Imgs.Radio-Unchecked.png"),
        propertyChanged: ImgSourceChanged
                );

        public ImageSource OffImg
        {
            get
            {
                return (ImageSource)this.GetValue(OffImgProperty);
            }
            set
            {
                this.SetValue(OffImgProperty, value);
            }
        }

        private static void ImgSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var chk = (Radio)bindable;
            chk.UpdateImageSource(chk.OnImg, chk.OffImg);
        }

        private void UpdateImageSource(ImageSource on, ImageSource off)
        {
            this.Icon.Source = this.IsSelected ? on : off;
        }

        #endregion


        private Image Icon;
        //private Label Lbl;
        private HtmlLabel Lbl;
        public StackOrientation RadioOrientation;

        public Radio()
        {
            Create();
        }
        public Radio(bool isHorizontal)
        {
            this.RadioOrientation = isHorizontal ? StackOrientation.Horizontal : StackOrientation.Vertical;
            Create();

        }
        public void Create()
        {
            //var layout = new StackLayout() {
            //    Orientation = StackOrientation.Horizontal
            //};


            var layout = new Grid()
            {
                ColumnDefinitions = new ColumnDefinitionCollection(),
                HorizontalOptions = LayoutOptions.StartAndExpand,
                VerticalOptions = LayoutOptions.StartAndExpand
            };
            layout.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
            layout.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Star });
            this.Content = layout;

            this.Icon = new Image()
            {
                Source = this.OffImg,
                WidthRequest = this.Size,
                HeightRequest = this.Size
            };
            this.Icon.SetBinding(Image.IsVisibleProperty, new Binding("ShowRadio", source: this));
            layout.Children.Add(this.Icon);
            Grid.SetColumn(this.Icon, 0);

            this.Lbl = new HtmlLabel()
            {
                VerticalTextAlignment = TextAlignment.Center,
                //HorizontalOptions = LayoutOptions.Fill,
                //HorizontalTextAlignment = TextAlignment.Center,
                //BackgroundColor = Color.Yellow
            };
            this.Lbl.SetBinding(Label.HorizontalTextAlignmentProperty, new Binding("TextAlignment", source: this));
            this.Lbl.Text = this.Text;
            this.Lbl.SetBinding(Label.TextColorProperty, new Binding("TextColor", source: this));
            this.Lbl.TextColor = this.TextColor;
            layout.Children.Add(this.Lbl);
            if (this.RadioOrientation.Equals(StackOrientation.Vertical))
                Grid.SetRow(this.Lbl, 1);//Vertical
            else
                Grid.SetColumn(this.Lbl, 1); //Horizontal
        }

        internal void SetTap(ICommand cmd)
        {
            TapBinder.SetCmd(this.Content, cmd);
            TapBinder.SetParam(this.Content, this);
        }
    }
}
