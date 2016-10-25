using Android.Support.V4.View;
using Android.Views;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AsNum.XFControls.Droid {
    public class FlipViewAdapter : PagerAdapter, ViewPager.IOnPageChangeListener {

        public event EventHandler<PosChangedEventArgs> PosChanged = null;

        private List<View> Items;

        public override int Count {
            get {
                return this.Items.Count() * 2;
            }
        }

        private ViewPager ViewPager = null;

        public FlipViewAdapter(ViewPager vp) {
            this.ViewPager = vp;
        }

        public void SetItems(List<View> items) {
            if (items == null)
                throw new ArgumentNullException("items");

            this.Items = items;
        }

        public override bool IsViewFromObject(View view, Java.Lang.Object objectValue) {
            return view.Equals(objectValue);
        }

        public override int GetItemPosition(Java.Lang.Object objectValue) {
            return PositionNone;
        }

        public override Java.Lang.Object InstantiateItem(ViewGroup container, int position) {
            position %= this.Items.Count();

            var item = this.Items.ElementAt(position);

            if (item.Parent != null) {
                var p = item.Parent as ViewGroup;
                if (p != null)
                    p.RemoveView(item);
            }

            container.AddView(item, 200, 200);
            return item;
        }

        public override void DestroyItem(ViewGroup container, int position, Java.Lang.Object objectValue) {
            //var vp = (ViewPager)container;
            //var chd = container.GetChildAt(position);
            //不能 Dispose, 如果释放了,会在 FinishUpdate 的时候, 报错
            //chd.Dispose();
            //container.RemoveView((View)chd);
        }


        private bool IsManual = false;
        /// <summary>
        /// only occured by gesture
        /// </summary>
        /// <param name="state"></param>
        public void OnPageScrollStateChanged(int state) {
            this.IsManual = true;
        }

        public void OnPageScrolled(int position, float positionOffset, int positionOffsetPixels) {
            //Do nothing
        }

        public void OnPageSelected(int pos) {
            if (IsManual) {
                if (pos == 0) {
                    pos = this.Items.Count;/////////////
                    this.ViewPager.SetCurrentItem(pos, false);
                }
                else if (pos == this.Count - 1) {
                    pos = this.Items.Count - 1;///////////////
                    this.ViewPager.SetCurrentItem(pos, false);
                }
            }

            if (this.PosChanged != null)
                this.PosChanged.Invoke(this, new PosChangedEventArgs(pos % this.Items.Count));
        }

        public void Next() {
            if (Items.Count == 0)
                return;
            var pos = (this.ViewPager.CurrentItem + 1) % this.Items.Count;
            this.Goto(pos);
        }

        public void Goto(int idx) {

            this.IsManual = false;
            if (idx < 0)
                idx = 0;
            else if (idx > this.Items.Count)
                idx = this.Items.Count;

            if (this.ViewPager.CurrentItem % this.Items.Count != idx)
                this.ViewPager.SetCurrentItem(idx, false);
        }


        public class PosChangedEventArgs : EventArgs {
            public int Pos { get; }
            public PosChangedEventArgs(int pos) {
                this.Pos = pos;
            }
        }

        protected override void Dispose(bool disposing) {
            base.Dispose(disposing);

            if (this.ViewPager != null)
                this.ViewPager.Dispose();
        }
    }
}