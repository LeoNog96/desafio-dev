using Cnab.Api.Application.NotificationPattern;
using Cnab.Api.Cache.Extensions;
using Cnab.Api.Data.Context;
using Cnab.Api.Data.Extensions;
using Cnab.Api.Logs;
using Cnab.Api.WebApi.Extensions;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Reflection;
using Cnab.Api.Services.Extensions;
namespace Cnab.Api.WebApi
{
    public class Startup
    {
        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                    .AddEnvironmentVariables();

            Configuration = builder.Build();

            Log.Logger = ConfigurationLog.ConfigureLog(env, Configuration);
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(options => options.Filters.Add<NotificationPatternFilter>());
            services.ConfigureNotificationPattern();
            services.ConfigureCache(Configuration);
            services.ConfigureDbContext(Configuration);
            services.ConfigureWebAPi();
            services.ConfigureAuthJwt(Configuration);
            services.ConfigureSwaggerWithBearerToken(Assembly.GetExecutingAssembly().GetName().Name);
            services.ConfigureRepositories();
            services.ConfigureServices();
            services.ConfigureMediator("Cnab.Api.Application");
        }

        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(_ => true)
                .AllowCredentials());

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            using var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
            serviceScope.ServiceProvider.GetService<CnabContext>().Database.Migrate();
        }
    }
}
