using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Views;
using Android.Widget;
using System;

namespace AsNum.XFControls.Droid {

    /// <summary>
    /// http://www.netmite.com/android/mydroid/cupcake/frameworks/base/core/java/com/android/internal/widget/NumberPicker.java
    /// 不能正常使用， 构造函数居然在 AddView / OnViewAdded 后面执行，导致颜色设置无效
    /// </summary>
    public class ColorNumberPicker : NumberPicker {

        private Color TextColor { get; }

        public ColorNumberPicker(Context ctx, Color textColor, Color dividerColor)
            : base(ctx) {

            this.TextColor = textColor;
            this.setDividerColor(dividerColor);
        }

        //public override void AddView(View child) {
        //    base.AddView(child);
        //    this.UpdateColor(child);
        //}

        //public override void AddView(View child, int index) {
        //    base.AddView(child, index);
        //    this.UpdateColor(child);
        //}

        //public override void AddView(View child, int index, ViewGroup.LayoutParams @params) {
        //    base.AddView(child, index, @params);
        //    this.UpdateColor(child);
        //}

        //public override void AddView(View child, int width, int height) {
        //    base.AddView(child, width, height);
        //    this.UpdateColor(child);
        //}

        //public override void AddView(View child, ViewGroup.LayoutParams @params) {
        //    base.AddView(child, @params);

        //    this.UpdateColor(child);
        //}

        //public override void OnViewAdded(View child) {
        //    base.OnViewAdded(child);

        //    this.UpdateColor(child);
        //}

        private void UpdateColor(View v) {
            if (this.TextColor == null)
                return;

            if (v is EditText) {
                var edt = (EditText)v;
                edt.SetTextColor(this.TextColor);
                edt.TextSize = 25;
            }
        }

        public void setDividerColor(Color color) {
            if (color == null)
                return;

            try {
                var fs = base.Class.GetDeclaredFields();
                var f = base.Class.GetDeclaredField("mSelectionDivider");
                f.Accessible = true;

                var d = (Drawable)f.Get(this);
                d.SetColorFilter(Color.Green, PorterDuff.Mode.SrcAtop);
                d.InvalidateSelf();
                this.PostInvalidate(); // Drawable is dirty
            } catch (Exception) {

            }
        }
    }
}