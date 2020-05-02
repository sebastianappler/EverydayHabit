using EverydayHabit.Application.Habits.Queries.GetHabitsListQuery;
using EverydayHabit.XamarinApp.Common.Views;
using System.Threading;

namespace Xamarin.HabitList
{
    public partial class HabitList : BaseView 
    {
        public HabitList()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var vm = await Mediator.Send(new GetHabitsListQuery(), CancellationToken.None);
            listView.ItemsSource = vm.Habits;
        }
    }
}