﻿using GymTracker.Services;
using GymTracker.View;

namespace GymTracker.ViewModel;

public partial class ExercisesPageViewModel : BaseViewModel
{
	private LocalDatabase _db;

	public ObservableCollection<Exercise> ExerciseList { get; } = new();

	public ExercisesPageViewModel(LocalDatabase db)
	{
		_db = db;
	}

	[RelayCommand]
	private async Task GoToExerciseForm()
	{
		await Shell.Current.GoToAsync(nameof(ExerciseForm));
	}

	[RelayCommand]
	private async Task GoToExerciseDetails(Exercise ex)
	{
		if (ex == null) return;

		await Shell.Current.GoToAsync(nameof(ExerciseDetailsPage), true, new Dictionary<string, object>
		{
			{ "Exercise", ex }
		});
	}

	public async Task loadExercises()
	{
		var exercises = await _db.GetExercisesAsync();

		if (ExerciseList.Count != 0) ExerciseList.Clear();
		foreach (var exercise in exercises)
		{
			ExerciseList.Add(exercise);
		}
	}

	[RelayCommand]
	private async Task Delete(Exercise ex)
	{
		bool res = await Application.Current.MainPage.DisplayAlert("Delete exercise", "Do you want delete this exercise? This is permanent.", "Yes", "No");
		if (!res) return;

		await _db.DeleteExerciseAsync(ex);
		await loadExercises();
	}
}