namespace GymTracker.View;

public partial class ImportExportPage : ContentPage
{
	ImportExportPageViewModel _vm;
	public ImportExportPage(ImportExportPageViewModel vm)
	{
		InitializeComponent();
		_vm = vm;
		BindingContext = vm;
	}
}