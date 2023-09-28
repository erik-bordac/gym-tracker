using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Storage;
using GymTracker.Helpers;
using GymTracker.Services;
using GymTracker.View;
using System.IO;

namespace GymTracker.ViewModel;

public partial class ImportExportPageViewModel : BaseViewModel
{
	LocalDatabase _db;
	public ImportExportPageViewModel(LocalDatabase db)
	{
		_db = db;
	}

	[RelayCommand]
	public async Task LoadFile()
	{
		var customFileType = new FilePickerFileType(
				new Dictionary<DevicePlatform, IEnumerable<string>>
				{
                    { DevicePlatform.Android, new[] { ".db3" } },
                    { DevicePlatform.WinUI, new[] { ".db3" } },
                });

		PickOptions options = new()
		{
			PickerTitle = "Please select a .db3 file",
			FileTypes = customFileType,
		};

		var result = await PickAndShow(options);
		
		if (result != null)
		{
			System.IO.File.Copy(result.FullPath, Constants.DatabasePath, true);
		}
	}

	private async Task<FileResult> PickAndShow(PickOptions options)
	{
		try
		{
			var result = await FilePicker.Default.PickAsync(options);

			return result;
		}
		catch (Exception ex)
		{
			await Application.Current.MainPage.DisplayAlert("Exception", $"{ex.Message}", "Ok");
		}

		return null;
	}

	[RelayCommand]
	async Task SaveFile(CancellationToken cancellationToken)
	{
		await _db.Database.CloseAsync();
		FileStream fStream = new FileStream(Constants.DatabasePath, FileMode.Open);
		var fileSaverResult = await FileSaver.Default.SaveAsync(Constants.DatabaseFilename, fStream, cancellationToken);
		if (fileSaverResult.IsSuccessful)
		{
			await Toast.Make($"The file was saved successfully to location: {fileSaverResult.FilePath}").Show(cancellationToken);
		}
		else
		{
			await Toast.Make($"The file was not saved successfully with error: {fileSaverResult.Exception.Message}").Show(cancellationToken);
		}
	}
}
