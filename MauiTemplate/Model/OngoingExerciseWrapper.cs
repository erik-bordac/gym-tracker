namespace GymTracker.Model;

public partial class OngoingExerciseWrapper: ObservableObject
{
	// This class is used for tracking exercises in OngoingRoutine

	[ObservableProperty]
	[NotifyPropertyChangedFor(nameof(IsNotFinished))]
	private bool isFinished;
	public bool IsNotFinished => !IsFinished;

	public int Index { get; set; }
	public Exercise Exercise { get; set; }

	public OngoingExerciseWrapper(int index, Exercise ex)
	{
		Index = index;
		Exercise = ex;
	}
}