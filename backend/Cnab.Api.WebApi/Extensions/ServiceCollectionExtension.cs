using Cnab.Api.Domain.Logs;
using Cnab.Api.WebApi.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Npgsql.Internal.TypeHandlers.NetworkHandlers;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Text;
using Cnab.Api.Domain.Jwt;
using Cnab.Api.Application.Jwt;
using Cnab.Api.Application.NotificationPattern;
using Cnab.Api.Domain.NotificationPattern;

namespace Cnab.Api.WebApi.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void ConfigureWebAPi(this IServiceCollection services)
        {
            services.AddCors();
            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling =
                                                    ReferenceLoopHandling.Ignore;
                options.SerializerSettings.ContractResolver =
                                                     new CamelCasePropertyNamesContractResolver();

            });

            services.AddEndpointsApiExplorer();

            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(50, 1);
            });

            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

            services.AddRouting(delegate (RouteOptions options)
            {
                options.LowercaseUrls = true;
            });
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<RequestLog>();
        }

        public static void ConfigureMediator(
            this IServiceCollection services, string assemblyCsproj)
        {
            var assembly = AppDomain.CurrentDomain.Load(assemblyCsproj);
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));
        }

        public static void ConfigureSwaggerWithBearerToken(
            this IServiceCollection services, string assemblyCsproj)
        {
            services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description =
                        "Autorização JWT usa-se Bearer token no cabeçalho." +
                            " \r\n\r\n Entre com 'Bearer' [espaço] e o token do seu usuario." +
                            "\r\n\r\nExemplo: \"Bearer 12345abcdef\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.UseAllOfToExtendReferenceSchemas();
                c.UseAllOfForInheritance();
                c.UseOneOfForPolymorphism();

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string> ()
                    }
                });

                var xmlFile = $"{assemblyCsproj}.xml";

                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                c.IncludeXmlComments(xmlPath);
            });

            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            services.AddTransient<IConfigureOptions<SwaggerUIOptions>, ConfigureSwaggerUIOptions>();
        }

        public static void ConfigureAuthJwt(this IServiceCollection services, IConfiguration configuration)
        {
                services.AddScoped<IJwtHandler, JwtHandler>();
                SigningConfigurations implementationInstance = new SigningConfigurations();
                services.AddSingleton(implementationInstance);
                TokenConfigurations tokenConfigurations = new TokenConfigurations();
                new ConfigureFromConfigurationOptions<TokenConfigurations>(configuration.GetSection("TokenConfigurations")).Configure(tokenConfigurations);
                services.AddSingleton(tokenConfigurations);
                SymmetricSecurityKey issuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenConfigurations.SecretKey));
                TokenValidationParameters tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = issuerSigningKey,
                    ValidateIssuer = true,
                    ValidIssuer = tokenConfigurations.Emit,
                    ValidateAudience = true,
                    ValidAudience = tokenConfigurations.App,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    RequireExpirationTime = true
                };
                services.AddAuthentication(delegate (AuthenticationOptions authOptions)
                {
                    authOptions.DefaultAuthenticateScheme = "Bearer";
                    authOptions.DefaultChallengeScheme = "Bearer";
                }).AddJwtBearer(delegate (JwtBearerOptions bearerOptions)
                {
                    bearerOptions.TokenValidationParameters = tokenValidationParameters;
                });
        }
        public static void ConfigureNotificationPattern(this IServiceCollection services)
        {
            services.AddScoped<INotificationContext, NotificationContext>();
        }
    }
}
