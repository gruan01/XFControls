# Marquee
跑马灯

Sample
~~~
...
xmlns:ctrls="clr-namespace:AsNum.XFControls;assembly=AsNum.XFControls"
...

<StackLayout Grid.Row="1" Orientation="Horizontal" Margin="2">

      <ctrls:Border BackgroundColor="Red"
                    CornerRadius="10"
                    WidthRequest="40"
                    Padding="5,2"
        >
        <Label Text="快报" TextColor="White" 
               HorizontalTextAlignment="Center" 
               VerticalTextAlignment="Center"
      />
      </ctrls:Border>

      <ctrls:Marquee ItemsSource="{Binding MarqueeSource}">
        <ctrls:Marquee.ItemTemplate>
          <DataTemplate>
            <Label VerticalOptions="Center"
                    LineBreakMode="TailTruncation"
                    Text="{Binding}"
                    Margin="2"
                   FontSize="18"
            />
          </DataTemplate>
        </ctrls:Marquee.ItemTemplate>
      </ctrls:Marquee>
    </StackLayout>
~~~

## Properties:
Name | Desc | Type | Default Value
|---|---|---|---|
ItemsSource | Data Sources | IEnumerable |
ItemTemplate | Item's apperance | DataTemplate
Interval | Autoplay interval | int | 3000