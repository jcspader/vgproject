using System.Reflection;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace VG.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                    .AddFluentValidation(opt =>
                    {
                        opt.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
                    })
                    .AddNewtonsoftJson();

            services.AddCors();

            services.AddMigrations(Configuration.GetConnectionString("DefaultConnectionDb"));
            services.AddDomainServices(Configuration.GetConnectionString("DefaultConnectionDb"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseCors(builder => builder
                          .AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader()
                          );

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/",
                    context => context.Response.WriteAsync("Web Api running....")
                );

                endpoints.MapControllers();
            });

            app.RunMigrations();
        }
    }
}
