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

            if (App.Current.Properties.ContainsKey("DarkModeEnabled"))
            {
                var darkModeEnabled = App.Current.Properties["DarkModeEnabled"] as bool?;

                DarkModeEnabled = darkModeEnabled ?? false;
                DarkModeSwitched();
            }
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

                App.Current.Properties["DarkModeEnabled"] = DarkModeEnabled;
            }
        }

        private bool _darkModeEnabled = false;
        public bool DarkModeEnabled
        {
            get => _darkModeEnabled;
            set => SetProperty(ref _darkModeEnabled, value);
        }
    }
}
