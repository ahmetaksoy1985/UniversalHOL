using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace AdaptiveUI.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DisplayInfo : Page
    {
        public DisplayInfo()
        {
            this.InitializeComponent();
            DisplayInformation information = DisplayInformation.GetForCurrentView();
            information.OrientationChanged += ScreenOrientationChanged;
            Loaded += DisplayInfoLoaded;
        }

        private void ScreenOrientationChanged(DisplayInformation information, object args)
        {
            CurrentOrientationText.Text = information.CurrentOrientation.ToString();
        }

        private void DisplayInfoLoaded(object sender, RoutedEventArgs e)
        {
            var information = DisplayInformation.GetForCurrentView();
            NativeOrientationText.Text = information.NativeOrientation.ToString();
            CurrentOrientationText.Text = information.CurrentOrientation.ToString();
            RawPixelsPerViewPixelText.Text = information.RawPixelsPerViewPixel.ToString();
            ViewPixelsDpiText.Text = information.LogicalDpi.ToString();
            RawDpiXText.Text = information.RawDpiX.ToString();
            RawDpiYText.Text = information.RawDpiY.ToString();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }
    }
}
