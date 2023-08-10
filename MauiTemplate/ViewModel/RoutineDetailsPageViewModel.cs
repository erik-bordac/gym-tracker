using GymTracker.Services;
using GymTracker.View;

namespace GymTracker.ViewModel;

[QueryProperty(nameof(Routine), "Routine")]
public partial class RoutineDetailsPageViewModel : BaseViewModel
{
	LocalDatabase _db;

	[ObservableProperty]
	private Routine routine;
	[ObservableProperty]
	private ObservableCollection<AddedExerciseWrapper> addedExercises = new();


	public RoutineDetailsPageViewModel(LocalDatabase db) 
	{
		_db = db;
	}
	
	public async Task LoadExercises()
	{
		var routineExercises = new ObservableCollection<RoutineExercise>(await _db.GetExercisesInRoutine(Routine.ID));
		int[] exerciseIDs = new int[routineExercises.Count];
		int i = 0;
		foreach (var rex in routineExercises)
		{
			exerciseIDs[i++] = rex.ExerciseID;
		}

		var exercises = await _db.GetExercisesFromIDArrAsync(exerciseIDs);

		i = 0;
		foreach (var ex in exercises)
		{
			AddedExercises.Add(new AddedExerciseWrapper()
			{
				Sets = routineExercises[i].Sets,
				Exercise = ex
			});
		}

		return;
	}

	[RelayCommand]
	private async Task GoToOngoingRoutine()
	{
		await Shell.Current.GoToAsync("OngoingRoutinePage");
	}
}
