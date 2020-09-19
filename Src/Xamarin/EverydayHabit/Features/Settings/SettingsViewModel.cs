using EverydayHabit.XamarinApp.Common.Services;
using EverydayHabit.XamarinApp.Common.Themes;
using EverydayHabit.XamarinApp.Common.ViewModels;
using Plugin.FilePicker;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace EverydayHabit.XamarinApp.Features.Settings
{
    public class SettingsViewModel : BasePageViewModel
    {
        public ICommand DarkModeSwitchedCommand => new Command(() => SetThemeFromDarkModeSwitch());
        public ICommand SundayStartOfWeekSwitchedCommand => new Command(async () => await SetStartDayOfWeek());
        public ICommand ExportBackupCommand => new Command(async () => await ExportBackup());
        public ICommand ImportBackupCommand => new Command(async () => await ImportBackup());

        public const string DARK_MODE_ENABLED = "DarkModeEnabled";
        public const string SUNDAY_START_OF_WEEK = "SundayStartOfWeek";
        public SettingsViewModel()
        {
            InitDarkMode();
            InitStartDayOfWeek();
        }

        public async Task ExportBackup()
        {
            try
            {
                var userWantsToExport = await App.Current.MainPage.DisplayAlert("Export",
                $"Do you want to export your data?",
                "Export data", "Cancel");

                if (!userWantsToExport)
                    return;

                if (await RequestStoragePermission() != PermissionStatus.Granted)
                    return;

                var storageService = DependencyService.Get<IDeviceStorageService>();
                var downloadPath = storageService.GetDefaultDownloadPath();
                var backupFileName = $"everyday_habit_backup_{DateTime.Now:yyyy-MM-dd-HHmmss}.sqlite";
                var exportFullPath = Path.Combine(downloadPath, backupFileName);
                var dbPath = Preferences.Get("dbPath", string.Empty).ToString(); ;

                if (string.IsNullOrEmpty(dbPath))
                {
                    await App.Current.MainPage.DisplayAlert("Export failed", $"Chould not get path for database.", "Ok");
                    return;
                }

                if (string.IsNullOrEmpty(downloadPath))
                {
                    await App.Current.MainPage.DisplayAlert("Export failed", $"Chould not get donwload path.", "Ok");
                    return;
                }

                File.Copy(dbPath, exportFullPath, overwrite: true);
                await App.Current.MainPage.DisplayAlert("Export success", 
                    $"Backup successfully exported to Downloads folder.\n\n" +
                    $"({exportFullPath})", "Ok");
            }
            catch(Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "Ok");
            }
        }
        
        public async Task ImportBackup()
        {
            try
            {
                var userWantsToImport = await App.Current.MainPage.DisplayAlert("Import",
                $"Import will overwrite all current data.\n" +
                $"Make sure to backup your data before importing.",
                "Select file to import", "Cancel");

                if (!userWantsToImport)
                    return;

                if (await RequestStoragePermission() != PermissionStatus.Granted)
                    return;

                var fileData = await CrossFilePicker.Current.PickFile();
                if (fileData == null)
                    return; // user canceled file picking

                var fileContent = System.Text.Encoding.UTF8.GetString(fileData.DataArray); ;

                if (IsValidDatabase(fileContent))
                {
                    var userConfirmedImport = await App.Current.MainPage.DisplayAlert(
                        "Import?",
                        $"All current data will be overwritten with data from the selected file.\n\n" +
                        $"Selected file:\n" +
                        $"{fileData.FileName}",
                        $"Start the import", "Cancel");

                    if (!userConfirmedImport)
                        return;

                    var selectedDbPath = fileData.FilePath;
                    var dbPath = Preferences.Get("dbPath", string.Empty).ToString(); ;

                    File.Copy(selectedDbPath, dbPath, overwrite: true);
                    await App.Current.MainPage.DisplayAlert("Import success", $"Backup successfully Imported. The app will now restart.", "Ok");
                    Startup.RestartApp();
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Import failed", $"File is not a valid database.", "Ok");
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "Ok");
            }
        }

        private async Task<PermissionStatus> RequestStoragePermission()
        {
            var status = await Permissions.CheckStatusAsync<Permissions.StorageWrite>();
            if (status != PermissionStatus.Granted)
            {
                var userAcceptedRequest = await App.Current.MainPage.DisplayAlert("Storage permission", $"The app will now ask for storage permission to perform your request.", "Ok", "Cancel");
                if (!userAcceptedRequest)
                    return status;

                status = await Permissions.RequestAsync<Permissions.StorageWrite>();
            }

            return status;
        }
        

        private bool IsValidDatabase(string databaseContent)
        {
            return 
            databaseContent.StartsWith("SQLite format 3") &&
            databaseContent.Contains("CREATE TABLE \"Habits\"") &&
            databaseContent.Contains("CREATE TABLE \"HabitCompletions\"") &&
            databaseContent.Contains("CREATE TABLE \"HabitVariations\"") &&
            databaseContent.Contains("CREATE TABLE \"HabitDifficulties\"");
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

        private async Task SetStartDayOfWeek()
        {
            App.Current.Properties[SUNDAY_START_OF_WEEK] = IsSundayStartOfWeek;
            
            var selectedAutomaticRestart = await App.Current.MainPage.DisplayAlert("Requires restart","You need to restart the app for the changes to take effect.", "Restart", "Cancel");
            if (selectedAutomaticRestart)
            {
               Startup.RestartApp();
            }
        }
        
        private void SetThemeFromDarkModeSwitch()
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
