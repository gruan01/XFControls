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
OnImg | Optional, ImageSource when Checked | ImageSource | ![](https://raw.githubusercontent.com/gruan01/XFControls/master/XFControls/Src/AsNum.XFControls/Imgs/Checkbox-Checked.png)
OffImg | Optional, ImageSource when UnChecked | ImageSource | ![](https://raw.githubusercontent.com/gruan01/XFControls/master/XFControls/Src/AsNum.XFControls/Imgs/Checkbox-Unchecked.png)

## Events:
CheckChanged



## Notice
This Control just a simulation; Android have native checkbox, but iOS not have equivalent control.
该控件只是模拟出的来的控件; Android 包含 checkbox ,但是 iOS 并没有等效的原生控件.

If you want Change this control's style , **please specify OnImg & OffImg**:
如果你想改变该控件的样式,请准设置 OnImg & OffImg .


## Effect
