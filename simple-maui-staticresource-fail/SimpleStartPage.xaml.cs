using System.Windows.Input;

namespace Nau.Simple.Maui.Staticresource.Fail;

public partial class SimpleStartPage : ContentPageBase
{
	public SimpleStartPage()
	{
		InitializeComponent();

		GoToListPage = new Command(NavigateToListPage);

		BindingContext = this;
	}

	public ICommand GoToListPage { get; }

	private void NavigateToListPage()
	{
		INavigationService service = InjectedServiceProvider.GetService<INavigationService>();

		service.NavigateTo(new WithListPage());
	}
}