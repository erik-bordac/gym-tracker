using SQLite;
namespace GymTracker.Model;

public class RoutineExercises
{
	[PrimaryKey, AutoIncrement]
    public int ID { get; set; }
    public int ExerciseID { get; set; }
	public int RoutineID { get; set; }
	public int Sets { get; set; }
}
