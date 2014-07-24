namespace PicApp
{
  using PicApp.Common;
  using Windows.UI.Xaml;
  using Windows.UI.Xaml.Controls;
  using Windows.UI.Xaml.Navigation;

  public sealed partial class MainPage : Page
  {
    private NavigationHelper navigationHelper;

    public NavigationHelper NavigationHelper
    {
      get { return this.navigationHelper; }
    }

    public MainPage()
    {
      this.InitializeComponent();

      this.NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Required;

      this.navigationHelper = new NavigationHelper(this);
      this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
      this.navigationHelper.SaveState += this.NavigationHelper_SaveState;

      this.Loaded += OnLoaded;
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
        // TODO: Create an appropriate data model for your problem domain to replace the sample data
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
        // TODO: Save the unique state of the page here.
    }
    void OnItemClick(object sender, ItemClickEventArgs e)
    {
      // keep it very simple
      int id = ((DataItem)e.ClickedItem).Id;
      this.Frame.Navigate(typeof(ImagePage), id);
    }
    async void OnLoaded(object sender, RoutedEventArgs e)
    {
      this.DataContext = await Data.GetItemsAsync();
    }

    #region NavigationHelper registration

    /// <summary>
    /// The methods provided in this section are simply used to allow
    /// NavigationHelper to respond to the page's navigation methods.
    /// <para>
    /// Page specific logic should be placed in event handlers for the  
    /// <see cref="NavigationHelper.LoadState"/>
    /// and <see cref="NavigationHelper.SaveState"/>.
    /// The navigation parameter is available in the LoadState method 
    /// in addition to page state preserved during an earlier session.
    /// </para>
    /// </summary>
    /// <param name="e">Provides data for navigation methods and event
    /// handlers that cannot cancel the navigation request.</param>
    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        this.navigationHelper.OnNavigatedTo(e);
    }

    protected override void OnNavigatedFrom(NavigationEventArgs e)
    {
        this.navigationHelper.OnNavigatedFrom(e);
    }

    #endregion
  }
}
