namespace SimpleMauiApp
{
	public partial class MainPage : ContentPage
	{
		int count = 0;

		public MainPage()
		{
			InitializeComponent();

			MainPageViewModel vm = new MainPageViewModel();

			BindingContext = vm;

			vm.BindToPage(this);

			Appearing += MainPage_Appearing;
			Disappearing += MainPage_Disappearing;
		}

		private void MainPage_Disappearing(object? sender, EventArgs e)
		{
			// Note: when using a straight MAUI first use case the appearing and disappearing events fire. Not so when using an embedded use case (starting Native going to MAUI page).
			System.Console.WriteLine("*** Disappearing ***");
		}

		private void MainPage_Appearing(object? sender, EventArgs e)
		{
			// Note: when using a straight MAUI first use case the appearing and disappearing events fire. Not so when using an embedded use case (starting Native going to MAUI page).
			System.Console.WriteLine("*** Appearing ***");
		}

		private void OnCounterClicked(object sender, EventArgs e)
		{
			count++;

			if (count == 1)
				CounterBtn.Text = $"Clicked {count} time";
			else
				CounterBtn.Text = $"Clicked {count} times";

			SemanticScreenReader.Announce(CounterBtn.Text);
		}
	}

}
