using Cnab.Api.Data.Context;
using Cnab.Api.Data.Repositories;
using Cnab.Api.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cnab.Api.Data.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddEntityFrameworkNpgsql().AddDbContext<CnabContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("CnabContext")));
        }

        public static void ConfigureRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
        }
    }
}
