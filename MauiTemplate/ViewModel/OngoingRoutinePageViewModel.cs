using GymTracker.Services;

namespace GymTracker.ViewModel;

public partial class OngoingRoutinePageViewModel : BaseViewModel
{
	private OngoingRoutineService _ors;
	private LocalDatabase _db;

	[ObservableProperty]
	private ObservableCollection<OngoingExerciseWrapper> frames;
	public OngoingRoutinePageViewModel(OngoingRoutineService ors, LocalDatabase db)
	{
		_ors = ors;
		_db = db;
		Frames = ors.Frames;
	}

	[RelayCommand]
	private async Task GoBack()
	{
		await Shell.Current.Navigation.PopToRootAsync();
	}

	[RelayCommand]
	private void FinishExercise(OngoingExerciseWrapper ong_ex)
	{
		//ong_ex.IsFinished = true;
		ong_ex.ChangeFinishedState();
		_ors.Frames[ong_ex.Index].IsFinished = true;
	}

	[RelayCommand]
	private void ResumeExercise(OngoingExerciseWrapper ong_ex)
	{
		//ong_ex.IsFinished = false;
		ong_ex.ChangeFinishedState();
		_ors.Frames[ong_ex.Index].IsFinished = false;
	}

	[RelayCommand]
	private async Task ExitRoutine()
	{
		bool res = await Application.Current.MainPage.DisplayAlert("Exit routine", "Do you want to exit routine? All progress will be lost", "Yes", "No");
		if (!res) return;

		_ors.Clear();
		await Shell.Current.Navigation.PopToRootAsync();
	}

	[RelayCommand]
	private void RepsMinus(OngoingExerciseWrapper ongoing_ex)
	{
		if (ongoing_ex.IsFinished) return;
		if (ongoing_ex.RepsDone <= 0) return;
		ongoing_ex.RepsDone--;
	}
	[RelayCommand]
	private void RepsPlus(OngoingExerciseWrapper ongoing_ex)
	{
		if (ongoing_ex.IsFinished) return;
		ongoing_ex.RepsDone++;
	}
	[RelayCommand]
	private void TimeMinus(OngoingExerciseWrapper ongoing_ex)
	{
		if (ongoing_ex.IsFinished) return;
		if (ongoing_ex.TimeDone <= 0) return;
		ongoing_ex.TimeDone--;
	}
	[RelayCommand]
	private void TimePlus(OngoingExerciseWrapper ongoing_ex)
	{
		if (ongoing_ex.IsFinished) return;
		ongoing_ex.TimeDone++;
	}
	[RelayCommand]
	private void WeightMinus(OngoingExerciseWrapper ongoing_ex)
	{
		if (ongoing_ex.IsFinished) return;
		if (ongoing_ex.WeightDone<= 0) return;
		ongoing_ex.WeightDone--;
	}
	[RelayCommand]
	private void WeightPlus(OngoingExerciseWrapper ongoing_ex)
	{
		if (ongoing_ex.IsFinished) return;
		ongoing_ex.WeightDone++;
	}

	[RelayCommand]
	private async Task Save()
	{
		bool res = await Application.Current.MainPage.DisplayAlert("Save Workout", "Do you want to finish your workout?", "Yes", "No");
		if (!res) return;

		List<ExerciseHistory> history = new List<ExerciseHistory>();

		foreach (var item in _ors.Frames)
		{
			if (item.IsNotFinished) continue;
			var x = new ExerciseHistory()
			{
				ExerciseID = item.Exercise.ID,
				DateTime = DateTime.Now,
				RepsCount = item.RepsDone,
				Weight = item.WeightDone,
				TimeInSeconds = item.TimeDone
			};
			history.Add(x);
		}
		
		await _db.SaveExerciseHistory(history);
		_ors.Clear();
		await Shell.Current.Navigation.PopToRootAsync();
	}
}
