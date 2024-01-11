using ButtonEventDemo;
using Foundation;
using Nau.Simple.Maui.Core;
using UIKit;

namespace Nau.Simple.Maui.Embedded
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

			var viewController = new ButtonEventDemoViewController("ButtonEventDemoViewController", null);

			Window.RootViewController = viewController;

			_window.MakeKeyAndVisible();

			InitializeMaui(viewController);

			return true;
		}

		private void InitializeMaui(UIViewController viewController)
		{
			var mauiApp = MauiProgram.CreateMauiApp((builder) =>
			{
				builder.Services.Add(new ServiceDescriptor(typeof(UIWindow), Window));
				builder.ConfigureFonts(fonts => { fonts.AddFont("MaterialDesignIconsDesktop.ttf", "MaterialDesignIconsDesktop"); });
			});

			var mauiContext = new MauiContext(mauiApp.Services);

			_mauiContext = mauiContext;

			Platform.Init(() => viewController);

			// Create an instance of the Cross Platform Framework application for consumption by the Pages in order to access global resource dictionaries.
			Microsoft.Maui.Controls.Application.Current = new Microsoft.Maui.Controls.Application()
			{
				// Note: this is for illustration purposes. In our actual app we have a class that extends ResourceDictionary that we set for Resource here.
				//		later in the initialization code we would add our various themes\style resource dictionaries to this so they are loaded and accessible to all pages.

				////Resources = new OurCustomAppDictionary()
			};

			CrossPlatformContextProvider.Initialize(mauiContext);
		}
	}
}
