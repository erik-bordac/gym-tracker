namespace GymTracker.Model;

public partial class OngoingExerciseWrapper: ObservableObject
{
	// This class is used for tracking exercises in OngoingRoutine

	public bool IsFinished { get; set; } = false;
	public int Index { get; set; }
	public Exercise Exercise { get; set; }

	public OngoingExerciseWrapper(int index, Exercise ex)
	{
		Index = index;
		Exercise = ex;
	}
}