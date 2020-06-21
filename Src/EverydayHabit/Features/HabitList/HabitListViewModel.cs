using EverydayHabit.Application.Habits.Queries.GetHabitDetail;
using EverydayHabit.Application.Habits.Queries.GetHabitsList;
using EverydayHabit.XamarinApp.Common.Components;
using EverydayHabit.XamarinApp.Common.Converters;
using EverydayHabit.XamarinApp.Common.ViewModels;
using EverydayHabit.XamarinApp.Features.HabitPage;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace EverydayHabit.XamarinApp.Features.HabitList
{
    public class HabitListViewModel : BasePageViewModel
    {
        public ICommand OnListItemSelected => new Command(async (item) => await OnListItemSelectedCommand(item));
        public ICommand AddHabit_Clicked => new Command(async () => await AddHabit_ClickedCommand());


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

        private async Task OnListItemSelectedCommand(object item)
        {
            if (item is ItemWithIconCellViewModel selectedHabit)
            {
                var vm = await Mediator.Send(new GetHabitDetailQuery { Id = selectedHabit.Id }, CancellationToken.None);

                await Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new HabitPageView
                {
                    BindingContext = new HabitPageViewModel
                    {
                        HabitItem = vm as HabitDetailVm
                    }
                });
            }
        }

        public async Task AddHabit_ClickedCommand()
        {
            await Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new HabitPageView
            {
                BindingContext = new HabitPageViewModel
                {
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
