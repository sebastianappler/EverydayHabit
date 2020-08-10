using Application.Habits.Commands.CreateHabit;
using EverydayHabit.Application.HabitCompletions.Commands.UpsertHabitCompletion;
using EverydayHabit.Application.HabitCompletions.Queries.GetHabitCompletionsList.Dtos;
using EverydayHabit.Application.Habits.Queries.GetHabitDetail;
using EverydayHabit.Application.Habits.Queries.GetHabitsList;
using EverydayHabit.Domain.Enums;
using EverydayHabit.XamarinApp.Common.Converters;
using EverydayHabit.XamarinApp.Common.Entities;
using EverydayHabit.XamarinApp.Common.ViewModels;
using EverydayHabit.XamarinApp.Features.Habits.HabitPage;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace EverydayHabit.XamarinApp.Features.Habits.HabitList
{
    public class HabitListViewModel : BasePageViewModel, INotifyPropertyChanged
    {
        public ICommand OnListItemSelectedCommand => new Command(async (item) => await OnListItemSelected(item));
        public ICommand AddHabitCommand => new Command(async () => await AddHabit());
        public ICommand SetMiniCompletedCommand => new Command(async (item) => await ToggleCompletedHabit(item, HabitDifficultyLevel.Mini));
        public ICommand SetPlusCompletedCommand => new Command(async (item) => await ToggleCompletedHabit(item, HabitDifficultyLevel.Plus));
        public ICommand SetEliteCompletedCommand => new Command(async (item) => await ToggleCompletedHabit(item, HabitDifficultyLevel.Elite));

        public HabitListViewModel()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                var vm = await Mediator.Send(new GetHabitsListQuery(), CancellationToken.None);
                var habitList = new ObservableCollection<ItemWithIcon>();

                foreach(var habit in vm.Habits)
                {
                    var today = DateTime.Now.Date;
                    var habitCompletionToday = await Mediator.Send(new GetHabitCompletionsListQuery
                    {
                        HabitId = habit.Id,
                        FromDate = today
                    });

                    var habitCompletion = habitCompletionToday.HabitCompletionsList.LastOrDefault();

                    habitList.Add(new ItemWithIcon
                    {
                        Id = habit.Id,
                        Name = habit.Name,
                        Icon = HabitTypeToIconConverter.ConvertToIcon(habit.HabitType),
                        HabitCompletionId = habitCompletion?.Id,
                        CompletedVariationId = habitCompletion?.HabitVariation.Id,
                        CompletedDifficultyId = habitCompletion?.HabitDifficulty.Id,
                        CompletedDifficulty = habitCompletion?.HabitDifficulty.DifficultyLevel ?? HabitDifficultyLevel.None
                    });
                }

                HabitList = new ObservableCollection<ItemWithIcon>(habitList);

                MessagingCenter.Subscribe<HabitPageViewModel, int>(this, "HabitUpserted", async (sender, habitId) =>
                {
                    await UpsertHabitInList(habitId);
                });

                MessagingCenter.Subscribe<HabitPageViewModel, int>(this, "HabitDeleted", async (sender, habitId) =>
                {

                });

            });
        }

        private async Task OnListItemSelected(object item)
        {
            if (item is ItemWithIcon selectedHabit)
            {
                var vm = await Mediator.Send(new GetHabitDetailQuery { Id = selectedHabit.Id }, CancellationToken.None);

                await Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new HabitPageView
                {
                    BindingContext = new HabitPageViewModel
                    {
                        HabitList = HabitList,
                        HabitItem = vm as HabitDetailVm
                    }
                });
            }
        }

        public async Task AddHabit()
        {
            await Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new HabitPageView
            {
                BindingContext = new HabitPageViewModel
                {
                    HabitList = HabitList,
                    HabitItem = new HabitDetailVm()
                }
            });
        }

        public async Task ToggleCompletedHabit(object item, HabitDifficultyLevel selectedDifficulty)
        {
            if (item is ItemWithIcon selectedHabit)
            {
                if(selectedHabit.CompletedDifficulty == selectedDifficulty)
                {
                    await Mediator.Send(new DeleteHabitCompletionCommand { Id = (int) selectedHabit.HabitCompletionId });
                    var indexOfHabit = HabitList.IndexOf(selectedHabit);
                    selectedHabit.HabitCompletionId = null;
                    selectedHabit.CompletedDifficultyId = null;
                    selectedHabit.CompletedDifficulty = HabitDifficultyLevel.None;

                    HabitList[indexOfHabit] = selectedHabit;
                    MessagingCenter.Send(this, "CompletionChanged", selectedHabit.Id);
                    return;
                }

                var habit = await Mediator.Send(new GetHabitDetailQuery { Id = selectedHabit.Id }, CancellationToken.None);
                var variationId = 
                    //TODO Get default variation for user
                    habit.VariationsList.Where(v => v.DifficultiesList
                        .Any(v => v.DifficultyLevel == selectedDifficulty && !string.IsNullOrEmpty(v.Description)))
                    .FirstOrDefault()?.Id; 

                if (variationId == null)
                {
                    await App.Current.MainPage.DisplayAlert("No variation", $"No variation {selectedDifficulty} for {habit.Name}", "Ok");
                    return;
                }

                var completionId = selectedHabit.HabitCompletionId ?? 0;
                var difficultyId = habit.VariationsList
                        .FirstOrDefault(v => v.Id == variationId).DifficultiesList
                        .FirstOrDefault(hd => hd.DifficultyLevel == selectedDifficulty).Id;
               
                completionId = await Mediator.Send(new UpsertHabitCompletionCommand
                {
                    Id = completionId,
                    HabitId = selectedHabit.Id,
                    Date = DateTime.Now,
                    HabitVariationId = (int)variationId,
                    HabitDifficultyId = difficultyId,
                });

                await UpsertHabitInList(selectedHabit.Id);

                MessagingCenter.Send(this, "CompletionChanged", selectedHabit.Id);
            }
        }

        public async Task UpsertHabitInList(int habitId)
        {
            var habit = await Mediator.Send(new GetHabitDetailQuery { Id = habitId });

            var isHabitInList = HabitList.Any(h => h.Id == habitId);
            if (isHabitInList)
            {
                var habitToUpdate = HabitList.FirstOrDefault(h => h.Id == habitId);
                var habitCompletion = await GetTodaysCompletion(habitId);

                habitToUpdate.Name = habit.Name;
                habitToUpdate.Icon = HabitTypeToIconConverter.ConvertToIcon(habit.HabitType);
                habitToUpdate.HabitCompletionId = habitCompletion?.Id;
                habitToUpdate.CompletedVariationId = habitCompletion?.HabitVariation.Id;
                habitToUpdate.CompletedDifficultyId = habitCompletion?.HabitDifficulty.Id;
                habitToUpdate.CompletedDifficulty = habitCompletion?.HabitDifficulty.DifficultyLevel ?? HabitDifficultyLevel.None;

                HabitList[HabitList.IndexOf(habitToUpdate)] = habitToUpdate;
            }
            else
            {
                var habitCompletion = await GetTodaysCompletion(habitId);
                HabitList.Add(new ItemWithIcon
                {
                    Id = habitId,
                    Name = habit.Name,
                    Icon = HabitTypeToIconConverter.ConvertToIcon(habit.HabitType),
                    HabitCompletionId = habitCompletion?.Id,
                    CompletedVariationId = habitCompletion?.HabitVariation.Id,
                    CompletedDifficultyId = habitCompletion?.HabitDifficulty.Id,
                    CompletedDifficulty = habitCompletion?.HabitDifficulty.DifficultyLevel ?? HabitDifficultyLevel.None
                });
            }
        }

        public async Task<HabitCompletionsListDto> GetTodaysCompletion(int habitId)
        {
            var today = DateTime.Now.Date;
            var habitCompletionToday = await Mediator.Send(new GetHabitCompletionsListQuery
            {
                HabitId = habitId,
                FromDate = today
            });

            var habitCompletion = habitCompletionToday.HabitCompletionsList.LastOrDefault();
            return habitCompletion;
        }

        private ObservableCollection<ItemWithIcon> _habitList = new ObservableCollection<ItemWithIcon>();
        public ObservableCollection<ItemWithIcon> HabitList
        {
            get => _habitList;
            set => SetProperty(ref _habitList, value);
        }
    }
}
