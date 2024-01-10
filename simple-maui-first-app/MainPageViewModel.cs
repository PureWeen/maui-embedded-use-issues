using System.Windows.Input;

namespace SimpleMauiApp
{
	public class MainPageViewModel
	{
		public MainPageViewModel()
		{
			PromptToNavigateCommand = new Command(async () => await PromptToNavigateAsync());
		}

		public ICommand PromptToNavigateCommand { get; }

		public void BindToPage(ContentPage page)
		{
			PageInContext = page;
		}

		private async Task PromptToNavigateAsync()
		{
			System.Console.WriteLine("*** Command from Tap Event Fires ***");

			// Note: the DisplayAlert executes as expected when using a straight MAUI first use case. Not so when using an embedded use case (starting Native going to MAUI page).
			bool shouldNavigate = await PageInContext.DisplayAlert("Continue?", "Navigate to next page?", "OK", "Cancel");

			if (shouldNavigate)
			{
				await PageInContext.Navigation.PushAsync(new PageDeux());
			}
			else
			{
				System.Console.WriteLine("*** Elected not to navigate from DisplayAlert. ***");
			}
		}

		private ContentPage PageInContext { get; set; }
	}
}
