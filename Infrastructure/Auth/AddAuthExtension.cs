using Application.Auth;
using Domain.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Infrastructure.Auth
{
    public static class AddAuthExtension
    {
        public static IServiceCollection AddAuth(
            this IServiceCollection services, IConfigurationManager config)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(config["JwtSettings:Secret"]!)),
                        ValidIssuer = config["JwtSettings:Issuer"],
                        ValidAudience = config["JwtSettings:Audience"],
                        ClockSkew = TimeSpan.Zero
                    };
                });

            services.AddAuthorizationBuilder()
                .AddPolicy("Admin", policy => policy.RequireRole(["admin"]));

            services.AddTransient<IAuthService, AuthService>();

            services.AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>();

            return services;
        }

        public static IApplicationBuilder UseAuth(this IApplicationBuilder app)
        {
            app.UseCors();
            app.UseAuthentication();
            app.UseAuthorization();

            return app;
        }
    }
}
