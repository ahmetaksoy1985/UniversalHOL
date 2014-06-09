using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace S04_ListDemo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ListViewPage : Page
    {
        public ListViewPage()
        {
            this.InitializeComponent();
            this.DataContext = App.AppDataSource;
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            if (SampleListView.CanReorderItems)
                SampleListView.CanReorderItems = false;
            else
                SampleListView.CanReorderItems = true; 
        }

        private void AppBarButton_Click_1(object sender, RoutedEventArgs e)
        {
            if (SampleListView.SelectionMode == ListViewSelectionMode.Single)
                SampleListView.SelectionMode = ListViewSelectionMode.Multiple;
            else
                SampleListView.SelectionMode = ListViewSelectionMode.Single;
        }
    }
}