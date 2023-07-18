﻿using GymTracker.Services;

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
}
