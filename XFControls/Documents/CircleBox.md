# CircleBox
Just like BoxView, but this control is a "Circle", and can put child element in it.
和 BoxView 一样,但是该控件是一个圆形的, 而且可以放置子控件在它里面.

## Xaml
~~~
<ctrls:CircleBox Radius="50" Grid.Row="1" BackgroundColor="Lime">
    <Image Source="http://v1.qzone.cc/pic/201407/13/11/05/53c1f77cdbd01600.jpg%21600x600.jpg" WidthRequest="100" HeightRequest="100" Aspect="AspectFill" />
</ctrls:CircleBox>
~~~

Properties:
Name | Desc | Type | Default Value
-|-|-|-
Radius | 半径 | double | 40

## Notice
Under Android SDK 18 (Not include), Renderer draw a Circle bitmap on canvas.
在 Android SDK 18 以下, Renderer 其实是绘制了一个圆形的位图在画布上. 

## Effect