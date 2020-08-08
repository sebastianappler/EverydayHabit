using EverydayHabit.XamarinApp.Common.Themes;
using EverydayHabit.XamarinApp.Common.ViewModels;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace EverydayHabit.XamarinApp.Features.Settings
{
    public class SettingsViewModel : BasePageViewModel
    {
        public ICommand DarkModeSwitchedCommand => new Command(() => SetThemeFromDarkModeSwitch());
        public ICommand SundayStartOfWeekSwitchedCommand => new Command(async () => await SetStartDayOfWeek());
        public const string DARK_MODE_ENABLED = "DarkModeEnabled";
        public const string SUNDAY_START_OF_WEEK = "SundayStartOfWeek";
        public SettingsViewModel()
        {
            InitDarkMode();
            InitStartDayOfWeek();
        }

        private void InitDarkMode()
        {
            if (App.Current.Properties.ContainsKey(DARK_MODE_ENABLED))
            {
                var darkModeEnabled = App.Current.Properties[DARK_MODE_ENABLED] as bool?;

                DarkModeEnabled = darkModeEnabled ?? false;
                SetThemeFromDarkModeSwitch();
            }
        }
        private void InitStartDayOfWeek()
        {
            if (App.Current.Properties.ContainsKey(SUNDAY_START_OF_WEEK))
            {
                var isSundayStartOfWeek = App.Current.Properties[SUNDAY_START_OF_WEEK] as bool?;
                IsSundayStartOfWeek = isSundayStartOfWeek ?? false;
            }
        }

        public async Task SetStartDayOfWeek()
        {
            App.Current.Properties[SUNDAY_START_OF_WEEK] = IsSundayStartOfWeek;
            
            var selectedAutomaticRestart = await App.Current.MainPage.DisplayAlert("Requires restart","You need to restart the app for the changes to take effect.", "Restart", "Cancel");
            if (selectedAutomaticRestart)
            {
               Startup.RestartApp();
            }
        }
        
        public void SetThemeFromDarkModeSwitch()
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

                App.Current.Properties[DARK_MODE_ENABLED] = DarkModeEnabled;
            }
        }

        private bool _isSundayStartOfWeek = false;
        public bool IsSundayStartOfWeek
        {
            get => _isSundayStartOfWeek;
            set => SetProperty(ref _isSundayStartOfWeek, value);
        }

        private bool _darkModeEnabled = false;
        public bool DarkModeEnabled
        {
            get => _darkModeEnabled;
            set => SetProperty(ref _darkModeEnabled, value);
        }
    }
}
