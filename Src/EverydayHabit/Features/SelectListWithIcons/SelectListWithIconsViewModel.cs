using EverydayHabit.XamarinApp.Common.ViewModels;
using EverydayHabit.XamarinApp.Common.Views;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace EverydayHabit.XamarinApp.Features.SelectListWithIcons
{
    public class SelectListWithIconsViewModel : BasePageViewModel
    {
        public ICommand OnListItemSelected => new Command(async (item) => await OnListItemSelectedCommand(item));

        public SelectListWithIconsViewModel()
        {
        }

        private async Task OnListItemSelectedCommand(object item)
        {
            if (item is ItemWithIconModel selectedItem)
            {
                await Xamarin.Forms.Application.Current.MainPage.Navigation.PopModalAsync();
            }
        }

        private ObservableCollection<ItemWithIconModel> _selectList = new ObservableCollection<ItemWithIconModel>();
        public ObservableCollection<ItemWithIconModel> SelectList
        {
            get => _selectList;
            set => SetProperty(ref _selectList, value);
        }
    }
}
