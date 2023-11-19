namespace GymTracker.Model;

public partial class OngoingExerciseWrapper: ObservableObject
{
	// This class is used for tracking exercises in OngoingRoutine
	[ObservableProperty]
	private int repsDone = 0;
	[ObservableProperty]
	private int weightDone = 0;
	[ObservableProperty]
	private int timeDone = 0;

	[ObservableProperty]
	private Color frameColor = Color.FromRgba("#424242");

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

	public void ChangeFinishedState(bool finished)
	{
		IsFinished = finished;
		FrameColor = finished ? Color.FromRgba("#61b53d") : Color.FromRgba("#424242");
	}
}