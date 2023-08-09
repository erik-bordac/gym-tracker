namespace GymTracker.View;

public partial class NewRoutinePage : ContentPage
{
	private NewRoutinePageViewModel _vm;

	public NewRoutinePage(NewRoutinePageViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
		_vm = vm;
	}

	private void Entry_TextChanged(object sender, TextChangedEventArgs e)
	{
		_vm.Name = e.NewTextValue;
	}
}