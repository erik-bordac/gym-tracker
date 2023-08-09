using SQLite;

namespace GymTracker.Model;

public class Routine
{
	[PrimaryKey, AutoIncrement]
	public int ID { get; set; }

	public string Name { get; set; }
}