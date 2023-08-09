using GymTracker.Services;
using GymTracker.View;

namespace GymTracker.ViewModel;

public partial class RoutinesPageViewModel : BaseViewModel
{

	private LocalDatabase _db;

	public ObservableCollection<Routine> RoutinesList { get; } = new();

	public RoutinesPageViewModel(LocalDatabase db) 
	{
		_db = db;
		//ExerciseList.Add(new Exercise { DefaultReps = 10, DefaultSets = 20, DefaultWeight = 30, ID = 2, Name = "exercise 2" });
		loadRoutines();
	}

	public async Task loadRoutines()
	{
		var routines = await _db.GetRoutinesAsync();

		if (RoutinesList.Count != 0) RoutinesList.Clear();
		foreach (var routine in routines)
		{
			RoutinesList.Add(routine);
		}
	}

	[RelayCommand]
	private async Task Delete(Routine r)
	{
		await _db.DeleteRoutineAsync(r);
		await loadRoutines();
	}

	[RelayCommand]
	async Task GoToNewRoutine()
	{
		await Shell.Current.GoToAsync(nameof(NewRoutinePage));
	}

	[RelayCommand]
	async Task RoutineTapped()
	{

	}


}
