using GymTracker.Services;
using GymTracker.View;

namespace GymTracker.ViewModel;

public partial class ImportExportPageViewModel : BaseViewModel
{
	public int Tmp { get; set; }

	public ImportExportPageViewModel()
	{
		Tmp = 50;
	}
}