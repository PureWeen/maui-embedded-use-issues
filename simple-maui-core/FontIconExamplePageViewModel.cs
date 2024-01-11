using Nau.Simple.Maui.Core.Controls;

namespace Nau.Simple.Maui.Core
{
	public class FontIconExamplePageViewModel : ContentPageViewModelBase
	{
		public FontIconExamplePageViewModel()
		{
			string sampleIcon = "\U000f0004";
			ImageIcon = new FontImageViewModel(sampleIcon, Colors.Red);
		}

		public FontImageViewModel ImageIcon { get => Get<FontImageViewModel>(); set => Set(value); }
	}
}
