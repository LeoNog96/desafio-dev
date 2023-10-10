using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Cnab.Api.Logs
{
    public static class ConfigurationLog
    {
        public static ILogger ConfigureLog(IHostEnvironment env, IConfiguration configuration)
        {
            var loggerConfiguration =
                new LoggerConfiguration()
                    .WriteTo.Debug()
                    .Enrich.FromLogContext()
                    .Enrich.WithProperty("Environment", env)
                    .ReadFrom.Configuration(configuration)
                    .WriteTo.Console();

            return loggerConfiguration.CreateLogger();
        }
    }
}