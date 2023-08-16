namespace GymTracker.View;

public partial class ExerciseHistoryPage : ContentPage
{
	public ExerciseHistoryPage(ExerciseHistoryPageViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}