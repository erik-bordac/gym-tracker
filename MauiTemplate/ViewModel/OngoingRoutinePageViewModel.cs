using GymTracker.Services;

namespace GymTracker.ViewModel;

public partial class OngoingRoutinePageViewModel : BaseViewModel
{
	private OngoingRoutineService _ors;

	[ObservableProperty]
	private ObservableCollection<AddedExerciseWrapper> addedExercises;
	public OngoingRoutinePageViewModel(OngoingRoutineService ors)
	{
		_ors = ors;
		AddedExercises = ors.AddedExercises;
	}

	[RelayCommand]
	private async Task GoBack()
	{
		await Shell.Current.Navigation.PopToRootAsync();
	}
}
