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
		setsCount = 3;
		setsCountLeft = setsCount;
		workTime = 5;
		workTimeLeft = workTime;
		restTime = 2;
		restTimeLeft = restTime+1;
	}

	public void IncrementSetsCount()
	{
		if (setsCount >= 30) return;

		setsCount++;
		setsCountLeft++;
	}
	public void DecrementSetsCount()
	{
		if (setsCount <= 1)
		{
			return;
		}

		setsCount--;
		setsCountLeft--;
	}
	public void IncrementWorkTime()
	{
		workTime++;
		workTimeLeft++;
	}
	public void DecrementWorkTime()
	{
		if(workTime <= 1)
		{
			return;
		}
		workTime--;
		workTimeLeft--;
	}
	public void IncrementRestTime()
	{
		restTime++;
		restTimeLeft++;
	}
	public void DecrementRestTime()
	{
		if (restTime <= 1)
		{
			return;
		}
		restTime--;
		restTimeLeft--;
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

