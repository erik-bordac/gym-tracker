using GymTracker.Services;

namespace GymTracker.ViewModel;
public partial class MainPageViewModel : BaseViewModel
{
	private const int Infinite = -1;

	private IntervalTimerService _intervalTimer;
	private System.Threading.Timer _timeToHoldTimer;
	private int _timeToStartHold = 1000;
	private System.Threading.Timer _holdTimer;
	private int _holdTickTime = 50;
	private enum BtnTypes 
	{
		Plus,
		Minus
	}
	private BtnTypes _heldBtnType;

	private bool _btnPressed;
	private string _heldBTnId;

	[ObservableProperty]
	private int setsCount;
	[ObservableProperty]
	private int setsCountLeft;

	[ObservableProperty]
	private int workTime;
	[ObservableProperty]
	private int workTimeLeft;

	[ObservableProperty]
	private int restTime;

	public MainPageViewModel(IntervalTimerService intervalTimer)
	{
		_intervalTimer = intervalTimer;
		UpdateProperties();
	}

	private void UpdateProperties() { 
		SetsCount = _intervalTimer.setsCount;
		SetsCountLeft = _intervalTimer.setsCountLeft;
		WorkTime = _intervalTimer.workTime;
		WorkTimeLeft = _intervalTimer.workTimeLeft;
		RestTime = _intervalTimer.restTime;
	}

	private void StartHold(object state)
	{
		_holdTimer = new System.Threading.Timer(HoldTick, null, _holdTickTime, _holdTickTime);
	}

	private void HoldTick(object state)
	{
		if (_heldBtnType == BtnTypes.Plus) PlusBtnAction(_heldBTnId);
		else MinusBtnAction(_heldBTnId);
	}

	public void OnPlusBtnPressed(object sender, EventArgs e)
	{
		if (_btnPressed) return;

		_btnPressed = true;
		var btn = (Button)sender;
		_heldBTnId = btn.ClassId;
		_heldBtnType = BtnTypes.Plus;

		_timeToHoldTimer = new System.Threading.Timer(StartHold, null, _timeToStartHold, Infinite);
		PlusBtnAction(btn.ClassId);
	}

	public void OnMinusBtnPressed(object sender, EventArgs e)
	{
		if (_btnPressed) return;

		_btnPressed = true;
		var btn = (Button)sender;
		_heldBTnId = btn.ClassId;
		_heldBtnType = BtnTypes.Minus;

		_timeToHoldTimer = new System.Threading.Timer(StartHold, null, _timeToStartHold, Infinite);
		MinusBtnAction(btn.ClassId);
	}

	public void OnBtnReleased(object sender, EventArgs e)
	{
		var btn = (Button)sender;
		// Released btn is different from button that is currently held
		if (btn.ClassId != _heldBTnId) return;
		
		_btnPressed = false;
		_heldBTnId = null;

		_timeToHoldTimer?.Dispose();
		_timeToHoldTimer = null;
		_holdTimer?.Dispose();
		_holdTimer = null;
	}

	public void PlusBtnAction(string btnName)
	{
		if (btnName == "sets")
		{
			_intervalTimer.IncrementSetsCount();
		}
		if (btnName == "workTime")
		{
			_intervalTimer.IncrementWorkTime();
		}
		if (btnName == "restTime")
		{
			_intervalTimer.IncrementRestTime();
		}

		UpdateProperties();
	}

	public void MinusBtnAction(string btnName)
	{
		if (btnName == "sets")
		{
			_intervalTimer.DecrementSetsCount();
		}
		if (btnName == "workTime")
		{
			_intervalTimer.DecrementWorkTime();
		}
		if (btnName == "restTime")
		{
			_intervalTimer.DecrementRestTime();
		}

		UpdateProperties();
	}
}
