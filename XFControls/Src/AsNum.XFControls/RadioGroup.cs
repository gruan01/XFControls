using Xamarin.Forms;

namespace AsNum.XFControls {
    /// <summary>
    /// 单选按钮组
    /// </summary>
    public class RadioGroup : RadioGroupBase {

        #region Orientation
        /// <summary>
        /// 方向
        /// </summary>
        public static readonly BindableProperty OrientationProperty =
            BindableProperty.Create("Orientation",
                                    typeof(StackOrientation),
                                    typeof(RadioGroup),
                                    StackOrientation.Horizontal,
                                    propertyChanged: OrientationChanged);


        private static void OrientationChanged(BindableObject bindable, object oldValue, object newValue) {
            var rg = (RadioGroup)bindable;
            ((StackLayout)rg.Container).Orientation = (StackOrientation)newValue;
            
        }

        /// <summary>
        /// 方向
        /// </summary>
        public StackOrientation Orientation {
            get {
                return (StackOrientation)this.GetValue(OrientationProperty);
            }
            set {
                this.SetValue(OrientationProperty, value);
            }
        }
        #endregion

        protected override Layout<View> GetContainer() {
            return new StackLayout() {
                Orientation = this.Orientation
            };
        }
    }
}
