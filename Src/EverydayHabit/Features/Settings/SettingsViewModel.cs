using EverydayHabit.XamarinApp.Common.Themes;
using EverydayHabit.XamarinApp.Common.ViewModels;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace EverydayHabit.XamarinApp.Features.Settings
{
    public class SettingsViewModel : BasePageViewModel
    {
        public ICommand DarkModeSwitchedCommand => new Command(() => DarkModeSwitched());


        public SettingsViewModel()
        {

        }
        public void DarkModeSwitched()
        {
            var mergedDictionaries = App.Current.Resources.MergedDictionaries;
            if (mergedDictionaries != null)
            {
                mergedDictionaries.Clear();

                if (DarkModeEnabled)
                {
                    mergedDictionaries.Add(new DarkTheme());
                }
                else
                {
                    mergedDictionaries.Add(new LightTheme());
                }
            }
        }

        private bool _darkModeEnabled = false;
        public bool DarkModeEnabled
        {
            get => _darkModeEnabled;
            set => SetProperty(ref _darkModeEnabled, value);
        }

        private Switch _darkModeSwitch = new Switch();
        public Switch DarkModeSwitch
        {
            get => _darkModeSwitch;
            set => SetProperty(ref _darkModeSwitch, value);
        }
    }
}
