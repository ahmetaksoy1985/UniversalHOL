using PicApp.Common;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace PicApp
{
    public sealed partial class ImagePage : Page
    {
        private NavigationHelper navigationHelper;
        private ViewModel viewModel;

        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }

        public ImagePage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;

            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.navigationHelper.SaveState += this.NavigationHelper_SaveState;
        }
        async protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            int id = (int)e.Parameter;

            // Because the page is cached we may already have a view model. If
            // so we keep it unless we're looking at different data.
            if ((this.viewModel == null) || (this.viewModel.DataItem.Id != id))
            {
                var dataItems = await Data.GetItemsAsync();
                var dataItem = dataItems[id];
                this.viewModel = new ViewModel()
                {
                    DataItem = dataItem
                };

                this.DataContext = this.viewModel;
            }
            navigationHelper.OnNavigatedTo(e);
        }
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            // If the user is going back but has left the page in edit
            // mode then we clear that edit for them rather than
            // assuming they wanted it.
            if ((e.NavigationMode == NavigationMode.Back) &&
                (this.viewModel.IsEditing))
            {
                this.viewModel.RevertTitleChanges();
                this.viewModel.SetReadMode();
            }
            navigationHelper.OnNavigatedFrom(e);
        }

        /// <summary>
        /// Populates the page with content passed during navigation. Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="sender">
        /// The source of the event; typically <see cref="NavigationHelper"/>.
        /// </param>
        /// <param name="e">Event data that provides both the navigation parameter passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested and
        /// a dictionary of state preserved by this page during an earlier
        /// session. The state will be null the first time a page is visited.</param>
        private void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
            if ((e.PageState != null) && e.PageState.ContainsKey("isEditing"))
            {
                this.viewModel.SetEditMode();
                this.viewModel.DataItem.Title = e.PageState["currentText"] as string;
            }
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache. Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="sender">The source of the event; typically <see cref="NavigationHelper"/>.</param>
        /// <param name="e">Event data that provides an empty dictionary to be populated with
        /// serializable state.</param>
        private void NavigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
            // Save the unique state of the page here.
            // Our only piece of state here really is whether the user was
            // in edit mode and, if so, what have they typed so far?
            if (this.viewModel.IsEditing)
            {
                e.PageState["isEditing"] = true;
                e.PageState["currentText"] = this.viewModel.DataItem.Title;
            }
        }

        void OnEdit(object sender, RoutedEventArgs e)
        {
            this.viewModel.SetEditMode();
        }
        async void OnKeep(object sender, RoutedEventArgs e)
        {
            this.viewModel.ConfirmTitleChanges();
            this.viewModel.SetReadMode();
            await Data.SaveItemsAsync();
        }
        void OnDiscard(object sender, RoutedEventArgs e)
        {
            this.viewModel.RevertTitleChanges();
            this.viewModel.SetReadMode();
        }
    }
}
