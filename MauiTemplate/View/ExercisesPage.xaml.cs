namespace GymTracker.View;

public partial class ExercisesPage : ContentPage 
{
	ExercisesPageViewModel _vm;
	public ExercisesPage(ExercisesPageViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
		_vm = vm;
	}

	protected override async void OnAppearing()
	{
		await _vm.loadExercises();
		base.OnAppearing();
	}
}