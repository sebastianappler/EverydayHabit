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
            var downloadPath = Path.Combine(Environment.ExternalStorageDirectory.AbsolutePath, Environment.DirectoryDownloads);
            return downloadPath;
        }
    }
}