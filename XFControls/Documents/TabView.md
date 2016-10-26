# TabView
TabView just like TabbedPage, but it's a view !
Can set tab position on top / left / right or boottom.
Can custom tab's header.

Sample
~~~
<ctrls:TabView Grid.Row="2"
                   ItemsSource="{Binding TabViewSource}"
                   TabHeightRequest="60"
                   TabPosition="Bottom">

      <ctrls:TabView.TabTemplate>
        <DataTemplate>
          <StackLayout>
            <ctrls:FontIcon Glyph="{Binding Glyph}"
                            FontFamily="{StaticResource font}"
                            FontSize="25"
                            Color="Gray"
                            HorizontalOptions="Center"
                          >
              <ctrls:FontIcon.Triggers>
                <DataTrigger TargetType="ctrls:FontIcon"
                             Binding="{Binding IsSelected}"
                             Value="True"
                             >
                  <Setter Property="Color" Value="Red" />
                </DataTrigger>
              </ctrls:FontIcon.Triggers>
            </ctrls:FontIcon>

            <Label Text="{Binding Title}"
                   TextColor="Gray"
                   HorizontalOptions="Center"
                   VerticalOptions="CenterAndExpand"
                   FontSize="10">

              <Label.Triggers>
                <DataTrigger TargetType="Label"
                             Binding="{Binding IsSelected}"
                             Value="True"
                           >
                  <Setter Property="TextColor" Value="Red" />
                </DataTrigger>
              </Label.Triggers>
            </Label>
            
          </StackLayout>
        </DataTemplate>
      </ctrls:TabView.TabTemplate>

      <ctrls:TabView.ItemTemplate>
        <DataTemplate>
          <Label Text="{Binding Title}"
                 HorizontalOptions="Center" />          
        </DataTemplate>
      </ctrls:TabView.ItemTemplate>
      
    </ctrls:TabView>
~~~

If you want show different color when tab is selected, you source model should implementation interface [**ISelectable**](https://github.com/gruan01/XFControls/blob/master/XFControls/Src/AsNum.XFControls/ISelectable.cs).  
~~~
    public class TabViewItem : ISelectable, INotifyPropertyChanged {

        public char Glyph { get; set; }

        public bool IsSelected {
            get; set;
        }

        public ICommand SelectedCommand {
            get; set;
        }

        public string Title { get; set; }

        public ICommand UnSelectedCommand {
            get; set;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyOfPropertyChange(string propertyName) {
            this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
~~~

## Properties:
Name | Desc | Type | Default Value
|---|---|---|---|
ItemsSource | Data Sources | IEnumerable |
ItemTemplate | Item's apperance Template | DataTemplate
ItemTemplateSelector | Item's template selector, *if ItemTemplateSelector has value, will not use ItemTemplate* | DataTemplateSelector
TabTemplate | Item's header Template | DataTemplate |
TabTemplateSelector | Item's header Template Selector, *if TabTemplateSelector has value, will not use TabTemplate* | DataTemplateSelector
TabControlTemplate | Item's header Control Template | ControlTemplate | [TabViewTabControlTemplate](https://github.com/gruan01/XFControls/blob/master/XFControls/Src/AsNum.XFControls/Templates/TabViewTabControlTemplate.xaml)
TabBarControlTemplate| TabBar's apperance. TabViewTabBarControlTemplate is default apperance, you can see how to define it. | ControlTemplate | [TabViewTabBarControlTemplate](https://github.com/gruan01/XFControls/blob/master/XFControls/Src/AsNum.XFControls/Templates/TabViewTabBarControlTemplate.xaml)
SelectedItem | Current Selected tab's binding context (View Model) | object
SelectedIndex | Current Selected index | int
TabWidthRequest | Tab's header WidthRequest | int | 80
TabHeightRequest | Tab's header HeightRequest | int | 40
TabPosition | Tab's header's position, can choice Top / Right / Bottom or Left , default is Top | TabViewPositions | Top
TabBarBackgroundColor | Tab's header area's background color | Color | EEEEEE
TransitionType | Can choice Fade or None | TabViewTransitionTypes | Fade

## Notice
Now only support MVVM , not support direct add TabpageView in to it. 
