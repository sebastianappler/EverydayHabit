using Xamarin.Plugin.Calendar.Models;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using EverydayHabit.XamarinApp.Features.HabitCalendar.Models;
using EverydayHabit.XamarinApp.Common.ViewModels;
using EverydayHabit.XamarinApp.Features.HabitCompletionSelectPage;

namespace EverydayHabit.XamarinApp.Features.HabitCalendar
{
    public class HabitCalendarViewModel : BasePageViewModel, INotifyPropertyChanged
    {
        public ICommand TodayCommand => new Command(() => { Year = DateTime.Today.Year; Month = DateTime.Today.Month; });
        public ICommand EventSelectedCommand => new Command(async (item) => await ExecuteEventSelectedCommand(item));
        public ICommand DayTappedCommand => new Command<DateTime>(async (date) => await DayTapped(date));

        public HabitCalendarViewModel() : base()
        {
            Events = new EventCollection
            {
                [DateTime.Now.AddDays(-3)] = new DayEventCollection<EventModel>(Color.Green, Color.Green) { new EventModel
                {
                    Name = $"Test event 1",
                    Description = $"This is test event 1's description!"
                }},
                [DateTime.Now.AddDays(-5)] = new DayEventCollection<EventModel>(Color.Red, Color.Red) { new EventModel
                {
                    Name = $"Test event 2",
                    Description = $"This is test event 2's description!"
                }},
            };
        }

        private static async Task DayTapped(DateTime dateSelected)
        {
            var message = $"Received tap event from date: {dateSelected}";

            await Xamarin.Forms.Application.Current.MainPage.Navigation.PushModalAsync(new HabitCompletionSelectPageView
            {
               BindingContext = new HabitCompletionSelectPageViewModel
               {
                   DateSelected = dateSelected
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
