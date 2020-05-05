using Application.Habits.Commands.CreateHabit;
using EverydayHabit.Application.Habits.Queries.GetHabitDetail;
using EverydayHabit.XamarinApp.Common.Views;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EverydayHabit.XamarinApp.Features
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HabitPage : BaseView
    {
        public HabitPage()
        {
            InitializeComponent();
        }

        async void OnSaveClicked(object sender, EventArgs e)
        {
            var habitItem = (HabitDetailVm) BindingContext;

            await Mediator.Send(new CreateHabitCommand{ Name = habitItem.Name});
            await Navigation.PopAsync();
        }
    }
}