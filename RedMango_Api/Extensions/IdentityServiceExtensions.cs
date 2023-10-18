using DataAccess.Data;
using DataAccess.Data.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using RedMango_Api.Config;
using RedMango_Api.Services;
using RedMango_Api.Services.Contracts;
using System.Text;

namespace RedMango_Api.Extensions
{
    public static class IdentityServiceExtensions
    {
        public static IServiceCollection AddMyIdentityServices(this IServiceCollection services, IConfiguration config)
        {

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options => {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 5;
                // options.Password.RequireDigit = true;
                options.Lockout.MaxFailedAccessAttempts = 3;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(2);
                // options.SignIn.RequireConfirmedEmail = true;
                // options.SignIn.RequireConfirmedAccount = true;
            });

            services.AddScoped<IDBInitializer, DBInitializer>();

            services.AddScoped<ITokenService, TokenService>();

            var apiSettings = config.GetSection("APISettings").Get<APISettings>();
            var key = Encoding.UTF8.GetBytes(apiSettings.SecretKey);

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateAudience = true,
                        ValidateIssuer = true,
                        ValidAudience = apiSettings.ValidAudience,
                        ValidIssuer = apiSettings.ValidIssuer,
                        ClockSkew = TimeSpan.Zero
                    };
                });

            return services;
        }

    }
}
