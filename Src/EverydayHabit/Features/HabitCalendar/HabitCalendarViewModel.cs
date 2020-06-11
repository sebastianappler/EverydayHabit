using Xamarin.Plugin.Calendar.Models;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using EverydayHabit.XamarinApp.Features.HabitCalendar.Models;
using EverydayHabit.XamarinApp.Common.ViewModels;
using EverydayHabit.XamarinApp.Features.HabitCompletionSelectPage;
using MediatR;
using EverydayHabit.Application.Habits.Queries.GetHabitsList;
using System.Linq;
using EverydayHabit.Domain.Enums;

namespace EverydayHabit.XamarinApp.Features.HabitCalendar
{
    public class HabitCalendarViewModel : BasePageViewModel
    {
        public ICommand TodayCommand => new Command(() => { Year = DateTime.Today.Year; Month = DateTime.Today.Month; });
        public ICommand EventSelectedCommand => new Command(async (item) => await ExecuteEventSelectedCommand(item));
        public ICommand DayTappedCommand => new Command<DateTime>(async (date) => await DayTapped(date));

        public HabitCalendarViewModel() : base()
        {
            Events = new EventCollection();

            Device.BeginInvokeOnMainThread(async () =>
            { 
                var habitCompletionVm = await Mediator.Send(new GetHabitCompletionsListQuery());

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
            });
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
            var message = $"Received tap event from date: {dateSelected}";

            var habit = await Mediator.Send(new GetHabitDetailQuery { Id = 1 });
            await Xamarin.Forms.Application.Current.MainPage.Navigation.PushModalAsync(new HabitCompletionSelectPageView
            {
               BindingContext = new HabitCompletionSelectPageViewModel
               {
                   DateSelected = dateSelected,
                   HabitSelected = habit
               }
            });

            Console.WriteLine(message);
        }

        public EventCollection Events { get; }

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
