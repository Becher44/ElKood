using ElKood.Shared.Models;
using Microsoft.AspNetCore.Identity;

namespace ElKood.Domain.Entities;

public class UserRoles : IdentityUserRole<Guid>
{
    public virtual ApplicationUser User { get; set; }

    public virtual RoleIdentity Role { get; set; }
}
