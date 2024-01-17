using Android.OS;
using Android.Views;
using Microsoft.Maui.Platform;
using Nau.Simple.Maui.Embedded.Platforms.Android;
using Fragment = Android.App.Fragment;

namespace Nau.Simple.Maui.Staticresource.Fail.Platforms.Android
{

	public class AndroidNavigation : INavigationService
	{
		public AndroidNavigation()
		{
		}

		public void NavigateBack()
		{
			if (HostNavigationActivity.FragmentManager.BackStackEntryCount == 0)
			{
				HostNavigationActivity.Finish();
			}
			else
			{
				HostNavigationActivity.FragmentManager.PopBackStack();
			}
		}

		public void NavigateTo(ContentPage page)
		{
			NavigateToXamarinPage(page);
		}

		public void SetNavigationHost(object nativeHost)
		{
			HostNavigationActivity = nativeHost as EmbeddedHostActivity ?? throw new ArgumentException($"Expected instance of {nameof(EmbeddedHostActivity)}", nameof(nativeHost));
		}

		private EmbeddedHostActivity HostNavigationActivity { get; set; }

		private void NavigateToXamarinPage(ContentPage page)
		{
			Fragment renderedFragment = RenderFragmentFromPage(page);

			HostNavigationActivity.FragmentManager.BeginTransaction()
					.Replace(Resource.Id.LeftFrameContainer, renderedFragment)
					.AddToBackStack(null)
					.Commit();
		}

		/// <summary>
		/// Configures the page to ensure access to global resources and returns rendered Android version of the page as a <see cref="Fragment"/>.
		/// </summary>
		/// <param name="page">The content page to render.</param>
		/// <returns>The rendered <see cref="Fragment"/> from the provided page.</returns>
		/// <remarks>
		/// See https://docs.microsoft.com/en-us/xamarin/xamarin-forms/platform/native-forms for more information on how to integrate Xamarin Forms with native.
		/// </remarks>
		public static Fragment RenderFragmentFromPage(ContentPage page)
		{
			// Setting the parent allows us access to the application level resource dictionary.
			page.Parent = Microsoft.Maui.Controls.Application.Current;

			Fragment renderedFragment = page.CreateSupportFragment(CrossPlatformContextProvider.GetCrossPlatformContext());

			// Important to prevent memory leak per the Microsoft documentation.
			page.Parent = null;

			return renderedFragment;
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

	#region class : PageExtensions

	/// <summary>
	/// Provides extension methods to render a native Android Fragment from the cross platform framework Page.
	/// </summary>
	/// <remarks>
	/// The code for this extension method is from the <see href="https://learn.microsoft.com/en-us/dotnet/maui/platform-integration/native-embedding">Microsoft Native Embedding Documentation</see>
	/// and should be altered with care.
	/// </remarks>
	public static class PageExtensions
	{
		#region Public Extension Method

		/// <summary>
		/// Creates the native Android Fragment from this cross platform Page.
		/// </summary>
		/// <param name="view">The cross platform page in context.</param>
		/// <param name="crossPlatformContext">The instance of the configured cross platform context required for the conversion.</param>
		/// <returns>Fragment.</returns>
		public static Fragment CreateSupportFragment(this ContentPage view, IMauiContext crossPlatformContext)
		{
			return new ScopedFragment(view, crossPlatformContext);
		}

		#endregion Public Extension Method

		#region class : ScopedFragment

		/// <summary>
		/// Provides the implementation of a native Fragment wrapping the converted Android View. 
		/// </summary>
		/// <seealso cref="Fragment" />
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

		#endregion class : ScopedFragment
	}

	#endregion class : PageExtensions
}
