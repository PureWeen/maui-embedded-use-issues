namespace Nau.Simple.Maui.Core
{
	/// <summary>
	/// Provides a view model for displaying and selecting from a list of values.
	/// </summary>
	/// <typeparam name="T">The data type of the values presented in the list.</typeparam>
	/// <seealso cref="ListPageViewModelBase" />
	public class ValueListPageViewModel<T> : ListPageViewModelBase
	{
		#region Private Fields
		private readonly Func<T, string> _formatListItem;
		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="ValueListPageViewModel{T}"/> class.
		/// </summary>
		/// <param name="headerTitle">The header title.</param>
		/// <param name="sectionTitle">The section title.</param>
		/// <param name="updateWithSelectedValueAction">The action to update an external value with the selected item from this view model.</param>
		/// <param name="valuesList">The values list.</param>
		/// <param name="formatListItem">A function to be used to format the list item.</param>
		public ValueListPageViewModel(string headerTitle, string sectionTitle, Action<T> updateWithSelectedValueAction, IEnumerable<T> valuesList, Func<T, string> formatListItem = null) : base()
		{
			PageNavigationHeaderTitle = headerTitle;
			SectionTitle = sectionTitle;
			UpdateWithSelectedValueAction = updateWithSelectedValueAction;
			ValuesList = valuesList;
			IsListUsingGroups = false;
			_formatListItem = formatListItem;

			ItemTappedCommand = new Command<ListPageListItemViewModel>(OnItemTappedCommand);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ValueListPageViewModel{T}"/> class.
		/// </summary>
		/// <param name="headerTitle">The header title.</param>
		/// <param name="sectionTitle">The section title.</param>
		/// <param name="currentSelectionValue">The currently selected value, if any, in the list - this will result in an indicator next to the corresponding list item.</param>
		/// <param name="updateWithSelectedValueAction">The action to update an external value with the selected item from this view model.</param>
		/// <param name="valuesList">The values list.</param>
		/// <param name="formatListItem">A function to be used to format the list item.</param>
		public ValueListPageViewModel(string headerTitle, string sectionTitle, T currentSelectionValue, Action<T> updateWithSelectedValueAction, IEnumerable<T> valuesList, Func<T, string> formatListItem = null) : this(headerTitle, sectionTitle, updateWithSelectedValueAction, valuesList, formatListItem)
		{
			ShouldDisplaySelectedIndicator = true;
			CurrentSelectionValue = currentSelectionValue;
		}

		#endregion Constructors

		#region ListPageViewModelBase Implementation

		/// <inheritdoc/>
		protected override void PopulateListItemSource()
		{
			ClearItemSource();

			foreach (T p in ValuesList)
			{
				string displayText;
				if (_formatListItem != null)
				{
					displayText = _formatListItem(p);
				}
				else
				{
					displayText = p.ToString();
				}

				AddItemToItemSource(p.ToString(), displayText);
			}
		}

		/// <inheritdoc/>
		protected sealed override void OnItemTappedCommand(ListPageListItemViewModel selectedItemVm)
		{

		}

		/// <inheritdoc/>
		protected override bool IsItemCurrentlySelected(string itemId)
		{
			bool isSelected = itemId == CurrentSelectionValue.ToString();
			return isSelected;
		}

		#endregion ListPageViewModelBase Implementation

		#region Protected Properties

		/// <summary>
		/// Gets the values list.
		/// </summary>
		protected IEnumerable<T> ValuesList { get; }

		/// <summary>
		/// Gets the currently selected value, if any, in the list at instantiation.
		/// </summary>
		protected T CurrentSelectionValue { get; }

		#endregion Protected Properties

		#region Protected Methods

		/// <summary>
		/// Gets the single instance from the values list matching the item selected from the display list.
		/// </summary>
		/// <param name="selectedItemVm">The view model backing the selected list item.</param>
		/// <returns>The matching value in the values list.</returns>
		protected virtual T GetSingleMatch(ListPageListItemViewModel selectedItemVm)
		{
			T selectedValue = ValuesList.Single(x => x.ToString() == selectedItemVm.ItemId);
			return selectedValue;
		}

		#endregion Protected Methods

		#region Private Properties

		/// <summary>
		/// Gets the update with selected value action.
		/// </summary>
		private Action<T> UpdateWithSelectedValueAction { get; }

		#endregion Private Properties

	}
}
