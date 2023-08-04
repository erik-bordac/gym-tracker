namespace GymTracker.View;

public partial class RoutinesPage : ContentPage
{
	RoutinesPageViewModel _vm;
	public RoutinesPage(RoutinesPageViewModel vm)
	{
		InitializeComponent();
		_vm = vm;
		BindingContext = vm;
	}

	private void RoutineTapped (object sender, TappedEventArgs e)
	{
		//_vm.x();
    }
}