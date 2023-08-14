namespace GymTracker.Services;

public class OngoingRoutineService
{
	public ObservableCollection<AddedExerciseWrapper> AddedExercises { get; set; }
	public ObservableCollection<OngoingExerciseWrapper> Frames { get; set; } = new();
	private int currentExerciseIdx = 0;

	public OngoingRoutineService()
	{ 
		
	}

	public void NextExercise()
	{
		currentExerciseIdx++;
	}

	public void PreviousExercise()
	{
		currentExerciseIdx--; 
	}

	public void SetFrames(ObservableCollection<AddedExerciseWrapper> addedExercises)
	{
		Frames = new();

		int index = 0;
		foreach (var item in addedExercises)
		{
			for (int i =0; i<item.Sets; i++)
			{
				Frames.Add(new OngoingExerciseWrapper(index++, item.Exercise));
			}
		}
	}
}
