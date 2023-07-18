using Microsoft.Extensions.Logging;
using GymTracker.View;
using GymTracker.Services;

namespace GymTracker;

public static class MauiProgram
{
	private static void RegisterViewModels(MauiAppBuilder builder)
	{
		builder.Services.AddSingleton<MainPageViewModel>();
		return;
	}
	private static void RegisterViews(MauiAppBuilder builder)
	{
		builder.Services.AddSingleton<MainPage>();
		return;
	}
	private static void RegisterServices(MauiAppBuilder builder)
	{
		builder.Services.AddSingleton<IntervalTimerService>();
		return;
	}

	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
			});


#if DEBUG
		builder.Logging.AddDebug();
#endif

		RegisterViewModels(builder);
		RegisterViews(builder);
		RegisterServices(builder);

		return builder.Build();
	}
}
