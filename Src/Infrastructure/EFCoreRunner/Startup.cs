using EverydayHabit.Persistence.Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.SQL;
using System;

namespace EFCoreRunner
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var databaseProvider = Configuration.GetSection(nameof(DatabaseProviderConfiguration)).Get<DatabaseProviderConfiguration>();

            switch (databaseProvider.ProviderType)
            {
                case DatabaseProviderType.SqlServer:
                    services.AddPersistenceSQL(Configuration);
                    break;
                case DatabaseProviderType.Sqlite:
                    services.AddPersistenceSQLite(Configuration);
                    break;
                default:
                    throw new Exception("No database provider set. Update your appsettings.json with eg. DatabaseProviderConfiguration.ProviderType: 'SqlServer'");
            }
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
        }
    }
}
