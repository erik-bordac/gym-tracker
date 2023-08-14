using GymTracker.Services;

namespace GymTracker.ViewModel;

public partial class OngoingRoutinePageViewModel : BaseViewModel
{
	private OngoingRoutineService _ors;

	[ObservableProperty]
	private ObservableCollection<OngoingExerciseWrapper> frames;
	public OngoingRoutinePageViewModel(OngoingRoutineService ors)
	{
		_ors = ors;
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
		ong_ex.IsFinished = true;
		_ors.Frames[ong_ex.Index].IsFinished = true;
	}

	[RelayCommand]
	private void ResumeExercise(OngoingExerciseWrapper ong_ex)
	{
		ong_ex.IsFinished = false;
		_ors.Frames[ong_ex.Index].IsFinished = false;
	}

	[RelayCommand]
	private async Task ExitRoutine()
	{
		_ors.Clear();
		await Shell.Current.Navigation.PopToRootAsync();
	}
}
