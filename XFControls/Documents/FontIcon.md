# FontIcon

## Sample

~~~
...
xmlns:ctrls="clr-namespace:AsNum.XFControls;assembly=AsNum.XFControls"
...
...
  <ContentPage.Resources>
    <ResourceDictionary>
      <OnPlatform x:TypeArguments="x:String"
                  Android="Fonts/iconfont.woff"
                  iOS="iconfont"
                  WinPhone="Fonts/iconfont.woff"
                  x:Key="font" />
    </ResourceDictionary>
  </ContentPage.Resources>    
...
...
    <ctrls:FontIcon FontFamily="{StaticResource font}" 
                    Glyph="&#xe626;" 
                    FontSize="20"
                    Color="Red"
                    Grid.Row="2" />
~~~

## Note
Please put font file to:
* Android : Assets/Fonts, and set it as AndroidAsset
* iOS : Resources/Fonts, and set it as BundleResource, open info.plist as xml file, append the following section 
~~~
    <key>UIAppFonts</key>
    <array>
      <string>Fonts/iconfont.woff</string>
    </array>  
~~~
to node: plist/dict 
"iconfont.woff" is your font file's name.
After this, it would like this:
~~~
<?xml version="1.0" encoding="UTF-8"?>
<!DOCTYPE plist PUBLIC "-//Apple//DTD PLIST 1.0//EN" "http://www.apple.com/DTDs/PropertyList-1.0.dtd">
<plist version="1.0">
  <dict>
  ...
    <key>UIAppFonts</key>
    <array>
      <string>Fonts/iconfont.woff</string>
    </array>    
  ...
</dict>
</plist>  
~~~

* Directory "Fonts" not rquired.

## Properties:
Name | Desc | Type | Default Value
|---|---|---|---|
FontFamily | | string
FontSize | | double | 12
Glyph | format: &amp;#xe626; | string
Color | Foreground Color | Color | Black
DisableColor | Foreground Color when disabled | Color | Gray
TapCmd | Command when Tap | ICommand | null
TapParam | TapCmd's param | object | null