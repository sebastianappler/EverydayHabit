using EverydayHabit.Application.HabitDifficulties.Commands.UpsertHabitDifficulty;
using EverydayHabit.Application.Habits.Queries.GetHabitDetail.Dtos;
using EverydayHabit.Application.HabitVariations.Commands.UpsertHabitVariation;
using EverydayHabit.Application.HabitVariations.Queries.GetHabitVariation;
using EverydayHabit.Domain.Enums;
using EverydayHabit.XamarinApp.Common.ViewModels;
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
                var habitVariationCommand = new UpsertHabitVariationCommand
                {
                    Id = HabitVariation.Id,
                    HabitId = HabitVariation.HabitId,
                    Name = HabitVariation.Name
                };

                var habitVariationId = await Mediator.Send(habitVariationCommand);

                await CreateHabitDifficulties(habitVariationId);
            }

            await Xamarin.Forms.Application.Current.MainPage.Navigation.PopAsync();
        }

        private async Task CreateHabitDifficulties(int habitVariationId)
        {
           
            var miniDifficultyCommand = new UpsertHabitDifficultyCommand
            {
                Id = Mini.Id,
                HabitVariationId = habitVariationId,
                DifficultyLevel = HabitDifficultyLevel.Mini,
                Description = Mini.Description,
            };

            await Mediator.Send(miniDifficultyCommand);

            var plusDifficultyCommand = new UpsertHabitDifficultyCommand
            {
                Id = Plus.Id,
                HabitVariationId = habitVariationId,
                DifficultyLevel = HabitDifficultyLevel.Plus,
                Description = Plus.Description,
            };

            await Mediator.Send(plusDifficultyCommand);

            var eliteDifficultyCommand = new UpsertHabitDifficultyCommand
            {
                Id = Elite.Id,
                HabitVariationId = habitVariationId,
                DifficultyLevel = HabitDifficultyLevel.Elite,
                Description = Elite.Description,
            };

            await Mediator.Send(eliteDifficultyCommand);
        }

        public async Task OnDeleteClickedCommand()
        {
            if (HabitVariation != null)
            {
            }
        }

        private HabitDifficultyDto _mini = new HabitDifficultyDto();
        public HabitDifficultyDto Mini
        {
            get => _mini;
            set => SetProperty(ref _mini, value);
        }

        private HabitDifficultyDto _plus = new HabitDifficultyDto();
        public HabitDifficultyDto Plus
        {
            get => _plus;
            set => SetProperty(ref _plus, value);
        }

        private HabitDifficultyDto _elite = new HabitDifficultyDto();
        public HabitDifficultyDto Elite
        {
            get => _elite;
            set => SetProperty(ref _elite, value);
        }
    }
}
