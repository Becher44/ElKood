using System.Security.Claims;
using ElKood.Shared.Models.AuthIdentity.UsersIdentity;

namespace ElKood.Application.Common.Interfaces;

public interface ITokenService
{
    string GenerateToken(User user);
    ClaimsPrincipal ValidateToken(string token);
    Task<TokenResult> GenerateToken(ApplicationUser user, string[] scopes, CancellationToken cancellationToken);
}
