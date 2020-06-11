﻿using EverydayHabit.Application.HabitCompletions.Commands.CreateHabitCompletion;
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
        public ICommand OnDifficultyItemSelected => new Command(async (item) => await OnDifficultyItemSelectedCommand(item));
        public ICommand OnSaveClicked => new Command(async () => await OnSaveClickedCommand());

        private async Task OnVariationItemSelectedCommand(object item)
        {
            if (item is HabitVariationDto selectedHabitVariation)
            {
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
        
        private async Task OnDifficultyItemSelectedCommand(object item)
        {
            if (item is HabitDifficultyDto selectedDifficulty)
            {
                SelectedDifficulty = selectedDifficulty;
            }
        }

        public async Task OnSaveClickedCommand()
        {
           if(SelectedDifficulty.Id > 0)
           {
                await Mediator.Send(new CreateHabitCompletionCommand
                {
                    HabitId = HabitSelected.Id,
                    Date = DateSelected,
                    HabitDifficultyLevel = SelectedDifficulty.DifficultyLevel
                });
           }

           Xamarin.Forms.Application.Current.MainPage = new NavigationPage(new HabitCalendar.HabitCalendarView());
        }

        private DateTime _selectedDate = DateTime.MinValue;
        public DateTime SelectedDate
        {
            get => _selectedDate;
            set => SetProperty(ref _selectedDate, value);
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
