using Xamarin.Plugin.Calendar.Models;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using EverydayHabit.XamarinApp.Features.HabitCalendar.Models;
using EverydayHabit.XamarinApp.Common.ViewModels;
using EverydayHabit.XamarinApp.Features.HabitCompletionSelectPage;
using EverydayHabit.Application.Habits.Queries.GetHabitsList;
using EverydayHabit.Domain.Enums;
using System.Collections.Generic;
using MediatR;
using System.Linq;
using System.Collections.ObjectModel;
using System.Threading;
using System.ComponentModel;

namespace EverydayHabit.XamarinApp.Features.HabitCalendar
{
    public class HabitCalendarViewModel : BasePageViewModel
    {
        public ICommand TodayCommand => new Command(() => { Year = DateTime.Today.Year; Month = DateTime.Today.Month; });
        public ICommand EventSelectedCommand => new Command(async (item) => await ExecuteEventSelectedCommand(item));
        public ICommand DayTappedCommand => new Command<DateTime>(async (date) => await DayTapped(date));
        public ICommand SelectedHabitChangedCommand => new Command(async (item) => await SelectedHabitChanged(item));

        public HabitCalendarViewModel() : base()
        {
            Events = new EventCollection();

            Device.BeginInvokeOnMainThread(async () =>
            {
                var habitsListVm = await Mediator.Send(new GetHabitsListQuery());

                if (!habitsListVm.Habits.Any()) return;

                HabitList = new ObservableCollection<HabitListDto>(habitsListVm.Habits);

                foreach(var habit in habitsListVm.Habits)
                {
                    PickerHabitList.Add(new KeyValuePair<int, string>(habit.Id, habit.Name));
                }

                var selectedHabit = habitsListVm.Habits.First();

                if(SelectedHabit.Key == 0)
                {
                    SelectedHabit = new KeyValuePair<int, string>(selectedHabit.Id, selectedHabit.Name);
                }

                await UpdateCalendarEvents(selectedHabit.Id);
            });
        }

        public async Task UpdateCalendarEvents(int selectedHabitId)
        {
            var habitCompletionVm = await Mediator.Send(new GetHabitCompletionsListQuery { HabitId = selectedHabitId });
            
            Events.Clear();

            foreach (var habitCompletion in habitCompletionVm.HabitCompletionsList)
            {
                var habitCompletionColor = GetHabitCompletionColor(habitCompletion.HabitDifficultyLevel);

                Events[habitCompletion.Date] = new DayEventCollection<EventModel>(habitCompletionColor, habitCompletionColor)
                {
                    new EventModel
                    {
                        Name = habitCompletion.CompletedHabit.Name,
                        Description = habitCompletion.HabitDifficultyLevel.ToString()
                    }
                };
            }
        }

        private Color GetHabitCompletionColor(HabitDifficultyLevel habitDifficultyLevel)
        {
            switch (habitDifficultyLevel)
            {
                case HabitDifficultyLevel.Mini:
                    return Color.Green;

                case HabitDifficultyLevel.Plus:
                    return Color.Orange;

                case HabitDifficultyLevel.Elite:
                    return Color.Red;
            }
            return Color.Black;
        }

        private async Task DayTapped(DateTime dateSelected)
        {
            if(dateSelected <= DateTime.Now)
            {
                var habit = await Mediator.Send(new GetHabitDetailQuery { Id = SelectedHabit.Key });
                await Xamarin.Forms.Application.Current.MainPage.Navigation.PushModalAsync(new HabitCompletionSelectPageView
                {
                    BindingContext = new HabitCompletionSelectPageViewModel
                    {
                        DateSelected = dateSelected,
                        HabitSelected = habit,
                        Parent = this,

                    }
                });
            }
        }

        private async Task SelectedHabitChanged(object item)
        {
            if (item is KeyValuePair<int, string> selectedHabit)
            {
                await UpdateCalendarEvents(selectedHabit.Key);
            }
        }
        public EventCollection Events { get; }

        private ObservableCollection<KeyValuePair<int, string>> _pickerHabitList = new ObservableCollection<KeyValuePair<int, string>>();
        public ObservableCollection<KeyValuePair<int, string>> PickerHabitList
        {
            get => _pickerHabitList;
            set => SetProperty(ref _pickerHabitList, value);
        }
        
        private ObservableCollection<HabitListDto> _habitList = new ObservableCollection<HabitListDto>();
        public ObservableCollection<HabitListDto> HabitList
        {
            get => _habitList;
            set => SetProperty(ref _habitList, value);
        }

        private KeyValuePair<int, string> _selectedHabit = new KeyValuePair<int, string>();
        public KeyValuePair<int, string> SelectedHabit
        {
            get => _selectedHabit;
            set => SetProperty(ref _selectedHabit, value);
        }


        private int _month = DateTime.Today.Month;
        public int Month
        {
            get => _month;
            set => SetProperty(ref _month, value);
        }

        public int _year = DateTime.Today.Year;
        public int Year
        {
            get => _year;
            set => SetProperty(ref _year, value);
        }


        private DateTime _selectedDate = DateTime.Today;
        public DateTime SelectedDate
        {
            get => _selectedDate;
            set => SetProperty(ref _selectedDate, value);
        }

        private DateTime _minimumDate = new DateTime(2019, 4, 29);
        public DateTime MinimumDate
        {
            get => _minimumDate;
            set => SetProperty(ref _minimumDate, value);
        }

        private DateTime _maximumDate = DateTime.Today.AddMonths(5);
        public DateTime MaximumDate
        {
            get => _maximumDate;
            set => SetProperty(ref _maximumDate, value);
        }

        private async Task ExecuteEventSelectedCommand(object item)
        {
            if (item is EventModel eventModel)
            {
                await Xamarin.Forms.Application.Current.MainPage.DisplayAlert(eventModel.Name, eventModel.Description, "Ok");
            }
        }
    }
}
