﻿using CommunityToolkit.Maui;
using GymTracker.Services;
using GymTracker.View;
using Microsoft.Extensions.Logging;
using SkiaSharp.Views.Maui.Controls.Hosting;

namespace GymTracker;

public static class MauiProgram
{
	private static void RegisterViewModels(MauiAppBuilder builder)
	{
		builder.Services.AddSingleton<IntervalTimerViewModel>();
		builder.Services.AddSingleton<ExercisesPageViewModel>();
		builder.Services.AddSingleton<RoutinesPageViewModel>();			
		builder.Services.AddTransient<ExerciseFormViewModel>();
		builder.Services.AddTransient<MainPageViewModel>();
		builder.Services.AddTransient<OngoingRoutinePageViewModel>();	
		builder.Services.AddTransient<ExerciseDetailsPageViewModel>();
		builder.Services.AddTransient<NewRoutinePageViewModel>();
		builder.Services.AddTransient<RoutineDetailsPageViewModel>();
		builder.Services.AddTransient<ExerciseHistoryPageViewModel>();
		builder.Services.AddTransient<ImportExportPageViewModel>();
	}

	private static void RegisterViews(MauiAppBuilder builder)
	{
		builder.Services.AddTransient<MainPage>();
		builder.Services.AddTransient<ExerciseHistoryPage>();
		builder.Services.AddSingleton<IntervalTimerPage>();
		builder.Services.AddSingleton<RoutinesPage>();
		builder.Services.AddSingleton<ExercisesPage>();
		builder.Services.AddTransient<ExerciseForm>();
		builder.Services.AddTransient<OngoingRoutinePage>();
		builder.Services.AddTransient<ExerciseDetailsPage>();
		builder.Services.AddTransient<NewRoutinePage>();
		builder.Services.AddTransient<RoutineDetailsPage>();
		builder.Services.AddTransient<ImportExportPage>();
	}

	private static void RegisterServices(MauiAppBuilder builder)
	{
		builder.Services.AddSingleton<IntervalTimerService>();
		builder.Services.AddSingleton<LocalDatabase>();
		builder.Services.AddSingleton<OngoingRoutineService>();
	}

	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiCommunityToolkit()
			.UseSkiaSharp(true)
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