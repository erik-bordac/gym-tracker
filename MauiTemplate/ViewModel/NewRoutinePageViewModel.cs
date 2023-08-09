namespace GymTracker.ViewModel;

public partial class NewRoutinePageViewModel : BaseViewModel
{
	public ObservableCollection<Exercise> ExerciseList { get; } = new();
	public ObservableCollection<AddedExerciseWrapper> AddedExerciseList { get; } = new();

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
			var ex_ = new Exercise()
			{
				ID = exercise.ID,
				Name = $"{exercise.Name} #{AddedExerciseCount[exercise.ID]}",
				TrackReps = exercise.TrackReps,
				TrackTime = exercise.TrackTime,
				TrackWeight = exercise.TrackWeight,
				Notes = exercise.Notes
			};
			var ex_2 = new AddedExerciseWrapper()
			{
				Sets = 1,
				Exercise = ex_
			};
			AddedExerciseList.Add(ex_2);
			return;
		}

		var ex = new AddedExerciseWrapper()
		{
			Sets = 1,
			Exercise = exercise
		};
		AddedExerciseList.Add(ex);
		AddedExerciseCount[ex.Exercise.ID] = 1;
	}
	[RelayCommand]
	private void DeleteExercise(AddedExerciseWrapper exercise)
	{
		AddedExerciseList.Remove(exercise);
		AddedExerciseCount[exercise.Exercise.ID]--;
	}
	[RelayCommand]
	private void IncreaseSets(AddedExerciseWrapper ex)
	{
		ex.Sets++;
	}
	[RelayCommand]
	private void DecreaseSets(AddedExerciseWrapper ex)
	{
		if (ex.Sets == 1) return;
		ex.Sets--;
	}
}
