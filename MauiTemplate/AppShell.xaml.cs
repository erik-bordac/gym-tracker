using GymTracker.View;

namespace GymTracker;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		Routing.RegisterRoute("MainPage", typeof(MainPage));
		Routing.RegisterRoute("IntervalTimerPage", typeof(IntervalTimerPage));
		Routing.RegisterRoute("RoutinesPage", typeof(RoutinesPage));
		Routing.RegisterRoute("ExercisesPage", typeof(ExercisesPage));
		Routing.RegisterRoute("ExerciseForm", typeof(ExerciseForm));
		Routing.RegisterRoute("ExerciseDetailsPage", typeof(ExerciseDetailsPage));
		Routing.RegisterRoute("NewRoutinePage", typeof(NewRoutinePage));
		Routing.RegisterRoute("RoutineDetailsPage", typeof(RoutineDetailsPage));
		Routing.RegisterRoute("OngoingRoutinePage", typeof(OngoingRoutinePage));
		Routing.RegisterRoute("ExerciseHistoryPage", typeof(ExerciseHistoryPage));
	}
}