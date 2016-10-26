# Border

## Xaml Sample
~~~
...
...
xmlns:ctrls="clr-namespace:AsNum.XFControls;assembly=AsNum.XFControls"
...
...

<ctrls:Border CornerRadius="15"
                Stroke="#999999"
                StrokeThickness="0.5"
                WidthRequest="60"
                IsClippedToBorder="True"
                IsClippedToBounds="True"
                Padding="5"
                >
    <Label Text="{Binding Data.Name}"
            HorizontalTextAlignment="Center"
            FontSize="12" />
</ctrls:Border>
~~~

## Properties:
Name | Desc | Type | Default Value
|---|---|---|---|
CornerRadius | Corner Radius 圆角半径 | CornerRadius | Null
Stroke |Border Color　边框颜色 | Color | Color.Default
StrokeThickness | Border Width 边框宽度 | Thickness | Null
IsClippedToBorder | Whether Clip Child 是否裁剪超出部分 | bool | True 

## Effect:
