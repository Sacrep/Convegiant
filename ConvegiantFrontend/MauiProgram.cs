using ConvegiantFrontend.Data;
using Microsoft.Extensions.Logging;

namespace ConvegiantFrontend;

public static class MauiProgram
{
	private const string ApiBaseAddress = "https://convegiant-api.azurewebsites.net";

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

		builder.Services.AddSingleton(new HttpClient());
		builder.Services.AddSingleton(sp => new RecipeService(ApiBaseAddress));


		return builder.Build();
	}
}
