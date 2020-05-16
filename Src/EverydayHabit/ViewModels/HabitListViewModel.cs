using EverydayHabit.Application.Habits.Queries.GetHabitDetail;
using EverydayHabit.Application.Habits.Queries.GetHabitsList;
using EverydayHabit.XamarinApp.Features;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace EverydayHabit.XamarinApp.ViewModels
{
    public class HabitListViewModel : BasePageViewModel 
    {
        public ICommand OnListItemSelected => new Command(async (item) => await OnListItemSelectedCommand(item));
        public ICommand AddHabit_Clicked => new Command(async () => await AddHabit_ClickedCommand());
        
      
        public HabitListViewModel()
        {
            Device.BeginInvokeOnMainThread(async () => {
                var vm = await Mediator.Send(new GetHabitsListQuery(), CancellationToken.None);
                HabitList = new ObservableCollection<HabitListDto>(vm.Habits);
            });
        }

        private async Task OnListItemSelectedCommand(object item)
        {
            if (item is HabitListDto selectedHabit)
            {
                var vm = await Mediator.Send(new GetHabitDetailQuery { Id = selectedHabit.Id }, CancellationToken.None);

                await Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new HabitPage
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
            await Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new HabitPage
            {
                BindingContext = new HabitPageViewModel { 
                    HabitItem = new HabitDetailVm() 
                }
            });
        }

        private ObservableCollection<HabitListDto> _habitList = new ObservableCollection<HabitListDto>();
        public ObservableCollection<HabitListDto> HabitList
        {
            get => _habitList;
            set => SetProperty(ref _habitList, value);
        }
    }
}
