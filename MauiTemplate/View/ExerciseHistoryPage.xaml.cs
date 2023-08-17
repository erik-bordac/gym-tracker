namespace GymTracker.View;

public partial class ExerciseHistoryPage : ContentPage
{
	ExerciseHistoryPageViewModel _vm;

	public ExerciseHistoryPage(ExerciseHistoryPageViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
		_vm = vm;
	}

	protected override void OnAppearing()
	{
		_vm.LoadHistory();
		base.OnAppearing();
	}
}