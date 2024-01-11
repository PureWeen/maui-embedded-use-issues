namespace Nau.Simple.Maui.Core
{
	/// <summary>
	/// Code-behind class for the xaml file. 
	/// </summary>
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ListPage
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="ListPage"/> class.
		/// </summary>
		/// <param name="viewModel">The view model to bind to the page.</param>
		public ListPage(ListPageViewModelBase viewModel)
		{
			InitializeComponent();

			viewModel.BindToPage(this);

			ConfigureListItemSource(viewModel);
		}

		#endregion Constructors

		#region Private Methods

		/// <summary>
		/// Configures the list item source depending on whether we're binding to a grouped list or not.
		/// </summary>
		/// <param name="viewModel">The view model bound to this page.</param>
		private void ConfigureListItemSource(ListPageViewModelBase viewModel)
		{
			bool isEmpty;

			// Typically we bind the ItemsSource in Xaml, however, since we want to allow for the possibility of using grouped lists or not, we need to 
			// actually set it in the code behind this time.
			if (viewModel.IsListUsingGroups)
			{
				ItemListView.ItemsSource = viewModel.GroupedListItemsSource;
				isEmpty = viewModel.GroupedListItemsSource.Count == 0;
			}
			else
			{
				ItemListView.ItemsSource = viewModel.ListItemsSource;
				isEmpty = viewModel.ListItemsSource.Count == 0;
			}

			viewModel.IsListSourceEmpty = isEmpty;
		}

		#endregion Private Methods
	}
}