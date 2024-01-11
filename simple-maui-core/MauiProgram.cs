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
				.UseMauiCommunityToolkit()
				.UseMauiCompatibility();

			additional?.Invoke(builder);
			return builder.Build();
		}
	}
}
