using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using RedMango_Api.Config;
using RedMango_Api.Services.Contracts;
using RedMango_Api.Services;
using Repositories;
using Repositories.Config;
using Repositories.Contracts;

namespace RedMango_Api.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddMyAppServices(this IServiceCollection services, IConfiguration config)

        {

            services.AddDbContext<ApplicationDbContext>(opt =>
            {
                opt.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            });


            var appSettingsSection = config.GetSection("APISettings");
            services.Configure<APISettings>(appSettingsSection);

            services.AddAutoMapper(typeof(MappingProfile));
           
            services.AddScoped<IMenuItemRepository, MenuItemRepository>();

            services.AddScoped<IFileUpload, FileUpload>();

            services.AddCors(opt =>
            {
                opt.AddPolicy("CorsPolicyAllowAll", policy =>
                {
                    policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
                    // .WithOrigins("http://localhost:4200")
                    // .WithOrigins("");
                });
            });

            //services.AddRouting(opt => opt.LowercaseUrls=true);

            return services;
        }

        //public static IApplicationBuilder UseStripe(this IApplicationBuilder app, IConfiguration config )
        //{
        //    StripeConfiguration.ApiKey = config.GetSection("Stripe")["ApiKey"];

        //    return app;
        //}

    }
}
