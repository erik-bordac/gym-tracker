using SQLite;

namespace GymTracker.Model;

public class Exercise
{
	[PrimaryKey, AutoIncrement]
	public int ID { get; set; }

	public string Name { get; set; }
	public string Notes { get; set; }
	public bool TrackReps { get; set; }
	public bool TrackWeight { get; set; }
	public bool TrackTime { get; set; }
}