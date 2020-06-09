using EverydayHabit.Application.Habits.Queries.GetHabitDetail;
using EverydayHabit.Application.Habits.Queries.GetHabitDetail.Dtos;
using EverydayHabit.XamarinApp.Common.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace EverydayHabit.XamarinApp.Features.HabitCompletionSelectPage
{
    public class HabitCompletionSelectPageViewModel : BasePageViewModel
    {
        public HabitDetailVm HabitSelected { get; set; }
        public DateTime DateSelected { get; set; }
        public string CompletionTitle => $"{DateSelected.ToString("%d MMMM")} - {HabitSelected?.Name}";
        public ICommand OnVariationItemSelected => new Command(async (item) => await OnVariationItemSelectedCommand(item));

        private async Task OnVariationItemSelectedCommand(object item)
        {
            if (item is HabitVariationDto selectedHabitVariation)
            {
                CurrentDifficultyList.Clear();
                foreach (var difficulty in selectedHabitVariation.DifficultiesList)
                {
                    CurrentDifficultyList.Add(difficulty);
                }
            }
        }

        private DateTime _selectedDate = DateTime.MinValue;
        public DateTime SelectedDate
        {
            get => _selectedDate;
            set => SetProperty(ref _selectedDate, value);
        }

        public ObservableCollection<HabitDifficultyDto> _currentDifficultyList = new ObservableCollection<HabitDifficultyDto> {};
        public ObservableCollection<HabitDifficultyDto> CurrentDifficultyList
        {
            get => _currentDifficultyList;
            set => SetProperty(ref _currentDifficultyList, value);
        }
    }
}
