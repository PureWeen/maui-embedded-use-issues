using System.Windows.Input;

namespace Nau.Simple.Maui.Staticresource.Fail;

public partial class WithListPage : ContentPageBase
{
	public WithListPage()
	{
		InitializeComponent();

		SimpleList = new List<string>()
		{
			"Entry One",
			"Entry Two"
		};

		GoBack = new Command(NavigateBack);

		BindingContext = this;
	}

	public IList<string> SimpleList { get; set; }

	public ICommand GoBack { get; }

	private void NavigateBack()
	{
		INavigationService service = InjectedServiceProvider.GetService<INavigationService>();

		service.NavigateBack();
	}
}