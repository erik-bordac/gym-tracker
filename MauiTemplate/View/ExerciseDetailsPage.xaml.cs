namespace GymTracker.View;

public partial class ExerciseDetailsPage : ContentPage
{
	private ExerciseDetailsPageViewModel _vm;

	public ExerciseDetailsPage(ExerciseDetailsPageViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
		_vm = vm;
	}
}