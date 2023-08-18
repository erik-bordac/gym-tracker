using GymTracker.Services;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;

namespace GymTracker.ViewModel;

[QueryProperty(nameof(Exercise), "Exercise")]
public partial class ExerciseDetailsPageViewModel : BaseViewModel
{
	private LocalDatabase _db;

	public ExerciseDetailsPageViewModel(LocalDatabase db)
	{
		_db = db;
		LoadSeries();
	}

	[ObservableProperty]
	private Exercise exercise;

	[ObservableProperty]
	private ISeries[] repsSeries;
	[ObservableProperty]
	private List<Axis> repsXAxis;
	[ObservableProperty]
	private List<Axis> repsYAxis;

	[ObservableProperty]
	private ISeries[] weightSeries;
	[ObservableProperty]
	private List<Axis> weightXAxis;
	[ObservableProperty]
	private List<Axis> weightYAxis;
	
	[ObservableProperty]
	private ISeries[] timeSeries;
	[ObservableProperty]
	private List<Axis> timeXAxis;
	[ObservableProperty]
	private List<Axis> timeYAxis;

	[RelayCommand]
	private async Task GoToHistory(Exercise Exercise)
	{
		await Shell.Current.GoToAsync("ExerciseHistoryPage", new Dictionary<string, object>
		{
			{"Exercise", Exercise }
		});
	}

	public async void LoadSeries()
	{
		var x = await _db.GetExerciseHistoryListAsync();
		x = new(x.Where(history => history.ExerciseID == Exercise.ID));
		Dictionary<string, List<double>> values = new();
		values["reps"] = new List<double>();
		values["weight"] = new List<double>();
		values["time"] = new List<double>();

		foreach (ExerciseHistory item in x)
		{
			values["reps"].Add(item.RepsCount);
			values["weight"].Add(item.Weight);
			values["time"].Add(item.TimeInSeconds);
		}

		InitializeRepsSeries(values["reps"]);
		InitializeWeightSeries(values["weight"]);
		InitializeTimeSeries(values["time"]);
	}

	private void InitializeRepsSeries(List<double> values)
	{
		int n = values.Count;
		string[] descendingLabels = Enumerable.Range(1, n).Select(i => (n - i + 1).ToString()).ToArray();
		SetRepsSeries(values.ToArray().Reverse(), descendingLabels);
	}
	private void InitializeWeightSeries(List<double> values)
	{
		int n = values.Count;
		string[] descendingLabels = Enumerable.Range(1, n).Select(i => (n - i + 1).ToString()).ToArray();
		SetWeightSeries(values.ToArray().Reverse(), descendingLabels);
	}
	private void InitializeTimeSeries(List<double> values)
	{
		int n = values.Count;
		string[] descendingLabels = Enumerable.Range(1, n).Select(i => (n - i + 1).ToString()).ToArray();
		SetTimeSeries(values.ToArray().Reverse(), descendingLabels);
	}

	private void SetRepsSeries(IEnumerable<double> values, string[] labels = null)
	{
		RepsSeries = new ISeries[]
		{
			new LineSeries<double>
			{
				Values = values,
				Fill = null,
				Stroke = new SolidColorPaint(new SKColor(212, 173, 252), 4),
				GeometrySize = 22,
				GeometryStroke = new SolidColorPaint(new SKColor(212, 173, 252), 4),
			}
		};

		if (labels == null) return;

		RepsXAxis = new List<Axis>()
		{
			new Axis()
			{
				MinLimit = 0,
				MaxLimit = 10,
				Name = "Set",
				NamePaint = new SolidColorPaint(SKColors.White),
				MinStep = 1,
				Labels = labels
			}
		};
		RepsYAxis = new List<Axis>()
		{
			new Axis()
			{
				Name = "Reps",
				NamePaint = new SolidColorPaint(SKColors.White),
				MinStep = 1,
			}
		};
	}
	private void SetWeightSeries(IEnumerable<double> values, string[] labels = null)
	{
		WeightSeries = new ISeries[]
		{
			new LineSeries<double>
			{
				Values = values,
				Fill = null,
				Stroke = new SolidColorPaint(new SKColor(206, 15, 15), 4),

				GeometrySize = 22,
				GeometryStroke = new SolidColorPaint(new SKColor(206, 15, 15), 4),
			}
		};

		if (labels == null) return;

		WeightXAxis = new List<Axis>()
		{
			new Axis()
			{
				MinLimit = 0,
				MaxLimit = 10,
				NamePaint = new SolidColorPaint(SKColors.White),
				Name = "Set",
				MinStep = 1,
				Labels = labels
			}
		};
		WeightYAxis = new List<Axis>()
		{
			new Axis()
			{
				Name = "Weight",
				NamePaint = new SolidColorPaint(SKColors.White),
				MinStep = 1,
			}
		};
	}
	private void SetTimeSeries(IEnumerable<double> values, string[] labels = null)
	{
		TimeSeries = new ISeries[]
		{
			new LineSeries<double>
			{
				Values = values,
				Fill = null,
				Stroke = new SolidColorPaint(new SKColor(63, 146, 209), 4),
				GeometrySize = 22,
				GeometryStroke = new SolidColorPaint(new SKColor(63, 146, 209), 4),
			}
		};

		if (labels == null) return;

		TimeXAxis = new List<Axis>()
		{
			new Axis()
			{
				MinLimit = 0,
				MaxLimit = 10,
				Name = "Set",
				NamePaint = new SolidColorPaint(SKColors.White),
				MinStep = 1,
				Labels = labels
			}
		};
		TimeYAxis = new List<Axis>()
		{
			new Axis()
			{
				Name = "Time",
				NamePaint = new SolidColorPaint(SKColors.White),
				MinStep = 1,
			}
		};
	}
}