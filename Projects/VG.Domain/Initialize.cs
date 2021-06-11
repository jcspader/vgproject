using AutoMapper;
using VG.Domain.Services;

namespace Microsoft.Extensions.DependencyInjection
{
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public static class Initialize
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection services, string connection_string)
        {
            services.AddPersistence(connection_string);

            services.AddTransient<IModelService, ModelService>();
            services.AddTransient<ITruckService, TruckService>();
            services.AddAutoMapper();


            return services;
        }
    }
}
