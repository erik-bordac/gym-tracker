using GymTracker.Helpers;
using SQLite;

namespace GymTracker.Services;

public class LocalDatabase
{
	public SQLiteAsyncConnection Database;

	public LocalDatabase()
	{
	}

	public async Task<int> SaveExerciseAsync(Exercise item)
	{
		await Init();
		if (item.ID != 0)
			return await Database.UpdateAsync(item);
		else
			return await Database.InsertAsync(item);
	}

	public async Task<int> DeleteExerciseAsync(Exercise item)
	{
		// TODO: Delete all routines containing the exercise
		await Init();
		return await Database.DeleteAsync(item);
	}

	public async Task<List<Exercise>> GetExercisesAsync()
	{
		await Init();
		return await Database.Table<Exercise>().ToListAsync();
	}

	public async Task<int> SaveRoutineExercisesAsync(List<RoutineExercise> rexes)
	{
		await Init();

		return await Database.InsertAllAsync(rexes);
	}

	public async Task<int> SaveRoutineAsync(Routine item)
	{
		await Init();
		if (item.ID != 0)
			return await Database.UpdateAsync(item);
		else
			return await Database.InsertAsync(item);
	}

	public async Task<int> DeleteRoutineAsync(Routine item)
	{
		await Init();
		return await Database.DeleteAsync(item);
	}

	public async Task<List<Routine>> GetRoutinesAsync()
	{
		await Init();
		return await Database.Table<Routine>().ToListAsync();
	}

	private async Task Init()
	{
		if (Database is not null)
			return;

		// DROP ALL TABLES ----- ONLY FOR DEVELOPMENT
		//Database.DropTableAsync<Routine>();
		//Database.DropTableAsync<Exercise>();
		//Database.DropTableAsync<ExerciseHistory>();
		//Database.DropTableAsync<RoutineExercises>();

		Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
		var result = await Database.CreateTableAsync<Routine>();
		result = await Database.CreateTableAsync<Exercise>();
		result = await Database.CreateTableAsync<ExerciseHistory>();
		result = await Database.CreateTableAsync<RoutineExercise>();
	}

	public async Task<int> GetLastIDAsync()
	{
		// Is this needed ???
		return await Database.ExecuteScalarAsync<int>("select last_insert_rowid()");
	}
}