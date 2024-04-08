using System.Reflection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace PMS.WebHost.Configurations;

/// <summary>
/// A static class used for injecting swagger configuration.
/// </summary>
public static class SwaggerConfiguration
{
    /// <summary>
    /// A static method used for injecting swagger configuration.
    /// </summary>
    /// <param name="services">The service collection.</param>
    public static void AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(opt =>
        {
            opt.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
            {
                Description = "Standard Authorization header using Bearer scheme (\"bearer {token}\")",
                In = ParameterLocation.Header,
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
            });

            opt.OperationFilter<SecurityRequirementsOperationFilter>();

            opt.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "PMS.Api",
                Description = "Pharmacy Management System API",
            });

            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            opt.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
        });
    }
}