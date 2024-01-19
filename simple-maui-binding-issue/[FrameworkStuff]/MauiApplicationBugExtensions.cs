using Nau.Simple.Maui.BindingIssue.Resources.Styles;
using Nau.Simple.Maui.BindingIssue.Resources.Themes;

namespace Nau.Simple.Maui.BindingIssue
{
	public static class MauiApplicationBugExtensions
	{
		/// <summary>
		/// Verify this application instance is configured with the expected merged resource dictionaries and if not, add them back.
		/// </summary>
		/// <param name="application">The current application instance expected to have the global merged resource dictionaries.</param>
		/// <remarks>
		/// There seems to be a bug in iOS in MAUI where the Application.Current instance sporadically loses all merged resource dictionaries.
		/// This causes key not found issues. This temporary extension method will allow us to share the resource dictionary configurations
		/// with the startup initialization and the instantiation of each Page until such time that this bug is fixed.
		/// </remarks>
		public static void VerifyMergedResourceDictionaries(this Application application)
		{
			if (application.Resources.MergedDictionaries.Count == 0)
			{
				application.Resources.MergedDictionaries.Add(new NauLightTheme());
				application.Resources.MergedDictionaries.Add(new CoreStyleDictionary());
			}
		}
	}
}
