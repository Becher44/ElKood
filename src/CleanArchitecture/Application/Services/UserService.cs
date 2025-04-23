using System.Security.Claims;
using ElKood.Application.Common.Exceptions;
using ElKood.Application.Common.Interfaces;
using ElKood.Shared.Models.AuthIdentity.UsersIdentity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ElKood.Application.Services;

public class UserService(
    UserManager<ApplicationUser> userManager,
    IUnitOfWork unitOfWork) : IUserService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly UserManager<ApplicationUser> _userManager = userManager;

    public async Task<List<UserViewModel>> Get(CancellationToken cancellationToken)
    {
        var users = await _userManager.Users
            .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
            .ToListAsync(cancellationToken: cancellationToken);

        List<UserViewModel> result = users.Select(x => new UserViewModel
        {
            Id = x.Id,
            Email = x.Email,
            UserName = x.UserName,
            FullName = x.Name,
            Roles = x.UserRoles.Select(ur => ur.Role.Name).ToList(),
        }).ToList();

        return result;
    }

    public async Task Update(UserUpdateRequest request, CancellationToken cancellationToken)
    {

        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken: cancellationToken)
            ?? throw AuthIdentityException.ThrowUserNotExist();

        user.Email = request.Email ?? user.Email;
        user.Name = request.Name ?? user.Name;


        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded)
        {
            List<IdentityError> errorList = result.Errors.ToList();
            var errors = string.Join(", ", errorList.Select(e => e.Description));
            throw AuthIdentityException.ThrowUpdateUnsuccessful(errors);
        }
    }

    public async Task Delete(string userId, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(userId)
            ?? throw AuthIdentityException.ThrowAccountDoesNotExist();

        var result = await _userManager.DeleteAsync(user);

        if (!result.Succeeded)
        {
            List<IdentityError> errorList = result.Errors.ToList();
            var errors = string.Join(", ", errorList.Select(e => e.Description));
            throw AuthIdentityException.ThrowDeleteUnsuccessful();
        }
    }

    public async Task RoleAssign(RoleAssignRequest request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId)
            ?? throw AuthIdentityException.ThrowAccountDoesNotExist();

        // Handle Role Removal
        var removedRoles = request.Roles.Where(x => !x.Selected).Select(x => x.Name).ToList();
        foreach (var roleName in removedRoles)
        {
            if (await _userManager.IsInRoleAsync(user, roleName))
            {
                await _userManager.RemoveFromRoleAsync(user, roleName);
            }
        }

        // Handle Role Assignment
        var addedRoles = request.Roles.Where(x => x.Selected).Select(x => x.Name).ToList();
        foreach (var roleName in addedRoles)
        {
            if (!await _userManager.IsInRoleAsync(user, roleName))
            {
                await _userManager.AddToRoleAsync(user, roleName);
            }
        }

        // Manage Scope Claims

        // Retrieve the existing scope claim
        var existingClaims = await _userManager.GetClaimsAsync(user);
        var scopeClaim = existingClaims.FirstOrDefault(c => c.Type == "scope");

        // Get scopes from the request
        var newScopes = request.Scopes.Where(x => x.Selected).Select(x => x.Name).ToList();

        if (scopeClaim != null)
        {
            // If there is an existing scope claim, remove it
            await _userManager.RemoveClaimAsync(user, scopeClaim);
        }

        if (newScopes.Any())
        {
            // Add the new scope claim
            var newScopeClaim = new Claim("scope", string.Join(" ", newScopes));
            await _userManager.AddClaimAsync(user, newScopeClaim);
        }
    }
}
