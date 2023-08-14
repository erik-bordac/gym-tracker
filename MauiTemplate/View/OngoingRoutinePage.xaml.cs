namespace GymTracker.View;

public partial class OngoingRoutinePage : ContentPage
{
	public OngoingRoutinePage(OngoingRoutinePageViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}