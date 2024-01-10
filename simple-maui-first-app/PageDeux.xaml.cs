namespace SimpleMauiApp
{
	public partial class PageDeux : ContentPage
	{
		public PageDeux()
		{
			InitializeComponent();

			PageDeuxViewModel vm = new PageDeuxViewModel();

			BindingContext = vm;

			vm.BindToPage(this);

			Appearing += PageDeux_Appearing;
		}

		private void PageDeux_Appearing(object? sender, EventArgs e)
		{
			System.Console.WriteLine("*** Page 2 Appearing ***");
		}
	}
}