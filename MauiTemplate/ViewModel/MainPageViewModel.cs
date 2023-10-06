
using GymTracker.Services;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;

namespace GymTracker.ViewModel;
public partial class MainPageViewModel : BaseViewModel
{
	[ObservableProperty]
	private IEnumerable<ISeries> ratioSeries;

	[ObservableProperty]
	private bool ongoingRoutine;

	[ObservableProperty]
	private DateOnly currentDate = DateOnly.FromDateTime(DateTime.Now);

	private OngoingRoutineService _ors;
	private LocalDatabase _db;

	public MainPageViewModel(OngoingRoutineService ors, LocalDatabase db) 
	{ 
		_ors = ors;
		_db = db;
	}

	public void OnAppearing()
	{
		OngoingRoutine = _ors.OngoingRoutine;

		// Load Work/Rest series
		Task.Run(async () =>
		{
			var historyList = await _db.GetExerciseHistoryListAsync();
			var _30daysAgo = DateTime.Now.Date.AddDays(-30);

			bool[] workDays = new bool[31];

			foreach(var item in historyList)
			{
				var diff = item.DateTime.Date - DateTime.Now.Date.AddDays(-30);
				int dayDiff = diff.Days;

				if (dayDiff >= 0)
				{
					workDays[dayDiff] = true;
				}
			}

			int workDaysCount = 0;
			foreach(var value in workDays)
			{
				if (value)
				{
					workDaysCount++;
				}
			}

			RatioSeries = new[] { 30-workDaysCount, workDaysCount }.AsPieSeries((value, series) =>
			{
				// pushes out the slice with the value of 6 to 30 pixels.
				if(value==workDaysCount)
				{
					series.Pushout = 10;
					series.Name = "Work";
					series.Fill = new SolidColorPaint(new SKColor(212, 173, 252));
				} else
				{
					series.Name = "Rest";
					series.Fill = new SolidColorPaint(new SKColor(43, 43, 43));
				}
				series.IsHoverable = false;
			});
		});
	}

	[RelayCommand]
	private async Task GoToOngoingRoutine()
	{
		await Shell.Current.GoToAsync("OngoingRoutinePage");
	}
}
