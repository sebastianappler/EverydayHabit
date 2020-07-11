using Android.App;
using Android.Content.PM;
using Android.OS;
using EverydayHabit.Android;

namespace EverydayHabit
{
    [Activity(Theme = "@style/MainTheme.Splash", MainLauncher = true,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            SetTheme(Resource.Style.MainTheme);

            base.OnCreate(bundle);
            global::Xamarin.Forms.Forms.Init(this, bundle);
            global::Xamarin.Forms.FormsMaterial.Init(this, bundle);

            LoadApplication(new App());
        }
    }
}