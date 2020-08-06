using Xamarin.Plugin.Calendar.Models;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using EverydayHabit.XamarinApp.Common.ViewModels;
using EverydayHabit.XamarinApp.Features.Calendar.HabitCompletionPage;
using EverydayHabit.Application.Habits.Queries.GetHabitsList;
using EverydayHabit.Domain.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using System.Globalization;
using EverydayHabit.XamarinApp.Features.Calendar.HabitCalendar.Models;
using EverydayHabit.XamarinApp.Features.Habits.HabitPage;
using EverydayHabit.XamarinApp.Features.Settings;
using EverydayHabit.XamarinApp.Common.Converters;
using EverydayHabit.XamarinApp.Features.Habits.HabitList;

namespace EverydayHabit.XamarinApp.Features.Calendar.HabitCalendar
{
    public class HabitCalendarViewModel : BasePageViewModel
    {
        public ICommand TodayCommand => new Command(() => { Year = DateTime.Today.Year; Month = DateTime.Today.Month; });
        public ICommand EventSelectedCommand => new Command(async (item) => await ExecuteEventSelectedCommand(item));
        public ICommand DayTappedCommand => new Command<DateTime>((date) => DayTapped(date));
        public ICommand HabitCompletedTapped => new Command(async () => await HabitCompleted());
        public ICommand SelectedHabitChangedCommand => new Command(async (item) => await SelectedHabitChanged(item));

        public HabitCalendarViewModel()
        {
            Events = new EventCollection();
            SetCalendarCulture();

            Device.BeginInvokeOnMainThread(async () =>
            {
                var habitsListVm = await Mediator.Send(new GetHabitsListQuery());

                if (!habitsListVm.Habits.Any()) return;

                HabitList = new ObservableCollection<HabitListDto>(habitsListVm.Habits);

                foreach (var habit in habitsListVm.Habits)
                {
                    PickerHabitList.Add(new KeyValuePair<int, string>(habit.Id, habit.Name));
                }

                var selectedHabit = habitsListVm.Habits.First();

                if (SelectedHabit.Key == 0)
                {
                    SelectedHabit = new KeyValuePair<int, string>(selectedHabit.Id, selectedHabit.Name);
                }

                await UpdateCalendarEvents(selectedHabit.Id);
            });

            MessagingCenter.Subscribe<HabitPageViewModel, int>(this, "HabitUpserted", async (sender, habitId) =>
            {
                var habit = await Mediator.Send(new GetHabitDetailQuery { Id = habitId });
                var habitToAdd = new KeyValuePair<int, string>(habit.Id, habit.Name);

                var habitToUpdate = PickerHabitList.FirstOrDefault(h => h.Key == habit.Id);
                var indexOfHabitToUpdate = PickerHabitList.IndexOf(habitToUpdate);

                if (indexOfHabitToUpdate == -1)
                {
                    PickerHabitList.Add(habitToAdd);

                    if (PickerHabitList.Count == 1)
                    {
                        SelectedHabit = habitToAdd;
                        await UpdateCalendarEvents(habitToAdd.Key);
                    }
                }
                else
                {
                    PickerHabitList.RemoveAt(indexOfHabitToUpdate);
                    PickerHabitList.Insert(indexOfHabitToUpdate, habitToAdd);
                }
            });

            MessagingCenter.Subscribe<HabitPageViewModel, int>(this, "HabitDeleted", async (sender, habitId) =>
            {
                var habit = PickerHabitList.Where(habit => habit.Key == habitId).FirstOrDefault();
                PickerHabitList.Remove(habit);

                if (habit.Key == SelectedHabit.Key)
                {
                    if (PickerHabitList.Any())
                    {
                        SelectedHabit = new KeyValuePair<int, string>(habit.Key, habit.Value);
                        await UpdateCalendarEvents(habit.Key);
                    }
                }
            });

            MessagingCenter.Subscribe<HabitListViewModel, int>(this, "CompletionChanged", async (sender, habitId) =>
            {
                await UpdateCalendarEvents(habitId);
            });
        }
        private void SetCalendarCulture()
        {
            bool? isSundayStartDayOfWeek = false;
            if (App.Current.Properties.ContainsKey(SettingsViewModel.SUNDAY_START_OF_WEEK))
            {
                isSundayStartDayOfWeek = App.Current.Properties[SettingsViewModel.SUNDAY_START_OF_WEEK] as bool?;
            }

            var calendarCulture = (bool) isSundayStartDayOfWeek ? "en-US" : "en-GB";
            Culture = CultureInfo.CreateSpecificCulture(calendarCulture);
        }
       
        public void AddHabitToPicker(int id, string name)
        {
            PickerHabitList.Add(new KeyValuePair<int, string>(id, name));
        }

        public async Task UpdateCalendarEvents(int habitId)
        {
            var habitCompletionVm = await Mediator.Send(new GetHabitCompletionsListQuery { HabitId = habitId });
            
            Events.Clear();

            foreach (var habitCompletion in habitCompletionVm.HabitCompletionsList)
            {
                var habitCompletionColor = GetHabitCompletionColor(habitCompletion.HabitDifficulty.DifficultyLevel);

                Events[habitCompletion.Date] = new DayEventCollection<EventModel>(habitCompletionColor, habitCompletionColor)
                {
                    new EventModel
                    {
                        Id = habitCompletion.Id,
                        Name = habitCompletion.HabitDifficulty.DifficultyLevel.ToString(), 
                        Description = $"{habitCompletion.HabitVariation.Name} - {habitCompletion.HabitDifficulty.Description}"
                    }
                };
            }
        }

        private Color GetHabitCompletionColor(HabitDifficultyLevel habitDifficultyLevel)
        {
            return (Color) HabitDifficultyToColorConverter.ConvertToColor(habitDifficultyLevel);
        }

        private void DayTapped(DateTime dateSelected)
        {
        }

        private int GetHabitCompletionId(DateTime date)
        {
            int habitCompletionId = 0;
            var selectedEvent = Events.SingleOrDefault(e => e.Key == date);
            var dayEventColletion = selectedEvent.Value as DayEventCollection<EventModel>;

            if (dayEventColletion != null)
            {
                var eventModel = dayEventColletion.FirstOrDefault();
                habitCompletionId = eventModel?.Id ?? 0;
            }

            return habitCompletionId;
        }

        private async Task HabitCompleted()
        {
            if (SelectedDate <= DateTime.Now.Date)
            {
                GetHabitCompletionId(SelectedDate);
                await OpenHabitCompletionPage(SelectedDate);
            }
        }

        private async Task OpenHabitCompletionPage(DateTime date)
        {
            var habitId = SelectedHabit.Key;
            if(habitId > 0)
            {
                var habit = await Mediator.Send(new GetHabitDetailQuery { Id = SelectedHabit.Key });
                await Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new HabitCompletionPageView
                {
                    BindingContext = new HabitCompletionPageViewModel
                    {
                        SelectedHabitCompletionId = GetHabitCompletionId(date),
                        DateSelected = date,
                        HabitSelected = habit,
                        Parent = this,
                    }
                });
            }
        }
        public int SelectedHabitCompletionId { get; set; }
        public EventCollection Events { get; }

        private async Task SelectedHabitChanged(object item)
        {
            if (item is KeyValuePair<int, string> selectedHabit)
            {
                await UpdateCalendarEvents(selectedHabit.Key);
            }
        }

        private CultureInfo _culture = CultureInfo.InvariantCulture;
        public CultureInfo Culture
        {
            get => _culture;
            set => SetProperty(ref _culture, value);
        }
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
