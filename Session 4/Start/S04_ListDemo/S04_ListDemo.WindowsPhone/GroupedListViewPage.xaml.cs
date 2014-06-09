using S04_ListDemo.Models;
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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace S04_ListDemo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GroupedListViewPage : Page
    {

        public GroupedListViewPage()
        {
            this.InitializeComponent();
            this.DataContext = App.AppDataSource;
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
           base.OnNavigatedTo(e);
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            //GroupedListView.CanReorderItems = true;
            //GroupedListView.ReorderMode = ListViewReorderMode.Enabled;
        }

        private void AppBarButton_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void Grid_Tapped(object sender, TappedRoutedEventArgs e)
        {
            
        }

        private void GroupedListView_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void IncrementalUpdateHandler(ListViewBase sender, ContainerContentChangingEventArgs args)
        {
            args.Handled = true;

            if (args.Phase != 0)
                throw new Exception("something went terribly wrong");
            else
            {
                // show the placeholder
                // We'll need to open our template and manually alter the 
                //   properties of the items therein.
                //   Our template looks like this:
                //   Grid
                //      |__Border name=templatePlaceholder 
                //      |__Image name=templateImage
                //      |__Grid
                //         |__TextBlock name=templateTitle
                //         |__TextBlock name=templateSubTitle
                Grid templateRoot = (Grid)args.ItemContainer.ContentTemplateRoot;
                Border placeholder = (Border)templateRoot.FindName("templatePlaceholder");
                Image itemImage = (Image)templateRoot.FindName("templateImage");
                TextBlock title = (TextBlock)templateRoot.FindName("templateTitle");
                TextBlock subtitle = (TextBlock)templateRoot.FindName("templateSubTitle");
                
                // Make the placeholder visible
                placeholder.Opacity = 1;

                // make everything else invisible
                itemImage.Opacity = 0;
                title.Opacity = 0;
                subtitle.Opacity = 0;

                args.RegisterUpdateCallback(ShowText);
            }
        }

//private void IncrementalUpdateHandler(ListViewBase sender, ContainerContentChangingEventArgs args)
//{
//    args.Handled = true;

//    if (args.Phase != 0)
//        throw new Exception("we will always be in phase 0");
//    else
//    {
//        // show a placeholder shape
//        args.RegisterUpdateCallback(ShowText);
//    }
//}

        private void ShowText(ListViewBase sender, ContainerContentChangingEventArgs args)
        {
            args.Handled = true;

            if (args.Phase != 1)
                throw new Exception("something went terribly wrong");
            else
            {
                SampleItem sItem = (SampleItem)args.Item;
                // show the title
                Grid templateRoot = (Grid)args.ItemContainer.ContentTemplateRoot;
                TextBlock title = (TextBlock)templateRoot.FindName("templateTitle");
                // Explicitly set the text for non-binding templates
                title.Text = sItem.Title;
                // Show the element 
                title.Opacity = 1;
                args.RegisterUpdateCallback(ShowImage);
            }
        }

        private void ShowImage(ListViewBase sender, ContainerContentChangingEventArgs args)
        {
            args.Handled = true;

            if (args.Phase != 2)
                throw new Exception("something went terribly wrong");
            else
            {
                SampleItem sItem = (SampleItem)args.Item;
                // show everything else 
                Grid templateRoot = (Grid)args.ItemContainer.ContentTemplateRoot;
                Image itemImage = (Image)templateRoot.FindName("templateImage");
                TextBlock subtitle = (TextBlock)templateRoot.FindName("templateSubTitle");
                
                // Explicit data setting can help speed up item rendering 
                itemImage.Source = new BitmapImage(new Uri(sItem.ItemImage));
                subtitle.Text = sItem.TargetGroup;
                
                // Show the elements
                itemImage.Opacity = 1;
                subtitle.Opacity = 1;
            }
        }
    }
}
