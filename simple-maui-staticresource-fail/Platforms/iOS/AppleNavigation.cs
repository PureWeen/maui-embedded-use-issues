using Microsoft.Maui.Platform;
using UIKit;

namespace Nau.Simple.Maui.Staticresource.Fail.Platforms.iOS
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
			HostNavigationController.PopViewController(true);
		}

		public void NavigateTo(ContentPage page)
		{
			NavigateToXamarinPage(page);
		}

		public void SetNavigationHost(object nativeHost)
		{
			HostNavigationController = nativeHost as UINavigationController ?? throw new ArgumentException($"Expected instance of {nameof(UINavigationController)}", nameof(nativeHost));
		}

		private UINavigationController HostNavigationController { get; set; }

		private void NavigateToXamarinPage(ContentPage page)
		{
			UIViewController renderedController = ConfigureAndRenderContentPage(page);
			HostNavigationController.PushViewController(renderedController, true);

		}

		protected UIViewController ConfigureAndRenderContentPage(ContentPage page)
		{
			// Setting the parent allows us access to the application level resource dictionary and ensures the iOS bug due to missing parent is not encountered.
			page.Parent = Application.Current;

			UIViewController renderedController = page.ToUIViewController(_crossPlatformContext);

			// Important to prevent memory leak per the Microsoft documentation (presume this still applies for MAUI).
			page.Parent = null;

			return renderedController;
		}
	}
}