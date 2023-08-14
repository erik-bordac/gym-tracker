namespace GymTracker.Services;

public class OngoingRoutineService
{
	public ObservableCollection<OngoingExerciseWrapper> Frames { get; set; } = new();
	public int currentExerciseIdx = 0;
	public bool OngoingRoutine { get; set; } = false; 

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

	public void Clear()
	{
		Frames.Clear();
		OngoingRoutine = false;
		currentExerciseIdx = 0;
	}
}
