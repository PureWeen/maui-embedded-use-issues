namespace Nau.Simple.Maui.Core;

public partial class FontIconExamplePage : ContentPage
{
	public FontIconExamplePage(FontIconExamplePageViewModel viewModel)
	{
		InitializeComponent();

		viewModel.BindToPage(this);
	}
}