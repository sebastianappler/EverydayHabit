using ElasticHabitCalendar.AndroidApplication;
using ElasticHabitCalendar.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Reflection;
using Xamarin.Essentials;

namespace ElasticHabitCalendar.AndroidApp
{
    public class Startup
    {
        public static IServiceProvider ServiceProvider { get; set; }
        public static void Init()
        {
            var a = Assembly.GetExecutingAssembly();
            using var stream = a.GetManifestResourceStream("ElasticHabitCalendar.AndroidApp.appsettings.json");

            var host = new HostBuilder()
                .ConfigureHostConfiguration(c =>
                {
                    // Tell the host configuration where to file the file (this is required for Xamarin apps)
                    c.AddCommandLine(new string[] { $"ContentRoot={FileSystem.AppDataDirectory}" });

                    //read in the configuration file!
                    c.AddJsonStream(stream);
                })
                .ConfigureServices((c, x) =>
                {
                    // Configure our local services and access the host configuration
                    ConfigureServices(c, x);
                })
                .ConfigureLogging(l => l.AddConsole(o =>
                {
                    //setup a console logger and disable colors since they don't have any colors in VS
                    o.DisableColors = true;
                }))
                .Build();

            //Save our service provider so we can use it later.
            ServiceProvider = host.Services;
        }

        public static void ConfigureServices(HostBuilderContext ctx, IServiceCollection services)
        {
            services.AddApplication();
            services.AddPersistence(ctx.Configuration);
        }
    }
}