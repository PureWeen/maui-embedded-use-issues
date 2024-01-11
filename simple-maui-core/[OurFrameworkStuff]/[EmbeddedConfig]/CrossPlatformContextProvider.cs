namespace Nau.Simple.Maui.Core
{
	/// <summary>
	/// Provides a means to persist and access the cross platform context created when the application launches from anywhere within the native application.
	/// </summary>
	/// <remarks>
	/// An implementation of <see cref="IMauiContext"/> is created when the application first launches and bootstraps and configures the embedding of the 
	/// MAUI cross platform framework with the native application.  This context is needed when rendering a native UI from the cross platform Page UI.
	/// <para>
	/// A static provider seemed to be the best way to access this as all other attempts to access it off the <see cref="Microsoft.Maui.Controls.Application"/>
	/// did not seem to work properly on account of not being a mix of Native and Cross Platform vs a pure MAUI application.
	/// </para>
	/// </remarks>
	public static class CrossPlatformContextProvider
	{
		#region Initialize Method

		/// <summary>
		/// Initializes this provider with the cross platform context for use throughout the application.
		/// </summary>
		/// <param name="crossPlatformContext">The cross platform framework context that was initialized at startup.</param>
		/// <exception cref="ArgumentNullException">when <paramref name="crossPlatformContext"/> is <c>null</c>.</exception>
		public static void Initialize(IMauiContext crossPlatformContext)
		{
			CrossPlatformContext = crossPlatformContext ?? throw new ArgumentNullException(nameof(crossPlatformContext), "Must provide a non null context.");
		}

		#endregion Initialize Method

		#region Public Methods

		/// <summary>
		/// Gets the cross platform context registered with this provider.
		/// </summary>
		/// <returns>The cross platform framework context that was initialized at startup.</returns>
		/// <exception cref="System.InvalidOperationException">When the context is <c>null</c>.</exception>
		public static IMauiContext GetCrossPlatformContext()
		{
			return CrossPlatformContext ?? throw new InvalidOperationException("You must call Initialize before accessing the context.");
		}

		#endregion Public Methods

		#region Private Properties

		/// <summary>
		/// Gets or sets the instance of the cross platform context required for embedding the cross platform framework Pages with the native Android views.
		/// </summary>
		private static IMauiContext CrossPlatformContext { get; set; }

		#endregion Private Properties
	}
}
