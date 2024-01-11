#region Using Statements
using System.Collections.ObjectModel;
using System.Windows.Input;
#endregion Using Statements

namespace Nau.Simple.Maui.Core
{
	/// <summary>
	/// Provides a base to be extended by concrete view models for business process specific display and selection of items in the 
	/// reusable ListPage for displaying and selecting from a simple list of values.
	/// </summary>
	/// <remarks>
	/// The reusable list page can handle grouped lists if so desired.  This base class provided all the infrastructure necessary to handle both
	/// grouped lists or "regular" lists without grouping.  It is up to the concrete view model to set the applicable value for the grouped property
	/// and ensure the applicable observable collection is being used.
	/// </remarks>
	/// <seealso cref="ContentPageViewModelBase" />
	public abstract class ListPageViewModelBase : ContentPageViewModelBase
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="ListPageViewModelBase"/> class.
		/// </summary>
		protected ListPageViewModelBase() : base()
		{
			GroupedListItemsSource = new ObservableCollection<ListViewItemGroupViewModel<ListPageListItemViewModel>>();
			ListItemsSource = new ObservableCollection<ListPageListItemViewModel>();
			BackButtonCommand = new Command(OnBackButtonCommand);
			ItemTappedCommand = new Command<ListPageListItemViewModel>(OnItemTappedCommand);
		}

		#endregion Constructors

		#region Bound Properties

		/// <summary>
		/// Gets or sets the value bound to the page navigation header title.
		/// </summary>
		public string PageNavigationHeaderTitle { get => Get<string>(); set => Set(value); }

		/// <summary>
		///  Gets or sets the value bound to the section title displayed prior to the list.
		/// </summary>
		public string SectionTitle { get => Get<string>(); set => Set(value); }

		/// <summary>
		///  Gets or sets the value bound to configure the list to either use groups or not - <c>true</c> if list is using grouping, else <c>false</c>.
		/// </summary>
		/// <remarks>
		/// If this is <c>true</c> then the <see cref="GroupedListItemsSource"/> will be bound to the list item source on the page this view model backs,
		/// else if <c>false</c> then <see cref="ListItemsSource"/> will be used.
		/// </remarks>
		public bool IsListUsingGroups { get => Get<bool>(); set => Set(value); }

		/// <summary>
		/// Gets or sets the observable collection of grouped list items that will be bound to the page list item source of <see cref="IsListUsingGroups"/> is <c>true</c>.
		/// </summary>
		public ObservableCollection<ListViewItemGroupViewModel<ListPageListItemViewModel>> GroupedListItemsSource { get => Get<ObservableCollection<ListViewItemGroupViewModel<ListPageListItemViewModel>>>(); set => Set(value); }

		/// <summary>
		/// Gets or sets the observable collection of "regular" non grouped list items that will be bound to the page list item source of <see cref="IsListUsingGroups"/> is <c>false</c>.
		/// </summary>
		public ObservableCollection<ListPageListItemViewModel> ListItemsSource { get => Get<ObservableCollection<ListPageListItemViewModel>>(); set => Set(value); }

		/// <summary>
		/// Gets or sets a value indicating whether the source for this list is empty; <c>true</c> if no items for the list, else <c>false</c> if the list has items to display.
		/// Note this will be set by the page in this case as we bind the list source there and should not be set directly.
		/// </summary>
		public bool IsListSourceEmpty { get => Get<bool>(); set => Set(value); }

		#endregion Bound Properties

		#region Bound Commands

		/// <summary>
		/// The bound command invoked when user taps to select an item in the list.
		/// </summary>
		public ICommand ItemTappedCommand { get; protected set; }

		#endregion Bound Commands

		#region Private Navigation Bar Commands

		/// <summary>
		/// The bound command invoked when user taps the Back button.
		/// </summary>
		private ICommand BackButtonCommand { get; }

		#endregion Private Navigation Bar Commands

		#region ContentPageViewModelBase Implementation


		/// <inheritdoc/>
		protected override void InitializeBoundProperties()
		{
			base.InitializeBoundProperties();

			PopulateListItemSource();
		}

		#endregion ContentPageViewModelBase Implementation

		#region Protected Properties

		/// <summary>
		/// Gets or sets a value indicating whether the list should display a selected disclosure indicator for the item in the list
		/// that corresponds to the currently selected value passed at the time of initialization (if applicable);
		/// <c>true</c> if this list is configured to display selected disclosure indicators, else <c>false</c> if no such disclosure indicator should be displayed.
		/// </summary>
		protected bool ShouldDisplaySelectedIndicator { get; set; }

		#endregion Protected Properties

		#region Protected Methods 

