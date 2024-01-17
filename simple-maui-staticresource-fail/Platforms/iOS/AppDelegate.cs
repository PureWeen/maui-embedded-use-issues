using Foundation;
using Nau.Simple.Maui.Staticresource.Fail.Platforms.iOS;
using Nau.Simple.Maui.Staticresource.Fail.Resources.Styles;
using UIKit;

namespace Nau.Simple.Maui.Staticresource.Fail
{
	[Register("AppDelegate")]
	public class AppDelegate : UIApplicationDelegate
	{
		public static MauiContext _mauiContext;

		UIWindow _window;
		public UIWindow Window
		{
			get
			{
				return _window;
			}
			set
			{
				_window = value;
			}
		}

		public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
		{
			// Perform your normal iOS registration

			// Register the Window
			_window = new UIWindow(UIScreen.MainScreen.Bounds);

			var viewController = new RootNavController();

			Window.RootViewController = viewController;

			_window.MakeKeyAndVisible();

			InitializeMaui(viewController);

			viewController.LoadInitialScreen();

			return true;
		}

		private void InitializeMaui(UIViewController viewController)
		{
			var mauiApp = MauiProgram.CreateMauiApp((builder) =>
			{
				builder.Services.Add(new ServiceDescriptor(typeof(UIWindow), Window));
				builder.Services.AddSingleton<INavigationService, AppleNavigation>();
			});

			var mauiContext = new MauiContext(mauiApp.Services);

			_mauiContext = mauiContext;

			Platform.Init(() => viewController);

			// Create an instance of the Cross Platform Framework application for consumption by the Pages in order to access global resource dictionaries.
			Application.Current = new Application()
			{
				Resources = new AppDictionary()
			};

			// Extension method to centralize setting up the merged dictionaries to reduce duplication from bug workaround and startup
			Application.Current.VerifyMergedResourceDictionaries();

			CrossPlatformContextProvider.Initialize(mauiContext);

			InjectedServiceProvider.Initialize(mauiApp.Services);
		}
	}
}
