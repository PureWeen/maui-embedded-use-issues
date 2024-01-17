using Android.App;
using Android.OS;
using Nau.Simple.Maui.Staticresource.Fail;
using Nau.Simple.Maui.Staticresource.Fail.Platforms.Android;
using Resource = Nau.Simple.Maui.Staticresource.Fail.Resource;

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
			// Note: this will launch the page that if is the first MAUI page loaded illustrates the static resource references crash the app when the requested resource is for an item in a list.
			//		Note that this should actually work just fine in Android - only seen it crash in iOS.
			case ExampleFeatures.FailingListButton:
				pageInContext = new WithListPage();
				break;

			// Note: this will launch the page to demo that if is the first MAUI page loaded the same request for a static resource outside of a list works just fine.
			case ExampleFeatures.WorkingNoListButton:
				mauiContext = RecreateMauiContext(savedInstanceState);
				pageInContext = new NoListPage();
				break;

			// Note: this will launch the page to demo that if an initial MAUI page is loaded, and then we navigate to the same page with a list that crashed trying to access a static resource
			//		that it works fine as the second page. Note this was never an issue for Android - the issue was always only in iOS.
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

	private IMauiContext RecreateMauiContext(Bundle savedInstanceState)
	{
		Platform.Init(this, savedInstanceState);

		var mauiApp = MauiProgram.CreateMauiApp();
		var mauiContext = new MauiContext(mauiApp.Services, this);

		return mauiContext;
	}

	public enum ExampleFeatures
	{
		Undefined = 0,
		FailingListButton = 1,
		WorkingNoListButton = 2,
		MultiPageNavigationButton = 3,
	}
}

