﻿using GymTracker.Services;

namespace GymTracker.ViewModel;

public partial class OngoingRoutinePageViewModel : BaseViewModel
{
	private OngoingRoutineService _ors;

	[ObservableProperty]
	private ObservableCollection<Exercise> frames;
	public OngoingRoutinePageViewModel(OngoingRoutineService ors)
	{
		_ors = ors;
		Frames = ors.Frames;
	}

	[RelayCommand]
	private async Task GoBack()
	{
		await Shell.Current.Navigation.PopToRootAsync();
	}
}
