using ElKood.Shared.Models.AuthIdentity.UsersIdentity;

namespace ElKood.Shared.Models.AuthIdentity.Roles;

public class UserRoleViewModel
{
    public UserUpdateRequest EditUser { get; set; }
    public RoleAssignRequest EditRole { get; set; }
}
