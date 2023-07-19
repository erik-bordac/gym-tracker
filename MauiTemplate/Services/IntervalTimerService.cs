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

	public IntervalTimerService()
	{
		setsCount = 3;
		setsCountLeft = setsCount;
		workTime = 5;
		workTimeLeft = workTime;
		restTime = 2;
		restTimeLeft = restTime;
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
	}
	public void DecrementRestTime()
	{
		if (restTime <= 1)
		{
			return;
		}
		restTime--;
	}

	public bool PassSecond()
	{
		if (workTimeLeft > 1)
		{
			workTimeLeft--;
			return false;
		}
		
		if (restTimeLeft > 1)
		{
			restTimeLeft--;
			return false;
		}
		
		if (setsCountLeft > 1)
		{
			setsCountLeft--;
			workTimeLeft = workTime;
			restTimeLeft = restTime;
			return false;
		}

		setsCountLeft = setsCount;
		workTimeLeft = workTime;
		restTimeLeft = restTime;
		return true;
	}
	
	public void resetTimer()
	{
		setsCountLeft = setsCount;
		workTimeLeft = workTime;
		restTimeLeft = restTime;
	}
}

