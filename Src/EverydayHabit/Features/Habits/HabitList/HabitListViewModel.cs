using EverydayHabit.Application.Habits.Queries.GetHabitDetail;
using EverydayHabit.Application.Habits.Queries.GetHabitsList;
using EverydayHabit.Domain.Enums;
using EverydayHabit.XamarinApp.Common.Components;
using EverydayHabit.XamarinApp.Common.Converters;
using EverydayHabit.XamarinApp.Common.ViewModels;
using EverydayHabit.XamarinApp.Features.Habits.HabitPage;
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

        public HabitListViewModel()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                var vm = await Mediator.Send(new GetHabitsListQuery(), CancellationToken.None);
                var habitList = vm.Habits.Select(habit => new ItemWithIconCellViewModel
                {
                    Id = habit.Id,
                    Name = habit.Name,
                    Icon = HabitTypeToIconConverter.Convert(habit.HabitType)
                });

                HabitList = new ObservableCollection<ItemWithIconCellViewModel>(habitList);
            });
        }

        private async Task OnListItemSelected(object item)
        {
            if (item is ItemWithIconCellViewModel selectedHabit)
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

        private ObservableCollection<ItemWithIconCellViewModel> _habitList = new ObservableCollection<ItemWithIconCellViewModel>();
        public ObservableCollection<ItemWithIconCellViewModel> HabitList
        {
            get => _habitList;
            set => SetProperty(ref _habitList, value);
        }
    }
}
