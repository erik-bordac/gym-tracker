﻿using Microsoft.Extensions.Logging;
using GymTracker.View;
using GymTracker.Services;

namespace GymTracker;

public static class MauiProgram
{
	private static void RegisterViewModels(MauiAppBuilder builder)
	{
		builder.Services.AddSingleton<MainPageViewModel>();
		builder.Services.AddTransient<RoutinesPageViewModel>();
		builder.Services.AddSingleton<ExercisesPageViewModel>();
		builder.Services.AddTransient<ExerciseFormViewModel>();
		builder.Services.AddTransient<ExerciseDetailsPageViewModel>();
		builder.Services.AddTransient<NewRoutinePageViewModel>();
	}
	private static void RegisterViews(MauiAppBuilder builder)
	{
		builder.Services.AddSingleton<MainPage>();
		builder.Services.AddSingleton<RoutinesPage>();
		builder.Services.AddSingleton<ExercisesPage>();
		builder.Services.AddTransient<ExerciseForm>();
		builder.Services.AddTransient<ExerciseDetailsPage>();
		builder.Services.AddTransient<NewRoutinePage>();
	}
	private static void RegisterServices(MauiAppBuilder builder)
	{
		builder.Services.AddSingleton<IntervalTimerService>();
		builder.Services.AddSingleton<LocalDatabase>();
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
