//#define SAVE_LOAD_STATE

namespace App1
{
  using System;
  using System.IO;
  using System.Threading.Tasks;
  using Windows.ApplicationModel;
  using Windows.ApplicationModel.Activation;
  using Windows.Storage;
  using Windows.UI.Xaml;
  using Windows.UI.Xaml.Controls;

  sealed partial class App : Application
  {
    DateTimeOffset suspensionTime;

    public App()
    {
      this.InitializeComponent();
      this.Suspending += OnSuspending;
      this.Resuming += OnResuming;
    }
    protected override async void OnLaunched(LaunchActivatedEventArgs args)
    {
#if SAVE_LOAD_STATE

      if (args.PreviousExecutionState == ApplicationExecutionState.Terminated)
      {
        Data.Value += await this.LoadStateFromLocalFile();
      }

#endif // SAVE_LOADSATE

      // Create the UI if necessary and navigate to the main page.
      this.CreateFrameAndNavigate(args);
    }
    async void OnSuspending(object sender, SuspendingEventArgs e)
    {
      this.suspensionTime = DateTimeOffset.Now;

      // If we are going to do async work we need to tell the OS and we need
      // to do it quickly.
      SuspendingDeferral deferral = e.SuspendingOperation.GetDeferral();

#if SAVE_LOAD_STATE

      // Do async work.
      await this.SaveStateToLocalFile(Data.Value);

#endif // SAVE_LOAD_STATE

      // Tell the OS we're done.
      deferral.Complete();
    }

    void OnResuming(object sender, object e)
    {
      Data.Value += this.CalculateOffsetTimeInDecimalSeconds(this.suspensionTime);
    }
    async Task SaveStateToLocalFile(decimal value)
    {
      var file = await ApplicationData.Current.LocalFolder.CreateFileAsync(
          FILENAME, CreationCollisionOption.ReplaceExisting);

      using (var netStream = await file.OpenStreamForWriteAsync())
      {
        using (var streamWriter = new StreamWriter(netStream))
        {
          streamWriter.WriteLine(value);
          streamWriter.WriteLine(this.suspensionTime);
        }
      }
    }
    async Task<decimal> LoadStateFromLocalFile()
    {
      decimal value = 0m;

      try
      {
        var file = await ApplicationData.Current.LocalFolder.GetFileAsync(FILENAME);

        using (var netStream = await file.OpenStreamForReadAsync())
        {
          using (var streamReader = new StreamReader(netStream))
          {
            value = decimal.Parse(streamReader.ReadLine());
            DateTimeOffset offset = DateTimeOffset.Parse(streamReader.ReadLine());
            value += this.CalculateOffsetTimeInDecimalSeconds(offset);
          }
        }
      }
      catch (FileNotFoundException)
      {

      }
      return (value);
    }
    decimal CalculateOffsetTimeInDecimalSeconds(DateTimeOffset time)
    {
      TimeSpan elapsedTime = DateTimeOffset.Now - time;

      double elapsedMilliseconds = elapsedTime.TotalMilliseconds;

      decimal elapsedDecimalSeconds =
          (decimal)Math.Round(elapsedMilliseconds / 1000.0, 1);

      return (elapsedDecimalSeconds);
    }
    void CreateFrameAndNavigate(LaunchActivatedEventArgs e)
    {
      Frame rootFrame = Window.Current.Content as Frame;

      // Do not repeat app initialization when the Window already has content,
      // just ensure that the window is active
      if (rootFrame == null)
      {
        // Create a Frame to act as the navigation context and navigate to the first page
        rootFrame = new Frame();

        // Set the default language
        rootFrame.Language = Windows.Globalization.ApplicationLanguages.Languages[0];

        // Place the frame in the current Window
        Window.Current.Content = rootFrame;
      }

      if (rootFrame.Content == null)
      {
        // When the navigation stack isn't restored navigate to the first page,
        // configuring the new page by passing required information as a navigation
        // parameter
        rootFrame.Navigate(typeof(MainPage), e.PreviousExecutionState);
      }
      // Ensure the current window is active
      Window.Current.Activate();
    }

    static readonly string FILENAME = "state.txt";
  }
}
