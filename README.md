# XFControl 是什么 ? What is it ?

* AsNum.XFControls 是一组用于 Xamarin.Forms 的自定义控件集.
* AsNum.XFControls is a set of Xamarin.Forms' Control & enhancement function. 

## 如何使用 How to use it ?
* XFControls 只依赖于 Xamarin.Forms
* XFControls only dependency Xamarin.Forms
* Nuget : Install-Package XFControls 
	* iOS Project Please insert the following code before **global::Xamarin.Forms.Forms.Init();** at file AppDelegate.cs
	~~~
	AsNumAssemblyHelper.HoldAssembly();
	~~~

	* Android Project also please insert the following code before **Xamarin.Forms.Forms.Init(this, bundle)** at file MainActivity.cs;
	~~~
	AsNumAssemblyHelper.HoldAssembly();
	~~~

## [Documents](https://github.com/gruan01/XFControls/blob/master/XFControls/Documents/index.md)

## XFControls include:
* Border
* CheckBox
* CircleBox
* DataPicker
* Flip
* FontIcon
* Marquee
* RadioGroup
* RadioButtonGroup
* RatingBar
* Segment
* Stepper
* TabView
* Repeater
* WrapLayout
* UniformGrid
