namespace GymTracker.ViewModel;

[QueryProperty(nameof(Exercise), "Exercise")]
public partial class ExerciseHistoryPageViewModel : BaseViewModel
{
	public ExerciseHistoryPageViewModel() { }

	[ObservableProperty]
	private Exercise exercise;

	[RelayCommand]
	private async Task GoBack()
	{
		await Shell.Current.Navigation.PopToRootAsync();
	}
}
