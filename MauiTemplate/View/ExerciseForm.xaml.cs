namespace GymTracker.View;

public partial class ExerciseForm : ContentPage
{
	private ExerciseFormViewModel _vm;

	public ExerciseForm(ExerciseFormViewModel vm)
	{
		_vm = vm;
		BindingContext = _vm;
		InitializeComponent();
	}

	private void NameChanged(object sender, TextChangedEventArgs e)
	{
		_vm.Name = e.NewTextValue;
	}

	private void NotesChanged(object sender, TextChangedEventArgs e)
	{
		_vm.Notes = e.NewTextValue;
	}

	private void TrackRepsChanged(object sender, CheckedChangedEventArgs e)
	{
		_vm.TrackReps = e.Value;
	}

	private void TrackWeightChanged(object sender, CheckedChangedEventArgs e)
	{
		_vm.TrackWeight = e.Value;
	}

	private void TrackTimeChanged(object sender, CheckedChangedEventArgs e)
	{
		_vm.TrackTime = e.Value;
	}
}