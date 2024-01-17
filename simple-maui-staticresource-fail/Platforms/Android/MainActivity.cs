using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Nau.Simple.Maui.Embedded.Platforms.Android;
using Nau.Simple.Maui.Staticresource.Fail;
using Nau.Simple.Maui.Staticresource.Fail.Platforms.Android;
using Nau.Simple.Maui.Staticresource.Fail.Resources.Styles;
using AndroidViews = Android.Views;
using Resource = Nau.Simple.Maui.Staticresource.Fail.Resource;

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

			FindViewById(Resource.Id.FailingListButton).SetOnClickListener(this);
			FindViewById(Resource.Id.WorkingNoListButton).SetOnClickListener(this);
			FindViewById(Resource.Id.MultiPageNavigationButton).SetOnClickListener(this);
		}

		public void OnClick(AndroidViews.View view)
		{
			int selectedFeature;

			switch (view.Id)
			{
				case Resource.Id.FailingListButton:
					selectedFeature = (int)EmbeddedHostActivity.ExampleFeatures.FailingListButton;
					break;

				case Resource.Id.WorkingNoListButton:
					selectedFeature = (int)EmbeddedHostActivity.ExampleFeatures.WorkingNoListButton;
					break;

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

			Microsoft.Maui.Controls.Application.Current.Resources.MergedDictionaries.Add(new SampleTheme());
			Microsoft.Maui.Controls.Application.Current.Resources.MergedDictionaries.Add(new SampleStyleDictionary());

			CrossPlatformContextProvider.Initialize(mauiContext);

			InjectedServiceProvider.Initialize(mauiApp.Services);
		}

	}


}
