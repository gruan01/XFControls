> Sorry, this project's dependency Xamarin.Forms' version is very old (2.xx), and I'm have no time to maintain it,  So say sorry for everyone.

 对不起，这个项目的依赖Xamarin.Forms的版本很旧（2.xx），我没有时间维护它，所以对大家说声抱歉。

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
