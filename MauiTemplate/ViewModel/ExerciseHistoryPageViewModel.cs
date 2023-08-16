namespace GymTracker.ViewModel;

[QueryProperty(nameof(Id), "Id")]
public partial class ExerciseHistoryPageViewModel : BaseViewModel
{
	public ExerciseHistoryPageViewModel() { }

	[ObservableProperty]
	private int id;

	[RelayCommand]
	private async Task GoBack()
	{
		await Shell.Current.Navigation.PopToRootAsync();
	}
}
