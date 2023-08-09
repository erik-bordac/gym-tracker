namespace GymTracker.ViewModel;

[QueryProperty(nameof(Exercise), "Exercise")]
public partial class ExerciseDetailsPageViewModel : BaseViewModel
{
	public ExerciseDetailsPageViewModel()
	{
	}

	[ObservableProperty]
	private Exercise exercise;
}