		/// <summary>
		/// Clears the observable collections of list items of any values.
		/// </summary>
		protected void ClearItemSource()
		{
			ListItemsSource.Clear();
			GroupedListItemsSource.Clear();
		}

		/// <summary>
		/// Populates the collection of list items with data.
		/// </summary>
		protected abstract void PopulateListItemSource();

		/// <summary>
		/// Adds an item to the collection of list items for grouped lists.
		/// </summary>
		/// <param name="itemId">The identifier (if applicable) of the item from a model serving as the source of this list item to allow for later identification and interaction.</param>
		/// <param name="itemDescription">The description text to display for the item in the list.</param>
		protected void AddItemToGroupedItemSource(string itemId, string itemDescription)
		{
			const string EmptyItemGroupingValue = " ";

			// For our purposes, the value displayed in the header for the item group (longName) as well as any device specific jump list (shortName) will
			// simply be A-Z style so we're use the first letter of the item description for both. If this should ever need to change we'll have to figure something
			// out to allow concrete view models to configure.

			// In addition, for some lists we allow an empty value to signify not selected, so if the item description is empty we'll use a single space for the starting
			// character so it's grouped at the top.

			string itemStartingCharacterValue = string.IsNullOrEmpty(itemDescription) ? EmptyItemGroupingValue : itemDescription.Substring(0, 1);
			string longName = itemStartingCharacterValue;
			string shortName = itemStartingCharacterValue;

			// See if we have an existing grouped view model this item should be part of or if we need to create a new group view model.
			ListViewItemGroupViewModel<ListPageListItemViewModel> groupedViewModel = GroupedListItemsSource.FirstOrDefault(x => x.GroupShortName == shortName);

			if (groupedViewModel == null)
			{
				groupedViewModel = new ListViewItemGroupViewModel<ListPageListItemViewModel>()
				{
					GroupLongName = longName,
					GroupShortName = shortName
				};

				GroupedListItemsSource.Add(groupedViewModel);
			}

			groupedViewModel.Add(BuildItemSource(itemId, itemDescription));
		}

		/// <summary>
		/// Adds an item to the collection of list items for "regular" lists without grouping.
		/// </summary>
		/// <param name="itemId">The identifier (if applicable) of the item from a model serving as the source of this list item to allow for later identification and interaction.</param>
		/// <param name="itemDescription">The description text to display for the item in the list.</param>
		protected void AddItemToItemSource(string itemId, string itemDescription)
		{
			ListItemsSource.Add(BuildItemSource(itemId, itemDescription));
		}

		/// <summary>
		/// Implementation of the Command triggered when user taps on back button in the list's navigation header.
		/// </summary>
		/// <remarks>
		/// Override to provide a different implementation.
		/// </remarks>
		protected virtual void OnBackButtonCommand()
		{

		}

		/// <summary>
		/// Implementation of the Command triggered when user taps on an item in the list.
		/// </summary>
		/// <param name="selectedItemVm">The selected item from the list.</param>
		protected abstract void OnItemTappedCommand(ListPageListItemViewModel selectedItemVm);

		/// <summary>
		/// Determines whether the provided list item is denoted as the currently selected item.
		/// </summary>
		/// <param name="itemId">The identifier of the item in the list.</param>
		/// <returns><c>true</c> if the provided identifier matches the identifier provided for the currently selected item, else <c>false</c> if not the currently selected item or the list is not configured to display the selected item indicator.</returns>
		/// <exception cref="NotImplementedException">When a concrete implementation has not been provided but the list is configured to displayed selected disclosure indicators.</exception>
		protected virtual bool IsItemCurrentlySelected(string itemId)
		{
			if (ShouldDisplaySelectedIndicator)
			{
				throw new NotImplementedException("Must provide an implementation if list is configured to display the selected item disclosure indicator");
			}

			return false;
		}

		#endregion Protected Methods 

		#region Private Methods

		/// <summary>
		/// Creates and returns the view model for an individual item in the list.
		/// </summary>
		/// <param name="itemId">The identifier (if applicable) of the item from a model serving as the source of this list item to allow for later identification and interaction.</param>
		/// <param name="itemDescription">The description text to display for the item in the list.</param>
		/// <returns>The configured list item view model instance.</returns>
		private ListPageListItemViewModel BuildItemSource(string itemId, string itemDescription)
		{
			ListPageListItemViewModel itemVm = new ListPageListItemViewModel
			{
				ItemId = itemId,
				ItemDescription = itemDescription,
				IsCurrentSelection = ShouldDisplaySelectedIndicator && IsItemCurrentlySelected(itemId),
			};

			return itemVm;
		}

		#endregion Private Methods
	}
}
