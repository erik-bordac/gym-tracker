namespace GymTracker.ViewModel;

[QueryProperty(nameof(Exercise), "Exercise")]
public partial class ExerciseDetailsPageViewModel : BaseViewModel
{
	public ExerciseDetailsPageViewModel()
	{
	}

	[ObservableProperty]
	private Exercise exercise;

	[RelayCommand]
	private async Task GoToHistory(int ID)
	{
		await Shell.Current.GoToAsync("ExerciseHistoryPage", new Dictionary<string, object>
		{
			{"Id", ID }
		});
	}
}