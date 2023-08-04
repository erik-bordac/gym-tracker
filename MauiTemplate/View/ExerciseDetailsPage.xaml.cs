namespace GymTracker.View;

public partial class ExerciseDetailsPage : ContentPage
{
	ExerciseDetailsPageViewModel _vm;
	public ExerciseDetailsPage(ExerciseDetailsPageViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
		_vm = vm;
	}
}