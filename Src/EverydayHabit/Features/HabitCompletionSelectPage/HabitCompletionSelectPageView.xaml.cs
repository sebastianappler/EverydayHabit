using EverydayHabit.XamarinApp.Features.HabitCalendar;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EverydayHabit.XamarinApp.Features.HabitCompletionSelectPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HabitCompletionSelectPageView : ContentPage
    {
        public HabitCompletionSelectPageView()
        {
            InitializeComponent();
        }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null) return;

            if (sender is ListView lv) lv.SelectedItem = null;
        }
    }
}