using Cnab.Api.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Cnab.Api.Services.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<ICnabService, CnabService>();
        }
    }
}
