using System.Text;
using BookShop.Configuration.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace BookShop.Configuration
{
    public static class AuthenticationConfiguration
    {
        public static void ConfigureAuthentication(this IServiceCollection services, AppConfiguration configuration)
        {
            services.AddAuthentication(opt => {
                    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = configuration.Authentication.Issuer,
                        ValidAudience = configuration.Authentication.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.Authentication.SecretKey))
                    };
                });
        }
    }
}