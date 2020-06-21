using Application.Habits.Commands.CreateHabit;
using EverydayHabit.Application.Habits.Commands.UpsertHabit;
using EverydayHabit.Application.Habits.Queries.GetHabitDetail;
using EverydayHabit.Application.Habits.Queries.GetHabitDetail.Dtos;
using EverydayHabit.Application.HabitVariations.Queries.GetHabitVariation;
using EverydayHabit.Domain.Enums;
using EverydayHabit.XamarinApp.Common.Components;
using EverydayHabit.XamarinApp.Common.Converters;
using EverydayHabit.XamarinApp.Common.ViewModels;
using EverydayHabit.XamarinApp.Features.HabitVariationPage;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
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
        public ICommand OnVariationListItemSelected => new Command(async (item) => await OnVariationListItemSelectedCommand(item));

        public HabitDetailVm HabitItem { get; set; }

        public HabitPageViewModel()
        {
            if(HabitItem?.Id > 0)
            {
                SelectedHabitType = PickerHabitTypes.SingleOrDefault(ht => ht.Id == (int) HabitItem.HabitType) ?? PickerHabitTypes.First();
            }
        }
        public async Task OnSaveClickedCommand()
        {
            await UpsertHabitAsync();

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

        public async Task OnAddVariationClickedCommand()
        {
            await UpsertHabitAsync();
            await Xamarin.Forms.Application.Current.MainPage.Navigation.PushModalAsync(new HabitVariationPageView
            {
                BindingContext = new HabitVariationPageViewModel
                {
                    HabitVariation = new HabitVariationDetailVm
                    {
                        HabitId = HabitItem.Id
                    }
                }
            });
        }

        private async Task UpsertHabitAsync()
        {
            if (HabitItem != null)
            {
                var habitId = await Mediator.Send(new UpsertHabitCommand
                {
                    Id = HabitItem.Id,
                    Name = HabitItem.Name,
                    Description = HabitItem.Description,
                    HabitType = (HabitType) SelectedHabitType.Id
                });

                HabitItem.Id = habitId;
            }
        }

        private async Task OnVariationListItemSelectedCommand(object item)
        {
            if (item is HabitVariationDto selectedHabitVariation)
            {
                var habitVariation = await Mediator.Send(new GetHabitVariationDetailQuery { Id = selectedHabitVariation.Id }, CancellationToken.None);

                var mini = new HabitDifficultyDto();
                var plus = new HabitDifficultyDto();
                var elite = new HabitDifficultyDto();

                if (habitVariation.DifficultiesList.Any())
                {
                    mini = habitVariation.DifficultiesList.SingleOrDefault(d => d.DifficultyLevel == HabitDifficultyLevel.Mini);
                    plus = habitVariation.DifficultiesList.SingleOrDefault(d => d.DifficultyLevel == HabitDifficultyLevel.Plus);
                    elite = habitVariation.DifficultiesList.SingleOrDefault(d => d.DifficultyLevel == HabitDifficultyLevel.Elite);
                }
               
                await Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new HabitVariationPageView
                {
                    BindingContext = new HabitVariationPageViewModel()
                    {
                        HabitVariation = habitVariation,
                        Mini = mini,
                        Plus = plus,
                        Elite = elite
                    }
                });
            }
        }

        private static ObservableCollection<ItemWithIconCellViewModel> GetHabitTypes()
        {
            var habitTypes = new ObservableCollection<ItemWithIconCellViewModel>();
            foreach (HabitType habitType in Enum.GetValues(typeof(HabitType)))
            {
                habitTypes.Add(new ItemWithIconCellViewModel
                {
                    Id = (int) habitType,
                    Name = habitType.ToString(),
                    Icon = HabitTypeToIconConverter.Convert(habitType)
                });
            }

            return habitTypes;
        }

        private ObservableCollection<ItemWithIconCellViewModel> _pickerHabitTypes = GetHabitTypes();

        public ObservableCollection<ItemWithIconCellViewModel> PickerHabitTypes
        {
            get => _pickerHabitTypes;
            set => SetProperty(ref _pickerHabitTypes, value);
        }

        private ItemWithIconCellViewModel _selectedHabitType = GetHabitTypes().First();

        public ItemWithIconCellViewModel SelectedHabitType
        {
            get => _selectedHabitType;
            set => SetProperty(ref _selectedHabitType, value);
        }
    }
}
