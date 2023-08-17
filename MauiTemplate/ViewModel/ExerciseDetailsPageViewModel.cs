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
	private async Task GoToHistory(Exercise Exercise)
	{
		await Shell.Current.GoToAsync("ExerciseHistoryPage", new Dictionary<string, object>
		{
			{"Exercise", Exercise }
		});
	}
}