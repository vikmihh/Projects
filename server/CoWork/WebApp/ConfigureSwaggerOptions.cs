﻿using System.Reflection;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace WebApplication;

/// <summary>
/// Configurations for Swagger
/// </summary>
public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
{
    private readonly IApiVersionDescriptionProvider _provider;

    /// <summary>
    /// Configurations for Swagger constructor
    /// </summary>
    /// <param name="provider">Gets a read-only list of discovered API version descriptions.</param>
    public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) =>
        _provider = provider;

    /// <summary>
    /// Configurations for Swagger constructor
    /// </summary>
    /// <param name="options">Provides configurations</param>
    public void Configure(SwaggerGenOptions options)
    {
        foreach (var description in _provider.ApiVersionDescriptions)
        {
            options.SwaggerDoc(
                description.GroupName,
                new OpenApiInfo()
                {
                    Title = $"API {description.ApiVersion}",
                    Version = description.ApiVersion.ToString()
                });
        }
        
        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        options.IncludeXmlComments(xmlPath);
        
        options.CustomSchemaIds(i => i.FullName);
        
        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
        {
            Description =
                "JWT Authorization header using the Bearer scheme.\r\n<br>" +
                "Enter 'Bearer'[space] and then your token in the text box below.\r\n<br>" +
                "Example: <b>Bearer eyJhbGciOiJIUzUxMiIsIn...</b>\r\n<br>" +
                "You will get the bearer from the <i>account/login</i> or <i>account/register</i> endpoint.",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer"
        });
        
        options.AddSecurityRequirement(new OpenApiSecurityRequirement()
        {
            {
                new OpenApiSecurityScheme()
                {
                    Reference = new OpenApiReference()
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    },
                    Scheme = "oauth2",
                    Name = "Bearer",
                    In = ParameterLocation.Header
                },
                new List<string>()
            }
        });
    }
}
