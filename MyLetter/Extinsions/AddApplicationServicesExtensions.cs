using Microsoft.EntityFrameworkCore;
using MyLetter.EF;
using MyLetter.Helpers;

namespace MyLetter.Extinsions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddMvc();

            // Add DbContext and Access to another project by migrations assembley.
            services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseNpgsql(config.GetConnectionString("DefaultConnection"),
                    m => m.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            services.AddTransient<IUnitOfWork, UnitOfWork>();

            //services.AddSingleton<PresenceTracker>();

            //services.AddScoped<IAuthService, AuthService>();
            //services.AddScoped<IUserServices, UserService>();
            //services.AddScoped<MessageHub>();
            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);




            return services;
        }
    }
}
