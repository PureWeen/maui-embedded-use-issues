#region Using Statements
#endregion Using Statements

namespace Nau.Simple.Maui.Core;

/// <summary>
/// Provides a base class to be extended by all view models backing a Xamarin Forms Content Page.
/// </summary>
/// <seealso cref="ViewModelBase" />

public abstract class ContentPageViewModelBase : ViewModelBase
{
	#region Constructors

	/// <summary>
	/// Initializes a new instance of the <see cref="ContentPageViewModelBase"/> class.
	/// </summary>
	protected ContentPageViewModelBase() : base()
	{
	}

	#endregion Constructors

	#region Protected Methods



	/// <summary>
	/// Initializes the bound properties of the view model bound to the page to new instances of their respective data types.
	/// </summary>
	/// <remarks>
	/// This is invoked when the view model is bound to the page and each view model should override
	/// to provide an implementation that sets its bound properties to new instances.  With the
	/// way the framework is set up for bound properties we cannot provide initialization at the property level.
	/// <para>
	/// Commands should not be instantiated in this method but rather in the constructor of the view model.
	/// Some Pages have behaviors that convert events to commands and those get configured when the page is instantiated
	/// so the command in the view model must be instantiated at that point as well.  If we were to wait and initialize the 
	/// command in this method it would be too late and the behavior would not work as expected.
	/// </para>
	/// <para>
	/// This method is not necessarily meant to set values from a model to the view model bound properties.
	/// Often, we wish to do that every time the page appears and usually provide a separate method to handle 
	/// updating the view model with actual data.
	/// </para>
	/// </remarks>
	protected virtual void InitializeBoundProperties()
	{
	}


	#endregion Protected Methods

	#region Xamarin Forms Page support

	/// <summary>
	/// Configures what is needed to make the view model and view to work together.
	/// </summary>
	/// <param name="page">The view (Page) being bound to the view model.</param>
	public void BindToPage(Page page)
	{
		WireEvents(page);
		page.BindingContext = this;

		CurrentPage = page;

		InitializeBoundProperties();


	}

	/// <summary>
	/// The current page configured to work together with the view model.
	/// </summary>
	/// <remarks>
	private Page CurrentPage { get; set; }

	/// <summary>
	/// Wires up the events to forward from view (Page) to view model. 
	/// </summary>
	/// <param name="page">The view (Page) being bound to the view model.</param>
	protected virtual void WireEvents(Page page)
	{
		page.Appearing += OnAppearing;
		page.Disappearing += OnDisappearing;
	}

	/// <summary>
	/// Unwires the wired events forwarded from view (Page) to view model. 
	/// </summary>
	/// <param name="page">The view (Page) being bound to the view model.</param>
	protected virtual void UnWireEvents(Page page)
	{
		page.Appearing -= OnAppearing;
		page.Disappearing -= OnDisappearing;
	}

	/// <summary>
	/// The forwarded OnAppearing event from the page.
	/// </summary>
	/// <param name="sender">The event initiator.</param>
	/// <param name="e">The event arguments.</param>
	protected virtual void OnAppearing(object sender, EventArgs e)
	{
	}

	/// <summary>
	/// The forwarded OnDisappearing event from the page.
	/// </summary>
	/// <param name="sender">The event initiator.</param>
	/// <param name="e">The event arguments.</param>
	protected virtual void OnDisappearing(object sender, EventArgs e)
	{
	}

	/// <summary>
	/// Method for consolidating the updates to the view from resource dictionary. Consolidation here helps ensure user experience is optimized.
	/// </summary>
	protected virtual void UpdateWithResources()
	{
		// Set page and view model defaults from resources.
		if (CurrentPage != null)
		{
			CurrentPage.Title = CurrentPage.GetType().Name;
		}
	}

	#endregion Xamarin Forms Page support

	#region Public Data Bound Properties

	/// <summary>
	/// Gets or sets the title to display in the navigation bar of the page.
	/// </summary>
	public string PageTitle { get => Get<string>(); set => Set(value); }

	#endregion Public Data Bound Properties
}

