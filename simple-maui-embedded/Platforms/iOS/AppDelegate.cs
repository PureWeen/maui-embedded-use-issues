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

			CrossPlatformContextProvider.Initialize(mauiContext);
		}
	}
}
