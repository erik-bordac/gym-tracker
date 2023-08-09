namespace GymTracker.View;

public partial class RoutinesPage : ContentPage
{
	private RoutinesPageViewModel _vm;

	public RoutinesPage(RoutinesPageViewModel vm)
	{
		InitializeComponent();
		_vm = vm;
		BindingContext = vm;
	}

	private void RoutineTapped(object sender, TappedEventArgs e)
	{
		//_vm.x();
	}

	protected override async void OnAppearing()
	{
		await _vm.loadRoutines();
		base.OnAppearing();
	}
}