using System;
using System.Reflection;
using FluentMigrator.Runner;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class Initialize
    {
        public static IServiceCollection AddMigrations(this IServiceCollection service, string connection_string)
        {
            service.AddFluentMigratorCore()
                     .ConfigureRunner(config =>
                     {
                         config.ScanIn(Assembly.GetExecutingAssembly()).For.Migrations();
                         config.ScanIn(Assembly.GetExecutingAssembly()).For.EmbeddedResources();


                         config.WithGlobalConnectionString(connection_string);

                         config.AddSQLite();
                     })
                     .AddLogging(lb => lb.AddFluentMigratorConsole());

            return service;
        }

        public static IServiceProvider RunMigrations(IServiceProvider serviceProvider)
        {
            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();
            ExecuteMigrations(runner);

            return serviceProvider;
        }

        public static IApplicationBuilder RunMigrations(this IApplicationBuilder app)
        {
            using (IServiceScope scope = app.ApplicationServices.CreateScope())
            {
                IMigrationRunner runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
                ExecuteMigrations(runner);
                return app;
            }
        }


        private static void ExecuteMigrations(IMigrationRunner runner)
        {
            using (var runnerScope = runner.BeginScope())
            {
                try
                {
                    runner.MigrateUp();
                    runnerScope.Complete();
                }
                catch (Exception)
                {
                    runnerScope.Cancel();

                    throw;
                }
            }
        }
    }
}
