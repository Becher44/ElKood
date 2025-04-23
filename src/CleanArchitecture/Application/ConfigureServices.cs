using ElKood.Application.Common;
using ElKood.Application.Common.Interfaces;
using ElKood.Application.Common.Utilities;
using ElKood.Application.Services;
using ElKood.Web.Services;

namespace ElKood.Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationService(this IServiceCollection services, AppSettings appsettings)
    {
        services.AddTransient<IAuthService, AuthService>();
        services.AddTransient<IItemService, ItemService>();
        services.AddTransient<IRoleService, RoleService>();
        services.AddTransient<IAuthIdentityService, AuthIdentityService>();
        services.AddTransient<IUserService, UserService>();

        services.AddTransient<ICurrentTime, CurrentTime>();
        services.AddScoped<ICurrentUser, CurrentUser>();
        services.AddTransient<ITokenService, TokenService>();
        services.AddTransient<ICookieService, CookieService>();

        return services;
    }
}
