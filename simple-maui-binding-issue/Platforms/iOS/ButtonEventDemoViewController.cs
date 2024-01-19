using Foundation;
using Nau.Simple.Maui.BindingIssue;
using Nau.Simple.Maui.BindingIssue.Platforms.iOS;
using UIKit;

namespace ButtonEventDemo
{
	/// <summary>
	/// Pulled simple controller from https://github.com/xamarin/ios-samples/tree/main/ButtonEventDemo and modified it for this sample's needs.
	/// </summary>
	public partial class ButtonEventDemoViewController : UIViewController
	{
		private bool _shouldPromptForAction = true;

		public ButtonEventDemoViewController(string nibName, NSBundle bundle) : base(nibName, bundle)
		{
		}

		public override void DidReceiveMemoryWarning()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning();

			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			//Here we register for the TouchUpInside event using an outlet to
			//the UIButton created in Interface Builder. Also see the target-action
			//approach for accomplishing the same thing below.
			aButton.TouchUpInside += (o, s) =>
			{
				PromptForDemoAction();
			};
		}

		public override void ViewDidUnload()
		{
			base.ViewDidUnload();

			aButton = null;
		}

		public override bool ShouldAutorotateToInterfaceOrientation(UIInterfaceOrientation toInterfaceOrientation)
		{
			// Return true for supported orientations
			return (toInterfaceOrientation != UIInterfaceOrientation.PortraitUpsideDown);
		}

		//This is an action method connected to the TouchUpInside event
		//of a UIButton. The action is connected via Interface Builder
		//The same thing can be accomplished with a .NET event registered
		//to the UIButton in code, as we do in the ViewDidLoad method above.

		partial void HandleButtonTouch(NSObject sender)
		{
			Console.WriteLine("button touched using the action method");
		}

		private void PromptForDemoAction()
		{
			// So, this is really an odd situation. If I do not present the action sheet before navigating, then the binding issue with the control template and content presenter is not an issue.
			// However, if it does present the action sheet and the navigate, the list on the second MAUI page does not render its contents the first time - but navigating back and then to the 
			// list page again does result in a proper rendering from that point on.
			//
			// In our actual production application, a different controller is presented from the initial login controller, so I tried to recreate a simplified version of that too.
			// Same result, the list on the second page does not render (nor is the command bound to the label for going back working). But if I tap back in the native navigation bar
			// and the tap to go to the list page again, everything is fine.
			//
			// Where it gets really weird is that if I simply push the rendered controller from here, then when I navigate to the next page the list and everything renders fine.
			// So it almost seems like if any controller is presented prior to attempting to navigate to an embedded MAUI page that it interrupts or loses something?


			const int DemoFailWithLoadingDialogPreNavigation = 1;

			const int DemoFailWithPresentingActionSheet = 2;

			const int DemoSucessWithSimplePushViewController = 3;

			const int DemoFailWithPresentingViewControllerModal = 4;

			const int DemoSuccessWithPresentingViewControllerFullScreen = 5;

			// Change the option here and run again to explore the different scenarios.
			int demoPathToRun = DemoFailWithLoadingDialogPreNavigation;

			switch (demoPathToRun)
			{
				case DemoFailWithLoadingDialogPreNavigation:

					// This path will repro the failure behavior since an alert dialog is presented first.

					DoAsyncWithLoadingDialog(() => Thread.Sleep(1000), () => NavigateToPage(new SimpleStartPage()));

					break;

				case DemoFailWithPresentingActionSheet:

					// This path will repro the failure behavior.

					UIAlertController alertController = UIAlertController.Create("Select Test", "Select which action to demo", UIAlertControllerStyle.ActionSheet);
					alertController.AddAction(UIAlertAction.Create("Multi Page Navigation", UIAlertActionStyle.Default, (action) => NavigateToPage(new SimpleStartPage())));

					if (DeviceInfo.Idiom != DeviceIdiom.Phone)
					{
						UIPopoverPresentationController presentationPopover = alertController.PopoverPresentationController;
						if (presentationPopover != null)
						{
							presentationPopover.PermittedArrowDirections = 0; // No arrow 
							presentationPopover.SourceView = View;
							presentationPopover.SourceRect = View.Bounds;
						}
					}

					PresentViewController(alertController, true, null);

					break;

				case DemoSucessWithSimplePushViewController:

					// This path does not repro and works as expected.

					NavigateToPage(new SimpleStartPage());

					break;

				case DemoFailWithPresentingViewControllerModal:

					// This path will repro the failure behavior if presenting modal.

					PresentPage(new SimpleStartPage(), false);

					break;

				case DemoSuccessWithPresentingViewControllerFullScreen:

					// This path does not repro the failure behavior if presenting full screen.

					PresentPage(new SimpleStartPage(), true);

					break;
			}
		}

		private void NavigateToPage(ContentPage pageInContext)
		{
			INavigationService service = InjectedServiceProvider.GetService<INavigationService>();

			service.SetNavigationHost(this.NavigationController);
			service.NavigateTo(pageInContext);
		}

		private void PresentPage(ContentPage pageInContext, bool shouldDisplayFullScreen)
		{
			INavigationService service = InjectedServiceProvider.GetService<INavigationService>();

			service.SetNavigationHost(this);
			service.Present(pageInContext, shouldDisplayFullScreen);
		}


		private AsynchronousWorker _doAsyncWorker;

		private void DoAsyncWithLoadingDialog(Action doWorkInBackground, Action workCompleted)
		{
			// Note: this is a very rough sample of a pattern we have all over our application. 
			//	 We'll display an alert dialog saying we're loading while we make a service call to get some data, update, etc.
			//	 and then we'll navigate to whatever target page after that service call has completed.
			//
			//  While this sample app demonstrates a number of different scenarios that repro the reported issue, I believe this
			//	pattern is why we see it in our production application. It falls in line with what I've found experimenting -
			//  that is if any kind of window is displayed (alert, action sheet, modal dialog, etc) then 
			//	we see the issue with bindings not taking on that first navigation from MAUI page one to MAUI page two, even though
			//  the bindings on MAUI page one always work, and the bindings on MAUI page two will work the second time you navigate.

			UIAlertView alertView = new UIAlertView("Loading some data...", string.Empty, null, null, null);

			new NSObject().InvokeOnMainThread(alertView.Show);

			_doAsyncWorker = new AsynchronousWorker(() =>
			{

				doWorkInBackground?.Invoke();

			}, () =>
			{
				workCompleted?.Invoke();

				new NSObject().InvokeOnMainThread(delegate ()
				{
					alertView.DismissWithClickedButtonIndex(-1, false);
				});
			}, true);
		}
	}
}