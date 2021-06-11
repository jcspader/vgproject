using Microsoft.EntityFrameworkCore;
using VG.Infra.Data.Context;
using VG.Infra.Data.Repositories;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class Initialize
    {
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        public static IServiceCollection AddPersistence(this IServiceCollection services, string connection_string)
        {
            services.AddTransient<IModelRepository, ModelRepository>();
            services.AddTransient<ITruckRepository, TruckRepository>();

            services.AddDbContext<DataBaseContext>(x => x.UseSqlite(connection_string));

            return services;
        }
    }
}
