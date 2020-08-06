using EverydayHabit.Application.HabitCompletions.Commands.UpsertHabitCompletion;
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
        public ICommand SetMiniCompletedCommand => new Command(async (item) => await CompleteHabit(item, HabitDifficultyLevel.Mini));
        public ICommand SetPlusCompletedCommand => new Command(async (item) => await CompleteHabit(item, HabitDifficultyLevel.Plus));
        public ICommand SetEliteCompletedCommand => new Command(async (item) => await CompleteHabit(item, HabitDifficultyLevel.Elite));

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

        public async Task CompleteHabit(object item, HabitDifficultyLevel selectedDifficulty)
        {
            if (item is ItemWithIcon selectedHabit)
            {
                var habit = await Mediator.Send(new GetHabitDetailQuery { Id = selectedHabit.Id }, CancellationToken.None);

                var completionId = selectedHabit.HabitCompletionId ?? 0;
                var variationId = selectedHabit.CompletedVariationId ?? habit.VariationsList.First().Id; //TODO Get default variation for user
                var difficultyId = habit.VariationsList
                        .FirstOrDefault(v => v.Id == variationId).DifficultiesList
                        .FirstOrDefault(hd => hd.DifficultyLevel == selectedDifficulty).Id;

                completionId = await Mediator.Send(new UpsertHabitCompletionCommand
                {
                    Id = completionId,
                    HabitId = selectedHabit.Id,
                    Date = DateTime.Now,
                    HabitVariationId = variationId,
                    HabitDifficultyId = difficultyId,
                });

                var habitToUpdate = HabitList.FirstOrDefault(h => h.Id == selectedHabit.Id);
                habitToUpdate.HabitCompletionId = completionId;
                habitToUpdate.CompletedDifficulty = selectedDifficulty;
                habitToUpdate.CompletedVariationId = variationId;
                habitToUpdate.CompletedDifficultyId = difficultyId;

                HabitList[HabitList.IndexOf(habitToUpdate)] = habitToUpdate;
            }
        }

        private ObservableCollection<ItemWithIcon> _habitList = new ObservableCollection<ItemWithIcon>();
        public ObservableCollection<ItemWithIcon> HabitList
        {
            get => _habitList;
            set => SetProperty(ref _habitList, value);
        }
    }
}
