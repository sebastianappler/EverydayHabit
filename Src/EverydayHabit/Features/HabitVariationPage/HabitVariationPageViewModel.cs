using EverydayHabit.Application.HabitDifficulties.Commands.CreateHabitDifficulty;
using EverydayHabit.Application.Habits.Queries.GetHabitDetail;
using EverydayHabit.Application.HabitVariations.Commands.UpsertHabitVariation;
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

        public HabitVariationPageViewModel()
        {
            //var mini = HabitVariation.DifficultiesList.SingleOrDefault(d => d.DifficultyLevel == HabitDifficultyLevel.Mini);
            //Mini.Text = mini.Description;
        }

        public async Task OnSaveClickedCommand()
        {
            if (HabitVariation != null)
            {
                int? habitVariation = null;
                if(HabitVariation.Id != 0)
                {
                    habitVariation = HabitVariation.Id;
                }

                var habitVariationCommand = new UpsertHabitVariationCommand
                {
                    Id = habitVariation,
                    HabitId = HabitVariation.HabitId,
                    Name = HabitVariation.Name
                };

                var habitVariationId = await Mediator.Send(habitVariationCommand);

                await CreateHabitDifficulties(habitVariationId);
            }

            Xamarin.Forms.Application.Current.MainPage = new NavigationPage(new MainPage());
        }

        private async Task CreateHabitDifficulties(int habitVariationId)
        {
            if (!string.IsNullOrEmpty(Mini.Text))
            {
                var miniDifficultyCommand = new CreateHabitDifficultyCommand
                {
                    HabitVariationId = habitVariationId,
                    DifficultyLevel = HabitDifficultyLevel.Mini,
                    Description = Mini.Text,
                };

                await Mediator.Send(miniDifficultyCommand);
            }

            if (!string.IsNullOrEmpty(Plus.Text))
            {
                var plusDifficultyCommand = new CreateHabitDifficultyCommand
                {
                    HabitVariationId = habitVariationId,
                    DifficultyLevel = HabitDifficultyLevel.Plus,
                    Description = Plus.Text,
                };

                await Mediator.Send(plusDifficultyCommand);
            }

            if (!string.IsNullOrEmpty(Elite.Text))
            {
                var eliteDifficultyCommand = new CreateHabitDifficultyCommand
                {
                    HabitVariationId = habitVariationId,
                    DifficultyLevel = HabitDifficultyLevel.Elite,
                    Description = Elite.Text,
                };

                await Mediator.Send(eliteDifficultyCommand);
            }
        }

        public async Task OnDeleteClickedCommand()
        {
            if (HabitVariation != null)
            {
            }
        }

        private Editor _mini = new Editor();
        public Editor Mini
        {
            get => _mini;
            set => SetProperty(ref _mini, value);
        }

        private Editor _plus = new Editor();
        public Editor Plus
        {
            get => _plus;
            set => SetProperty(ref _plus, value);
        }

        private Editor _elite = new Editor();
        public Editor Elite
        {
            get => _elite;
            set => SetProperty(ref _elite, value);
        }
    }
}
