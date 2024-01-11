using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Microsoft.Maui;
using Microsoft.Maui.ApplicationModel;
using Nau.Simple.Maui.Core;
using System;
using AndroidViews = Android.Views;

namespace Nau.Simple.Android.Embedded
{
	[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
	public class MainActivity : Activity, AndroidViews.View.IOnClickListener
	{
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.activity_main);

			InitializeMaui(savedInstanceState);

			FindViewById(Resource.Id.MainEmbeddedExampleButton).SetOnClickListener(this);
			FindViewById(Resource.Id.FontIconWorkExampleButton).SetOnClickListener(this);
			FindViewById(Resource.Id.FontIconFailExampleButton).SetOnClickListener(this);
			FindViewById(Resource.Id.GroupedListExampleButton).SetOnClickListener(this);
		}

		public void OnClick(AndroidViews.View view)
		{
			int selectedFeature;

			switch (view.Id)
			{
				case Resource.Id.MainEmbeddedExampleButton:
					selectedFeature = (int)EmbeddedHostActivity.ExampleFeatures.BasicEmbedded;
					break;

				case Resource.Id.FontIconWorkExampleButton:
					selectedFeature = (int)EmbeddedHostActivity.ExampleFeatures.FontIconWorking;
					break;

				case Resource.Id.FontIconFailExampleButton:
					selectedFeature = (int)EmbeddedHostActivity.ExampleFeatures.FontIconFailing;
					break;

				case Resource.Id.GroupedListExampleButton:
					selectedFeature = (int)EmbeddedHostActivity.ExampleFeatures.GroupedList;
					break;

				default:
					throw new InvalidOperationException($"Do not recognized click event for id {view.Id}");
			}

			Intent intent = new Intent(this, typeof(EmbeddedHostActivity));
			intent.PutExtra(EmbeddedHostActivity.SelectedFeatureExtra, selectedFeature);

			StartActivity(intent);

			// Note: Calling Finish to end this initial activity seems to be the root of why the font icon example that tries to use the initial MAUI context does not work.
			//		If I comment this out, then the "FontIconFailExample" does correctly display the expected icon. However, this seems like a common thing one might do (calling Finish on the initial acivity)
			//		and this was never an issue in Xamarin Forms.
			Finish();
		}

		private void InitializeMaui(Bundle savedInstanceState)
		{
			Platform.Init(this, savedInstanceState);

			var mauiApp = MauiProgram.CreateMauiApp();

			var mauiContext = new MauiContext(mauiApp.Services, this);

			CrossPlatformContextProvider.Initialize(mauiContext);
		}

	}


}
