using System.Windows.Input;

namespace SimpleMauiApp
{
	public class PageDeuxViewModel
	{
		public PageDeuxViewModel()
		{
			PageDeuxTestCommand = new Command(async () => await TestPageTwpAsync());
		}

		public ICommand PageDeuxTestCommand { get; }

		public void BindToPage(ContentPage page)
		{
			PageInContext = page;
		}

		private async Task TestPageTwpAsync()
		{
			bool result = await PageInContext.DisplayAlert("Test", "Pleas select a response", "True", "False");

			System.Console.WriteLine($"*** Replied with {result} ***");
		}

		private ContentPage PageInContext { get; set; }
	}
}