using EverydayHabit.Application.Habits.Queries.GetHabitDetail;
using EverydayHabit.Application.Habits.Queries.GetHabitsList;
using EverydayHabit.XamarinApp.Common.Views;
using System.Threading;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EverydayHabit.XamarinApp.Features
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
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

        async void OnListItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {

                var selectedHabit = e.SelectedItem as HabitListDto;

                var vm = await Mediator.Send(new GetHabitDetailQuery { Id = selectedHabit.Id}, CancellationToken.None);

                await Navigation.PushAsync(new HabitPage
                {
                    BindingContext = vm as HabitDetailVm
                });
            }
        }

        async void AddHabit_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new HabitPage
            {
                BindingContext = new HabitDetailVm()
            });
        }
    }
}