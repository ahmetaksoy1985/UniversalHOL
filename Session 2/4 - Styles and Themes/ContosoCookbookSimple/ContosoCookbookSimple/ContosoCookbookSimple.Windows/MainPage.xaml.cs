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

namespace ContosoCookbookSimple
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //List<string> ingredients = new List<string> {
            //    "Shortcrust Pastry: ","220g plain flour","120g cold butter",
            //    "1 tablespoon very cold water","1 large egg",
            //    "separated into yolk and white","Filling: ","1 teaspoon oil",
            //    "1 small onion","220g bacon","finely chopped 5 large eggs",
            //    "125ml cream","1/4 teaspoon ground nutmeg Salt and pepper",
            //    "110g grated tasty cheddar cheese"
            //};

            //IngredientsListBox.ItemsSource = ingredients;

            var item = ContosoCookbookSimple.DataModel.RecipeDataSource.GetItem("2001");

            this.DataContext = item;
        }

        private void AboutMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AboutPage));
        }
    }
}
