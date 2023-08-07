using SQLite;
using GymTracker.Helpers;

namespace GymTracker.Services;

public class LocalDatabase
{
	SQLiteAsyncConnection Database;

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
		await Init();
		return await Database.DeleteAsync(item);
	}
	public async Task<List<Exercise>> GetExercisesAsync()
	{
		await Init();
		return await Database.Table<Exercise>().ToListAsync();
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

	async Task Init()
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
		result = await Database.CreateTableAsync<RoutineExercises>();
	}
}
