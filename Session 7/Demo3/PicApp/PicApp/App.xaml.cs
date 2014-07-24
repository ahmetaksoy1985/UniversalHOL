#define USE_SUSPENSION_MANAGER

namespace PicApp
{
  using PicApp.Common;
  using Windows.ApplicationModel;
  using Windows.ApplicationModel.Activation;
  using Windows.Phone.UI.Input;
  using Windows.UI.Xaml;
  using Windows.UI.Xaml.Controls;

  sealed partial class App : Application
  {

    public App()
    {
      this.InitializeComponent();
      this.Suspending += OnSuspending;
    }


    protected async override void OnLaunched(LaunchActivatedEventArgs e)
    {

#if DEBUG
      if (System.Diagnostics.Debugger.IsAttached)
      {
        this.DebugSettings.EnableFrameRateCounter = true;
      }
#endif
      Frame rootFrame = Window.Current.Content as Frame;


      if (rootFrame == null)
      {
        // Create a Frame to act as the navigation context and navigate to the first page
        rootFrame = new Frame();

        SuspensionManager.RegisterFrame(rootFrame, "AppFrame");

        // Set the default language
        rootFrame.Language = Windows.Globalization.ApplicationLanguages.Languages[0];

        if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
        {
          // Restore the saved session state only when appropriate
          try
          {
#if USE_SUSPENSION_MANAGER

            await SuspensionManager.RestoreAsync();

#endif // USE_SUSPENSION_MANAGER
          }
          catch (SuspensionManagerException)
          {
            //Something went wrong restoring state.
            //Assume there is no state and continue
          }
        }

        // Place the frame in the current Window
        Window.Current.Content = rootFrame;
      }

      if (rootFrame.Content == null)
      {
        // When the navigation stack isn't restored navigate to the first page,
        // configuring the new page by passing required information as a navigation
        // parameter
        rootFrame.Navigate(typeof(MainPage), e.Arguments);
      }
      // Ensure the current window is active
      Window.Current.Activate();
    }

    /// <summary>
    /// Invoked when application execution is being suspended.  Application state is saved
    /// without knowing whether the application will be terminated or resumed with the contents
    /// of memory still intact.
    /// </summary>
    /// <param name="sender">The source of the suspend request.</param>
    /// <param name="e">Details about the suspend request.</param>
    async void OnSuspending(object sender, SuspendingEventArgs e)
    {
      var deferral = e.SuspendingOperation.GetDeferral();

#if USE_SUSPENSION_MANAGER

      await SuspensionManager.SaveAsync();

#endif // USE_SUSPENSION_MANAGER

      deferral.Complete();
    }
  }
}
