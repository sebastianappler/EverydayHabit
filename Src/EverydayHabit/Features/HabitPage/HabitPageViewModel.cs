﻿using Application.Habits.Commands.CreateHabit;
using EverydayHabit.Application.Habits.Queries.GetHabitDetail;
using EverydayHabit.Domain.Entities;
using EverydayHabit.XamarinApp.Common.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace EverydayHabit.XamarinApp.Features.HabitPage
{
    public class HabitPageViewModel : BasePageViewModel
    {
        public ICommand OnSaveClicked => new Command(async () => await OnSaveClickedCommand());
        public ICommand OnDeleteClicked => new Command(async () => await OnDeleteClickedCommand());
        public ICommand OnAddVariationClicked => new Command(async () => await OnAddVariationClickedCommand());
        public HabitDetailVm HabitItem { get; set; }

        public async Task OnAddVariationClickedCommand()
        {
            await Mediator.Send(new CreateHabitVariationCommand
                {
                    HabitId = HabitItem.Id,
                    Name = HabitItem.Name,
                });
        }

        public async Task OnSaveClickedCommand()
        {
            if (HabitItem != null)
            {
                if (HabitItem.Id != 0)
                {
                    await Mediator.Send(new UpdateHabitCommand
                    {
                        Id = HabitItem.Id,
                        Name = HabitItem.Name,
                        Description = HabitItem.Description,
                    });
                }
                else
                {
                    await Mediator.Send(new CreateHabitCommand
                    {
                        Name = HabitItem.Name,
                        Description = HabitItem.Description,
                    });
                }
            }

            Xamarin.Forms.Application.Current.MainPage = new NavigationPage(new MainPage());
        }

        public async Task OnDeleteClickedCommand()
        {
            if (HabitItem != null)
            {
                await Mediator.Send(new DeleteHabitCommand { Id = HabitItem.Id });
                Xamarin.Forms.Application.Current.MainPage = new NavigationPage(new MainPage());
            }
        }


    }
}
