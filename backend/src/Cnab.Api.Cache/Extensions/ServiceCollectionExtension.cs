using Cnab.Api.Domain.Cache;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cnab.Api.Cache.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void ConfigureCache(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration.GetValue<string>("Redis");
            });

            services.AddScoped<ICacheService, CacheService>();
        }
    }
}
