using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Nau.Simple.Maui.BindingIssue;
using Nau.Simple.Maui.BindingIssue.Platforms.Android;
using Nau.Simple.Maui.BindingIssue.Resources.Styles;
using Nau.Simple.Maui.Embedded.Platforms.Android;
using AndroidViews = Android.Views;
using Resource = Nau.Simple.Maui.BindingIssue.Resource;

namespace Nau.Simple.Maui.Embedded
{
	[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
	public class MainActivity : Activity, AndroidViews.View.IOnClickListener
	{
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.activity_main);

			InitializeMaui(savedInstanceState);

			FindViewById(Resource.Id.MultiPageNavigationButton).SetOnClickListener(this);
		}

		public void OnClick(AndroidViews.View view)
		{
			int selectedFeature;

			switch (view.Id)
			{
				case Resource.Id.MultiPageNavigationButton:
					selectedFeature = (int)EmbeddedHostActivity.ExampleFeatures.MultiPageNavigationButton;
					break;

				default:
					throw new InvalidOperationException($"Do not recognized click event for id {view.Id}");
			}

			Intent intent = new Intent(this, typeof(EmbeddedHostActivity));
			intent.PutExtra(EmbeddedHostActivity.SelectedFeatureExtra, selectedFeature);

			StartActivity(intent);
			Finish();
		}

		private void InitializeMaui(Bundle savedInstanceState)
		{
			Platform.Init(this, savedInstanceState);

			var mauiApp = MauiProgram.CreateMauiApp((builder) =>
			{
				builder.Services.AddSingleton<INavigationService, AndroidNavigation>();
			});

			var mauiContext = new MauiContext(mauiApp.Services, this);

			// Create an instance of the Cross Platform Framework application for consumption by the Pages in order to access global resource dictionaries.
			Microsoft.Maui.Controls.Application.Current = new Microsoft.Maui.Controls.Application()
			{
				Resources = new AppDictionary()
			};

			// Extension method to centralize setting up the merged dictionaries to reduce duplication from bug workaround and startup
			Microsoft.Maui.Controls.Application.Current.VerifyMergedResourceDictionaries();

			CrossPlatformContextProvider.Initialize(mauiContext);

			InjectedServiceProvider.Initialize(mauiApp.Services);
		}

	}


}
