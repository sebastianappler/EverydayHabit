using EverydayHabit.Application.HabitDifficulties.Commands.UpsertHabitDifficulty;
using EverydayHabit.Application.Habits.Queries.GetHabitDetail;
using EverydayHabit.Application.Habits.Queries.GetHabitDetail.Dtos;
using EverydayHabit.Application.Habits.Queries.GetHabitsList;
using EverydayHabit.Application.HabitVariations.Commands.DeleteHabitVariation;
using EverydayHabit.Application.HabitVariations.Commands.UpsertHabitVariation;
using EverydayHabit.Application.HabitVariations.Queries.GetHabitVariation;
using EverydayHabit.Domain.Enums;
using EverydayHabit.XamarinApp.Common.ViewModels;
using EverydayHabit.XamarinApp.Features.Habits.HabitPage;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace EverydayHabit.XamarinApp.Features.Habits.HabitVariationPage
{
    public class HabitVariationPageViewModel : BasePageViewModel
    {
        public ICommand OnSaveCommand => new Command(async () => await OnSave());
        public ICommand OnDeleteCommand => new Command(async () => await OnDelete());
        public ICommand OnCloseCommand => new Command(async () => await OnClose());

        public HabitVariationDetailVm HabitVariation { get; set; }
        public bool IsDeletePossible { get; set; }

        public async Task OnSave()
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

            await NavigateToHabitPage();
        }

        public async Task OnDelete()
        {
            if (HabitVariation != null)
            {
                await Mediator.Send(new DeleteHabitVariationCommand { Id = HabitVariation.Id });

                await NavigateToHabitPage();
            }
        }

        public async Task OnClose()
        {
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

        private async Task NavigateToHabitPage()
        {
            var vm = await Mediator.Send(new GetHabitDetailQuery { Id = HabitVariation.HabitId }, CancellationToken.None);

            //Pop current page and previous page before pushing a updated page to the navigation stack
            await Xamarin.Forms.Application.Current.MainPage.Navigation.PopAsync(animated: false);
            await Xamarin.Forms.Application.Current.MainPage.Navigation.PopAsync(animated: false);
            await Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new HabitPageView
            {
                BindingContext = new HabitPageViewModel
                {
                    HabitItem = vm as HabitDetailVm
                }
            });
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
