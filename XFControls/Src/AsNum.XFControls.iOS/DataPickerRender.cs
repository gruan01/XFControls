using AsNum.XFControls;
using AsNum.XFControls.iOS;
using Foundation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

///http://www.gooorack.com/2013/07/18/xamarin-uipickerview-as-a-combobox/
[assembly: ExportRenderer(typeof(DataPicker), typeof(DataPickerRender))]
namespace AsNum.XFControls.iOS
{
	public class DataPickerRender : ViewRenderer<DataPicker, UIPickerView>
	{

		private DataPickerModel Model;

		private bool IsDisposed = false;

		protected override void OnElementChanged(ElementChangedEventArgs<DataPicker> e)
		{
			base.OnElementChanged(e);

			if (e.NewElement != null)
			{
				var picker = new UIPickerView();
				picker.ShowSelectionIndicator = true;
				this.SetNativeControl(picker);
				this.InitPicker();
				this.UpdatePickerSelected(this.Element.SelectedIndex);
			}
		}

		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);

			if (e.PropertyName.Equals(DataPicker.ItemsSourceProperty.PropertyName))
			{
				this.UpdateDatas();
			}
			else if (e.PropertyName.Equals(DataPicker.FontSizeProperty.PropertyName) ||
				     e.PropertyName.Equals(DataPicker.TextColorProperty.PropertyName) ||
					 e.PropertyName.Equals(DataPicker.DividerColorProperty.PropertyName))
			{
				this.UpdateApperance();
			}
			else if (e.PropertyName.Equals(DataPicker.SelectedItemProperty.PropertyName))
			{
				this.UpdatePickerSelected(this.Element.SelectedIndex);
			}
		}

		private void InitPicker()
		{
			if (this.Model == null)
			{
				this.Model = new DataPickerModel(
						this.Element.StringValues,
						this.Element.TextColor.ToUIColor(),
						this.Element.DividerColor.ToUIColor(),
						this.Element.FontSize
						);
				this.Model.PickerChanged += Model_PickerChanged;
				this.Control.Model = this.Model;
				this.UpdateSelectedItem(this.Element.SelectedIndex);
			}
		}

		private void UpdateDatas()
		{
			this.Model.SetValues(this.Element.StringValues);
			this.Control.ReloadAllComponents();
			this.UpdatePickerSelected(this.Element.SelectedIndex);
			this.UpdateSelectedItem(this.Element.SelectedIndex);
		}

		private void UpdateApperance()
		{
			this.Model.SetApperances(this.Element.TextColor.ToUIColor(),
								 this.Element.DividerColor.ToUIColor(),
								 this.Element.FontSize);
			this.Control.ReloadAllComponents();
		}

		private void Model_PickerChanged(object sender, PickerChangedEventArgs e)
		{
			this.UpdateSelectedItem(e.SelectedIndex);
		}

		private void UpdateSelectedItem(int idx)
		{

			if (this.Element.ItemsSource != null)
			{
				var source = this.Element.ItemsSource.Cast<object>();
				if (idx < 0)
					idx = 0;
				if (idx > source.Count())
					idx = 0;
				
				this.Element.SelectedItem = source.ElementAt(idx);
			}
			else {
				this.Element.SelectedItem = null;
			}
		}

		private void UpdatePickerSelected(int idx)
		{
			if (idx != -1)
			{
				this.Control.Select(idx, 0, true);
			}
			else {
				this.Control.Select(0, 0, true);
			}
		}


		protected override void Dispose(bool disposing)
		{
			if (disposing && !this.IsDisposed)
			{
				this.IsDisposed = true;

				if (this.Model != null)
				{
					this.Model.Dispose();
					this.Model = null;
				}
			}
		}
	}

	public class DataPickerModel : UIPickerViewModel
	{
		private IList<string> Values;

		private UIColor TextColor = null;

		private UIColor DividerColor = null;

		private nfloat FontSize;

		public event EventHandler<PickerChangedEventArgs> PickerChanged;


		public void SetValues(IList<string> values)
		{
			this.Values = values;
		}

		public void SetApperances(UIColor textColor, UIColor dividerColor, nfloat fontSize)
		{
			this.TextColor = textColor;
			this.DividerColor = dividerColor;
			this.FontSize = fontSize;
		}


		public DataPickerModel(IList<string> values, UIColor txtColor, UIColor dividerColor, nfloat fontSize)
		{
			this.Values = values;
			this.TextColor = txtColor;
			this.DividerColor = dividerColor;
			this.FontSize = fontSize;
		}

		public override nint GetComponentCount(UIPickerView picker)
		{
			return 1;
		}

		public override nint GetRowsInComponent(UIPickerView pickerView, nint component)
		{
			return Values.Count;
		}


		public override string GetTitle(UIPickerView pickerView, nint row, nint component)
		{
			return Values[(int)row];
		}


		public override nfloat GetRowHeight(UIPickerView pickerView, nint component)
		{
			return 40f;
		}


		public override void Selected(UIPickerView pickerView, nint row, nint component)
		{
			if (this.PickerChanged != null)
			{
				this.PickerChanged(this, new PickerChangedEventArgs { SelectedIndex = (int)row });
			}
		}

		public override UIView GetView(UIPickerView pickerView, nint row, nint component, UIView view)
		{
			//Only work here, if move this method invoke to other position, pickerView.SubViews' count always 0
			this.UpdateDividerColor(pickerView, this.DividerColor);

			//var lbl = base.GetView(pickerView, row, component, view);
			var size = pickerView.RowSizeForComponent(component);
			var lbl = new UILabel()
			{
				Frame = new CoreGraphics.CGRect(0, 0, size.Width, size.Height),
				TextAlignment = UITextAlignment.Center
			};

			lbl.Text = Values[(int)row];
			lbl.TextColor = this.TextColor;// this.Element.TextColor.ToUIColor();
			lbl.Font = UIFont.SystemFontOfSize(this.FontSize);

			return lbl;
		}

		private void UpdateDividerColor(UIPickerView picker, UIColor color)
		{
			foreach (var v in picker.Subviews)
			{
				if (v.Frame.Size.Height < 1)
				{
					v.BackgroundColor = color;
				}
			}
		}
	}

	public class PickerChangedEventArgs : EventArgs
	{
		public int SelectedIndex { get; set; }
	}
}
