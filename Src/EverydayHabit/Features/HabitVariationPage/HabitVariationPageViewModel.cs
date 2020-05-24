using Application.Habits.Commands.CreateHabit;
using EverydayHabit.Application.Habits.Queries.GetHabitDetail;
using EverydayHabit.Application.HabitVariations.Queries.GetHabitVariation;
using EverydayHabit.XamarinApp.Common.ViewModels;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace EverydayHabit.XamarinApp.Features.VariationPage
{
    public class HabitVariationPageViewModel : BasePageViewModel
    {
        public ICommand OnSaveClicked => new Command(async () => await OnSaveClickedCommand());
        public ICommand OnDeleteClicked => new Command(async () => await OnDeleteClickedCommand());

        public HabitVariationDetailVm HabitVariation { get; set; }

        public async Task OnSaveClickedCommand()
        {
            if (HabitVariation != null)
            {
                if (HabitVariation.Id != 0)
                {
                    //await Mediator.Send(new UpdateHabitVariationCommand
                    //{
                    //    Id = HabitItem.Id,
                    //    Name = HabitItem.Name,
                    //    Description = HabitItem.Description,
                    //});
                }
                else
                {
                    await Mediator.Send(new CreateHabitVariationCommand
                    {
                        Name = HabitVariation.Name,
                    });
                }
            }

            Xamarin.Forms.Application.Current.MainPage = new NavigationPage(new MainPage());
        }

        public async Task OnDeleteClickedCommand()
        {
            if (HabitVariation != null)
            {
                //await Mediator.Send(new DeleteHabitCommand { Id = HabitItem.Id });
                //Xamarin.Forms.Application.Current.MainPage = new NavigationPage(new MainPage());
            }
        }


    }
}
