namespace Nau.Simple.Maui.BindingIssue
{
	public abstract class ContentPageBase : ContentPage
	{
		protected ContentPageBase()
		{
			if (Application.Current.Resources.MergedDictionaries.Count == 0)
			{
				System.Console.WriteLine("*** Merged Resource Dictionaries Gone ***");
			}

			// This is a workaround to resolve the issue of the Application instance losing reference to the merged dictionaries and crashing in iOS.
			Application.Current.VerifyMergedResourceDictionaries();
		}
	}
}
