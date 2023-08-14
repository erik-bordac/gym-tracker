using GymTracker.Services;
using GymTracker.View;

namespace GymTracker.ViewModel;

[QueryProperty(nameof(Routine), "Routine")]
public partial class RoutineDetailsPageViewModel : BaseViewModel
{
	LocalDatabase _db;
	OngoingRoutineService _ors;

	[ObservableProperty]
	private Routine routine;
	[ObservableProperty]
	private ObservableCollection<AddedExerciseWrapper> addedExercises = new();


	public RoutineDetailsPageViewModel(LocalDatabase db, OngoingRoutineService ors) 
	{
		_db = db;
		_ors = ors;
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
		if (_ors.OngoingRoutine)
		{
			bool res = await Application.Current.MainPage.DisplayAlert("Active routine", "There is an active routine. Want to start new?", "Yes", "No");
			if (!res) return;
		}

		_ors.Clear();
		_ors.SetFrames(AddedExercises);
		_ors.OngoingRoutine = true;
		await Shell.Current.GoToAsync("OngoingRoutinePage");
	}
}
