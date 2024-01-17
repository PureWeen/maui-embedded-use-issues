namespace Nau.Simple.Maui.Staticresource.Fail
{
	public interface INavigationService
	{
		void NavigateTo(ContentPage page);

		void NavigateBack();

		void SetNavigationHost(object nativeHost);
	}
}
