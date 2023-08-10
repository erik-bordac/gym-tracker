namespace GymTracker.View;

public partial class RoutineDetailsPage : ContentPage
{
	RoutineDetailsPageViewModel _vm;
	public RoutineDetailsPage(RoutineDetailsPageViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
		_vm = vm;
	}

	protected override async void OnAppearing()
	{
		await _vm.LoadExercises();
		base.OnAppearing();
	}

}