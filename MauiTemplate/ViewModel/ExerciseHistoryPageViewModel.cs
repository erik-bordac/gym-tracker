using GymTracker.Services;

namespace GymTracker.ViewModel;

[QueryProperty(nameof(Exercise), "Exercise")]
public partial class ExerciseHistoryPageViewModel : BaseViewModel
{
	private LocalDatabase _db;

	[ObservableProperty]
	private ObservableCollection<ExerciseHistory> exerciseHistory;

	public ExerciseHistoryPageViewModel(LocalDatabase db) 
	{ 
		_db = db;
	}

	public async void LoadHistory()
	{
		//var x = await _db.GetExerciseHistoryListAsync().Result.Where(history => history.ID == Exercise.ID);
		var x = await _db.GetExerciseHistoryListAsync();
		ExerciseHistory = new(x.Where(x=> x.ExerciseID == Exercise.ID));
	}

	[ObservableProperty]
	private Exercise exercise;

	[RelayCommand]
	private async Task GoBack()
	{
		await Shell.Current.Navigation.PopToRootAsync();
	}
}
