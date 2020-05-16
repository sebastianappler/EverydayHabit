using EverydayHabit.XamarinApp.Common.Views;
using EverydayHabit.XamarinApp.Models;
using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Plugin.Calendar.Models;

namespace EverydayHabit.XamarinApp.Features
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HabitCalendar : BaseView, INotifyPropertyChanged
    {
        public HabitCalendar()
        {
            InitializeComponent();
        }
    }
}