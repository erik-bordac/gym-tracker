using SQLite;

namespace GymTracker.Model;

public class ExerciseHistory
{
	[PrimaryKey, AutoIncrement]
	public int ID { get; set; }

	public DateTime DateTime { get; set; }
	public int ExerciseID { get; set; }
	public int RepsCount { get; set; }
	public int Weight { get; set; }
	public int TimeInSeconds { get; set; }
}