using Android.App;
using Android.OS;
using Android.Views;
using Microsoft.Maui;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Platform;
using Nau.Simple.Maui.Core;
using System;

namespace Nau.Simple.Android.Embedded;

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
			// Note: this will launch the page to demo that simple MAUI functionality like OnAppearing and DisplayAlert do not work when using embedded.
			case ExampleFeatures.BasicEmbedded:
				pageInContext = new MainPage(new MainPageViewModel());
				break;

			// Note: this will launch the page to demo that rendering an image from a font icon file works if the MAUI context is recreated.
			case ExampleFeatures.FontIconWorking:
				mauiContext = RecreateMauiContext(savedInstanceState);
				pageInContext = new FontIconExamplePage(new FontIconExamplePageViewModel());
				break;

			// Note: this will launch the page to demo that rendering an image from a font icon file fails when trying to use the MAUI context that was created when the app loaded.
			case ExampleFeatures.FontIconFailing:
				pageInContext = new FontIconExamplePage(new FontIconExamplePageViewModel());
				break;

			// Note: this will launch the page to demo that a grouped list which throws an exception in Android when Android is its own project and not part of the single project MAUI model.
			case ExampleFeatures.GroupedList:
				pageInContext = new ListPage(new CountyListPageViewModel());
				break;

			default:
				throw new InvalidOperationException($"Do not recognize requested feature {selectedFeature}");
		}

		// Note: Unless we otherwise recreated the context for the requested feature, we'll obtain and use the MAUI context instance that was created when the app loaded.
		if (mauiContext == null)
		{
			mauiContext = CrossPlatformContextProvider.GetCrossPlatformContext();
		}

		// Setting the parent allows us access to the application level resource dictionary.
		pageInContext.Parent = Microsoft.Maui.Controls.Application.Current;

		Fragment renderedFragment = new ScopedFragment(pageInContext, mauiContext);

		// Important to prevent memory leak per the Microsoft documentation.
		pageInContext.Parent = null;

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
		BasicEmbedded = 1,
		FontIconWorking = 2,
		FontIconFailing = 3,
		GroupedList = 4
	}
}

internal class ScopedFragment : Fragment
{
	#region Private Variables
	readonly IMauiContext _mauiContext;
	#endregion Private Variables

	#region Constructors

	/// <summary>
	/// Initializes a new instance of the <see cref="ScopedFragment"/> class.
	/// </summary>
	/// <param name="detailView">The cross platform page in context to be rendered as native Android.</param>
	/// <param name="mauiContext">The cross platform context to render the native view.</param>
	public ScopedFragment(IView detailView, IMauiContext mauiContext)
	{
		DetailView = detailView;
		_mauiContext = mauiContext;
	}

	#endregion Constructors

	#region Fragment Lifecycle

	/// <summary>
	/// Handles the functionality for the lifecycle event for this fragment to draw the view for the first time.
	/// </summary>
	/// <param name="inflater">The LayoutInflater object that can be used to inflate any views in the fragment.</param>
	/// <param name="container">
	/// If non-null, this is the parent view that the fragment's UI should be attached to. 
	/// The fragment should not add the view itself, but this can be used to generate the LayoutParams of the view. This value may be <c>null</c>.
	/// </param>
	/// <param name="savedInstanceState">If non-null, this fragment is being re-constructed from a previous saved state detailed in this bundle.</param>
	/// <returns>the View for the fragment's UI.</returns>
	public override global::Android.Views.View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
	{
		return DetailView.ToPlatform(_mauiContext);
	}

	#endregion Fragment Lifecycle

	#region Public Properties

	/// <summary>
	/// Gets the cross platform page in context to be rendered as native Android.
	/// </summary>
	public IView DetailView { get; private set; }

	#endregion Public Properties
}