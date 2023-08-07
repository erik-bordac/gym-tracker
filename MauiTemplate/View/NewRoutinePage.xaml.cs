namespace GymTracker.View;

public partial class NewRoutinePage : ContentPage
{
	public NewRoutinePage(NewRoutinePageViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}