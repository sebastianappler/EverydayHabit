using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EverydayHabit.XamarinApp.Features.Calendar.HabitCalendar
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HabitCalendarView : ContentPage
    {
        public HabitCalendarView()
        {
            var minScreenHeight = 2000;
            var isSmallDisplay = DeviceDisplay.MainDisplayInfo.Height < minScreenHeight;
            if (isSmallDisplay)
            {
                NavigationPage.SetHasNavigationBar(this, false);
            }

            InitializeComponent();
        }
    }
}