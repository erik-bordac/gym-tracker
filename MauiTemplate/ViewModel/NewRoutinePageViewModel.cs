namespace GymTracker.ViewModel;

public partial class NewRoutinePageViewModel : BaseViewModel
{
	public ObservableCollection<Exercise> ExerciseList { get; } = new();
	public ObservableCollection<Exercise> AddedExerciseList { get; } = new();

	private Dictionary<int, int> AddedExerciseCount = new();

	public NewRoutinePageViewModel(ExercisesPageViewModel exercises_vm)
	{
		exercises_vm.loadExercises();
		ExerciseList = exercises_vm.ExerciseList;
	}

	[RelayCommand]
	private void AddExercise(Exercise exercise)
	{
		if (AddedExerciseCount.ContainsKey(exercise.ID) && AddedExerciseCount[exercise.ID] > 0)
		{
			AddedExerciseCount[exercise.ID]++;
			var ex = new Exercise()
			{
				ID = exercise.ID,
				Name = $"{exercise.Name} #{AddedExerciseCount[exercise.ID]}",
				TrackReps = exercise.TrackReps,
				TrackTime = exercise.TrackTime,
				TrackWeight = exercise.TrackWeight,
				Notes = exercise.Notes
			};
			AddedExerciseList.Add(ex);
			return;
		}

		AddedExerciseList.Add(exercise);
		AddedExerciseCount[exercise.ID] = 1;
	}
	[RelayCommand]
	private void DeleteExercise(Exercise exercise)
	{
		AddedExerciseList.Remove(exercise);
		AddedExerciseCount[exercise.ID]--;
	}
}
