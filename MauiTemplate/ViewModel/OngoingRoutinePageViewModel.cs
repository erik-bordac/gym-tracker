namespace GymTracker.ViewModel;

public partial class OngoingRoutinePageViewModel : BaseViewModel
{
	public OngoingRoutinePageViewModel()
	{
		
	}

	[RelayCommand]
	private async Task GoBack()
	{
		await Shell.Current.Navigation.PopToRootAsync();
		await Shell.Current.GoToAsync("///MainPage");
	}
}
