using Application.Habits.Commands.CreateHabit;
using EverydayHabit.Application.Habits.Commands.UpsertHabit;
using EverydayHabit.Application.Habits.Queries.GetHabitDetail;
using EverydayHabit.Application.Habits.Queries.GetHabitDetail.Dtos;
using EverydayHabit.Application.HabitVariations.Queries.GetHabitVariation;
using EverydayHabit.Domain.Enums;
using EverydayHabit.XamarinApp.Common.Converters;
using EverydayHabit.XamarinApp.Common.Entities;
using EverydayHabit.XamarinApp.Common.ViewModels;
using EverydayHabit.XamarinApp.Features.Habits.HabitVariationPage;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace EverydayHabit.XamarinApp.Features.Habits.HabitPage
{
    public class HabitPageViewModel : BasePageViewModel
    {
        public ICommand OnSaveCommand => new Command(async () => await OnSave());
        public ICommand OnDeleteCommand => new Command(async () => await OnDelete());
        public ICommand OnAddVariationCommand => new Command(async () => await OnAddVariation());
        public ICommand OnVariationListItemSelectedCommand => new Command(async (item) => await OnVariationListItemSelected(item));

        public HabitDetailVm HabitItem { get; set; }
        public ObservableCollection<ItemWithIcon> HabitList { get; set; }

        public HabitPageViewModel()
        {
            if(HabitItem?.Id > 0)
            {
                SelectedHabitType = PickerHabitTypes.SingleOrDefault(ht => ht.Id == (int) HabitItem.HabitType) ?? PickerHabitTypes.First();
            }
        }

        public async Task OnSave()
        {
            await UpsertHabitAsync();
            UpsertHabitList();

            MessagingCenter.Send(this, "HabitUpserted", HabitItem.Id);

            await Xamarin.Forms.Application.Current.MainPage.Navigation.PopAsync();
        }

        public async Task OnDelete()
        {
            if (HabitItem != null && HabitItem.Id > 0)
            {
                var isConfirmed = await App.Current.MainPage.DisplayAlert("Delete habit?", $"Are you sure you want to delete \"{HabitItem.Name}\"?", "Yes", "No");
                if(isConfirmed)
                {
                    await Mediator.Send(new DeleteHabitCommand { Id = HabitItem.Id });

                    MessagingCenter.Send(this, "HabitDeleted", HabitItem.Id);

                    var habitInList = HabitList?.FirstOrDefault(habit => habit.Id == HabitItem.Id);
                    if (habitInList != null)
                    {
                        HabitList.Remove(habitInList);
                    }

                    await Xamarin.Forms.Application.Current.MainPage.Navigation.PopAsync();
                }
            }
        }

        public async Task OnAddVariation()
        {
            await UpsertHabitAsync();
            UpsertHabitList();

            await Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new HabitVariationPageView
            {
                BindingContext = new HabitVariationPageViewModel
                {
                    HabitVariation = new HabitVariationDetailVm
                    {
                        HabitId = HabitItem.Id,
                    },
                    HabitList = HabitList
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
                    HabitType = (HabitType) SelectedHabitType.Id
                });

                HabitItem.Id = habitId;
            }
        }

        private void UpsertHabitList()
        {
            if (HabitItem != null && HabitList != null)
            {
                var habitInList = HabitList.FirstOrDefault(habit => habit.Id == HabitItem.Id);

                var newHabitItem = new ItemWithIcon
                {
                    Id = HabitItem.Id,
                    Name = HabitItem.Name,
                    Icon = HabitTypeToIconConverter.ConvertToIcon((HabitType)SelectedHabitType.Id)
                };

                if (habitInList != null)
                {
                    HabitList[HabitList.IndexOf(habitInList)] = newHabitItem;
                }
                else
                {
                    HabitList.Add(newHabitItem);
                }
            }
        }

        private async Task OnVariationListItemSelected(object item)
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
                        HabitList = HabitList,
                        Mini = mini,
                        Plus = plus,
                        Elite = elite
                    }
                });
            }
        }

        private static ObservableCollection<ItemWithIcon> GetHabitTypes()
        {
            var habitTypes = new ObservableCollection<ItemWithIcon>();
            foreach (HabitType habitType in Enum.GetValues(typeof(HabitType)))
            {
                habitTypes.Add(new ItemWithIcon
                {
                    Id = (int) habitType,
                    Name = EnumToFormattedStringConverter.Convert(habitType),
                    Icon = HabitTypeToIconConverter.ConvertToIcon(habitType)
                });
            }

            return habitTypes;
        }

        private ObservableCollection<ItemWithIcon> _pickerHabitTypes = GetHabitTypes();
        
        public bool _isDeletePossible = false;
        public bool IsDeletePossible
        {
            get => HabitItem?.Id > 0 ? true : false;
            set => SetProperty(ref _isDeletePossible, value);
        }
        public ObservableCollection<ItemWithIcon> PickerHabitTypes
        {
            get => _pickerHabitTypes;
            set => SetProperty(ref _pickerHabitTypes, value);
        }

        private ItemWithIcon _selectedHabitType = GetHabitTypes().First();

        public ItemWithIcon SelectedHabitType
        {
            get => _selectedHabitType;
            set => SetProperty(ref _selectedHabitType, value);
        }
    }
}
