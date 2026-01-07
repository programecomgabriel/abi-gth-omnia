using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Ambev.DeveloperEvaluation.WebApi.Swagger;

public static class SecurityExtensions
{
    public static void AddSecurity(this SwaggerGenOptions swaggerOptions)
    {
        const string bearer = "Bearer";

        swaggerOptions.AddSecurityRequeriment(bearer);
        swaggerOptions.AddSecurityDefinition(bearer, new OpenApiSecurityScheme
        {
            Description = "Inform the access token.",
            Name = "Authorization",
            Scheme = "Bearer",
            BearerFormat = "JWT",
            Type = SecuritySchemeType.Http,
        });
    }

    public static void AddSecurityRequeriment(this SwaggerGenOptions swaggerOptions, string id)
    {
        swaggerOptions.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = id,
                        },
                    },
                    Array.Empty<string>()
                },
            });
    }
}
