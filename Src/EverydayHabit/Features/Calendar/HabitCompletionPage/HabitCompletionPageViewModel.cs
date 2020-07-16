using Application.Habits.Commands.CreateHabit;
using EverydayHabit.Application.HabitCompletions.Commands.UpsertHabitCompletion;
using EverydayHabit.Application.Habits.Queries.GetHabitDetail;
using EverydayHabit.Application.Habits.Queries.GetHabitDetail.Dtos;
using EverydayHabit.XamarinApp.Common.ViewModels;
using EverydayHabit.XamarinApp.Features.Calendar.HabitCalendar;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace EverydayHabit.XamarinApp.Features.Calendar.HabitCompletionPage
{
    public class HabitCompletionPageViewModel : BasePageViewModel
    {
        public ICommand OnVariationItemSelectedCommand => new Command((item) => OnVariationItemSelected(item));
        public ICommand OnDifficultyItemSelectedCommand => new Command((item) => OnDifficultyItemSelected(item));
        public ICommand OnSaveCommand => new Command(async () => await OnSave());
        public ICommand OnDeleteCommand => new Command(async () => await OnDelete());
        public ICommand OnCloseCommand => new Command(async () => await OnClose());

        public HabitDetailVm HabitSelected { get; set; }
        public DateTime DateSelected { get; set; }
        public string CompletionTitle => $"{DateSelected.ToString("%d MMMM")} - {HabitSelected?.Name}";
        public int SelectedHabitCompletionId { get; set; }
        public HabitCalendarViewModel Parent { get; internal set; }

        private void OnVariationItemSelected(object item)
        {
            if (item is HabitVariationDto selectedHabitVariation)
            {
                SelectedHabitVariation = selectedHabitVariation;
              
                if(SelectedDifficulty.Id > 0 && SelectedDifficulty?.HabitVariation.HabitId != selectedHabitVariation.Id)
                {
                    SelectedDifficulty = new HabitDifficultyDto();
                }

                CurrentDifficultyList.Clear();

                foreach (var difficulty in selectedHabitVariation.DifficultiesList)
                {
                    if(!string.IsNullOrEmpty(difficulty.Description))
                    {
                        CurrentDifficultyList.Add(difficulty);
                    }
                }

                SelectedDifficulty = CurrentDifficultyList.FirstOrDefault();
            }
        }
        
        private void OnDifficultyItemSelected(object item)
        {
            if (item is HabitDifficultyDto selectedDifficulty)
            {
                SelectedDifficulty = selectedDifficulty;
            }
        }

        public async Task OnSave()
        {
            if(SelectedHabitVariation.Id > 0 && SelectedDifficulty.Id > 0)
            {
                await Mediator.Send(new UpsertHabitCompletionCommand
                {
                    Id = SelectedHabitCompletionId,
                    HabitId = HabitSelected.Id,
                    HabitVariationId = SelectedHabitVariation.Id,
                    Date = DateSelected,
                    HabitDifficultyId = SelectedDifficulty.Id
                });

                await Parent.UpdateCalendarEvents(HabitSelected.Id);
            }

            await Xamarin.Forms.Application.Current.MainPage.Navigation.PopAsync();
        }
        
        public async Task OnDelete()
        {
            if(SelectedHabitCompletionId > 0)
            {
                var isConfirmed = await App.Current.MainPage.DisplayAlert("Delete entry?", $"Are you sure you want to delete completion of habit?", "Yes", "No");
                if (isConfirmed)
                {

                    await Mediator.Send(new DeleteHabitCompletionCommand
                    {
                        Id = SelectedHabitCompletionId
                    });

                    await Parent.UpdateCalendarEvents(HabitSelected.Id);

                    await Xamarin.Forms.Application.Current.MainPage.Navigation.PopAsync();
                }
            }
        }
        
        public async Task OnClose()
        {
            await Xamarin.Forms.Application.Current.MainPage.Navigation.PopAsync();
        }

        public bool _isDeletePossible = false;
        public bool IsDeletePossible
        {
            get => SelectedHabitCompletionId > 0 ? true : false;
            set => SetProperty(ref _isDeletePossible, value);
        }

        private DateTime _selectedDate = DateTime.MinValue;
        public DateTime SelectedDate
        {
            get => _selectedDate;
            set => SetProperty(ref _selectedDate, value);
        }
        
        private HabitVariationDto _selectedHabitVariation = new HabitVariationDto();
        public HabitVariationDto SelectedHabitVariation
        {
            get => _selectedHabitVariation;
            set => SetProperty(ref _selectedHabitVariation, value);
        }
        
        private HabitDifficultyDto _selectedDifficulty = new HabitDifficultyDto();
        public HabitDifficultyDto SelectedDifficulty
        {
            get => _selectedDifficulty;
            set => SetProperty(ref _selectedDifficulty, value);
        }

        public ObservableCollection<HabitDifficultyDto> _currentDifficultyList = new ObservableCollection<HabitDifficultyDto> {};
        public ObservableCollection<HabitDifficultyDto> CurrentDifficultyList
        {
            get => _currentDifficultyList;
            set => SetProperty(ref _currentDifficultyList, value);
        }
    }
}
