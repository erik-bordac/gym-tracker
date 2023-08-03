namespace GymTracker.View;

public partial class RoutinesPage : ContentPage
{
	public RoutinesPage(RoutinesPageViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}