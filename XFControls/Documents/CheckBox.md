# CheckBox

## Xaml
~~~
...
...
xmlns:ctrls="clr-namespace:AsNum.XFControls;assembly=AsNum.XFControls"
...
...
<ctrls:CheckBox Grid.Row="8"
                Grid.Column="1"
                ShowLabel="True"
                Text="保存为常用地址"
                HorizontalOptions="End"
                Checked="True"
            />
~~~

## Properties

Name | Description | Type | Default Value
---| ---| ---|---
Checked | | bool | False
ShowLabel | Wheather show label 是否显示标签文本 | bool | False
Text | Label's Text 标签文本 | string | null
Size | CheckBox's size , not apply it on text, CheckBox 的大小,只应用于 CheckBox ,不应用在标签文本上 | double |25
CheckChangedCmd | Invoke when State changed. 当选中状态变化时,调用该 Command  | ICommand | null


## Events:
CheckChanged



## Notice
This Control just a simulation; Android have native checkbox, but iOS not have equivalent control.
该控件只是模拟出的来的控件; Android 包含 checkbox ,但是 iOS 并没有等效的原生控件.

If you want Change this control's style , please prepare two images:
如果你想改变该控件的样式,请准备如下两张图片放到对应的目录下:

    * Checkbox-Checked.png 
    * Checkbox-UnChecked.png

And put them into resource directory :

    * Android : Resources/DrawableXXX directory (Not support 9patch format image 不支持 9patch 文件)
    * iOS : Resources directory (You can use @2x or @3x 可以使用 @2x 或者 @3x)


## Effect
