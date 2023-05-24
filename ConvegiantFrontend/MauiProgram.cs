using Convegiant.Infrastructure;
using Convegiant.Infrastructure.InMemory;
using ConvegiantFrontend.Data;
using Microsoft.Extensions.Logging;

namespace ConvegiantFrontend;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
			});

		builder.Services.AddMauiBlazorWebView();

#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif

		builder.Services.AddSingleton<WeatherForecastService>();
		builder.Services.AddSingleton<IRecipeRepository, InMemoryRecipeRepository>();

		return builder.Build();
	}
}
