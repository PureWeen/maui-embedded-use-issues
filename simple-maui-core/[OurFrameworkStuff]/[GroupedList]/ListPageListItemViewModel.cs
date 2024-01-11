namespace Nau.Simple.Maui.Core
{
	/// <summary>
	/// Provides a view model representing an item in the items source that backs the reusable ListPage for displaying and selecting from a simple list of values.
	/// </summary>
	/// <seealso cref="ViewModelBase" />
	/// <seealso cref="IListViewItemViewModel" />
	public class ListPageListItemViewModel : ViewModelBase, IListViewItemViewModel
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="ListPageListItemViewModel"/> class.
		/// </summary>
		public ListPageListItemViewModel() : base()
		{
		}

		#endregion Constructors

		#region IListViewItemViewModel Implementation

		/// <inheritdoc/>
		public string ItemId { get => Get<string>(); set => Set(value); }

		/// <inheritdoc/>
		public string ItemDescription { get => Get<string>(); set => Set(value); }

		/// <inheritdoc/>
		public bool IsCurrentSelection { get => Get<bool>(); set => Set(value); }

		#endregion IListViewItemViewModel Implementation
	}
}
