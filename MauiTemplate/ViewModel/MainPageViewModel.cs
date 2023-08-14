
using GymTracker.Services;

namespace GymTracker.ViewModel;
public partial class MainPageViewModel : BaseViewModel
{
	[ObservableProperty]
	private bool ongoingRoutine;

	private OngoingRoutineService _ors;

	public MainPageViewModel(OngoingRoutineService ors) 
	{ 
		_ors = ors;
	}

	public void OnAppearing()
	{
		OngoingRoutine = _ors.OngoingRoutine;
	}

	[RelayCommand]
	private async Task GoToOngoingRoutine()
	{
		await Shell.Current.GoToAsync("OngoingRoutinePage");
	}
}
