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

	public IntervalTimerService()
	{
		setsCount = 7;
		setsCountLeft = 7;
		workTime = 57;
		workTimeLeft = 57;
		restTime = 15;
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
}

