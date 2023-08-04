using GymTracker.Services;

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

	private async void loadRoutines()
	{
		var routines = await _db.GetRoutinesAsync();

		if (RoutinesList.Count != 0) RoutinesList.Clear();
		foreach (var routine in routines)
		{
			RoutinesList.Add(routine);
		}
	}
}
