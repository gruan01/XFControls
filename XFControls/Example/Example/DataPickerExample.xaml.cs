using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Example {
    public partial class DataPickerExample : ContentPage {

        public IEnumerable<DataPickerItem> Datas { get; }
        = new List<DataPickerItem>() {
            new DataPickerItem() { ID = 1, Name= "Asia", Children=new List<DataPickerItem>() { new DataPickerItem() { Name = "China" }, new DataPickerItem() { Name="Japan"}, new DataPickerItem() { Name= "Singapore" } } },
            new DataPickerItem() { ID = 1, Name= "America", Children=new List<DataPickerItem>() { new DataPickerItem() { Name = "USA" }, new DataPickerItem() { Name="Brazil"}, new DataPickerItem() { Name="Canda" } } },
            new DataPickerItem() { ID = 1, Name= "Europe", Children=new List<DataPickerItem>() { new DataPickerItem() { Name = "English" }, new DataPickerItem() { Name="Franch"} , new DataPickerItem() { Name="Germany"} } },
        };

        public DataPickerExample() {
            InitializeComponent();

            this.BindingContext = this;
        }

        public class DataPickerItem {

            public int ID { get; set; }

            public string Name { get; set; }

            public IEnumerable<DataPickerItem> Children {
                get; set;
            }
        }
    }
}
