namespace Nau.Simple.Maui.BindingIssue
{
	public interface INavigationService
	{
		void NavigateTo(ContentPage page);

		void Present(ContentPage page, bool shouldDisplayFullScreen);

		void NavigateBack();

		void SetNavigationHost(object nativeHost);
	}
}
