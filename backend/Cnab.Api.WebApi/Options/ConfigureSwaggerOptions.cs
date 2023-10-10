using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Cnab.Api.WebApi.Options
{
    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _provider;
        private readonly IConfiguration _configuration;

        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider, IConfiguration configuration)
        {
            _provider = provider;
            _configuration = configuration;
        }

        public void Configure(SwaggerGenOptions options)
        {
            foreach (var description in _provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description, _configuration["ApplicationName"]));
            }
        }

        private static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description, string ApiName)
        {
            var info = new OpenApiInfo
            {
                Version = description.ApiVersion.ToString(),
                Title = $"API - {ApiName}",
                Description = $"Documentação da {ApiName}",
                Contact = new OpenApiContact
                {
                    Name = "Leonardo Nogueira da Silva",
                    Email = "leonardo.lns@outlook.com",
                },
                License = new OpenApiLicense
                {
                    Name = "MIT",
                }
            };

            if (description.IsDeprecated)
            {
                info.Description += " Esssa versão está depreciada";
            }

            return info;
        }
    }
}
