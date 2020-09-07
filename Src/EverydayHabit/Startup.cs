using EverydayHabit.Application;
using EverydayHabit.Infrastructure;
using EverydayHabit.Persistence;
using EverydayHabit.XamarinApp;
using EverydayHabit.XamarinApp.Common.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Reflection;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace EverydayHabit
{
    public class Startup
    {
        public static IServiceProvider ServiceProvider { get; set; }

        public static void Init()
        {
            var dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "EverydayHabitDatabase.db");
            Preferences.Set("dbPath", dbPath);

            var assembly = Assembly.Load("EverydayHabit.Android");
            using var stream = assembly.GetManifestResourceStream("EverydayHabit.Android.appsettings.json");

            var host = new HostBuilder()
                .ConfigureHostConfiguration(configBuilder =>
                {
                    // Tell the host configuration where to file the file (this is required for Xamarin apps)
                    configBuilder.AddCommandLine(new string[] { $"ContentRoot={FileSystem.AppDataDirectory}" });

                    //read in the configuration file!
                    configBuilder.AddJsonStream(stream);
                })
                .ConfigureServices((context, service) =>
                {
                    // Configure our local services and access the host configuration
                    ConfigureServices(context, service);
                })
                .ConfigureLogging(loggiingBuilder => loggiingBuilder.AddConsole(loggerOptions =>
                {
                    //setup a console logger and disable colors since they don't have any colors in VS
                    loggerOptions.DisableColors = true;
                }))
                .Build();

            //Save our service provider so we can use it later.
            ServiceProvider = host.Services;

            var context = ServiceProvider.GetRequiredService<EverydayHabitDbContext>();
            context.Database.Migrate();
        }

        public static NavigationBar GenerateMainPage()
        {
            var mainPage = new NavigationBar(new MainPage());
            App.Current.Resources.TryGetValue("PageBackgroundColor", out var pageBackgroundColor);
            mainPage.BackgroundColor = (Color) pageBackgroundColor;

            return mainPage;
        }

        public static void RestartApp()
        {
            App.Current.MainPage = Startup.GenerateMainPage();
        }

        public static void ConfigureServices(HostBuilderContext ctx, IServiceCollection services)
        {
            services.AddApplication();
            services.AddInfrasctructure(ctx.Configuration);
            services.AddPersistence(ctx.Configuration);
        }
    }
}
