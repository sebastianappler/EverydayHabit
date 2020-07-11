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
            InitializeComponent();
            var nav = new NavigationBar(new MainPage());
            Current.Resources.TryGetValue("PageBackgroundColor", out var pageBackgroundColor);
            nav.BackgroundColor = (Color) pageBackgroundColor;
            
            MainPage = nav;
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