using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymTracker.Services;

public class IntervalTimerService
{
	public int setsCount;
	public int setsCountLeft;
	public int workTime;
	public int workTimeLeft;
	public int restTime;
	public int restTimeLeft;

	public bool WorkTimeStateChanged;
	public bool IsRestTime;

	public IntervalTimerService()
	{
		setsCount = Preferences.Default.Get("SetsCount", 3);
		setsCountLeft = setsCount;
		workTime = Preferences.Default.Get("WorkTime", 30);
		workTimeLeft = workTime;
		restTime = Preferences.Default.Get("RestTime", 15);
		restTimeLeft = restTime+1;
	}

	public void IncrementSetsCount()
	{
		if (setsCount >= 30) return;

		setsCount++;
		Preferences.Default.Set("SetsCount", setsCount);
		setsCountLeft++;
	}
	public void DecrementSetsCount()
	{
		if (setsCount <= 1)
		{
			return;
		}

		setsCount--;
		Preferences.Default.Set("SetsCount", setsCount);
		setsCountLeft--;
	}
	public void IncrementWorkTime()
	{
		workTime++;
		workTimeLeft++;
		Preferences.Default.Set("WorkTime", workTime);
	}
	public void DecrementWorkTime()
	{
		if(workTime <= 1)
		{
			return;
		}
		workTime--;
		workTimeLeft--;
		Preferences.Default.Set("WorkTime", workTime);
	}
	public void IncrementRestTime()
	{
		restTime++;
		restTimeLeft++;
		Preferences.Default.Set("RestTime", restTime);
	}
	public void DecrementRestTime()
	{
		if (restTime <= 1)
		{
			return;
		}
		restTime--;
		restTimeLeft--;
		Preferences.Default.Set("RestTime", restTime);
	}

	public bool PassSecond()
	{
		WorkTimeStateChanged = false;

		if (workTimeLeft > 1)
		{
			workTimeLeft--;
			return false;
		}
		
		if(workTimeLeft == 1 && restTimeLeft == restTime + 1)
		{
			WorkTimeStateChanged = true;
		} else
		{
			WorkTimeStateChanged = false;
		}
		
		if(setsCountLeft == 1)
		{
			resetTimer();
			return true;
		}

		if (restTimeLeft > 1)
		{
			IsRestTime = true;
			restTimeLeft--;
			return false;
		}

		WorkTimeStateChanged = true;
		
		if (setsCountLeft > 1)
		{
			IsRestTime = false;
			setsCountLeft--;
			workTimeLeft = workTime;
			restTimeLeft = restTime + 1;
			return false;
		}

		resetTimer();
		return true;
	}
	
	public void resetTimer()
	{
		IsRestTime = false;
		WorkTimeStateChanged = false;
		setsCountLeft = setsCount;
		workTimeLeft = workTime;
		restTimeLeft = restTime + 1;
	}
}

