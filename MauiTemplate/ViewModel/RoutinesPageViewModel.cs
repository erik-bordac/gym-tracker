using GymTracker.Services;

namespace GymTracker.ViewModel;

public partial class RoutinesPageViewModel : BaseViewModel
{
	public ObservableCollection<Exercise> ExerciseList { get; } = new();

	private ExerciseDatabase db;
	public RoutinesPageViewModel(ExerciseDatabase _db) 
	{
		db = _db;
		//ExerciseList.Add(new Exercise { DefaultReps = 10, DefaultSets = 20, DefaultWeight = 30, ID = 1, Name = "exercise 1" });
		//ExerciseList.Add(new Exercise { DefaultReps = 10, DefaultSets = 20, DefaultWeight = 30, ID = 2, Name = "exercise 2" });
		loadExercises();
	}

	private async void loadExercises()
	{
		//await db.SaveItemAsync(new Exercise { DefaultReps = 10, DefaultSets = 20, DefaultWeight = 30, Name="exercise 2" });
		var exercises = await db.GetItemsAsync();

		if (ExerciseList.Count != 0) ExerciseList.Clear();
		foreach (var exercise in exercises)
		{
			ExerciseList.Add(exercise);
		}
	}
}
