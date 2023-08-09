using GymTracker.Services;

namespace GymTracker.ViewModel;

public partial class ExerciseFormViewModel : BaseViewModel
{
	private LocalDatabase _db;

	public string Name { get; set; }
	public string Notes { get; set; }
	public bool TrackReps { get; set; }
	public bool TrackWeight { get; set; }
	public bool TrackTime { get; set; }

	public ExerciseFormViewModel(LocalDatabase db)
	{
		_db = db;
	}

	[RelayCommand]
	public async Task AddExerciseToDb()
	{
		if (Name is null) return;

		var exercise = new Exercise()
		{
			Name = Name,
			Notes = Notes,
			TrackReps = TrackReps,
			TrackWeight = TrackWeight,
			TrackTime = TrackTime
		};

		await _db.SaveExerciseAsync(exercise);
		await Shell.Current.GoToAsync("..");
	}
}