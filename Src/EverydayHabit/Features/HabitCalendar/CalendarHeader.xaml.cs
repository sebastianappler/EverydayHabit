using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EverydayHabit.XamarinApp.Features.HabitCalendar
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CalendarHeader : DataTemplate
    {
        public CalendarHeader()
        {
            InitializeComponent();
        }
    }
}