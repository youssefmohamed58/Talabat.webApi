using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Talabat.Apis.Helpers;
using Talabat.Core.Entities.Identity_Entities;
using Talabat.Core.Repositories;
using Talabat.Repository;
using Talabat.Repository.Identity;
using Talabat.Service;

namespace Talabat.Apis.Extensions
{
    public static class IdentityServiceExtension
    {
        public static IServiceCollection AddIdentityService(this IServiceCollection services , IConfiguration configuration)
        {
            services.AddScoped<ITokenService, TokenService>();  
            services.AddIdentity<AppUser, IdentityRole>()
                     .AddEntityFrameworkStores<IdentityModuleDbContext>();
            services.AddAuthentication(Options =>
            {
                Options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                Options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                                 .AddJwtBearer(Options =>
                                 {
                                     Options.TokenValidationParameters = new TokenValidationParameters()
                                     {
                                         ValidateIssuer = true,
                                         ValidIssuer = configuration["JWT:ValidIssuer"],
                                         ValidateAudience = true,
                                         ValidAudience = configuration["JWT:ValidAudiance"],
                                         ValidateLifetime = true,
                                         ValidateIssuerSigningKey = true,
                                         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]))
                                     };
                                 });

            return services;

        }
    }
}
