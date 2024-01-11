using System.Windows.Input;

namespace Nau.Simple.Maui.Core
{
	public class MainPageViewModel
	{
		public MainPageViewModel()
		{
			ShowAlertCommand = new Command(async () => await ShowAlertAsync());
			ShowActionSheetCommand = new Command(async () => await ShowActionSheetAsync());
		}

		public ICommand ShowAlertCommand { get; }

		public ICommand ShowActionSheetCommand { get; }

		public void BindToPage(ContentPage page)
		{
			PageInContext = page;
		}

		private ContentPage PageInContext { get; set; }

		private async Task ShowAlertAsync()
		{
			System.Console.WriteLine("*** Display Alert Should Show (but won't) ***");

			bool selectedOption = await PageInContext.DisplayAlert("Did this appear?", "Please select an option", "True", "False");

			// Note: this will never be hit with the embedded use case of MAUI as the alert will never be displayed and no result captured.
			System.Console.WriteLine($"*** Selected value from DisplayAlert is {selectedOption}. ***");
		}

		private async Task ShowActionSheetAsync()
		{
			System.Console.WriteLine("*** Display Action Sheet Should Show (but won't) ***");

			string selectedOption = await PageInContext.DisplayActionSheet("Please select an option", "Cancel Option", "Destructive Option", "Option One", "Option Two");

			// Note: this will never be hit with the embedded use case of MAUI as the action sheet will never be displayed and no result captured.
			System.Console.WriteLine($"*** Selected value from DisplayActionSheet is {selectedOption}. ***");
		}
	}
}
