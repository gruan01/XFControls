# Flip
轮播组件

## Sample
~~~
...
xmlns:ctrls="clr-namespace:AsNum.XFControls;assembly=AsNum.XFControls"
...
<ctrls:Flip Grid.Row="0"
                AutoPlay="True"
                Interval="3000"
                ItemsSource="{Binding FlipSource}"
      >

      <ctrls:Flip.ItemTemplate>
        <DataTemplate>
          <Grid>
            <Image Source="{Binding Img}" Aspect="AspectFill" />
            <Label Text="{Binding Desc}"
                   HorizontalTextAlignment="Start"
                   VerticalTextAlignment="End"
                   TextColor="White"
                   Margin="10"
                   />
          </Grid>
        </DataTemplate>
      </ctrls:Flip.ItemTemplate>

    </ctrls:Flip>
~~~

## Properties:
Name | Desc | Type | Default Value
|---|---|---|---|
ItemsSource | Data Sources | IEnumerable |
ItemTemplate | Item's apperance | DataTemplate
AutoPlay | | bool | false
Interval | Autoplay interval | int | 2000
ShowIndicator | Whether show indiactor (White dot) bar | bool | true
Current | Current index (From 0) | int | 0
Index | Readonly, current index (From 1)
Total | Readonly, Item's count | int

## Events
* NextRequired
* IndexRequired