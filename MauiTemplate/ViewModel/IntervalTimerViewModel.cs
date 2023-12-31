﻿using GymTracker.Services;
using Plugin.Maui.Audio;

namespace GymTracker.ViewModel;

public partial class IntervalTimerViewModel : BaseViewModel
{
	private const int Infinite = -1;
	private const int Second = 1000;

	private readonly Color defaultDark        = Color.FromRgba("#191919");
	private readonly Color defaultLight       = Color.FromRgba("#191919");
	private readonly Color pausedRestDark     = Color.FromRgba("#1d2647");
	private readonly Color pausedRestLight    = Color.FromRgba("#1d2647");
	private readonly Color pausedRunningDark  = Color.FromRgba("#072828");
	private readonly Color pausedRunningLight = Color.FromRgba("#072828");
	private readonly Color restDark           = Color.FromRgba("#576CBC");
	private readonly Color restLight          = Color.FromRgba("#576CBC");
	private readonly Color runningDark        = Color.FromRgba("#116D6E");
	private readonly Color runningLight       = Color.FromRgba("#116D6E");
	private bool _btnPressed;
	private string _heldBtnId;
	private BtnTypes _heldBtnType;
	private int _holdTickTime = 50;
	private System.Threading.Timer _holdTimer;
	private System.Threading.Timer _intervalRunningTimer;
	private Stopwatch _elapsedTimeStopwatch = new();
	private IntervalTimerService _intervalTimer;
	private System.Threading.Timer _timeToHoldTimer;
	private int _timeToStartHold = 600;
	private int _remainingTimeToTick = Second;

	[ObservableProperty]
	private Color backgroundColor;

	[ObservableProperty]
	private Color ellipseColor;

	[ObservableProperty]
	[NotifyPropertyChangedFor(nameof(IsNotRestTime))]
	private bool isRestTime;

	[ObservableProperty]
	[NotifyPropertyChangedFor(nameof(IsNotRunningAndIsStarted))]
	private bool isRunning;

	[ObservableProperty]
	[NotifyPropertyChangedFor(nameof(IsNotStarted))]
	[NotifyPropertyChangedFor(nameof(IsNotRunningAndIsStarted))]
	private bool isStarted;

	[ObservableProperty]
	private string restTime;

	[ObservableProperty]
	private string restTimeLeft;

	[ObservableProperty]
	private int setsCount;

	[ObservableProperty]
	private int setsCountLeft;

	[ObservableProperty]
	private string workTime;

	[ObservableProperty]
	private string workTimeLeft;

	public IntervalTimerViewModel(IntervalTimerService intervalTimer)
	{
		_intervalTimer = intervalTimer;
		backgroundColor = this.defaultDark;
		ellipseColor = this.defaultLight;
		UpdateProperties();
	}

	private enum BtnTypes
	{
		Plus,
		Minus
	}

	public bool IsNotRestTime => (!IsRestTime);
	public bool IsNotRunningAndIsStarted => (!IsRunning && IsStarted);
	public bool IsNotStarted => !IsStarted;

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

	public void OnMinusBtnPressed(object sender, EventArgs e)
	{
		if (_btnPressed) return;
		_heldBtnType = BtnTypes.Minus;

		OnBtnPressed(sender, e);
	}

	public void OnPlusBtnPressed(object sender, EventArgs e)
	{
		if (_btnPressed) return;
		_heldBtnType = BtnTypes.Plus;

		OnBtnPressed(sender, e);
	}

	private void BtnAction(string btnName)
	{
		switch (btnName)
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

	private void HoldTick(object state)
	{
		BtnAction(_heldBtnId);
	}

	private void OnBtnPressed(object sender, EventArgs e)
	{
		_btnPressed = true;
		var btn = (Button)sender;
		_heldBtnId = btn.ClassId;

		_timeToHoldTimer = new System.Threading.Timer(StartHold, null, _timeToStartHold, Infinite);
		BtnAction(_heldBtnId);
	}

	private void OnTick(object state)
	{
		_elapsedTimeStopwatch.Restart();

		if (_intervalTimer.PassSecond())
		{
			_elapsedTimeStopwatch.Stop();
			_remainingTimeToTick = Second;
			StopInterval();
			PlayWhistleSound();
			return;
		}

		if (_intervalTimer.WorkTimeStateChanged)
		{
			PlayWhistleSound();
		}

		if (_intervalTimer.IsRestTime)
		{
			BackgroundColor = this.restDark;
			EllipseColor = this.restLight;
		}
		else
		{
			BackgroundColor = this.runningDark;
			EllipseColor = this.runningLight;
		}
		UpdateProperties();
	}

	[RelayCommand]
	private void PauseInterval()
	{
		IsRunning = false;
		var ms = (int)_elapsedTimeStopwatch.ElapsedMilliseconds;
		_remainingTimeToTick = Math.Abs(Second - ms);	// Absolute value for occasional ms>1000 (by a small difference)
		_intervalRunningTimer.Change(Infinite, Infinite);
		_elapsedTimeStopwatch.Stop();

		if (IsRestTime)
		{
			BackgroundColor = this.pausedRestDark;
			EllipseColor = this.pausedRestLight;
		}
		else
		{
			BackgroundColor = this.pausedRunningDark;
			EllipseColor = this.pausedRunningLight;
		}
	}

	private async void PlayWhistleSound()
	{
		// add dependency injection ?
		var audioPlayer = AudioManager.Current.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("Sounds/whistle_sound.mp3"));

		audioPlayer.Play();
	}

	[RelayCommand]
	private void ResumeInterval()
	{
		IsRunning = true;
		_intervalRunningTimer.Change(_remainingTimeToTick, Second);
		_elapsedTimeStopwatch.Start();

		if (_intervalTimer.IsRestTime)
		{
			BackgroundColor = this.restDark;
			EllipseColor = this.restLight;
		}
		else
		{
			BackgroundColor = this.runningDark;
			EllipseColor = this.runningLight;
		}
	}

	private void StartHold(object state)
	{
		_holdTimer = new System.Threading.Timer(HoldTick, null, _holdTickTime, _holdTickTime);
	}

	[RelayCommand]
	private void StartInterval()
	{
		PlayWhistleSound();

		_intervalRunningTimer = new Timer(OnTick, null, Second, Second);
		_elapsedTimeStopwatch.Start();
		BackgroundColor = this.runningDark;
		EllipseColor = this.runningLight;
		IsStarted = true;
		IsRunning = true;
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

	private void UpdateProperties()
	{
		SetsCount = _intervalTimer.setsCount;
		SetsCountLeft = _intervalTimer.setsCountLeft;
		WorkTime = GetTimeFromSeconds(_intervalTimer.workTime);
		WorkTimeLeft = GetTimeFromSeconds(_intervalTimer.workTimeLeft);
		RestTime = GetTimeFromSeconds(_intervalTimer.restTime);
		RestTimeLeft = GetTimeFromSeconds(_intervalTimer.restTimeLeft);
		IsRestTime = _intervalTimer.IsRestTime;
	}
	
	private string GetTimeFromSeconds(int seconds)
	{
		var result = TimeSpan.FromSeconds(seconds);

		if (result.Seconds < 10)
		{
			return result.Minutes.ToString() + ":0" + result.Seconds.ToString();
		}

		return result.Minutes.ToString() + ":" + result.Seconds.ToString();
	}
}