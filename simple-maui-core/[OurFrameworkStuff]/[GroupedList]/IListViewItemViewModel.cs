namespace Nau.Simple.Maui.Core
{
	/// <summary>
	/// Provides an interface to be implemented by any view model representing an item in the items source that backs the reusable ListPage for displaying and selecting from a simple list of values.
	/// </summary>
	public interface IListViewItemViewModel
	{
		#region Properties

		/// <summary>
		/// Gets or sets the identifier (if applicable) of the item from a model list backing this view model to allow for later identification and interaction.
		/// </summary>
		string ItemId { get; set; }

		/// <summary>
		/// Gets or sets the description text to display for the item in the list.
		/// </summary>
		string ItemDescription { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is the currently selected item in cases where there is an existing selection;
		/// <c>true</c> if the item is the currently selected value, else <c>false</c> if not the currently selected value or the list is not configured
		/// to denote the selected item.
		/// </summary>
		bool IsCurrentSelection { get; set; }

		#endregion Properties
	}
}
