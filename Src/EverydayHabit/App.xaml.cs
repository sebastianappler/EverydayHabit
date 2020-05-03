using Application.Habits.Commands.CreateHabit;
using EverydayHabit.Persistence;
using EverydayHabit.XamarinApp;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace EverydayHabit
{
    public partial class App : Xamarin.Forms.Application
    {
        public App()
        {
            InitializeComponent();
            var nav = new NavigationPage(new MainPage());
            MainPage = nav;

            Startup.Init();

            //var services = Startup.ServiceProvider;

            //var context = services.GetRequiredService<EverydayHabitDbContext>();
            //context.Database.Migrate();
            //var mediator = services.GetRequiredService<IMediator>();

            //var habitId = Task.Run(async () => await mediator.Send(new CreateHabitCommand { Name = "Training" }, CancellationToken.None))
            //    .Result;

            //var createdHabit = context.Habits.Find(habitId);
        }
        
        protected override void OnStart()
        {

        }

        protected override void OnSleep()
        {

        }

        protected override void OnResume()
        {

        }
    }
}