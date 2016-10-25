using System.Diagnostics;
using Xamarin.Forms;

namespace AsNum.XFControls {

    /// <summary>
    /// 圆角半径(左上右下)
    /// </summary>
    [DebuggerDisplay("TopLeft={Left}, TopRight={Top}, BottomRight={Right}, BottomLeft={Bottom}")]
    [TypeConverter(typeof(CornerRadiusConverter))]
    public struct CornerRadius {

        public double TopLeft {
            get;
            set;
        }

        public double TopRight {
            get;
            set;
        }

        public double BottomRight {
            get;
            set;
        }

        public double BottomLeft {
            get;
            set;
        }

        public double HorizontalThickness {
            get {
                return this.TopLeft + this.BottomRight;
            }
        }


        public CornerRadius(double uniformSize) {
            this = new CornerRadius(uniformSize, uniformSize, uniformSize, uniformSize);
        }

        public CornerRadius(double horizontalSize, double verticalSize) {
            this = new CornerRadius(horizontalSize, verticalSize, horizontalSize, verticalSize);
        }

        public CornerRadius(double tl, double tr, double br, double bl) {
            this = default(CornerRadius);
            this.TopLeft = tl;
            this.TopRight = tr;
            this.BottomRight = br;
            this.BottomLeft = bl;
        }

        public static implicit operator CornerRadius(Size size) {
            return new CornerRadius(size.Width, size.Height, size.Width, size.Height);
        }


        public static implicit operator CornerRadius(double uniformSize) {
            return new CornerRadius(uniformSize);
        }

        private bool Equals(CornerRadius other) {
            return this.TopLeft.Equals(other.TopLeft)
                && this.TopRight.Equals(other.TopRight)
                && this.BottomRight.Equals(other.BottomRight)
                && this.BottomLeft.Equals(other.BottomLeft);
        }


        public static bool operator ==(CornerRadius left, CornerRadius right) {
            return left.Equals(right);
        }

        public static bool operator !=(CornerRadius left, CornerRadius right) {
            return !left.Equals(right);
        }

        public override int GetHashCode() {
            return ((this.TopLeft.GetHashCode() * 397
                ^ this.TopRight.GetHashCode()) * 397
                ^ this.BottomRight.GetHashCode()) * 397
                ^ this.BottomLeft.GetHashCode();
        }

        public override bool Equals(object obj) {
            return obj != null && obj is CornerRadius && this.Equals((CornerRadius)obj);
        }

    }
}
