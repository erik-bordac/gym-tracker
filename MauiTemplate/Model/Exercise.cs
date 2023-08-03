using SQLite;
namespace GymTracker.Model;

public class Exercise
{
	[PrimaryKey, AutoIncrement]
    public int ID { get; set; }
    public string Name { get; set; }
    public int DefaultSets { get; set; }
    public int DefaultReps { get; set; }
    public int DefaultWeight { get; set; }
}
