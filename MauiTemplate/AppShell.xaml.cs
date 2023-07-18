using GymTracker.View;

namespace GymTracker;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		Routing.RegisterRoute("RoutinesPage", typeof(RoutinesPage));
	}
}