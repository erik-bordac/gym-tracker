namespace GymTracker.View;

public partial class MainPage : ContentPage
{
	MainPageViewModel _vm;
	public MainPage(MainPageViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
		_vm = vm;
	}

	protected override void OnAppearing()
	{
		_vm.OnAppearing();
		base.OnAppearing();
	}
}