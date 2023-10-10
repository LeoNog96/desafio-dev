using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Cnab.Api.WebApi.Extensions
{
    public static class SwaggerUIOptionsExtensions
    {
        public static void UseSwaggerVersion(this SwaggerUIOptions options, WebApplication app)
        {
            var provider = app.Services
                                .GetRequiredService<IApiVersionDescriptionProvider>();
        }
    }
}
