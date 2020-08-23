using Android.Content;
using Android.OS;
using Android.Provider;
using EverydayHabit.XamarinApp.Common.Services;
using System.IO;
using Xamarin.Essentials;
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