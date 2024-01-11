using Foundation;
using Microsoft.Maui.Platform;
using Nau.Simple.Maui.Core;
using Nau.Simple.Maui.Embedded;
using UIKit;

namespace ButtonEventDemo
{
	/// <summary>
	/// Pulled simple controller from https://github.com/xamarin/ios-samples/tree/main/ButtonEventDemo and modified it for this sample's needs.
	/// </summary>
	public partial class ButtonEventDemoViewController : UIViewController
	{
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

			UIAlertController alertController = UIAlertController.Create("Select Test", "Select which action to demo", UIAlertControllerStyle.ActionSheet);
			alertController.AddAction(UIAlertAction.Create("Basic (OnAppearing, Display Alerts)", UIAlertActionStyle.Default, (action) => NavigateToPage(new MainPage(new MainPageViewModel()))));
			alertController.AddAction(UIAlertAction.Create("Font Icon (Works)", UIAlertActionStyle.Default, (action) => NavigateToPage(new FontIconExamplePage(new FontIconExamplePageViewModel()))));
			alertController.AddAction(UIAlertAction.Create("Grouped List (Works)", UIAlertActionStyle.Default, (action) => NavigateToPage(new ListPage(new CountyListPageViewModel()))));

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
		}

		private void NavigateToPage(ContentPage pageInContext)
		{
			// Setting the parent allows us access to the application level resource dictionary and ensures the iOS bug due to missing parent is not encountered.
			pageInContext.Parent = Application.Current;

			var renderedController = pageInContext.ToUIViewController(AppDelegate._mauiContext);

			// Important to prevent memory leak per the Microsoft documentation (presume this still applies for MAUI).
			pageInContext.Parent = null;

			renderedController.ModalPresentationStyle = UIModalPresentationStyle.FullScreen;
			PresentViewController(renderedController, true, null);
		}
	}
}