namespace Nau.Simple.Maui.Core.Controls;

/// <summary>
/// Provides a view model representing a font image used for display in buttons and other visual elements to be used when 
/// the color is required to be set in code vs. a Xamarin style in the view.
/// </summary>
public class FontImageViewModel : ViewModelBase
{
	#region Constructors

	/// <summary>
	/// Initializes a new instance of the <see cref="FontImageViewModel"/> class.
	/// </summary>
	public FontImageViewModel() : base()
	{
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="FontImageViewModel"/> class with a specific icon code and icon color.
	/// </summary>
	/// <param name="iconCode">The code of the icon to display as the image.</param>
	/// <param name="iconColor">The color to render the icon in the image.</param>
	public FontImageViewModel(string iconCode, Color iconColor)
	{
		Glyph = iconCode;
		Color = iconColor;
	}

	#endregion Constructors

	#region Properties

	/// <summary>
	/// Gets or sets the code of the glyph from the font set to be displayed.
	/// </summary>
	public string Glyph { get => Get<string>(); set => Set(value); }

	/// <summary>
	/// Gets or sets the color to apply to the font icon.
	/// </summary>
	public Color Color { get => Get<Color>(); set => Set(value); }

	#endregion Properties

}
