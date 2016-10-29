using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Util;
using Android.Views;
using Android.Widget;
using Java.Util.Jar;
using System;
using Xamarin.Forms;
using AView = Android.Views.View;
using AColor = Android.Graphics.Color;
using Java.Lang;


namespace AsNum.XFControls.Droid {

    ///// <summary>
    ///// NOT WORK CORRECTLLY
    ///// BECAUSE ADD VIEW INVOKED BEFORE CONSTRUCTOR
    ///// AND 
    ///// </summary>
    //public class ColorNumberPicker : NumberPicker {

    //    public AColor TextColor { get; }

    //    public ColorNumberPicker(Context ctx, AColor txtColor)
    //        : base(ctx) {

    //        this.TextColor = txtColor;

    //        /*
    //         * Notice : Because NumberPicker's AddView invoke bofore this constructor
    //         * So, TextColor can't apply
    //         */

    //        this.Update();
    //        this.SetDividerColor(txtColor);

    //        this.ChildViewAdded += ColorNumberPicker_ChildViewAdded;
    //    }

    //    private void ColorNumberPicker_ChildViewAdded(object sender, ChildViewAddedEventArgs e) {
    //        System.Diagnostics.Debug.WriteLine("22222");
    //    }

    //    public ColorNumberPicker(Context ctx, IAttributeSet attrs)
    //        : base(ctx, attrs) {

    //    }

    //    public ColorNumberPicker(Context ctx, IAttributeSet attrs, int defStyleAttr)
    //        : base(ctx, attrs, defStyleAttr) {
    //    }

    //    // AddView 先于构造函数运行，无法获取到 TextColor
    //    public override void AddView(AView child) {
    //        base.AddView(child);

    //        this.UpdateView(child);
    //        System.Diagnostics.Debug.WriteLine("1");
    //    }

    //    public override void AddView(AView child, int index, ViewGroup.LayoutParams @params) {
    //        base.AddView(child, index, @params);

    //        this.UpdateView(child);
    //        System.Diagnostics.Debug.WriteLine("A");
    //    }

    //    public override void AddView(AView child, int index) {
    //        base.AddView(child, index);
    //        System.Diagnostics.Debug.WriteLine("B");
    //    }

    //    public override void AddView(AView child, int width, int height) {
    //        base.AddView(child, width, height);
    //        System.Diagnostics.Debug.WriteLine("C");
    //    }

    //    public override void AddView(AView child, ViewGroup.LayoutParams @params) {
    //        base.AddView(child, @params);
    //        System.Diagnostics.Debug.WriteLine("D");
    //    }

    //    public override void OnViewAdded(AView child) {
    //        base.OnViewAdded(child);
    //        System.Diagnostics.Debug.WriteLine("E");
    //    }

    //    protected override bool AddViewInLayout(AView child, int index, ViewGroup.LayoutParams @params) {
    //        System.Diagnostics.Debug.WriteLine("F");
    //        return base.AddViewInLayout(child, index, @params);
    //    }

    //    protected override bool AddViewInLayout(AView child, int index, ViewGroup.LayoutParams @params, bool preventRequestLayout) {
    //        System.Diagnostics.Debug.WriteLine("G");
    //        return base.AddViewInLayout(child, index, @params, preventRequestLayout);
    //    }

    //    private void Update() {
    //        for (var i = 0; i < this.ChildCount; i++) {
    //            var c = this.GetChildAt(i);
    //            this.UpdateView(c);
    //        }
    //    }

    //    public void UpdateView(AView view) {
    //        if (view is EditText) {
    //            ((EditText)view).SetTextColor(this.TextColor);
    //        }
    //    }


    //    public void SetDividerColor(AColor color) {
    //        if (color == null)
    //            return;

    //        try {
    //            var fs = base.Class.GetDeclaredFields();
    //            var f = base.Class.GetDeclaredField("mSelectionDivider");
    //            f.Accessible = true;

    //            var d = (Drawable)f.Get(this);
    //            d.SetColorFilter(color, PorterDuff.Mode.SrcAtop);
    //            d.InvalidateSelf();
    //            this.PostInvalidate(); // Drawable is dirty
    //        } catch (Exception) {

    //        }
    //    }

    //}
}