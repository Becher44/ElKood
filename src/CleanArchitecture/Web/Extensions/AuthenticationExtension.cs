using System.Security.Claims;
using System.Text;
using ElKood.Application.Common;
using ElKood.Domain.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
namespace ElKood.Web.Extensions;
public static class AuthenticationExtensions
{
    public static void AddAuth(this IServiceCollection services, Identity identitySettings)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = $"{JwtBearerDefaults.AuthenticationScheme}_{identitySettings.Issuer}";
            options.DefaultChallengeScheme = $"{JwtBearerDefaults.AuthenticationScheme}_{identitySettings.Issuer}";
        });

        var authenticationBuilder = services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme);

        authenticationBuilder.AddJwtBearer($"{JwtBearerDefaults.AuthenticationScheme}_{identitySettings.Issuer}", options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateLifetime = true,
                ValidateAudience = true,
                ValidIssuer = identitySettings.Issuer,
                ValidAudience = identitySettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(identitySettings.Key)),
                ValidateIssuerSigningKey = true,
                RoleClaimType = ClaimTypes.Role
            };

            options.Authority = identitySettings.Issuer;
            options.RequireHttpsMetadata = identitySettings.ValidateHttps;
        });

        services.AddAuthorization(options =>
        {
            var authSchemes = $"{JwtBearerDefaults.AuthenticationScheme}_{identitySettings.Issuer}";

            options.DefaultPolicy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .AddAuthenticationSchemes(authSchemes)
                .Build();

            options.AddPolicy("user_read", policy => policy.Requirements.Add(
                new HasScopeRequirement(
                    identitySettings.ScopeBaseDomain,
                    identitySettings.ScopeBaseDomain + "/read",
                    identitySettings.Issuer)));

            options.AddPolicy("user_write", policy => policy.Requirements.Add(
                new HasScopeRequirement(
                    identitySettings.ScopeBaseDomain,
                    identitySettings.ScopeBaseDomain + "/write",
                    identitySettings.Issuer)));
        });

    }
    public static void AddAuthLocal(this IServiceCollection services, Identity identitySettings)
    {
        var authenticationBuilder = services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme);
        authenticationBuilder.AddJwtBearer($"{JwtBearerDefaults.AuthenticationScheme}_{identitySettings.Issuer}", options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateLifetime = false,
                ValidateAudience = true,
                ValidIssuer = identitySettings.Issuer,
                ValidAudience = identitySettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(identitySettings.Key)),
                ValidateIssuerSigningKey = true,
            };
            options.Authority = identitySettings.Issuer;
            options.RequireHttpsMetadata = identitySettings.ValidateHttps;
        });

        services.AddAuthorization(options =>
        {
            var authSchemes = $"{JwtBearerDefaults.AuthenticationScheme}_{identitySettings.Issuer}"; options.DefaultPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().AddAuthenticationSchemes(authSchemes).Build();

            options.AddPolicy("user_read", policy => policy.Requirements.Add(
                new HasScopeRequirement(
                identitySettings.ScopeBaseDomain,
                 identitySettings.ScopeBaseDomain + "/read",
                 identitySettings.Issuer)));

            options.AddPolicy("user_write", policy => policy.Requirements.Add(
                new HasScopeRequirement(
                    identitySettings.ScopeBaseDomain,
                    identitySettings.ScopeBaseDomain + "/write",
                    identitySettings.Issuer)));
        });
    }
}
