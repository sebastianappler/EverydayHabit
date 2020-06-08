using EverydayHabit.XamarinApp.Common.ViewModels;
using System;

namespace EverydayHabit.XamarinApp.Features.HabitCompletionSelectPage
{
    public class HabitCompletionSelectPageViewModel : BasePageViewModel
    {
        public DateTime DateSelected { get; set; }
        public string FormattedDate => DateSelected.AddDays(5).ToString("%d MMMM");


        public HabitCompletionSelectPageViewModel()
        {
        }

        private DateTime _selectedDate = DateTime.MinValue;
        public DateTime SelectedDate
        {
            get => _selectedDate;
            set => SetProperty(ref _selectedDate, value);
        }
    }
}
