using Xamarin.Forms;

namespace AsNum.XFControls {
    internal static class RepeaterContainerFactory {
        public static RepeaterContainerBase Get(RepeaterOrientation orientation) {
            switch (orientation) {
                case RepeaterOrientation.Horizontal:
                    return new HorizontalRepeaterContainer();
                case RepeaterOrientation.Vertical:
                    return new VerticalRepeaterContainer();
                default:
                    return new WrapRepeaterContainer();
            }
        }
    }

    internal abstract class RepeaterContainerBase {

        public abstract Layout<View> Layout {
            get;
        }

    }

    internal class WrapRepeaterContainer : RepeaterContainerBase {

        public override Layout<View> Layout {
            get {
                return new WrapLayout();
            }
        }
    }

    internal class VerticalRepeaterContainer : RepeaterContainerBase {
        public override Layout<View> Layout {
            get {
                return new StackLayout() {
                    Orientation = StackOrientation.Vertical
                };
            }
        }
    }

    internal class HorizontalRepeaterContainer : RepeaterContainerBase {
        public override Layout<View> Layout {
            get {
                return new StackLayout() {
                    Orientation = StackOrientation.Horizontal
                };
            }
        }
    }
}
