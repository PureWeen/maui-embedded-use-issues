namespace Nau.Simple.Maui.Core.Controls
{
	/// <summary>
	/// Code behind for the custom image control that renders the image from a font icon.
	/// </summary>
	/// <seealso cref="ContentView" />
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FontIconImage : Image
	{
		#region Static BindableProperty Definitions 

		/// <summary>The bindable property exposed by this control to set the icon displayed for the action.</summary>
		public static readonly BindableProperty ImageIconProperty = BindableProperty.Create(nameof(ImageIcon), typeof(FontImageViewModel), typeof(FontIconImage));

		#endregion Static BindableProperty Definitions 

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="FontIconImage"/> class.
		/// </summary>
		public FontIconImage()
		{
			InitializeComponent();
		}

		#endregion Constructors

		#region Bindable Property Implementations

		/// <summary>
		/// Gets or sets the icon to display as the image content.
		/// </summary>
		public FontImageViewModel ImageIcon
		{
			get => (FontImageViewModel)GetValue(ImageIconProperty);
			set => SetValue(ImageIconProperty, value);
		}

		#endregion Bindable Property Implementations
	}
}