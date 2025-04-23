using ElKood.Application.Common.Interfaces;
using ElKood.Shared.Models.AuthIdentity.UsersIdentity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ElKood.Web.Controller;
public class AuthIdentityController(IAuthIdentityService authIdentityService) : BaseController
{
    private readonly IAuthIdentityService _authIdentityService = authIdentityService;
    /// <summary>
    /// Login
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("login")]
    [AllowAnonymous]
    [SwaggerResponse(200, "Successfully authenticated.", typeof(TokenResult))]
    [SwaggerResponse(400, "Invalid request.")]
    [SwaggerResponse(401, "Invalid credentials.")]
    public async Task<IActionResult> Authenticate([FromBody] LoginRequest request, CancellationToken cancellationToken)
    {
        var result = await _authIdentityService.Authenticate(request, cancellationToken);

        SetTokenInCookie(result.Token);
        return Ok(result);
    }

    /// <summary>
    /// Register
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("register")]
    [AllowAnonymous]
    [SwaggerResponse(201, "User registered successfully.")]
    [SwaggerResponse(400, "Invalid request.")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request, CancellationToken cancellationToken)
    {
        await _authIdentityService.Register(request, cancellationToken);
        return NoContent();
    }

    /// <summary>
    /// Logout
    /// </summary>
    /// <returns></returns>
    [HttpDelete("logout")]
    [SwaggerResponse(204, "Successfully logged out.")]
    public async Task<IActionResult> Logout()
    {
        await _authIdentityService.LogOut();
        RemoveTokenInCookie();
        return NoContent();
    }

    /// <summary>
    /// refresh token
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("refreshToken")]
    [Authorize]
    [SwaggerResponse(200, "Token refreshed successfully.", typeof(TokenResult))]
    [SwaggerResponse(401, "User is not authorized.")]
    public async Task<IActionResult> RefreshToken(CancellationToken cancellationToken)
    {
        var refreshToken = GetTokenInCookie();
        var response = await _authIdentityService.RefreshTokenAsync(refreshToken, cancellationToken);
        return Ok(response);
    }

    /// <summary>
    /// get current profile by userId
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("profile")]
    [Authorize]
    [SwaggerResponse(200, "Successfully retrieved user profile.", typeof(UserViewModel))]
    [SwaggerResponse(401, "User is not authorized.")]
    public async Task<IActionResult> Profile(CancellationToken cancellationToken)
        => Ok(await _authIdentityService.Get(cancellationToken));

    private string GetTokenInCookie() => Request.Cookies["token_key"];

    private void SetTokenInCookie(string refreshToken)
    {
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Expires = DateTime.UtcNow.AddDays(10),
        };
        Response.Cookies.Append("token_key", refreshToken, cookieOptions);
    }

    private void RemoveTokenInCookie()
    {
        Response.Cookies.Delete("token_key");
    }
}
