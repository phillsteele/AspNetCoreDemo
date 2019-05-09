using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.IO;
using System.Reflection;

namespace AspNetCoreDemo.Setup
{
    public static class Swagger
    {
        public static void Configure(SwaggerGenOptions options)
        {
            options.SwaggerDoc("SLCS v2", 
                new Info {
                    Version = "SLCS v2",
                    Title = "SLCS API",
                    Description = "Sage Lifecyle Service - Handles subscription and associated events"

                    // We can add contact and additional info here if required
                });

            // Set the comments path for the Swagger JSON and UI.
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            options.IncludeXmlComments(xmlPath);
        }   
    }
}


