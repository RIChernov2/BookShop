using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace BookShop.Configuration
{
    public static class SwaggerConfiguration
    {
        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.IncludeXmlComments(string.Format(@"{0}/BookShop.WebApi.xml", System.AppDomain.CurrentDomain.BaseDirectory));
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "BookShop.WebApi", Version = "v1" });
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter your token in the text input below.",
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });
        }
    }
}