using Xamarin.Forms;

namespace EverydayHabit.XamarinApp.Features.SelectListWithIcons
{
    public partial class SelectListWithIconsView : ContentPage
    {
        public SelectListWithIconsView()
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