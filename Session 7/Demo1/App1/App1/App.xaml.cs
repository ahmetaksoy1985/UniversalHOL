//#define ADDHANDLERS

namespace App1
{
  using System;
  using Windows.ApplicationModel;
  using Windows.ApplicationModel.Activation;
  using Windows.UI.Xaml;
  using Windows.UI.Xaml.Controls;

  sealed partial class App : Application
  {
    DateTimeOffset suspensionTime;

    public App()
    {
      this.InitializeComponent();

#if ADDHANDLERS

      this.Suspending += OnSuspending;
      this.Resuming += OnResuming;

#endif // ADDHANDLERS 
    }

    void OnSuspending(object sender, SuspendingEventArgs e)
    {
      this.suspensionTime = DateTimeOffset.Now;
    }
    void OnResuming(object sender, object e)
    {
      TimeSpan elapsedTime = DateTimeOffset.Now - this.suspensionTime;

      double elapsedMilliseconds = elapsedTime.TotalMilliseconds;

      decimal elapsedDecimalSeconds =
          (decimal)Math.Round(elapsedMilliseconds / 1000.0, 1);

      Data.Value += elapsedDecimalSeconds;
    }

    #region REST_OF_CODE

    protected override void OnLaunched(LaunchActivatedEventArgs e)
    {

#if DEBUG
      if (System.Diagnostics.Debugger.IsAttached)
      {
        this.DebugSettings.EnableFrameRateCounter = true;
      }
#endif
      Frame rootFrame = Window.Current.Content as Frame;

      // Do not repeat app initialization when the Window already has content,
      // just ensure that the window is active
      if (rootFrame == null)
      {
        // Create a Frame to act as the navigation context and navigate to the first page
        rootFrame = new Frame();

        // Set the default language
        rootFrame.Language = Windows.Globalization.ApplicationLanguages.Languages[0];

        if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
        {
          //TODO: Load state from previously suspended application
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
    #endregion // REST_OF_CODE

  }
}
