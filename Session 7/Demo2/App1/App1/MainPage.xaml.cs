using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace App1
{

    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            this.timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0, 200);
            timer.Tick += OnTimerTick;
            timer.Start();
        }
        void OnTimerTick(object sender, object e)
        {
            Data.Value += 0.2m;
            this.txtValue.Text = string.Format("{0}s", Data.Value);
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            txtPreviousExecutionState.Text =
                string.Format("App Previously [{0}]", ((ApplicationExecutionState)e.Parameter));
        }
        DispatcherTimer timer;
    }
}
