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

            if(habitItem.Id != 0)
            {
                await Mediator.Send(new UpdateHabitCommand
                {
                    Id = habitItem.Id,
                    Name = habitItem.Name,
                    Description = habitItem.Description,
                });
            }
            else
            {
                await Mediator.Send(new CreateHabitCommand
                {
                    Name = habitItem.Name,
                    Description = habitItem.Description,
                });
            }
           

            await Navigation.PopAsync();
        }

        async void OnDeleteClicked(object sender, EventArgs e)
        {
            var habitItem = (HabitDetailVm)BindingContext;

            await Mediator.Send(new DeleteHabitCommand { Id = habitItem.Id });
            await Navigation.PopAsync();
        }
    }
}