using System;
using AsNum.XFControls.Templates;
using Xamarin.Forms;

namespace AsNum.XFControls {
    /// <summary>
    /// 单选按钮(Button)组
    /// </summary>
    public class RadioButtonGroup : RadioGroupBase {

        #region ShowRadio
        /// <summary>
        /// 是否显示图标
        /// </summary>
        public static BindableProperty ShowRadioProperty =
            BindableProperty.Create("ShowRadio",
                            typeof(bool),
                            typeof(RadioButtonGroup),
                            false
                );

        /// <summary>
        /// 是否显示图标
        /// </summary>
        public bool ShowRadio {
            get {
                return (bool)this.GetValue(ShowRadioProperty);
            }
            set {
                this.SetValue(ShowRadioProperty, value);
            }
        }
        #endregion


        public RadioButtonGroup() {
            this.SelectedItemControlTemplate = new DefaultRadioButtonSelectedControlTemplate();
            this.UnSelectedItemControlTemplate = new DefaultRadioButtonUnSelectedControlTemplate();
        }

        protected override Layout<View> GetContainer() {
            var layout = new WrapLayout();
            layout.SetBinding(View.HorizontalOptionsProperty, new Binding(nameof(this.HorizontalOptions), source: this));
            return layout;
        }

        protected override Radio GetRadio(object data) {
            var radio = base.GetRadio(data);
            radio.SetBinding(Radio.ShowRadioProperty, new Binding("ShowRadio", source: this));
            radio.Content.HorizontalOptions = LayoutOptions.FillAndExpand;
            radio.TextAlignment = TextAlignment.Center;

            if (!this.ShowRadio) {
                ((Grid)radio.Content).ColumnSpacing = 0;
            }

            return radio;
        }

        protected override StackOrientation GetOrientation()
        {
            return this.Orientation;
        }
    }
}
