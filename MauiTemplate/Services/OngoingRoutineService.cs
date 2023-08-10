namespace GymTracker.Services;

public class OngoingRoutineService
{
	public ObservableCollection<AddedExerciseWrapper> AddedExercises { get; set; }
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
}
