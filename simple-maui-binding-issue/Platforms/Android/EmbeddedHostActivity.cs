using Android.App;
using Android.OS;
using Nau.Simple.Maui.BindingIssue;
using Nau.Simple.Maui.BindingIssue.Platforms.Android;
using Resource = Nau.Simple.Maui.BindingIssue.Resource;

namespace Nau.Simple.Maui.Embedded.Platforms.Android;

[Activity(Label = "EmbdeddedHostActivity", Theme = "@style/Theme.MaterialComponents.Light.DarkActionBar.Bridge")]
public class EmbeddedHostActivity : Activity
{
	public const string SelectedFeatureExtra = "SELECTED_FEATURE";

	protected override void OnCreate(Bundle? savedInstanceState)
	{
		base.OnCreate(savedInstanceState);

		ExampleFeatures selectedFeature = ExampleFeatures.Undefined;

		if (Intent?.Extras != null)
		{
			selectedFeature = (ExampleFeatures)Intent.Extras.GetInt(SelectedFeatureExtra);
		}

		if (selectedFeature == ExampleFeatures.Undefined)
		{
			throw new InvalidOperationException($"No known feature for passed value of {Intent.Extras.GetInt(SelectedFeatureExtra)}");
		}

		SetContentView(Resource.Layout.activity_onepane);

		INavigationService service = InjectedServiceProvider.GetService<INavigationService>();
		service.SetNavigationHost(this);

		Fragment fragment = GenerateEmbeddedFragment(selectedFeature, savedInstanceState);
		FragmentManager.BeginTransaction().Add(Resource.Id.LeftFrameContainer, fragment).Commit();
	}

	public ExampleFeatures SelectedMauiFeaturePage { get; set; }

	private Fragment GenerateEmbeddedFragment(ExampleFeatures selectedFeature, Bundle savedInstanceState)
	{
		IMauiContext mauiContext = null;
		ContentPage pageInContext;

		switch (selectedFeature)
		{
			case ExampleFeatures.MultiPageNavigationButton:
				pageInContext = new SimpleStartPage();
				break;

			default:
				throw new InvalidOperationException($"Do not recognize requested feature {selectedFeature}");
		}

		// Note: Unless we otherwise recreated the context for the requested feature, we'll obtain and use the MAUI context instance that was created when the app loaded.
		if (mauiContext == null)
		{
			mauiContext = CrossPlatformContextProvider.GetCrossPlatformContext();
		}

		Fragment renderedFragment = AndroidNavigation.RenderFragmentFromPage(pageInContext);

		return renderedFragment;
	}

	public enum ExampleFeatures
	{
		Undefined = 0,
		MultiPageNavigationButton = 3,
	}
}

