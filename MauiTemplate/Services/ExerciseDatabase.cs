using SQLite;
using GymTracker.Helpers;

namespace GymTracker.Services;

public class ExerciseDatabase
{
	SQLiteAsyncConnection Database;

	public ExerciseDatabase()
	{
	}

	public async Task<int> SaveItemAsync(Exercise item)
	{
		await Init();
		if (item.ID != 0)
			return await Database.UpdateAsync(item);
		else
			return await Database.InsertAsync(item);
	}

	public async Task<List<Exercise>> GetItemsAsync()
	{
		await Init();
		//await Database.DropTableAsync<Exercise>();
		return await Database.Table<Exercise>().ToListAsync();
	}

	async Task Init()
	{
		if (Database is not null)
			return;

		Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
		var result = await Database.CreateTableAsync<Exercise>();
	}
}
