using ElKood.Shared.Models.AuthIdentity.UsersIdentity;

namespace ElKood.Application.Common.Interfaces;
public interface IAuthIdentityService
{
    Task LogOut();
    Task<TokenResult> RefreshTokenAsync(string token, CancellationToken cancellationToken);
    Task<TokenResult> Authenticate(LoginRequest request, CancellationToken cancellationToken);
    Task Register(RegisterRequest request, CancellationToken cancellationToken);
    Task<UserViewModel> Get(CancellationToken cancellationToken);
}
