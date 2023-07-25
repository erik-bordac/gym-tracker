using GymTracker.Services;
using Plugin.Maui.Audio;

namespace GymTracker.ViewModel;
public partial class MainPageViewModel : BaseViewModel
{
	private const int Infinite = -1;
	private const int Second = 1000;

	private readonly Color defaultDark = Color.FromRgba("#7B8FA1");
	private readonly Color defaultLight = Color.FromRgba("#7B8FA1");
	private readonly Color runningDark = Color.FromRgba("#75BAAE");
	private readonly Color runningLight = Color.FromRgba("#9FD9BA");
	private readonly Color restDark = Color.FromRgba("#84BCDC");
	private readonly Color restLight = Color.FromRgba("#ADD5EB");

	private readonly Color pausedRunningDark = Color.FromRgba("#6A948C");
	private readonly Color pausedRunningLight= Color.FromRgba("#7BAD92");
	private readonly Color pausedRestDark = Color.FromRgba("#658DA4");
	private readonly Color pausedRestLight = Color.FromRgba("#81A4B8");

	private IntervalTimerService _intervalTimer;
	private System.Threading.Timer _timeToHoldTimer;
	private int _timeToStartHold = 600;
	private System.Threading.Timer _holdTimer;
	private int _holdTickTime = 50;
	private System.Threading.Timer _intervalRunningTimer;


	private enum BtnTypes 
	{
		Plus,
		Minus
	}
	private BtnTypes _heldBtnType;

	private bool _btnPressed;
	private string _heldBtnId;

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

	[ObservableProperty]
	private int restTimeLeft;

	[ObservableProperty]
	[NotifyPropertyChangedFor(nameof(IsNotRunningAndIsStarted))]
	private bool isRunning;
	public bool IsNotRunningAndIsStarted => (!IsRunning && IsStarted);

	[ObservableProperty]
	[NotifyPropertyChangedFor(nameof(IsNotRestTime))]
	private bool isRestTime;
	public bool IsNotRestTime => (!IsRestTime);

	[ObservableProperty]
	[NotifyPropertyChangedFor(nameof(IsNotStarted))]
	[NotifyPropertyChangedFor(nameof(IsNotRunningAndIsStarted))]
	private bool isStarted;
	public bool IsNotStarted=> !IsStarted;

	[ObservableProperty]
	private Color backgroundColor;

	[ObservableProperty]
	private Color ellipseColor;

	public MainPageViewModel(IntervalTimerService intervalTimer)
	{
		_intervalTimer = intervalTimer;
		backgroundColor = Color.FromRgba("#7B8FA1");
		ellipseColor= Color.FromRgba("#7B8FA1");
		UpdateProperties();
	}
	
	private async void PlayWhistleSound()
	{
		var audioPlayer = AudioManager.Current.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("whistle_sound.mp3"));

		audioPlayer.Play();
	}

	private void UpdateProperties() { 
		SetsCount = _intervalTimer.setsCount;
		SetsCountLeft = _intervalTimer.setsCountLeft;
		WorkTime = _intervalTimer.workTime;
		WorkTimeLeft = _intervalTimer.workTimeLeft;
		RestTime = _intervalTimer.restTime;
		RestTimeLeft = _intervalTimer.restTimeLeft;
		IsRestTime = _intervalTimer.IsRestTime;
	}

	private void StartHold(object state)
	{
		_holdTimer = new System.Threading.Timer(HoldTick, null, _holdTickTime, _holdTickTime);
	}

	private void HoldTick(object state)
	{
		BtnAction(_heldBtnId);
	}

	public void OnPlusBtnPressed(object sender, EventArgs e)
	{
		if (_btnPressed) return;
		_heldBtnType = BtnTypes.Plus;
		
		OnBtnPressed(sender, e);
	}
	public void OnMinusBtnPressed(object sender, EventArgs e)
	{
		if (_btnPressed) return;
		_heldBtnType = BtnTypes.Minus;

		OnBtnPressed(sender, e);
	}
	private void OnBtnPressed(object sender, EventArgs e)
	{
		_btnPressed = true;
		var btn = (Button)sender;
		_heldBtnId = btn.ClassId;

		_timeToHoldTimer = new System.Threading.Timer(StartHold, null, _timeToStartHold, Infinite);
		BtnAction(_heldBtnId);
	}
	public void OnBtnReleased(object sender, EventArgs e)
	{
		var btn = (Button)sender;
		// Released btn is different from button that is currently held
		if (btn.ClassId != _heldBtnId) return;
		
		_btnPressed = false;
		_heldBtnId = null;

		_timeToHoldTimer?.Dispose();
		_timeToHoldTimer = null;
		_holdTimer?.Dispose();
		_holdTimer = null;
	}

	private void BtnAction(string btnName)
	{
		switch(btnName)
		{
			case "sets":
				(_heldBtnType == BtnTypes.Plus ? (Action)_intervalTimer.IncrementSetsCount : _intervalTimer.DecrementSetsCount)();
				break;
			case "workTime":
				(_heldBtnType == BtnTypes.Plus ? (Action)_intervalTimer.IncrementWorkTime : _intervalTimer.DecrementWorkTime)();
				break;
			case "restTime":
				(_heldBtnType == BtnTypes.Plus ? (Action)_intervalTimer.IncrementRestTime : _intervalTimer.DecrementRestTime)();
				break;
		}

		UpdateProperties();
	}

	[RelayCommand]	
	private void StartInterval()
	{
		PlayWhistleSound();

		_intervalRunningTimer = new Timer(OnTick, null, Second, Second);
		BackgroundColor = this.runningDark;
		EllipseColor = this.runningLight;
		IsStarted = true;
		IsRunning = true;
	}
	private void OnTick(object state)
	{
		if (_intervalTimer.PassSecond())
		{
			StopInterval();
			PlayWhistleSound();
			return;
		}

		if(_intervalTimer.WorkTimeStateChanged)
		{
			PlayWhistleSound();
		}

		if (_intervalTimer.IsRestTime)
		{
			BackgroundColor = this.restDark;
			EllipseColor = this.restLight;
		} else
		{
			BackgroundColor = this.runningDark;
			EllipseColor = this.runningLight;
		}
		UpdateProperties();
	}

	[RelayCommand]	
	private void StopInterval()
	{
		BackgroundColor = this.defaultDark;
		EllipseColor = this.defaultLight;

		IsStarted = false;
		IsRunning = false;

		_intervalTimer.resetTimer();
		UpdateProperties();

		_intervalRunningTimer?.Dispose();
		_intervalRunningTimer = null;
	}

	[RelayCommand]	
	private void PauseInterval()
	{
		IsRunning = false;
		_intervalRunningTimer.Change(Infinite, Infinite);

		if (IsRestTime)
		{
			BackgroundColor = this.pausedRestDark;
			EllipseColor = this.pausedRestLight;
		} else
		{
			BackgroundColor = this.pausedRunningDark;
			EllipseColor = this.pausedRunningLight;
		}
	}

	[RelayCommand]	
	private void ResumeInterval()
	{
		IsRunning = true;
		_intervalRunningTimer.Change(Second, Second);

		if (_intervalTimer.IsRestTime)
		{
			BackgroundColor = this.restDark;
			EllipseColor = this.restLight;
		} else
		{
			BackgroundColor = this.runningDark;
			EllipseColor = this.runningLight;
		}

	}
}
