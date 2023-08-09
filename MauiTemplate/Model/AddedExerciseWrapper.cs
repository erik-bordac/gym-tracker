namespace GymTracker.Model;
public partial class AddedExerciseWrapper : ObservableObject
{
	// This class is used for setting Sets in NewRoutinePage

	[ObservableProperty]
	private int sets;
	public Exercise Exercise { get; set; }
}
