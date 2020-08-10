using EverydayHabit.XamarinApp;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace EverydayHabit
{
    public partial class App : Xamarin.Forms.Application
    {
        public App()
        {
            Device.SetFlags(new string[] { "RadioButton_Experimental" });
            InitializeComponent();
            var mainPage = Startup.GenerateMainPage();
            MainPage = mainPage;
            Startup.Init();
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