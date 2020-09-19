using Android.OS;
using EverydayHabit.XamarinApp.Common.Services;
using System.IO;
using Xamarin.Forms;

[assembly: Dependency(typeof(EverydayHabit.Android.DeviceStorageService))]

namespace EverydayHabit.Android
{
    public class DeviceStorageService : IDeviceStorageService
    {
        public string GetDefaultDownloadPath()
        {
#pragma warning disable CS0618 // Type or member is obsolete
            var downloadPath = Path.Combine(Environment.ExternalStorageDirectory.AbsolutePath, Environment.DirectoryDownloads);
#pragma warning restore CS0618 // Type or member is obsolete

            return downloadPath;
        }

        public string GetDatabasePath()
        {
            var dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "EverydayHabitDatabase.db");
            return dbPath;
        }
    }
}