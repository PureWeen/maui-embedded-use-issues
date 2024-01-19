using Android.App;
using Android.Runtime;
using Nau.Simple.Maui.BindingIssue;

namespace simple_maui_binding_issue
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
