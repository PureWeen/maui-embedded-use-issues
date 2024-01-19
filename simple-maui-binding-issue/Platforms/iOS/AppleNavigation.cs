using Microsoft.Maui.Platform;
using UIKit;

namespace Nau.Simple.Maui.BindingIssue.Platforms.iOS
{
	public class AppleNavigation : INavigationService
	{
		private readonly IMauiContext _crossPlatformContext;

		public AppleNavigation()
		{
			_crossPlatformContext = CrossPlatformContextProvider.GetCrossPlatformContext();
		}

		public void NavigateBack()
		{
			NavigationControllerInContext.PopViewController(true);
		}

		public void NavigateTo(ContentPage page)
		{
			NavigateToMauiPage(page);
		}

		public void Present(ContentPage page, bool shouldDisplayFullScreen)
		{
			UIViewController presentingController = HostController;

			UIViewController renderedController = ConfigureAndRenderContentPage(page);

			UINavigationController navController = new UINavigationController();
			navController.SetViewControllers(new UIViewController[] { renderedController }, false);

			if (shouldDisplayFullScreen)
			{
				navController.ModalPresentationStyle = UIModalPresentationStyle.FullScreen;
			}

			// Setting to the new navigation controller so we can navigate from page to page once we present the new controller.
			HostController = navController;

			presentingController.PresentViewController(navController, true, null);
		}

		public void SetNavigationHost(object nativeHost)
		{
			HostController = nativeHost as UIViewController ?? throw new ArgumentException($"Expected instance of {nameof(UIViewController)}", nameof(nativeHost));
		}

		private UIViewController HostController { get; set; }

		private void NavigateToMauiPage(ContentPage page)
		{
			UIViewController renderedController = ConfigureAndRenderContentPage(page);

			NavigationControllerInContext.PushViewController(renderedController, true);

		}

		private UIViewController ConfigureAndRenderContentPage(ContentPage page)
		{
			// Setting the parent allows us access to the application level resource dictionary and ensures the iOS bug due to missing parent is not encountered.
			page.Parent = Application.Current;

			UIViewController renderedController = page.ToUIViewController(_crossPlatformContext);

			// Important to prevent memory leak per the Microsoft documentation (presume this still applies for MAUI).
			page.Parent = null;

			return renderedController;
		}

		private UINavigationController NavigationControllerInContext
		{
			get
			{
				UINavigationController navigationController = HostController is UINavigationController ? HostController as UINavigationController : HostController.NavigationController;
				return navigationController;
			}
		}
	}
}