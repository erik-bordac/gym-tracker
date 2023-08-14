namespace GymTracker.View;

public partial class IntervalTimerPage : ContentPage
{
	private IntervalTimerViewModel _viewModel;

	public IntervalTimerPage(IntervalTimerViewModel viewModel)
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