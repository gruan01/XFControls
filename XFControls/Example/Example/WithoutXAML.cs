using AsNum.XFControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace Example {
    public class WithoutXAML : ContentPage {
        public WithoutXAML() {

            var RadioGroupSource = new List<RadioItem>() {
                new RadioItem() {ID = 0, Name = "Hi" },
                new RadioItem() {ID = 1, Name="123" },
                new RadioItem() {ID = 2, Name = "Hello" }};

            var radioButton = new RadioGroup() {
                ItemsSource = RadioGroupSource,
                DisplayPath = "Name",
                Orientation = StackOrientation.Vertical
            };

            this.Content = new StackLayout {
                Children = {
                    radioButton
                }
            };
        }
    }
}
