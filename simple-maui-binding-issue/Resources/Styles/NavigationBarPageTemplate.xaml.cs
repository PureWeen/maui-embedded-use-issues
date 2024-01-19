namespace Nau.Simple.Maui.BindingIssue.Resources.Templates
{
	/// <summary>
	/// Code behind class for the resource dictionary providing the control template to include a custom navigation bar control
	/// above the content for each page.
	/// </summary>
	/// <seealso cref="ResourceDictionary" />
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NavigationBarPageTemplate : ResourceDictionary
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="NavigationBarPageTemplate"/> class.
		/// </summary>
		public NavigationBarPageTemplate()
		{
			InitializeComponent();
		}

		#endregion Constructors
	}
}