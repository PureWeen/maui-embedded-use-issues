using Android.App;
using Android.Runtime;
using Nau.Simple.Maui.Core;

namespace Nau.Simple.Maui.Embedded
{
	[Application]
	public class MainApplication : MauiApplication
	{
		public MainApplication(IntPtr handle, JniHandleOwnership ownership)
			: base(handle, ownership)
		{
		}

		protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
	}
}
