using Application.Habits.Commands.CreateHabit;
using EverydayHabit.Application.HabitDifficulties.Commands.CreateHabitDifficulty;
using EverydayHabit.Application.Habits.Queries.GetHabitDetail;
using EverydayHabit.Application.HabitVariations.Queries.GetHabitVariation;
using EverydayHabit.Domain.Enums;
using EverydayHabit.XamarinApp.Common.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace EverydayHabit.XamarinApp.Features.VariationPage
{
    public class HabitVariationPageViewModel : BasePageViewModel
    {
        public ICommand OnSaveClicked => new Command(async () => await OnSaveClickedCommand());
        public ICommand OnDeleteClicked => new Command(async () => await OnDeleteClickedCommand());

        public HabitVariationDetailVm HabitVariation { get; set; }

        public async Task OnSaveClickedCommand()
        {
            if (HabitVariation != null)
            { 
              
            }

            Xamarin.Forms.Application.Current.MainPage = new NavigationPage(new MainPage());
        }

        public async Task OnDeleteClickedCommand()
        {
            if (HabitVariation != null)
            {
            }
        }
        private Editor _mini;
        public Editor Mini
        {
            get => _mini;
            set => SetProperty(ref _mini, value);
        }
    }
}
