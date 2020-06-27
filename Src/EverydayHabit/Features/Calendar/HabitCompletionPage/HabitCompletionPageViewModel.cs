using EverydayHabit.Application.HabitCompletions.Commands.UpsertHabitCompletion;
using EverydayHabit.Application.Habits.Queries.GetHabitDetail;
using EverydayHabit.Application.Habits.Queries.GetHabitDetail.Dtos;
using EverydayHabit.XamarinApp.Common.ViewModels;
using EverydayHabit.XamarinApp.Features.Calendar.HabitCalendar;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace EverydayHabit.XamarinApp.Features.Calendar.HabitCompletionPage
{
    public class HabitCompletionPageViewModel : BasePageViewModel
    {
        public HabitDetailVm HabitSelected { get; set; }
        public DateTime DateSelected { get; set; }
        public string CompletionTitle => $"{DateSelected.ToString("%d MMMM")} - {HabitSelected?.Name}";
        public ICommand OnVariationItemSelected => new Command((item) => OnVariationItemSelectedCommand(item));
        public ICommand OnDifficultyItemSelected => new Command((item) => OnDifficultyItemSelectedCommand(item));
        public ICommand SaveHabitCompletionCommand => new Command(async () => await SaveHabitCompletion());
        public ICommand OnCloseCommand => new Command(async () => await OnClose());

        private void OnVariationItemSelectedCommand(object item)
        {
            if (item is HabitVariationDto selectedHabitVariation)
            {
                SelectedHabitVariation = selectedHabitVariation;

                CurrentDifficultyList.Clear();
                foreach (var difficulty in selectedHabitVariation.DifficultiesList)
                {
                    CurrentDifficultyList.Add(difficulty);
                }

                if(SelectedDifficulty.Id > 0 && SelectedDifficulty?.HabitVariation.HabitId != selectedHabitVariation.Id)
                {
                    SelectedDifficulty = new HabitDifficultyDto();
                }
            }
        }
        
        private void OnDifficultyItemSelectedCommand(object item)
        {
            if (item is HabitDifficultyDto selectedDifficulty)
            {
                SelectedDifficulty = selectedDifficulty;
            }
        }

        public async Task SaveHabitCompletion()
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
        
        public async Task OnClose()
        {
            await Xamarin.Forms.Application.Current.MainPage.Navigation.PopAsync();
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
        public HabitCalendarViewModel Parent { get; internal set; }
        public int SelectedHabitCompletionId { get; set; }
    }
}
