using ButtonEventDemo;
using UIKit;

namespace Nau.Simple.Maui.BindingIssue.Platforms.iOS
{
	public class RootNavController : UINavigationController
	{
		public void LoadInitialScreen()
		{
			UIViewController initialController = new ButtonEventDemoViewController("ButtonEventDemoViewController", null);
			SetViewControllers(new UIViewController[] { initialController }, false);
		}
	}
}
