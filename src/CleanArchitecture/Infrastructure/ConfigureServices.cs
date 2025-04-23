using ElKood.Application;
using ElKood.Application.Common;
using ElKood.Application.Repositories;
using ElKood.Infrastructure.Data;
using ElKood.Infrastructure.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ElKood.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructuresService(this IServiceCollection services, AppSettings configuration)
    {
        if (configuration.UseInMemoryDatabase)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase("ElKood"));
        }
        else
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.ConnectionStrings.DefaultConnection,
                sqlOptions => sqlOptions.EnableRetryOnFailure(maxRetryCount: 5,
                                                              maxRetryDelay: TimeSpan.FromSeconds(10),
                                                              errorNumbersToAdd: null)));
        }

        services.AddIdentity<ApplicationUser, RoleIdentity>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

        // register services
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IItemRepository, ItemRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddTransient<ApplicationDbContextInitializer>();

        return services;
    }
}
