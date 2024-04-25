namespace Nau.Simple.Maui.Core
{
	public partial class MainPage : ContentPage
	{
		int count = 0;

		public MainPage(MainPageViewModel viewModel)
		{
			InitializeComponent();

			BindingContext = viewModel;

			viewModel.BindToPage(this);

			Appearing += MainPage_Appearing;
			Disappearing += MainPage_Disappearing;
		}

		private void OnCounterClicked(object sender, EventArgs e)
		{
			count++;

			if (count == 1)
			{
				CounterBtn.Text = $"Clicked {count} time";
			}
			else
			{
				CounterBtn.Text = $"Clicked {count} times";
			}
		}

		private void MainPage_Appearing(object sender, EventArgs e)
		{
			// Note: this will never be hit with the embedded use case of MAUI.
			System.Console.WriteLine("*** Appearing ***");
		}

		private void MainPage_Disappearing(object sender, EventArgs e)
		{
			// Note: this will never be hit with the embedded use case of MAUI.
			System.Console.WriteLine("*** Disappearing ***");
		}

        protected override void OnParentSet()
        {
            base.OnParentSet();
        }
    }

}
