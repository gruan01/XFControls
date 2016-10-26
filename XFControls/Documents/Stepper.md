# Stepper
Xamarin.Forms' Native Stepper is very ugly. So I'm rewrite one to instead.

## Sample
~~~
...
xmlns:ctrls="clr-namespace:AsNum.XFControls;assembly=AsNum.XFControls"
...
<ctrls:Stepper Grid.Column="3"
                Grid.Row="1"
                Min="1"
                Value="{Binding Count}"
                BackgroundColor="White"
                />
~~~

## Properties:
Name | Desc | Type | Default Value
|---|---|---|---|
Min | | double | double.MinValue
Max | | double | double.MaxValue
Step | Change Step | double | 1
Value | | double | 0
Format | Value's show format | string | "0"
BorderColor | Border color | Color | #cccccc
TextColor | Text Color | Color | #cccccc