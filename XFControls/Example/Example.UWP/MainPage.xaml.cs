using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace Example.UWP {
    public sealed partial class MainPage {
        public MainPage() {
            this.InitializeComponent();

            LoadApplication(new Example.App());

            UpdateBounds(ApplicationView.GetForCurrentView().VisibleBounds);
            ApplicationView.GetForCurrentView().VisibleBoundsChanged += (view, sender) => { UpdateBounds(view.VisibleBounds); };
        }

        private void UpdateBounds(Rect bounds) {
            double height = bounds.Height;
            double width = bounds.Width;
            

            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.UI.ViewManagement.StatusBar")) {
                var statusBar = StatusBar.GetForCurrentView();

                bool landscape = Windows.UI.ViewManagement.ApplicationView.GetForCurrentView().Orientation == ApplicationViewOrientation.Landscape;
                if (landscape)
                    width += statusBar.OccludedRect.Width;
                else
                    height += statusBar.OccludedRect.Height;
            }

            this.Width = width;
            this.Height = height;
        }
    }
}
