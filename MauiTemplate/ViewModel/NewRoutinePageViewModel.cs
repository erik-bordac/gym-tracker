using GymTracker.Services;

namespace GymTracker.ViewModel;

public partial class NewRoutinePageViewModel : BaseViewModel
{
	LocalDatabase _db;
	public ObservableCollection<Exercise> ExerciseList { get; } = new();
	public ObservableCollection<AddedExerciseWrapper> AddedExerciseList { get; } = new();

	private Dictionary<int, int> AddedExerciseCount = new();

	public string Name { get; set; }

	public NewRoutinePageViewModel(ExercisesPageViewModel exercises_vm, LocalDatabase db)
	{
		exercises_vm.loadExercises();
		ExerciseList = exercises_vm.ExerciseList;
		_db = db;
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
	[RelayCommand]
	private async Task SaveRoutine()
	{
		var routine = new Routine() { Name = Name };
		await _db.SaveRoutineAsync(routine);

		// Save exercises
		int id = await _db.GetLastIDAsync();

		var rexes = new List<RoutineExercise>();
		foreach (var item in AddedExerciseList)
		{
			rexes.Add(new RoutineExercise()
			{
				ExerciseID = item.Exercise.ID,
				RoutineID = id,
				Sets = item.Sets
			});
		}
		
		await _db.SaveRoutineExercisesAsync(rexes);

		await Shell.Current.GoToAsync("..");
	}
}
