using ElKood.Shared.Models.AuthIdentity.Roles;

namespace ElKood.Application.Common.Interfaces;

public interface IRoleService
{
    Task<List<RoleViewModel>> GetAll();
}
