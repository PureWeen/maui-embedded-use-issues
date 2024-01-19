using System.Windows.Input;

namespace Nau.Simple.Maui.BindingIssue;

public partial class SimpleStartPage : ContentPageBase
{
	public SimpleStartPage()
	{
		InitializeComponent();

		GoToListPage = new Command(NavigateToListPage);

		SomeName = "Bob Smith";

		BindingContext = this;
	}

	public string SomeName { get; set; }

	public ICommand GoToListPage { get; }

	private void NavigateToListPage()
	{
		INavigationService service = InjectedServiceProvider.GetService<INavigationService>();

		service.NavigateTo(new WithListPage());
	}
}