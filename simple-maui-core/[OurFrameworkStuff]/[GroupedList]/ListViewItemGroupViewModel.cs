#region Using Statements
using System.Collections.ObjectModel;
#endregion Using Statements

namespace Nau.Simple.Maui.Core
{
	/// <summary>
	/// Provides a view model for use in the collection backing the list item source on the reusable ListPage when grouped list rendering is desired.
	/// </summary>
	/// <typeparam name="TListViewItemViewModel">The type of the view model included in the grouped collection.</typeparam>
	public class ListViewItemGroupViewModel<TListViewItemViewModel> : ObservableCollection<TListViewItemViewModel> where TListViewItemViewModel : IListViewItemViewModel
	{
		#region Public Properties

		/// <summary>
		/// The long name for the group displayed as the text of the group header in the list.
		/// </summary>
		public string GroupLongName { get; set; }

		/// <summary>
		/// The short name used for the jump item style navigation rendered for grouped lists on devices that support them.
		/// </summary>
		public string GroupShortName { get; set; }

		#endregion Public Properties
	}
}
