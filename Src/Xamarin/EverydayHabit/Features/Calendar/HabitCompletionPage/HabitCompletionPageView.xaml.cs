using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EverydayHabit.XamarinApp.Features.Calendar.HabitCompletionPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HabitCompletionPageView : ContentPage
    {
        public HabitCompletionPageView()
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