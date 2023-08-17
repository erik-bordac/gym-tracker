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
	private List<Axis> xAxis;

	[ObservableProperty]
	private ISeries[] series;
	
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

		var reps_values = new List<double>();
		foreach (ExerciseHistory item in x)
		{
			reps_values.Add(item.RepsCount);
		}

		Series = new ISeries[]
		{
				new LineSeries<double>
				{
					Values = reps_values.ToArray().Reverse(),
					Fill = null,
					Stroke = new SolidColorPaint(new SKColor(212, 173, 252), 4),
					GeometrySize = 22,
					GeometryStroke = new SolidColorPaint(new SKColor(212, 173, 252), 4),
				}
		};

		int n = reps_values.Count;
		string[] descendingLabels = Enumerable.Range(1, n).Select(i => (n - i + 1).ToString()).ToArray();
		XAxis = new List<Axis>()
		{
			new Axis()
			{
				MinLimit = 0,
				MaxLimit = 10,
				Name = "Reps",
				MinStep = 1,
				Labels = descendingLabels
			}
		};
	}
}