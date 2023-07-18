namespace GymTracker.View;

public partial class MainPage : ContentPage
{
	private MainPageViewModel _viewModel;

	public MainPage(MainPageViewModel viewModel)
	{
		InitializeComponent();
		_viewModel = viewModel;
		BindingContext = viewModel;
	}

	private void OnBtnReleased(object sender, EventArgs e)
	{
		_viewModel.OnBtnReleased(sender, e);
    }

	private void OnPlusBtnPressed(object sender, EventArgs e)
	{
		_viewModel.OnPlusBtnPressed(sender, e);
    }

	private void OnMinusBtnPressed(object sender, EventArgs e)
	{
		_viewModel.OnMinusBtnPressed(sender, e);
	}
}

