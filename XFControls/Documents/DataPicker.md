# DataPicker

## Sample
~~~
...
xmlns:ctrls="clr-namespace:AsNum.XFControls;assembly=AsNum.XFControls"
...
<ctrls:DataPicker Grid.Column="0" Grid.Row="0"
                    ItemsSource="{Binding Datas}"
                    DisplayPath="AreaName"
                    SelectedItem="{Binding Province}"
                    x:Name="d1"
                    />

<ctrls:DataPicker Grid.Column="1" Grid.Row="0"
                    ItemsSource="{Binding Path=SelectedItem.Children, Source={x:Reference d1}}"
                    DisplayPath="AreaName"
                    SelectedItem="{Binding City}"
                    x:Name="d2"
                    />

<ctrls:DataPicker Grid.Column="2" Grid.Row="0"
                    ItemsSource="{Binding Path=SelectedItem.Children, Source={x:Reference d2}}"
                    DisplayPath="AreaName"
                    SelectedItem="{Binding County}"
                    />
~~~

## Properties:
Name | Desc | Type | Default Value
|---|---|---|---|
ItemsSource | Data Sources | IEnumerable |
SelectedItem | Choiced Item | object | null
DisplayPath | Display property's name | string
SelectedIndex | readonly | int 
TextColor | | Color | Black
FontSize | | float | 15
DividerColorProperty | | Color | Gray

## ISSUE
Can't dynamic set font's color