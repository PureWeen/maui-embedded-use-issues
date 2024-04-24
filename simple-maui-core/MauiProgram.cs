using CommunityToolkit.Maui;
using Microsoft.Maui.Controls.Compatibility.Hosting;
using Microsoft.Maui.Embedding;

namespace Nau.Simple.Maui.Core
{
	public static class MauiProgram
	{
		public static MauiApp CreateMauiApp(Action<MauiAppBuilder> additional = null)
		{
			var builder = MauiApp.CreateBuilder();

			builder
				.UseMauiEmbedding<Application>()
				.ConfigureAppearingDisappearingEvents()
				.UseMauiCommunityToolkit()
				.UseMauiCompatibility();

			builder.Services.AddScoped(typeof(Window), (s) => new Window() { Parent = Application.Current });

			additional?.Invoke(builder);
			var mauiApp =  builder.Build();
			
			return mauiApp;
		}
	}
}
