namespace Nau.Simple.Maui.Core
{
	public class MobileDomainItem
	{
		public string Code { get; set; }
		public string Description { get; set; }
	}

	public class CountyListPageViewModel : ListPageViewModelBase
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="CountyListPageViewModel"/> class.
		/// </summary>
		public CountyListPageViewModel() : base()
		{
			PageNavigationHeaderTitle = "Select County";
			SectionTitle = "Counties";
			IsListUsingGroups = true;
		}

		#endregion Constructors

		#region ListPageViewModelBase Implementation

		/// <summary>
		/// Populates the collection of list items with data.
		/// </summary>
		protected override void PopulateListItemSource()
		{
			ClearItemSource();

			List<MobileDomainItem> counties = new List<MobileDomainItem>()
			{
				new MobileDomainItem(){Code = "Cass", Description = "Cass"},
				new MobileDomainItem(){Code = "Clay", Description = "Clay"},
				new MobileDomainItem(){Code = "Dodge", Description = "Dodge"},
				new MobileDomainItem(){Code = "Washington", Description = "Washington"},
			};

			foreach (MobileDomainItem county in counties)
			{
				if (IsListUsingGroups)
				{
					AddItemToGroupedItemSource(county.Code, county.Description);
				}
				else
				{
					AddItemToItemSource(county.Code, county.Description);
				}
			}
		}

		/// <summary>
		/// Implementation of the Command triggered when user taps on an item in the list.
		/// </summary>
		/// <param name="selectedItemVm">The selected item from the list.</param>
		protected override void OnItemTappedCommand(ListPageListItemViewModel selectedItemVm)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Command used to go back in the navigation stack.
		/// </summary>
		protected override void OnBackButtonCommand()
		{

			throw new NotImplementedException();
		}

		#endregion ListPageViewModelBase Implementation

	}
}
